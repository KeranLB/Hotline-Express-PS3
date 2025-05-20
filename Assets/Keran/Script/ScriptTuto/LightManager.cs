using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [Header("Lights")]
    [SerializeField] private Light firstLight;
    [SerializeField] private Light secondLight;
    [SerializeField] private List<Light> sequentialLights;

    [Header("Settings")]
    [SerializeField] private float delayBetweenLights = 1.5f;

   
    public void ActivateFirstLight()
    {
        if (firstLight != null)
        {
            firstLight.enabled = true;
        }
    }

    public void SwitchToSecondLight()
    {
        if (firstLight != null)
            firstLight.enabled = false;

        if (secondLight != null)
        {
            secondLight.enabled = true;
        }
    }

    public void ActivateSequentialLights()
    {
        StartCoroutine(ActivateLightsSequentially());
    }

    private IEnumerator ActivateLightsSequentially()
    {
        foreach (Light l in sequentialLights)
        {
            if (l != null)
            {
                l.enabled = true;
                yield return new WaitForSeconds(delayBetweenLights);
            }
        }
    }
}