using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICharacterDamage, IHeal
{
    public Status status;
    private float _playerSpeed;
    private Rigidbody _rigidbody;
    private Animator _animator;
    float movHor, movVer;
    private RaycastHit _hit;
    public LayerMask layerMask;
    public bool alive;
    public GameManager gameManager;
    public int hp;
    public UIManager uIManager;
    [SerializeField] private AudioClip _audioClipDamage;
    private PlayerMovement _playerMovement;
    private Vector3 _direction;

    void Awake()
    {
        hp = status.initialHP;
        _playerSpeed = status.speed;
    }
    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        alive = true;
        _animator = gameObject.GetComponent<Animator>();
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        Time.timeScale = 1;
    }
    void Update()
    {
        if (alive)
        {
            isWalking(_direction.magnitude);
            Debug.Log("Direction magnitude" + _direction.magnitude);
        }
    }
    public void isWalking(float isMoving)
    {
        _animator.SetFloat("isMoving", isMoving);
    }
    void FixedUpdate()
    {
        _playerMovement.Movement(_playerSpeed);
        _playerMovement.PlayerMov(layerMask, _hit);
    }
    public void Damage(int damage)
    {
        hp -= damage;
        uIManager.SetHP();
        AudioManager.instance.PlayOneShot(_audioClipDamage, 0.6f);
        if (hp <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Time.timeScale = 0;
        alive = false;
        uIManager.GameOver();
    }
    public void Heal(int healAmount)
    {
        hp += healAmount;
        if (hp > status.initialHP)
        {
            hp = status.initialHP;
        }
        uIManager.SetHP();
    }
}
