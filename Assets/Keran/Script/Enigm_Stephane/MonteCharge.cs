using UnityEngine;

public class MonteCharge : MonoBehaviour
{
    [SerializeField] private GameObject _rouage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            other.gameObject.SetActive(false);
            _rouage.SetActive(true);
        }
    }
}
