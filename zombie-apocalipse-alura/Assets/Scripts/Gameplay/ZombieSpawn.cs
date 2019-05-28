using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    [SerializeField] private FixedSizeBox _zombieBox;

    public Player player;
    private float _spawnCooldown, _spawnDistance, _timeNextIncreaseDifficult, _countIncreaseDifficult;
    private int _randomNumber;
    public LayerMask layerMaskZumbi;



    void Start()
    {
        _timeNextIncreaseDifficult = 30f;
        _countIncreaseDifficult = _timeNextIncreaseDifficult;   
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

        if (Time.timeSinceLevelLoad > _countIncreaseDifficult)
        {
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
        if (this._zombieBox.StackIsNotEmpity())
        {
            GameObject zombie = this._zombieBox.GetObject();
            zombie.transform.position = spawnPosition;
            Zombie zombieControl = zombie.GetComponent<Zombie>();
            zombieControl._mySpawn = this;
           zombieControl.ActiveZombie();
            
            
        }
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
