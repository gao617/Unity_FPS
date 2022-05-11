using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    float rotationY = 0F;
    float rotationX = 0F;

    public Camera fpsCam;
    public GameObject bullet;
    public float damage = 10f;
    public int count;

    private void Start()
    {
        count = 0;
    }

    void Update ()
    {
        // if (axes == RotationAxes.MouseXAndY)
        // {
        //     rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        //     rotationX = Mathf.Clamp(rotationX, -40, 40);
        //
        //     rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        //     rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
        //
        //     transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        // }
        // else if (axes == RotationAxes.MouseX)
        // {
        //     transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        // }
        // else
        // {
        //     rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        //     rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
        //
        //     transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        // }

        //Ray ray = fpsCam.ScreenPointToRay(Input.mousePosition);
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log("Object: " + hit.collider.name);
            // Debug.DrawLine(ray.origin, hit.point, Color.green, 2, false);
            // if (Input.GetButtonDown("Fire1"))
            // {
            Target target = hit.transform.GetComponent<Target>();

            if (count == 150)
            {
                GameObject gb = Instantiate(bullet, transform.position, transform.rotation);
                count = 0;
                Rigidbody rb = gb.GetComponent<Rigidbody>();
                rb.AddForce(ray.direction * 3000f);
                Destroy(gb, 0.5f);
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                count = 0;
            }

            count++;



            // }
        }

    }

    //void showObject()
}
