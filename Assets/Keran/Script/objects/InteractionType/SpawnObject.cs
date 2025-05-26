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
            _importantObjectToSpawn.transform.position = _spawnPoint.position;
            _spawnCount++;
        }
        else if (_spawnCount <= _limitSpawnCount)
        {
            Instantiate(_objectToSpawn, _spawnPoint.position, Quaternion.identity);
            _spawnCount++;
        }
    }
}
