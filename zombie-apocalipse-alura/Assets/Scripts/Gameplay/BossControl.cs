using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossControl : MonoBehaviour
{

    private Transform _player;
    private NavMeshAgent _agent;
    public Status bossStatus;
    private Animator _animator;
    private CharactersMovement _bossMovement;
    public bool dead;
    private UIManager _uiManager;
    private int _hp = 7;
    private int _maxHP;
    public GameObject MedicKit,bloodParticle;
    public Slider sliderBossHP;
    public Image imageSlider;

    [SerializeField] private AudioClip _zombieClip;
    [SerializeField] private AudioClip _deathZombieClip;
    public Color minColor, maxColor;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _bossMovement = GetComponent<CharactersMovement>();
        _uiManager = GameObject.FindObjectOfType(typeof(UIManager)) as UIManager;
        _agent.speed = bossStatus.speed;
        _maxHP = _hp;
        sliderBossHP.maxValue = _hp;
        UpdateLifebar();
    }
    void Update()
    {

        if (!dead)
        {
            _agent.SetDestination(_player.position);

            if (_agent.velocity.magnitude > 0.5f)
            {
                BossMoving(true);
            }
            else
            {
                BossMoving(false);
            }
            if (_agent.hasPath) // i need to have a path to calculate if i'm actually on range
            {
                bool attackDistance = _agent.remainingDistance <= _agent.stoppingDistance;
                if (attackDistance)
                {
                    BossAttack(true);
                    Vector3 direcao = _player.position - this.transform.position;
                    _bossMovement.Rotation(direcao);
                }
                else
                {
                    BossAttack(false);
                }
            }
        }
    }
    private void BossMoving(bool move)
    {
        _animator.SetBool("isMoving", move);
    }
    private void BossAttack(bool attack)
    {
        _animator.SetBool("Attacking", attack);
    }
    public void AttackingPlayer()
    {
        int randomDamage = Random.Range(30, 40);
        _player.GetComponent<Player>().Damage(randomDamage);
    }
    public void TakeDamage(int damage)
    {
        _hp -= damage;
        UpdateLifebar();
        AudioManager.instance.PlayOneShot(_zombieClip, 0.7f);
        if (_hp <= 0)
        {
            Die();
        }
    }
    public void BloodParticle(Vector3 position, Quaternion rotation)
    {
        Instantiate(bloodParticle, position, rotation);
    }
    void Die()
    {
        Instantiate(MedicKit, this.transform.position, this.transform.rotation);
        dead = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Collider collider = GetComponent<Collider>();
        AudioManager.instance.PlayOneShot(_deathZombieClip, 0.7f);
        collider.enabled = false;
        _animator.SetTrigger("isDead");
        _uiManager.UpdateZombieCount();
        _agent.enabled = false;
        Destroy(this.gameObject, 10f);
    }
    void UpdateLifebar()
    {
        sliderBossHP.value = _hp;
        float lifePercentage = (float)_hp / _maxHP;
        Color lifebar = Color.Lerp(minColor, maxColor,lifePercentage);
        imageSlider.color = lifebar;
    }
    
}
