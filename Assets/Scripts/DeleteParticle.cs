using UnityEngine;
using System.Collections;

public class DeleteParticle : MonoBehaviour
{
    float Delay;

    void Start ()
    {
        Destroy(gameObject, Delay);
    }
}