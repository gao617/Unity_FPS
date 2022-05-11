using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPoint : MonoBehaviour
{
    public GameObject gun;
    //[SerializeField] private LayerMask layerMask;

    void Update()
    {
        Ray ray = new Ray(gun.transform.position, gun.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit raycastHit) && !raycastHit.collider.CompareTag("bullet"))
        {
            transform.position = raycastHit.point;
        }

    }
}
