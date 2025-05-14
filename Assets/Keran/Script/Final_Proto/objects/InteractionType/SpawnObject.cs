using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private GameObject _importantObjectToSpawn;
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private Transform _spawnPoint;
    private int _spawnCount;
    [SerializeField] private int _limitSpawnCount;

    public void Interact()
    {
        if (_spawnCount == 0)
        {
            Debug.Log("Distrib Item Eude");
            Instantiate(_importantObjectToSpawn, _spawnPoint.position, Quaternion.identity);
            _spawnCount++;
        }
        else if (_spawnCount <= _limitSpawnCount)
        {
            Debug.Log("Spawn canette");
            Instantiate(_objectToSpawn, _spawnPoint.position, Quaternion.identity);
            _spawnCount++;
        }
    }
}
