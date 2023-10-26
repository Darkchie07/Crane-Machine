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

    [Header("Claw Object")]
    public GameObject rotor;
    public GameObject clawHands;

    [Header("Limit Position")]
    public float limitLeft;
    public float limitRight;
    public float limitFront;
    public float limitBack;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoveable)
        {
            if(rotor.transform.position.z < limitBack)
            {
                //move back
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    rotor.transform.Translate(0, 0, speed * Time.deltaTime);
                }
            }
            else if(rotor.transform.position.z > limitFront)
            {
                //move front
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    rotor.transform.Translate(0, 0, speed * -1 * Time.deltaTime);
                }
            }
            else if(clawHands.transform.position.x > limitLeft)
            {
                //move Left
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    clawHands.transform.Translate(speed * -1 * Time.deltaTime, 0, 0);
                }
            }
            else if(clawHands.transform.position.x < limitRight)
            {
                //move Right
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    clawHands.transform.Translate(speed * Time.deltaTime, 0, 0);
                }
            }
        }

        switch (currentMode)
        {
            case stateClaw.OpenState:
                clawAnimation.SetTrigger("Open");
                break;
            case stateClaw.CloseState:
                clawAnimation.SetTrigger("Close");
                break;
            case stateClaw.RisingState:
                // Implement jumping logic
                break;
        }
    }
}
