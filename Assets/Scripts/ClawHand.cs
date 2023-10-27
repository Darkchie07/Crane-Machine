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
    public bool hasObject;

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

    public GameObject EnterCoin;
    public GameObject panelPrize;
    public GameObject panelControl;
    public GameObject panelCountDown;

    public SimpleJoystick joystick;

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
                isBacktoDefault = false;
                break;
            case stateClaw.CloseState:
                StartCoroutine(GrabbingObject());
                isMoveable = false;
                break;
            case stateClaw.RisingState:
                StartCoroutine(ClawUp());
                break;
        }
        
        if (isMoveable && !EnterCoin.activeSelf)
        {
            float horizontalInput = joystick.Horizontal();
            float verticalInput = joystick.Vertical();

            Vector3 movementDirection = Vector3.zero;

            if (Mathf.Abs(horizontalInput) > Mathf.Abs(verticalInput))
            {
                // Left or right
                float targetX = Mathf.Clamp(clawHands.transform.position.x + horizontalInput * speed * Time.deltaTime, limitLeft, limitRight);
                clawHands.transform.position = new Vector3(targetX, clawHands.transform.position.y, clawHands.transform.position.z);
            }
            else
            {
                // Up or down 
                float targetZ = Mathf.Clamp(rotor.transform.position.z + verticalInput * speed * Time.deltaTime, limitFront, limitBack);
                rotor.transform.position = new Vector3(rotor.transform.position.x, rotor.transform.position.y, targetZ);

                // For clawHands, apply the same movement to maintain relative positions
                float targetZclaw = Mathf.Clamp(clawHands.transform.position.z + verticalInput * speed * Time.deltaTime, limitFront, limitBack);
                clawHands.transform.position = new Vector3(clawHands.transform.position.x, clawHands.transform.position.y, targetZclaw);
            }

            if (Input.GetKey(KeyCode.UpArrow))
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
                clawAnimation.ResetTrigger("Default");
                clawAnimation.SetTrigger("Open");
                hasObject = true;
                StartCoroutine(ClawDown());
            }
            
        }
        if (isBacktoDefault && hasObject)
        {
            StartCoroutine(ReleaseAnimation());
            Debug.Log("Woe");
        }
        if (!isBacktoDefault && !hasObject)
        {
            clawAnimation.ResetTrigger("Release");
            clawAnimation.SetTrigger("Default");
        }

    }

    public IEnumerator GrabbingObject()
    {
        yield return new WaitForSeconds(1f);
        /*clawAnimation.SetBool("Open", false);
        clawAnimation.SetBool("Close", true);*/
        clawAnimation.ResetTrigger("Down");
        clawAnimation.SetTrigger("Close");
        yield return new WaitForSeconds(3f);
        currentMode = stateClaw.RisingState;
        yield break;
    }

    public IEnumerator ClawDown()
    {
        yield return new WaitForSeconds(2f);
        clawAnimation.ResetTrigger("Open");
        clawAnimation.SetTrigger("Down");
        yield return new WaitForSeconds(2f);
        currentMode = stateClaw.CloseState;
        yield break;
    }
    
    public IEnumerator ClawUp()
    {
        yield return new WaitForSeconds(2f);
        clawAnimation.ResetTrigger("Close");
        clawAnimation.SetTrigger("Up");
        yield return new WaitForSeconds(2f);
        clawAnimation.ResetTrigger("Up");
        BackToDefault();
        /*if (rotor.transform.position.z <= limitFront && clawHands.transform.position.x <= limitLeft)
        {
            isBacktoDefault = true;
        }
        if (isBacktoDefault && hasObject)
        {
            yield return new WaitForSeconds(2f);
            clawAnimation.SetInteger("Movement", 5);
            yield return new WaitForSeconds(2f);
            clawAnimation.SetInteger("Movement", 6);
            yield return new WaitForSeconds(10f);
            ResetAllTriggers();
            currentMode = stateClaw.OpenState;
        }
        yield break;*/
        if (rotor.transform.position.z <= limitFront && clawHands.transform.position.x <= limitLeft)
        {
            isBacktoDefault = true;
        }
        yield break;
        /*if (isBacktoDefault && hasObject)
        {
            StartCoroutine(ReleaseAnimation());
            Debug.Log("Hehe");
            StartCoroutine(DefaultAnimation());
            ResetAllTriggers();
            currentMode = stateClaw.OpenState;
        }*/
    }

    public IEnumerator ReleaseAnimation()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("panggil");
        clawAnimation.SetTrigger("Release");
        yield return new WaitForSeconds(2f);
        hasObject = false;
        currentMode = stateClaw.OpenState;
        yield break;
    }

    public IEnumerator DefaultAnimation()
    {
        yield return new WaitForSeconds(2f);
        clawAnimation.ResetTrigger("Release");
        clawAnimation.SetTrigger("Default");
        yield return new WaitForSeconds(2f);
        hasObject = false;
        yield break;
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

    public void Drop()
    {
        clawAnimation.ResetTrigger("Default");
        clawAnimation.SetTrigger("Open");
        hasObject = true;
        StartCoroutine(ClawDown());
    }

    public void ResetAllTriggers()
    {
        int triggerCount = clawAnimation.parameterCount;
        for (int i = 0; i < triggerCount; i++)
        {
            AnimatorControllerParameter parameter = clawAnimation.parameters[i];
            if (parameter.type == AnimatorControllerParameterType.Trigger)
            {
                clawAnimation.ResetTrigger(parameter.name);
            }
        }
    }

    public void PlayAgain()
    {
        panelPrize.SetActive(false);
        EnterCoin.SetActive(true);
    }
}
