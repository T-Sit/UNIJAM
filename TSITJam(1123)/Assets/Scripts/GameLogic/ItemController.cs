using UnityEngine;

public class ItemController : Freezable
{
    [SerializeField] private Transform _groundChecker;
    private float _yScalingVelocity;
    private bool _isInHands = false;
    private Transform _oldParent;
    private BoxCollider _collider;

    override protected void Awake()
    {
        base.Awake();
        _collider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (_isInHands)
        {
            transform.localPosition = DesignSettings.Instance.ItemsLocalPosition;
        }
        //else
        //    ApplyGravity();
    }

    public void PickUp(GameObject byWhom)
    {
        _oldParent = transform.parent;
        Rotator.RotationStart -= FreezeTime;
        Rotator.RotationEnd -= ReleaseTime;
        _isInHands = true;
        transform.SetParent(byWhom.transform);
        _rb.useGravity = false;
        transform.localPosition = DesignSettings.Instance.ItemsLocalPosition;
        _collider.enabled = false;

    }

    public void DropDown()
    {
        Rotator.RotationStart += FreezeTime;
        Rotator.RotationEnd += ReleaseTime;
        _isInHands = false;
        transform.SetParent(_oldParent);
        transform.localScale = Vector3.one;
        _rb.useGravity = true;
        _collider.enabled = true;
    }

    //private void ApplyGravity()
    //{
    //    if (!_isFreezed)
    //    {
    //        if (IsGrounded)
    //        {
    //            _yScalingVelocity = 0;
    //        }
    //        else
    //        {
    //            float yVelocity = DesignSettings.Instance.GravityFactor;
    //            _yScalingVelocity += yVelocity;
    //            _rb.velocity = new Vector3(0, _yScalingVelocity, 0);
    //        }
    //    }
    //}

    private bool IsGrounded => Physics.OverlapSphere(_groundChecker.position,
                                                     DesignSettings.Instance.FootScanRadius,
                                                     DesignSettings.Instance.LayersToStay).Length != 0;

}
