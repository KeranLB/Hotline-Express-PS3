using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] private Light firstLight;
    [SerializeField] private Light secondLight;
    [SerializeField] private List<Light> sequentialLights; // Toutes les autres lumières à allumer en cascade
    [SerializeField] private float delayBetweenLights = 1.5f;

    // Appelée à la fin du tuto
    public void ActivateFirstLight()
    {
        if (firstLight != null)
            firstLight.enabled = true;
    }

    // Appelée après interaction au niveau de la première lumière
    public void SwitchToSecondLight()
    {
        if (firstLight != null)
            firstLight.enabled = false;
        if (secondLight != null)
            secondLight.enabled = true;
    }

    // Appelée après interaction avec la deuxième lumière
    public void ActivateSequentialLights()
    {
        StartCoroutine(ActivateLightsSequentially());
    }

    private IEnumerator ActivateLightsSequentially()
    {
        foreach (Light l in sequentialLights)
        {
            if (l != null)
                l.enabled = true;
            yield return new WaitForSeconds(delayBetweenLights);
        }
    }
}