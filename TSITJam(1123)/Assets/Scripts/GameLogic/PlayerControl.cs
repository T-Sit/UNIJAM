using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerControl : Freezable
{
    private float _lastLevelRotation;
    [SerializeField] private Rotator rotator;
    private Vector3 _baseEu = new(0, 0, 0);
    [SerializeField] private Transform _footPoint;

    override protected void Awake()
    {
        base.Awake();
    }
    void Update()
    {
        DoMovement();
        DoLevelRotation();
    }


    override protected void ReleaseTime()
    {
        base.ReleaseTime();
        transform.SetParent(null);
    }
    override protected void FreezeTime()
    {
        base.FreezeTime();
        transform.SetParent(rotator.transform);
    }


    private void DoLevelRotation()
    {
        if (!_isFreezed || _lastLevelRotation + DesignSettings.Instance.RotationTime - DesignSettings.Instance.RotationTimeRange < Time.time)
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
        else
        {
            transform.eulerAngles = _baseEu;
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
