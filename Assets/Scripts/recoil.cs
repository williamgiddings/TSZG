using UnityEngine;
using System.Collections;

public class recoil : MonoBehaviour
{

    bool isRecoiling;
    MouseLook ml;

    private void Start()
    {
        ml = GetComponent<MouseLook>();
    }

    public void recoiling (float x, float y)
    {
        isRecoiling = true;
        if( y > 0 && transform.localRotation.x < 90)
        {
            ml.ViewKick(x, y);
        
        }
        isRecoiling = false;
   
    }

    float GetPlusMinus (float rec)
    {
        if (Random.Range(0,1) == 1)
        {
            return (0 - rec);
        }
        else
        {
            return rec;
        }

    }
}