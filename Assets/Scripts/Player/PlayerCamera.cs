using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] [Range(0.1f, 3f)] private float _sense = 1.3f;
    [SerializeField] private float _followSpeed = 10f; 
    [SerializeField] private float _yClampAngle = 90f;
    [SerializeField] private float _xClampAngle = 90f;
    private float _senseMultiplier = 150f;
    //
    private float _x;
    private float _y;
    private void Update()
    {
        var position = Vector3.Lerp(transform.position, _player.position, (Time.deltaTime * _followSpeed));
        transform.position = position;

        CameraRotation();
    }

    private void CameraRotation()
    {
        float x = Input.GetAxis("Mouse X");
        float y = -Input.GetAxis("Mouse Y");

        _y += y * (Time.deltaTime * _sense * _senseMultiplier);
        _x += x * (Time.deltaTime * _sense * _senseMultiplier);

        _y = Mathf.Clamp(_y, -_yClampAngle, _yClampAngle);
        _x = Mathf.Clamp(_x, -_xClampAngle, _xClampAngle);

        transform.rotation = Quaternion.Euler(new Vector3(_y, _x, 0f));
    }
}
