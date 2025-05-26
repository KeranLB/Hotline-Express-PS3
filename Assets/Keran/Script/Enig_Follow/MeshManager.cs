using UnityEngine;

public class MeshManager : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshActif;
    [SerializeField] private Mesh _meshClose;
    [SerializeField] private Mesh _meshOpen;

    [SerializeField] private BoxCollider _boxColliderClose;
    [SerializeField] private BoxCollider _boxColliderOpen;

    [SerializeField] private MeshRenderer _materialActif;
    [SerializeField] private Material _materialClose;
    [SerializeField] private Material _materialOpen;

    private void Start()
    {
        _boxColliderClose.enabled = true;
        _boxColliderOpen.enabled = false;

        _materialActif.material = _materialClose;

        _meshActif.mesh = _meshClose;
    }

    public void Open()
    {
        _meshActif.mesh = _meshOpen;
        _materialActif.material = _materialOpen;

        _boxColliderClose.enabled = false;
        _boxColliderOpen.enabled = true;
    }
}
