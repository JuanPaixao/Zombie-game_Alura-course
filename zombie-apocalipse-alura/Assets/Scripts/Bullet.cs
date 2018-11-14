﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    private Rigidbody _rb;
    private GameManager _gameManager;
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
    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Enemy":
                Zombie zommbie = other.GetComponent<Zombie>();
                if (!zommbie.dead)
                {
                    zommbie.Damage(10);

                    _gameManager.ShakeCamera();
                }
                break;

            case "Boss":
                {
                    BossControl boss = other.GetComponent<BossControl>();
                    if (!boss.dead)
                    {
                        boss.TakeDamage(1);
                        _gameManager.ShakeCamera();
                    }
                }
                break;
        }
        Destroy(this.gameObject);
    }
}
