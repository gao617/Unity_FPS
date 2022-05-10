using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public GameObject bullet;
    public GameObject cross;
    
    public Transform scigun;
    float GunAngle_h;
    float GunAngle_v;
    public float GunSpeed;

    public Camera fpsCam;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        //RotateGun();
    }

    void Shoot()
    {
        //Ray ray = fpsCam.ScreenPointToRay(Input.mousePosition);
        Ray ray = new Ray(transform.position, -transform.forward);
        /*
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            //Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();

            GameObject gb = Instantiate(bullet, transform.position, transform.rotation);
            Rigidbody rb = gb.GetComponent<Rigidbody>();
            rb.AddForce(ray.direction * 3000f);
            Destroy(gb, 0.5f);

            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }*/

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log("Object: " + hit.collider.name);
            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }
    }
    /*
    void RotateGun()
    {
        GunAngle_h += Input.GetAxis("Mouse X") * GunSpeed * -Time.deltaTime;
        GunAngle_h = Mathf.Clamp(GunAngle_h, -55, 40);

        GunAngle_v += Input.GetAxis("Mouse Y") * GunSpeed * -Time.deltaTime;
        GunAngle_v = Mathf.Clamp(GunAngle_v, -30, 30);

        scigun.localRotation = Quaternion.Euler(new Vector3(GunAngle_v, -GunAngle_h, 0));
        //scigun.Rotate = Quaternion.

        //scigun.localRotation = Quaternion.AngleAxis(GunAngle_h, Vector3.down);

        //GunAngle_v += Input.GetAxis("Mouse Y") * GunSpeed * -Time.deltaTime;
        //GunAngle_v = Mathf.Clamp(GunAngle_v, -20, 20);
        //scigun.localRotation = Quaternion.AngleAxis(GunAngle_v, Vector3.right);
    }*/
}
