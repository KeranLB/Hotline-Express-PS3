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
            Debug.Log("First light activated");
        }
    }

    public void SwitchToSecondLight()
    {
        if (firstLight != null)
            firstLight.enabled = false;

        if (secondLight != null)
        {
            secondLight.enabled = true;
            Debug.Log("Second light activated");
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
                Debug.Log($"Light {l.name} activated");
                yield return new WaitForSeconds(delayBetweenLights);
            }
        }
    }
}