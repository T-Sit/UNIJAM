using System.Collections;
using UnityEngine;

public class Freezable : MonoBehaviour
{
    [SerializeField] protected Vector3 FootScanHalfBox;
    [SerializeField] protected Vector3 FootpointShift;
    public bool IsBounced = false;
    protected bool _isFreezed;
    protected Rigidbody _rb;
    protected Vector3 _freezedVel;
    protected bool _isVelSaved = false;
    protected float _yVel = 0f;


    virtual protected void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
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
        // _rb.velocity = _freezedVel;
        _isVelSaved = false;
    }

    virtual protected void FreezeTime()
    {
        _isFreezed = true;
        // if (!_isVelSaved)
        // {
        //     _freezedVel = _rb.velocity;
        //     _isVelSaved = true;
        // }
        _rb.velocity = new(0, 0, 0);
        _yVel = 0;
    }

    protected bool IsGrounded()
    {
        Collider[] col = ScanFoot();
        foreach (Collider c in col)
        {
            if ((DesignSettings.Instance.LayersToStay & 1 << c.gameObject.layer) != 0)
                if (!ReferenceEquals(c.gameObject, gameObject))
                {
                    return true;
                }
        }
        return false;
    }

    protected Collider[] ScanFoot()
    {
        Vector3 pos = transform.position + FootpointShift;
        Debug.DrawLine(pos + FootScanHalfBox.x * Vector3.right, pos + FootScanHalfBox.x * Vector3.left, Color.blue);
        Debug.DrawLine(pos + FootScanHalfBox.y * Vector3.up, pos + FootScanHalfBox.y * Vector3.down, Color.green);
        return Physics.OverlapBox(center: pos,
                                            halfExtents: FootScanHalfBox,
                                            Quaternion.identity
                                            );
    }

    virtual protected void Update()
    {
        ApplyGravity();
    }


    virtual protected void ApplyGravity()
    {
        if (_isFreezed) { return; }
        Collider[] col = ScanFoot();
        foreach (Collider c in col)
        {
            if (ReferenceEquals(c.gameObject, gameObject)) { continue; }
            if (0 != (1 << c.gameObject.layer & DesignSettings.Instance.LayersToBounce))
            {
                //jump
                BouncePlatform b = c.gameObject.GetComponent<BouncePlatform>();
                if (b is not null) { _yVel *= -b.BounceFactor; }
                _rb.MovePosition(transform.position + _yVel * Time.deltaTime * Vector3.up);
                return;
            }
            else if (0 != (1 << c.gameObject.layer & DesignSettings.Instance.LayersToStay))
            {
                _yVel = 0;  //stay
                return;
            }
        }
        _yVel -= DesignSettings.Instance.Gravity * Time.deltaTime;
        _rb.MovePosition(_yVel * Time.deltaTime * Vector3.up + transform.position);
    }


}
