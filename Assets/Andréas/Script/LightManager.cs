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
        Debug.Log("Activation de la premi�re lumi�re");
        SetLightState(light1, true);
    }

    public void SwitchToLight2()
    {
        Debug.Log("Switch vers la deuxi�me lumi�re");
        SetLightState(light1, false);
        SetLightState(light2, true);
    }

    public void StartSequentialLights()
    {
        Debug.Log("D�marrage de l'activation s�quentielle des lumi�res");
        StartCoroutine(ActivateLightsOneByOne());
    }

    private IEnumerator ActivateLightsOneByOne()
    {
        foreach (var l in sequentialLights)
        {
            if (l != null)
            {
                l.enabled = true;
                Debug.Log("Lumi�re activ�e : " + l.name);
                yield return new WaitForSeconds(delayBetweenLights);
            }
        }
    }
}