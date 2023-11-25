using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : Freezable
{
    private bool _isInHands=false;
    private Transform _oldParent;
    public void PickUp(GameObject byWhom){
        _oldParent = transform.parent;
        Rotator.RotationStart -= FreezeTime;
        Rotator.RotationEnd -= ReleaseTime;
        _isInHands = true;
        transform.SetParent(byWhom.transform);
        _rb.useGravity = false;
    }
    public void DropDown(){
        Rotator.RotationStart += FreezeTime;
        Rotator.RotationEnd += ReleaseTime;
        _isInHands = false;
        transform.SetParent(_oldParent);
        transform.localScale = Vector3.one;
        _rb.useGravity = true;
    }
    private void Update()
    {
        if (_isInHands)
        {
            transform.localPosition = DesignSettings.Instance.ItemsLocalPosition;
        }
    }
}
