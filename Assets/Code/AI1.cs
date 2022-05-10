using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI1 : MonoBehaviour
{
    public GameObject ai1;
    [SerializeField] private float cooldown = 3;
    private float cooldownTimer = 3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (ai1.activeSelf == false)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer > 0) return;

            cooldownTimer = cooldown;
            ai1.SetActive(true);
            Target t = ai1.GetComponent<Target>();
            t.health = 30f;
        }

    }
}
