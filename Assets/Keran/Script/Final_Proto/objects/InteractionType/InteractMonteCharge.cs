using UnityEngine;

public class InteractMonteCharge : MonoBehaviour
{
    [SerializeField] private MonteCharge _monteCharge;

    public void Interact()
    {
        Debug.Log("le bouton marche");
        _monteCharge.closeDoor();
    }
}
