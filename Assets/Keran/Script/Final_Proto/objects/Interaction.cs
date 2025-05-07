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
                gameObject.GetComponent<SpawnObject>().Interact();
                break;
            case InteractionType.Rotation:
                gameObject.GetComponent<InteractRouage>().Interact();
                break;
            case InteractionType.MonteCharge:
                gameObject.GetComponent<InteractMonteCharge>().Interact();
                break;
            case InteractionType.FinaleEnigme:
                gameObject.GetComponent<FinalEnigmeManager>().Interact();
                break;
            case InteractionType.StateChange:
                gameObject.GetComponent<TutoManager>().Interact();
                break;
        }
    }
}
