﻿using UnityEngine;

public class CameraMoves : MonoBehaviour
{
    [SerializeField] private  Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private  float _smoothTime = 0.25f;
    private void Start()
    {
        transform.position = _target.position + _offset;
    }
    private void FixedUpdate()
    {
        if (_target != null)
        {
            Vector3 _nextPos = _target.position + _offset;
            Vector3 _newPos = Vector3.Lerp(transform.position, _nextPos, _smoothTime);
            transform.position = _newPos;
        }

    }
}
