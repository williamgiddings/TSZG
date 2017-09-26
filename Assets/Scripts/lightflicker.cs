using UnityEngine;
using System.Collections;

public class lightflicker : MonoBehaviour
{
    float flickerspeedMin;
    float flickerspeedMax;
    float MinIntensity;
    float MaxIntensity;

    private bool changing;

    void Update ()
    {
        if (!changing)
        {
            StartCoroutine(LightChange());
        }
    }

    IEnumerator LightChange ()
    {
        changing = true;
        GetComponent<Light>().intensity = (Random.Range(MinIntensity, MaxIntensity));
        yield return new WaitForSeconds(Random.Range(flickerspeedMin, flickerspeedMax));
        changing = false;
    }
}