using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] private Light light1;
    [SerializeField] private Light light2;
    [SerializeField] private List<Light> sequentialLights;
    [SerializeField] private float delayBetweenLights = 1.5f;

    private void SetLightState(Light light, bool state)
    {
        if (light != null) light.enabled = state;
    }
    public void ActivateLight1()
    {
        Debug.Log("Activation de la première lumière");
        SetLightState(light1, true);
    }

    public void SwitchToLight2()
    {
        Debug.Log("Switch vers la deuxième lumière");
        SetLightState(light1, false);
        SetLightState(light2, true);
    }

    public void StartSequentialLights()
    {
        Debug.Log("Démarrage de l'activation séquentielle des lumières");
        StartCoroutine(ActivateLightsOneByOne());
    }

    private IEnumerator ActivateLightsOneByOne()
    {
        foreach (var l in sequentialLights)
        {
            if (l != null)
            {
                l.enabled = true;
                Debug.Log("Lumière activée : " + l.name);
                yield return new WaitForSeconds(delayBetweenLights);
            }
        }
    }
}