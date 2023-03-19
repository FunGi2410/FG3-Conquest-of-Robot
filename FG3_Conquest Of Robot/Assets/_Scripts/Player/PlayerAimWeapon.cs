using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    public Camera cam;
    Vector2 mousePos;
    Vector2 lookDir;

    private void Update()
    {
        this.mousePos = this.cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        Vector2 mPos = new Vector2(transform.position.x, transform.position.y);
        this.lookDir = this.mousePos - mPos;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //transform.eulerAngles = new Vector3(0, 0, angle);
        transform.localEulerAngles = new Vector3(0, 0, angle);
    }
}
