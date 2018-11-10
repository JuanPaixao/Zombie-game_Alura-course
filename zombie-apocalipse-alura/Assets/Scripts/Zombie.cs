using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{

    public float speed;
    private Rigidbody _rb;
    private GameObject _player;
    private GameManager _gameManager;
    private float _distance;
    private int _selectZombie;
    void Start()
    {
        _selectZombie = Random.Range(1, 28);
        transform.GetChild(_selectZombie).gameObject.SetActive(true);
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = gameObject.GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        Vector3 direction = _player.transform.position - transform.position;
        _distance = Vector3.Distance(_player.transform.position, this.transform.position);

        Quaternion newRot = Quaternion.LookRotation(direction);
        _rb.MoveRotation(newRot);

        if (_distance > 2.25f)
        {
            _rb.MovePosition(_rb.position + direction.normalized * Time.deltaTime * speed);
            GetComponent<Animator>().SetBool("Attacking", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Attacking", true);
        }
    }
    void AttackingPlayer()
    {
        if (_distance <= 2.45f)
        {
            Time.timeScale = 0;
            _player.GetComponent<Player>().alive = false;
            _gameManager.GameOver();
        }
    }
}
