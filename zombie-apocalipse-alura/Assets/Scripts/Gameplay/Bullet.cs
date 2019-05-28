using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour,ISetBox
{
    public float bulletSpeed;
    private Rigidbody _rb;
    private GameManager _gameManager;
    private FixedSizeBox _box;
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _rb = GetComponent<Rigidbody>();
        Destroy(this.gameObject, 1f);
    }
    void Update()
    {
        _rb.MovePosition(_rb.position + transform.forward * bulletSpeed * Time.deltaTime);
        // Vector3.forward will shoot on unity's global forward, 
        // transform forward will shot on MY transform forward position.
    }

    public void SetBox(FixedSizeBox box)
    {
      //  this._box = box;
    }
    void OnTriggerEnter(Collider other)
    {
        Quaternion oppositeBulletRotation = Quaternion.LookRotation(-transform.forward); //transform a direction in a rotation
        switch (other.tag)
        {
            case "Enemy":
                Zombie zommbie = other.GetComponent<Zombie>();
                if (!zommbie.dead)
                {
                    zommbie.Damage(10);
                    zommbie.BloodParticle(this.transform.position, oppositeBulletRotation);
                    _gameManager.ShakeCamera();
                }
                break;

            case "Boss":
                {
                    BossControl boss = other.GetComponent<BossControl>();
                    if (!boss.dead)
                    {
                        boss.TakeDamage(1);
                        boss.BloodParticle(this.transform.position, oppositeBulletRotation);
                        _gameManager.ShakeCamera();
                    }
                }
                break;
        }
        Destroy(this.gameObject);
    }
}
