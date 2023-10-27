using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prize : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Claw")
        {
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            this.GetComponent<Rigidbody>().drag = 100;
        }
        else if (collision.gameObject.tag == "Ground")
        {
            this.GetComponent<Rigidbody>().drag = 10;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Claw")
        {
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            this.GetComponent<Rigidbody>().drag = 0;
        }
        else if (collision.gameObject.tag == "Ground")
        {
            this.GetComponent<Rigidbody>().drag = 0;
        }
    }
}
