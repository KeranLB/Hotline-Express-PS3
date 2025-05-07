using UnityEngine;

public class TrashItem : MonoBehaviour
{
    [SerializeField] private Transform targetSpot;
    [SerializeField] private float placementTolerance = 0.2f;
    [SerializeField] private TrashManager trashManager;

    private bool hasBeenPlaced = false;

    private void Update()
    {
        if (hasBeenPlaced) return;

        if (Vector3.Distance(transform.position, targetSpot.position) <= placementTolerance)
        {
            hasBeenPlaced = true;
            Debug.Log("Trash item placed.");
            trashManager.RegisterObjectPlacement();
        }
    }
}