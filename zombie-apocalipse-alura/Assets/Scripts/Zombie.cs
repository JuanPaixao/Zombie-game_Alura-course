using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour, ICharacterDamage
{
    public float speed;
    private GameObject _player;
    private float _distance;
    private int _selectZombie, _hp;
    private CharactersMovement _enemyMovement;
    [SerializeField] private AudioClip _deathZombieClip;

    void Start()
    {
        _hp = 10;
        _enemyMovement = GetComponent<CharactersMovement>();
        _selectZombie = Random.Range(1, 28);
        transform.GetChild(_selectZombie).gameObject.SetActive(true);
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    void FixedUpdate()
    {
        Vector3 direction = _player.transform.position - transform.position;
        _distance = Vector3.Distance(_player.transform.position, this.transform.position);
        _enemyMovement.Rotation(direction);


        if (_distance > 2.25f)
        {
            _enemyMovement.Movement(direction, speed);
            GetComponent<Animator>().SetBool("Attacking", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Attacking", true);
        }
    }
    void AttackingPlayer()
    {
        int randomDamage = Random.Range(20, 31);
        if (_distance <= 2.45f)
        {
            _player.GetComponent<Player>().Damage(randomDamage);
        }
    }
    public void Damage(int damage)
    {
        _hp -= damage;
        AudioManager.instance.PlayOneShot(_deathZombieClip, 0.7f);
        if (_hp <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(this.gameObject);
    }
}
