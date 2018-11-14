using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{

    private float _spawnCooldown, _timeToSpawn;
    public GameObject boss;

    void Start()
    {
		_spawnCooldown = 20f;
        _timeToSpawn = _spawnCooldown;
    }
    void Update()
    {
        if (Time.timeSinceLevelLoad > _timeToSpawn)
        {
            Instantiate(boss, this.transform.position, this.transform.rotation);
            _timeToSpawn = Time.timeSinceLevelLoad + _spawnCooldown;
        }
    }
}
