using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_shoot : MonoBehaviour
{
    public GameObject fps;
    [SerializeField] private float cooldown = 3;
    private float cooldownTimer;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer > 0) return;

        cooldownTimer = cooldown;

        transform.rotation = Quaternion.LookRotation(fps.transform.position - transform.position, transform.up);

        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Target target = hit.transform.GetComponent<Target>();
            GameObject gb = Instantiate(bullet, transform.position, transform.rotation);
            Rigidbody rb = gb.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 500f);
            Destroy(gb, 2f);

            if (target != null)
            {
                target.TakeDamage(10f);
            }

        }

        
    }
}
