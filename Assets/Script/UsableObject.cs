using UnityEngine;

public class UsableObject : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float useDistance = 4f;
    public Transform playerCamera;
    public GameObject interactionPrompt;

    [Header("Target Action")]
    public GameObject targetToAffect;
    public enum ActionType { ToggleActive, Move }
    public ActionType action = ActionType.ToggleActive;
    public Vector3 moveOffset = new Vector3(2f, 0f, 0f);

    private bool isInRange = false;
    private bool hasMoved = false;
    private Vector3 originalPosition;

    void Start()
    {
        if (action == ActionType.Move && targetToAffect != null)
        {
            originalPosition = targetToAffect.transform.position;
        }
    }

    void Update()
    {
        CheckPlayerDistance();

        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            ExecuteAction();
        }
    }

    void CheckPlayerDistance()
    {
        float dist = Vector3.Distance(transform.position, playerCamera.position);
        bool shouldShow = dist <= useDistance;

        if (interactionPrompt != null)
            interactionPrompt.SetActive(shouldShow);

        isInRange = shouldShow;
    }

    void ExecuteAction()
    {
        if (targetToAffect == null) return;

        switch (action)
        {
            case ActionType.ToggleActive:
                targetToAffect.SetActive(!targetToAffect.activeSelf);
                break;

            case ActionType.Move:
                if (!hasMoved)
                {
                    targetToAffect.transform.position += moveOffset;
                    hasMoved = true;
                }
                else
                {
                    targetToAffect.transform.position = originalPosition;
                    hasMoved = false;
                }
                break;
        }
    }
}