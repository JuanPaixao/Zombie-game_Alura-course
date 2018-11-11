using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _zombie;
    public Player player;
    private float _spawnCooldown, _spawnDistance;
    private int _randomNumber;
    public LayerMask layerMaskZumbi;



    void Start()
    {
        _spawnDistance = 3;
        _randomNumber = Random.Range(1, 5);
    }
    void Update()
    {
        _spawnCooldown += Time.deltaTime;
        if (_spawnCooldown >= _randomNumber)
        {
            StartCoroutine(SpawnZombie());
            _spawnCooldown = 0f;
        }
    }
    private IEnumerator SpawnZombie()
    {
        Vector3 spawnPosition = RandomSpawnPosition();
        Collider[] colliders = Physics.OverlapSphere(spawnPosition, 1, layerMaskZumbi);
        while (colliders.Length > 0)
        {
            spawnPosition = RandomSpawnPosition();
            colliders = Physics.OverlapSphere(spawnPosition, 1, layerMaskZumbi);
            yield return null;
        }
        Instantiate(_zombie, spawnPosition, this.transform.rotation);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, _spawnDistance);
    }
    private Vector3 RandomSpawnPosition()
    {
        Vector3 pos = Random.insideUnitSphere * _spawnDistance;
        pos += this.transform.position;
        pos.y = this.transform.position.y;
        return pos;
    }
}
