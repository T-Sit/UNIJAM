using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody _rb;
    private float _lastLevelRotation;
    [SerializeField] private Rotator rotator;
    private Vector3 _freezedVel;

    private void Awake()
    {
        // _rotator = transform.parent.GetComponent<Rotator>();
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        DoMovement();
        DoLevelRotation();
    }
    private void OnEnable()
    {
        Rotator.RotationStart += FreezeGravity;
        Rotator.RotationEnd += ReleaseGravity;
    }
    private void OnDisable()
    {
        Rotator.RotationStart -= FreezeGravity;
        Rotator.RotationEnd -= ReleaseGravity;
    }

    private void ReleaseGravity()
    {
        Debug.Log("Release");
        _rb.useGravity = true;
        _rb.velocity = _freezedVel;
    }

    private void FreezeGravity()
    {
        Debug.Log("Freeze");
        _rb.useGravity = false;
        _freezedVel = _rb.velocity;
        _rb.velocity = new(0, 0, 0);
    }


    private void DoLevelRotation()
    {
        if (Time.time > _lastLevelRotation + DesignSettings.Instance.RotationTime)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                _lastLevelRotation = Time.time;
                rotator.RotateLeft();
            }
            else if (Input.GetKey(KeyCode.E))
            {
                _lastLevelRotation = Time.time;
                rotator.RotateRight();
            }
        }
    }

    private void DoMovement()
    {
        transform.Translate(CurrentMovement);
    }
    private Vector3 CurrentMovement => DesignSettings.Instance.MoveSpeed * new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

}
