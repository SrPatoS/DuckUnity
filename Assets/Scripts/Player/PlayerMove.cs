using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private CharacterController _controller;    
    [SerializeField] private float _moveSpeed = 5f;
    private float rotateSpeed = 10f;
    private Vector3 _targetDirection;
    private bool _aiming;
    private Vector3 _move;
    private Vector3 _smoothMove;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        _aiming = Input.GetKey(KeyCode.Mouse1);
        Move();   
    }

    private void Move()
    {
        float y = Input.GetAxisRaw("Vertical");
        float x = Input.GetAxisRaw("Horizontal");

        if(y != 0 || x != 0)
        {
            _smoothMove = Vector3.Lerp(_smoothMove, _move, Time.deltaTime * 10);
            if(_aiming)
            {
                ChangePlayerDirection(new Vector3(_camera.forward.x, 0f, _camera.forward.z));
                ChangeInput(_camera.forward);
            } 
            else
            {
                ChangePlayerDirection(new Vector3(x, 0f, y));
                ChangeInput(new Vector3(x, 0f, y));
            }

            Quaternion targetRotation = Quaternion.LookRotation(_targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        }
        else
        {
            _smoothMove = Vector3.Lerp(_smoothMove, Vector3.zero, Time.deltaTime * 10);
        }

        _controller.Move(_smoothMove * (Time.deltaTime * _moveSpeed));
    }

    private void ChangePlayerDirection(Vector3 dir)
    {
        _targetDirection = dir;
    }

    private void ChangeInput(Vector3 dir)
    {
        _move = dir;
    }
}   
