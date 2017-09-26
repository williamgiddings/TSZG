using UnityEngine;
using System.Collections;

public class sway : MonoBehaviour {
    public float MoveAmount = 1;
    public float MoveSpeed = 2;
    public GameObject GUN;
    float MoveOnX;
    float MoveOnY;
    public Vector3 DefaultPos; 
    public Vector3 NewGunPos;
    public Vector3 ironsightPos;
    public bool  isIron;
    public float ironSpeed;

    void Update ()
    {
        if (!isIron)
        {
            MoveOnX = Input.GetAxis("Mouse X") * MoveAmount;
            MoveOnY = Input.GetAxis("Mouse Y") * MoveAmount;
            NewGunPos = new Vector3 (DefaultPos.x+MoveOnX, DefaultPos.y+MoveOnY, DefaultPos.z);
            GUN.transform.localPosition = Vector3.Lerp(GUN.transform.localPosition, NewGunPos, MoveSpeed);
        }
        else
        {
            MoveOnX = Input.GetAxis("Mouse X") * (MoveAmount/2);
            MoveOnY = Input.GetAxis("Mouse Y") * (MoveAmount/2);
            NewGunPos = new Vector3 (ironsightPos.x+MoveOnX, ironsightPos.y+MoveOnY, ironsightPos.z);
            GUN.transform.localPosition = Vector3.Lerp(GUN.transform.localPosition, NewGunPos, ironSpeed);
        }
    }
 
}