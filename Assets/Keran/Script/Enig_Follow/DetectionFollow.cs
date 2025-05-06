using UnityEngine;

public class DetectionFollow : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Prompteur>().isShowingTime = false;
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<Prompteur>().isShowingTime = true;
    }
}
