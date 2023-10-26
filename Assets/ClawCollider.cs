using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Prize")
        {
            Debug.Log("Prize");
            collision.gameObject.GetComponent<Rigidbody>().drag = 100;
        }
    }
}
