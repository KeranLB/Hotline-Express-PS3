using UnityEngine;

public class ObjectGrabber : MonoBehaviour
{
    [Header("Grab Settings")]
    public float grabRange = 3f;
    public Transform holdPoint;
    public LayerMask interactableLayer;
    public float moveSpeed = 10f;

    private Rigidbody grabbedObject = null;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Clic gauche
        {
            if (grabbedObject == null)
                TryGrabObject();
            else
                DropObject();
        }

        if (grabbedObject != null)
        {
            MoveObject();
        }
    }

    void TryGrabObject()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, grabRange, interactableLayer))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null && interactable.interactType == InteractType.Movable)
            {
                grabbedObject = hit.rigidbody;
                grabbedObject.useGravity = false;
                grabbedObject.linearDamping = 10f; // pour �viter les effets glissants
            }
        }
    }

    void MoveObject()
    {
        Vector3 targetPosition = holdPoint.position;
        Vector3 direction = targetPosition - grabbedObject.position;
        grabbedObject.linearVelocity = direction * moveSpeed;
    }

    void DropObject()
    {
        grabbedObject.useGravity = true;
        grabbedObject.linearDamping = 1f;
        grabbedObject = null;
    }
}