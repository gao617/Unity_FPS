using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield : MonoBehaviour
{
    private float mZcoord;
    private Vector3 mOffset;

    [SerializeField] private Camera fpsCam;
    //[SerializeField] private LayerMask layerMask;

    void Update()
    {
        Vector3 pos = fpsCam.WorldToScreenPoint(transform.position);
        Vector3 mouse_pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, pos.z);
        transform.position = fpsCam.ScreenToWorldPoint(mouse_pos);
    }

    /*
    void MoveShield()
    {
        mZcoord = fpsCam.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWoldPos();
        //transform.position = GetMouseWoldPos() + mOffset;
    }

    private Vector3 GetMouseWoldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZcoord;
        return fpsCam.ScreenToViewportPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWoldPos() + mOffset;
    }
    
    */
}
