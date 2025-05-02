using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] private Light firstLight;
    [SerializeField] private Light secondLight;
    [SerializeField] private List<Light> sequentialLights; // Toutes les autres lumi�res � allumer en cascade
    [SerializeField] private float delayBetweenLights = 1.5f;

    // Appel�e � la fin du tuto
    public void ActivateFirstLight()
    {
        if (firstLight != null)
            firstLight.enabled = true;
    }

    // Appel�e apr�s interaction au niveau de la premi�re lumi�re
    public void SwitchToSecondLight()
    {
        if (firstLight != null)
            firstLight.enabled = false;
        if (secondLight != null)
            secondLight.enabled = true;
    }

    // Appel�e apr�s interaction avec la deuxi�me lumi�re
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