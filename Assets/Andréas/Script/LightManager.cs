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
        Debug.Log(">>> ActivateFirstLight() called");
        if (firstLight != null)
        {
            firstLight.enabled = true;
            Debug.Log(">>> First light enabled");
        }
    }


    // Appel�e apr�s interaction au niveau de la premi�re lumi�re
    public void SwitchToSecondLight()
    {
        Debug.Log(">>> SwitchToSecondLight() called");
        if (firstLight != null) firstLight.enabled = false;
        if (secondLight != null)
        {
            secondLight.enabled = true;
            Debug.Log(">>> Second light enabled");
        }
    }

    // Appel�e apr�s interaction avec la deuxi�me lumi�re
    public void ActivateSequentialLights()
    {
        Debug.Log(">>> ActivateSequentialLights() called");
        StartCoroutine(ActivateLightsSequentially());
    }

    private IEnumerator ActivateLightsSequentially()
    {
        foreach (Light l in sequentialLights)
        {
            if (l != null)
            {
                l.enabled = true;
                Debug.Log($">>> Light {l.name} enabled");
            }
            else
            {
                Debug.LogWarning(">>> One of the sequentialLights is null!");
            }

            yield return new WaitForSeconds(delayBetweenLights);
        }
    }
}