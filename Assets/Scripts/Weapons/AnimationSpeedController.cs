using UnityEngine;
using System.Collections;

public class AnimationSpeedController : MonoBehaviour
{
    public float[] AnimationSpeeds;

    void Start (){
        int iteration = 0;
        foreach (AnimationState state in GetComponent<Animation>())
        {
            transform.GetComponent<Animation>()[state.name].speed = AnimationSpeeds[iteration];
            iteration += 1;
        }
        
    }
}