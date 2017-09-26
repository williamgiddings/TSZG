using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraCast : MonoBehaviour
{
    public enum Colors {red, green, blue };
    public Colors RayColor;
    private Color color;
    public float len;

    private void Start()
    {
        switch (RayColor)
        {
            case Colors.red:
                color = Color.red;
                break;
            case Colors.green:
                color = Color.green;
                break;
            case Colors.blue:
                color = Color.blue;
                break;
        }
    }

    void Update ()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward);

        if (Physics.Raycast(transform.position, transform.forward, out hit, len ))
        {
            //Debug.DrawLine(transform.position, hit.transform.position, color);
        }
	}
}
