using UnityEngine;

public class Interaction : MonoBehaviour
{
    [Header("Button Parameters :")]
    [SerializeField] private bool _isButton;
    [SerializeField] private bool _is;

    [SerializeField] private bool _isInteraction;

    public void Interact()
    {
        Debug.Log("interaction");
    }
}
