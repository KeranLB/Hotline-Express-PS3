using UnityEngine;

public class InteractMonteCharge : MonoBehaviour
{
    [SerializeField] private MonteCharge _monteCharge;
    [HideInInspector] public bool asFired = false;

    public void Interact()
    {
        if (!asFired)
        {
            _monteCharge.closeDoor();
        }
    }
}
