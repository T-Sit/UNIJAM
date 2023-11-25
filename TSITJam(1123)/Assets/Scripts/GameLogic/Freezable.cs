using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezable : MonoBehaviour
{
    protected bool _isFreezed;
    protected Rigidbody _rb;
    protected Vector3 _freezedVel;
    protected bool _isVelSaved = false;
    virtual protected void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    virtual protected void OnEnable()
    {
        Rotator.RotationStart += FreezeTime;
        Rotator.RotationEnd += ReleaseTime;
    }
    virtual protected void OnDisable()
    {
        Rotator.RotationStart -= FreezeTime;
        Rotator.RotationEnd -= ReleaseTime;
    }
    virtual protected void ReleaseTime()
    {
        _isFreezed = false;
        _rb.useGravity = true;
        _rb.velocity = _freezedVel;
        _isVelSaved = false;
    }

    virtual protected void FreezeTime()
    {
        _isFreezed = true;
        _rb.useGravity = false;
        if (!_isVelSaved)
        {
            _freezedVel = _rb.velocity;
            _isVelSaved = true;
        }
        _rb.velocity = new(0, 0, 0);
    }

}
