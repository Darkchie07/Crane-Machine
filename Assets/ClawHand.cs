using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawHand : MonoBehaviour
{
    public enum stateClaw
    {
        OpenState,
        CloseState,
        RisingState
    }

    public stateClaw currentMode;
    public Animator clawAnimation;
    public bool isMoveable;
    public bool isDoneMove;
    public bool isBacktoDefault;

    [Header("Claw Object")]
    public GameObject rotor;
    public GameObject clawHands;

    [Header("Limit Position")]
    public float limitLeft;
    public float limitRight;
    public float limitFront;
    public float limitBack;

    public float speed;
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentMode)
        {
            case stateClaw.OpenState:
                isMoveable = true;
                isDoneMove = false;
                isBacktoDefault = false;
                // Implement
                break;
            case stateClaw.CloseState:
                StartCoroutine(GrabbingObject());
                isMoveable = false;
                break;
            case stateClaw.RisingState:
                StartCoroutine(ClawUp());
                break;
        }
        
        if (isMoveable)
        {
            if(Input.GetKey(KeyCode.UpArrow))
            {
                //move back
                if (rotor.transform.position.z < limitBack)
                {
                    rotor.transform.Translate(0, 0, speed * Time.deltaTime);
                    clawHands.transform.Translate(0, 0, speed * Time.deltaTime);
                }
            }
            else if(Input.GetKey(KeyCode.DownArrow))
            {
                //move front
                if (rotor.transform.position.z > limitFront)
                {
                    rotor.transform.Translate(0, 0, speed * -1 * Time.deltaTime);
                    clawHands.transform.Translate(0, 0, speed * -1 * Time.deltaTime);
                }
            }
            else if(Input.GetKey(KeyCode.LeftArrow))
            {
                //move Left
                if (clawHands.transform.position.x > limitLeft)
                {
                    clawHands.transform.Translate(speed * -1 * Time.deltaTime, 0, 0);
                }
            }
            else if(Input.GetKey(KeyCode.RightArrow))
            {
                //move Right
                if (clawHands.transform.position.x < limitRight)
                {
                    clawHands.transform.Translate(speed * Time.deltaTime, 0, 0);
                }
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                clawAnimation.SetTrigger("Open");
                StartCoroutine(ClawDown());
            }
        }
    }

    public IEnumerator GrabbingObject()
    {
        yield return new WaitForSeconds(1f);
        clawAnimation.ResetTrigger("Down");
        clawAnimation.SetTrigger("Close");
        yield return new WaitForSeconds(3f);
        currentMode = stateClaw.RisingState;
    }

    public IEnumerator ClawDown()
    {
        yield return new WaitForSeconds(2f);
        clawAnimation.ResetTrigger("Open");
        clawAnimation.SetTrigger("Down");
        yield return new WaitForSeconds(1f);
        currentMode = stateClaw.CloseState;
    }
    
    public IEnumerator ClawUp()
    {
        yield return new WaitForSeconds(2f);
        clawAnimation.ResetTrigger("Close");
        clawAnimation.SetTrigger("Up");
        yield return new WaitForSeconds(3f);
        BackToDefault();
        if (rotor.transform.position.z <= limitFront && clawHands.transform.position.x <= limitLeft)
        {
            isBacktoDefault = true;
            clawAnimation.ResetTrigger("Up");
            clawAnimation.SetTrigger("Release");
            Debug.Log("Lepas");
        }
        if (isBacktoDefault)
        {
            isBacktoDefault = false;
            yield return new WaitForSeconds(2f);
            clawAnimation.ResetTrigger("Release");
            clawAnimation.SetTrigger("Close");
            currentMode = stateClaw.OpenState;
        }
    }

    public void BackToDefault()
    {
        if (rotor.transform.position.z >= limitFront)
        {
            rotor.transform.Translate(0, 0, limitFront * Time.deltaTime * 0.15f);
            clawHands.transform.Translate(0, 0, limitFront * Time.deltaTime * 0.15f);
        }
        else if (clawHands.transform.position.x >= limitLeft)
        {
            clawHands.transform.Translate(limitLeft * Time.deltaTime * 0.25f, 0, 0);
        }
    }
}
