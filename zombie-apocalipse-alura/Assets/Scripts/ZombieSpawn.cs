using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _zombie;
    public Player player;
    private float _spawnCooldown;
	private int  _randomNumber;
    void Start()
    {
        _randomNumber = Random.Range(1, 5);
    }
    void Update()
    {
        _spawnCooldown += Time.deltaTime;
        if (_spawnCooldown >= _randomNumber)
        {
            Instantiate(_zombie, this.transform.position, this.transform.rotation);
            _spawnCooldown = 0f;
        }
    }
}
