using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody _rb;
    private float _lastLevelRotation;
    [SerializeField] private Rotator rotator;
    private Vector3 _freezedVel;
    private bool _isFreezed;
    private Vector3 _freezedEu;
    [SerializeField] private Transform _footPoint;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        DoMovement();
        DoLevelRotation();
    }
    private void OnEnable()
    {
        Rotator.RotationStart += FreezeTime;
        Rotator.RotationEnd += ReleaseTime;
    }
    private void OnDisable()
    {
        Rotator.RotationStart -= FreezeTime;
        Rotator.RotationEnd -= ReleaseTime;
    }

    private void ReleaseTime()
    {
        _isFreezed = false;
        _rb.useGravity = true;
        _rb.velocity = _freezedVel;
        transform.SetParent(null);
        transform.eulerAngles = _freezedEu;
    }

    private void FreezeTime()
    {
        _isFreezed = true;
        _rb.useGravity = false;
        _freezedVel = _rb.velocity;
        _freezedEu = transform.eulerAngles;
        _rb.velocity = new(0, 0, 0);
        transform.SetParent(rotator.transform);
    }


    private void DoLevelRotation()
    {
        if (Time.time > _lastLevelRotation + DesignSettings.Instance.RotationTime && IsGrounded)
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
        if (!_isFreezed && IsGrounded)
        {
            _rb.AddForce(CalculateMovement(), ForceMode.Force);
        }
    }

    private bool IsGrounded => Physics.OverlapSphere(_footPoint.position,
                                                     DesignSettings.Instance.FootScanRadius,
                                                     DesignSettings.Instance.LayersToStay).Length != 0;


    private Vector3 CalculateMovement()
    {
        float movement = Input.GetAxisRaw("Horizontal") * DesignSettings.Instance.MoveSpeed;
        float speedDif = movement - _rb.velocity.x;
        float accelFactor = Mathf.Abs(movement) > 1e-3f ? DesignSettings.Instance.AccelerationFactor : DesignSettings.Instance.DeccelerationFactor;
        return accelFactor * speedDif * transform.right;
    }
}
