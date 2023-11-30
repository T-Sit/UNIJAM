using UnityEngine;

public class ItemController : Freezable
{
    private bool _isInHands = false;
    private Transform _oldParent;
    private BoxCollider _collider;

    override protected void Awake()
    {
        base.Awake();
        _collider = GetComponent<BoxCollider>();
    }

    override protected void Update()
    {
        base.Update();
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
        _yVel = 0;
    }

}
