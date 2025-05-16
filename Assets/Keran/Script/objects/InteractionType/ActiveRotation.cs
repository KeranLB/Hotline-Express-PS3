using System.Collections.Generic;
using UnityEngine;

public class ActiveRotation : MonoBehaviour
{
    [SerializeField] private RotationDirection _direction;
    [SerializeField] private List<InteractRouage> _rouages;

    public void Interact()
    {
        switch (_direction)
        {
            case RotationDirection.Positif:
                foreach (var rouage in _rouages)
                {
                    rouage.Interact(1);
                }
                break;
            case RotationDirection.Negatif:
                foreach (var rouage in _rouages)
                {
                    rouage.Interact(-1);
                }
                break;
        }
    }
}
