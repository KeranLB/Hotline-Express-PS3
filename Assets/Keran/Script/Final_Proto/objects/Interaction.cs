using UnityEngine;

public class Interaction : MonoBehaviour
{
    [Header("Button Parameters :")]
    [SerializeField] private InteractionType _interactionType;

    public void Interact()
    {
        switch (_interactionType)
        {
            case InteractionType.EnterCode :
                gameObject.GetComponent<EnterCode>().Interact();
                break;
            case InteractionType.SpawnObject:
                // get script and call launch fonction
                break;
            case InteractionType.OpenUI:
                // get script and call launch fonction
                break;
            case InteractionType.StateChange:
                // get script and call launch fonction
                break;
            case InteractionType.Rotation:
                gameObject.GetComponent<InteractRouage>().Interact();
                break;
        }
    }
}
