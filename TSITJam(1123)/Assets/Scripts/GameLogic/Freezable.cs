using UnityEngine;
using UnityEngine.UI;

public class Freezable : MonoBehaviour
{
    [SerializeField] protected Vector3 FootScanHalfBox;
    [SerializeField] protected Vector3 FootpointShift;
    protected bool _isFreezed;
    protected Rigidbody _rb;
    protected Vector3 _freezedVel;
    protected bool _isVelSaved = false;
    protected float _yScalingVelocity;

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
        // _rb.useGravity = true;
        _rb.velocity = _freezedVel;
        _isVelSaved = false;
    }

    virtual protected void FreezeTime()
    {
        _isFreezed = true;
        // _rb.useGravity = false;
        if (!_isVelSaved)
        {
            _freezedVel = _rb.velocity;
            _isVelSaved = true;
        }
        _rb.velocity = new(0, 0, 0);
    }

    protected bool IsGrounded()
    {
        // // Collider[] col = Physics.OverlapSphere(transform.position + FootpointShift, FootScanRadius, DesignSettings.Instance.LayersToStay);
        Vector3 pos = transform.position + FootpointShift;
        Collider[] col = Physics.OverlapBox(center: pos,
                                            halfExtents: FootScanHalfBox,
                                            Quaternion.identity,
                                            DesignSettings.Instance.LayersToStay);
        // Debug.DrawLine(pos + FootScanHalfBox.x * Vector3.right, pos + FootScanHalfBox.x * Vector3.left,Color.blue);
        // Debug.DrawLine(pos + FootScanHalfBox.y * Vector3.up, pos + FootScanHalfBox.y * Vector3.down,Color.green);

        foreach (Collider c in col)
        {
            if (!ReferenceEquals(c.gameObject, gameObject))
            {
                return true;    // if there is another object of this layers in the scan sphere
            }
        }
        return false;
    }
    virtual protected void Update()
    {
        ApplyGravity();
    }
    virtual protected void ApplyGravity()
    {
        if (!_isFreezed)
        {
            if (IsGrounded())
            { _yScalingVelocity = 0; }
            else
            {
                float yVelocity = DesignSettings.Instance.GravityFactor;
                _yScalingVelocity += yVelocity;
                _rb.velocity = new Vector3(0, _yScalingVelocity, 0);
            }
        }
    }
}
