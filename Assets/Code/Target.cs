using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;

    /*private void Start()
    {
        StartCoroutine(LateCall(3f));
    }*/

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f) {
            Die();
        }
    }
    void Die()
    {
        gameObject.SetActive(false);
    }
    /*
    IEnumerator LateCall(float seconds)
    {
        if (gameObject.activeSelf == false)
        {
            yield return new WaitForSeconds(seconds);
            gameObject.SetActive(true);
        }
        
    }*/
}
