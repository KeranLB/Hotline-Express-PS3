using UnityEngine;

public class MeshManager : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshActif;
    [SerializeField] private Mesh _meshFerme;
    [SerializeField] private Mesh _meshOuvert;

    [SerializeField] private BoxCollider _boxColliderFerme;
    [SerializeField] private BoxCollider _boxColliderOuvert;

    private void Start()
    {
        _boxColliderFerme.enabled = true;
        _boxColliderOuvert.enabled = false;

        _meshActif.mesh = _meshFerme;
    }

    public void Open()
    {
        _meshActif.mesh = _meshOuvert;

        _boxColliderFerme.enabled = false;
        _boxColliderOuvert.enabled = true;
    }
}
