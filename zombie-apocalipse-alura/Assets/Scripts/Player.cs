using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICharacterDamage
{
    [SerializeField] private Status _status;
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
        hp = _status.initialHP;
        _playerSpeed = _status.speed;
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
            movHor = Input.GetAxis("Horizontal");
            movVer = Input.GetAxis("Vertical");
            _direction = new Vector3(movHor, 0, movVer);
            isWalking(_direction.magnitude);
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                gameManager.RestartGame();
            }
        }
    }
    public void isWalking(float isMoving)
    {
        _animator.SetFloat("isMoving", isMoving);
    }
    void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _direction * _playerSpeed * Time.deltaTime);

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
            gameManager.GameOver();
        }
    }
    public void Die()
    {
        Time.timeScale = 0;
        alive = false;
    }
}
