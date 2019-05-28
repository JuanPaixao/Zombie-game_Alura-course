using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour, ICharacterDamage, ISetBox
{
    public float speed;
    private GameObject _player;
    private float _distance, _changeRandomPosition, _cooldownChangePos, _dropChance;
    private int _selectZombie, _hp;
    private CharactersMovement _enemyMovement;
    [SerializeField] private AudioClip _deathZombieClip;
    private Vector3 _randomPosition, _direction;
    public bool followingPlayer, closeEnough, dead;
    private Animator _animator;
    public GameObject medicalKit;
    private UIManager _uiManager;
    [HideInInspector] public ZombieSpawn _mySpawn;
    [SerializeField] private GameObject _bloodParticle;
    [SerializeField] private FixedSizeBox _zombieBox;
    void Start()
    {
        _dropChance = 0.2f;
        _animator = GetComponent<Animator>();
        _cooldownChangePos = 3;
        _hp = 10;
        _enemyMovement = GetComponent<CharactersMovement>();
        _selectZombie = Random.Range(1, transform.childCount);
        transform.GetChild(_selectZombie).gameObject.SetActive(true);
        _player = GameObject.FindGameObjectWithTag("Player");
        _uiManager = GameObject.FindObjectOfType(typeof(UIManager)) as UIManager;
    }
    void FixedUpdate()
    {
        if (!dead)
        {
            if (_direction.magnitude > 2)
            {
                ZombieMoving(true);
            }
            else
            {
                ZombieMoving(false);
            }
            _distance = Vector3.Distance(_player.transform.position, this.transform.position);
            if (_distance > 15f)
            {
                followingPlayer = false;
                WalkRandom();
                _enemyMovement.Rotation(_direction);
            }
            else if (_distance > 2.25f)
            {
                _direction = _player.transform.position - transform.position;
                _enemyMovement.SetDirection(_direction);
                followingPlayer = true;
                _enemyMovement.Rotation(_direction);
                _enemyMovement.Movement(speed);
                GetComponent<Animator>().SetBool("Attacking", false);
            }
            else
            {
                GetComponent<Animator>().SetBool("Attacking", true);
                _enemyMovement.Rotation(_direction);
            }
        }
    }


    void WalkRandom()
    {
        _changeRandomPosition -= Time.deltaTime;
        if (_changeRandomPosition <= 0)
        {
            _randomPosition = RandomPos();
            _changeRandomPosition = _cooldownChangePos + Random.Range(-1f, 7.5f);
        }
        closeEnough = (Vector3.Distance(transform.position, _randomPosition) <= 1);
        if (!closeEnough)
        {
            _direction = _randomPosition - this.transform.position;
            _enemyMovement.SetDirection(_direction);
            _enemyMovement.Movement(speed);
        }
    }
    private Vector3 RandomPos()
    {
        Vector3 pos = Random.insideUnitSphere * 15;
        pos += this.transform.position;
        pos.y = transform.position.y;

        return pos;
    }
    void AttackingPlayer()
    {
        int randomDamage = Random.Range(20, 31);
        if (_distance <= 2.9f)
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
    public void BloodParticle(Vector3 position, Quaternion rotation)
    {
        Instantiate(_bloodParticle, position, rotation);
    }
    public void Die()
    {
        dead = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
        _animator.SetTrigger("isDead");
        _uiManager.UpdateZombieCount();
        MedicalKitSpawn(_dropChance);
        Invoke("ReturnToBox", 2); //execute my function after 2 seconds
    }
    private void ZombieMoving(bool move)
    {
        _animator.SetBool("isMoving", move);
    }
    private void MedicalKitSpawn(float spawnPercentage)
    {
        Debug.Log(spawnPercentage);
        if (Random.value <= spawnPercentage)
        {
            Instantiate(medicalKit, this.transform.position, this.transform.rotation);
        }
    }
    private void ReturnToBox()
    {
        this._zombieBox.ReturnObject(this.gameObject);
    }
    public void SetBox(FixedSizeBox box)
    {
        this._zombieBox = box;
    }
    public void ActiveZombie()
    {
        Collider collider = GetComponent<Collider>();
        collider.enabled = true;
        dead = false;
    }
}
