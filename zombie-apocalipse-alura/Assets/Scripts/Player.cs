using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed;
    private Rigidbody _rigidbody;
    private Animator _animator;
    float movHor, movVer;
    private RaycastHit _hit;
    public LayerMask layerMask;
    public bool alive;
    public GameManager gameManager;
    void Start()
    {
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

            if (movHor != 0f || movVer != 0f)
            {
                isWalking(true);
            }
            else
            {
                isWalking(false);
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                gameManager.RestartGame();
            }
        }
    }
    public void isWalking(bool isWalking)
    {
        _animator.SetBool("isWalking", isWalking);
    }
    void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + (new Vector3(movHor, 0, movVer) * playerSpeed * Time.deltaTime));
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.magenta);
        if (Physics.Raycast(ray, out _hit, 100f, layerMask))
        {
            Vector3 aimPosition = _hit.point - transform.position;
            aimPosition.y = this.transform.position.y;

            Quaternion look = Quaternion.LookRotation(aimPosition);
            _rigidbody.MoveRotation(look);
        }
    }
}
