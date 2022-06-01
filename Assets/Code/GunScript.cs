using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    // float rotationY = 0F;
    // float rotationX = 0F;

    public Camera fpsCam;
    public GameObject bullet;
    public float damage = 10f;
    public int count;
    public bool gunTriggered; 

    private void Start()
    {
        count = 0;
    }

    void Update ()
    {

        var gunTransform = GameObject.Find("GunPrefab").GetComponent<Transform>(); //get the transform of gun prefab
        Ray ray = new Ray(gunTransform.position, gunTransform.forward);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // Debug.Log("Object: " + hit.collider.name);
            // Debug.DrawLine(ray.origin, hit.point, Color.green, 2, false);
            // if (Input.GetButtonDown("Fire1"))
            // {
            Target target = hit.transform.GetComponent<Target>();

            if (gunTriggered == true)
            {
                Debug.Log("gun is triggered!!!!!!!!!!!!!!!!!!!!!!!");
                GameObject gb = Instantiate(bullet, gunTransform.position, gunTransform.rotation);
                count = 0;
                Rigidbody rb = gb.GetComponent<Rigidbody>();
                rb.AddForce(ray.direction * 3000f);
                Destroy(gb, 0.5f);
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                // count = 0;
            }
            //
            // count++;
            //


            // }
        }

    }

    //void showObject()
}
