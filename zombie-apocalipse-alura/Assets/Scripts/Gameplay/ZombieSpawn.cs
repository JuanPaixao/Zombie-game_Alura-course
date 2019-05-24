using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _zombie;
    public Player player;
    private float _spawnCooldown, _spawnDistance, _timeNextIncreaseDifficult, _countIncreaseDifficult;
    private int _randomNumber, _maxZombiesNumber, _zombiesNumber;
    public LayerMask layerMaskZumbi;



    void Start()
    {
        _timeNextIncreaseDifficult = 30f;
        _countIncreaseDifficult = _timeNextIncreaseDifficult;
        _maxZombiesNumber = 3;
        _spawnDistance = 3;
        _randomNumber = Random.Range(1, 5);
        for (int i = 0; i < _maxZombiesNumber; i++)
        {
            StartCoroutine(SpawnZombie());
        }
    }
    void Update()
    {
        _spawnCooldown += Time.deltaTime;
        if (_spawnCooldown >= _randomNumber && _zombiesNumber < _maxZombiesNumber)
        {
            StartCoroutine(SpawnZombie());
            _spawnCooldown = 0f;
        }

        if (Time.timeSinceLevelLoad > _countIncreaseDifficult)
        {
            _maxZombiesNumber++;
            _countIncreaseDifficult += _timeNextIncreaseDifficult;
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
        Zombie zombie = Instantiate(_zombie, spawnPosition, this.transform.rotation).GetComponent<Zombie>();
        zombie._mySpawn = this;
        _zombiesNumber++;
    }
    public void ZombieKilled()
    {
        _zombiesNumber--;
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
