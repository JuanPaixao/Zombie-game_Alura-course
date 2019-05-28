using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{

    private float _spawnCooldown, _timeToSpawn;
    public GameObject boss;
    private UIManager _uIManager;
    public Transform[] spawnPositions;
    private Transform _player;
    private FixedSizeBox _box;

    void Start()
    {
        _uIManager = GameObject.FindObjectOfType(typeof(UIManager)) as UIManager;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _spawnCooldown = 20f;
        _timeToSpawn = _spawnCooldown;
    }
    void Update()
    {
        if (Time.timeSinceLevelLoad > _timeToSpawn)
        {
            Vector3 spawnPos = CheckDistantSpawnPos();
            Instantiate(boss, spawnPos, Quaternion.identity);
            _timeToSpawn = Time.timeSinceLevelLoad + _spawnCooldown;
            _uIManager.BossSpawnText();
        }
    }
    private Vector3 CheckDistantSpawnPos()
    {
        Vector3 biggerDistancePosition = Vector3.zero;
        float biggerDistance = 0;
        foreach (Transform spawnPosition in spawnPositions)
        {
            float distanceToPlayer = Vector3.Distance(spawnPosition.position, _player.transform.position);

            if (distanceToPlayer > biggerDistance)
            {
                biggerDistance = distanceToPlayer;
                biggerDistancePosition = spawnPosition.position;
            }
        }
        return biggerDistancePosition;
    }
}
 
