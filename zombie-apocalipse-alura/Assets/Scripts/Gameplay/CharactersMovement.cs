using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersMovement : MonoBehaviour
{

    private Rigidbody _rb;
    public Vector3 direction { get; protected set; }
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void Movement(float speed)
    {
        _rb.MovePosition(_rb.position + direction * Time.deltaTime * speed);
    }
    public void SetDirection(Vector2 direction)
    {
        this.direction = new Vector3(direction.x, 0, direction.y);
    }
    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }
    public void Rotation(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Quaternion newRot = Quaternion.LookRotation(direction);
            _rb.MoveRotation(newRot);
        }
    }

}
