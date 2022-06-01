using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    //public GameObject shield;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
            // Debug.Log("hithit");
            Wrapper script = GameObject.Find("Camera").gameObject.GetComponent<Wrapper>(); //get the wrapper script under camera game object
            script.shieldHit = true;
            // Debug.Log("Shield is hit ");
        }
    }
}