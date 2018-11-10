using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootControll : MonoBehaviour
{
    public GameObject bullet;
    public Transform aim;
    private float cooldown, nextFire;

    void Start()
    {
        cooldown = 0f;
        nextFire = 0.25f;
    }
    void Update()
    {
        if (Input.GetMouseButton(0) & Time.time > cooldown)
        {
            Instantiate(bullet, aim.transform.position,aim.transform.rotation);
            cooldown = Time.time + nextFire;
        }
    }
}
