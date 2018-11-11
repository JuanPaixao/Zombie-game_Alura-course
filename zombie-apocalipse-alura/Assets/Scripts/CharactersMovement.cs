using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersMovement : MonoBehaviour
{

    private Rigidbody _rb;
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void Movement(Vector3 direction, float speed)
    {
        _rb.MovePosition(_rb.position + direction.normalized * Time.deltaTime * speed);
    }
    public void Rotation(Vector3 direction)
    {
        Quaternion newRot = Quaternion.LookRotation(direction);
        _rb.MoveRotation(newRot);
    }
}
