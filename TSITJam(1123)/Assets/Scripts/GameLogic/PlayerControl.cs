using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerControl : Freezable
{
    private float _lastLevelRotation;
    [SerializeField] private Rotator rotator;
    private Vector3 _baseEu = new(0, 0, 0);
    [SerializeField] private Transform _footPoint;
    private Vector3 _rightRot = new(0, 0, 0);
    private Vector3 _leftRot = new(0, 180, 0);
    private PlayerItemController _playerItemController;

    override protected void Awake()
    {
        base.Awake();
        _playerItemController = GetComponent<PlayerItemController>();
    }
    void Update()
    {
        DoMovement();
        DoPlayerRotation();
        DoPickUpCheck();
        DoLevelRotation();
    }

    private void DoPickUpCheck()
    {
        if (!_isFreezed && IsGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _playerItemController.ParsePickDropButton();
            }
        }

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
        if ((IsGrounded
            && !_isFreezed)
            || (Time.time <= _lastLevelRotation  + DesignSettings.Instance.RotationTime+ DesignSettings.Instance.RotationTimeRange/2
            && Time.time >= _lastLevelRotation + DesignSettings.Instance.RotationTime-+ DesignSettings.Instance.RotationTimeRange/2))
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

    private void DoPlayerRotation()
    {
        if (!_isFreezed && IsGrounded)
        {
            float x = Input.GetAxisRaw("Horizontal");
            if (x > 0)
            {
                transform.eulerAngles = _rightRot;
            }
            else if (x < 0)
            {
                transform.eulerAngles = _leftRot;
            }
        }
    }
    private void DoMovement()
    {
        if (!_isFreezed && IsGrounded)
        {
            Vector3 movement = CalculateMovement();

            _rb.AddForce(movement, ForceMode.Force);
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
        return accelFactor * speedDif * Vector3.right;
    }
}
