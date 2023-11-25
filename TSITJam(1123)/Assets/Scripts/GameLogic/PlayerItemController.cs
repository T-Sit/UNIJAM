using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerItemController : MonoBehaviour
{
    private ItemController _pickedItem;


    public void ParsePickDropButton()
    {
        if (_pickedItem is null)
        {
            Pick();
        }
        else
        {
            Drop();
        }
    }

    private void Drop()
    {
        _pickedItem.DropDown();
        _pickedItem = null;
    }

    private void Pick()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, DesignSettings.Instance.PickUpDistance, DesignSettings.Instance.ItemsLayer);
        foreach (Collider c in col)
        {
            ItemController ic = c.GetComponent<ItemController>();
            if (ic is not null && CanPickUp(c))
            {
                ic.PickUp(gameObject);
                _pickedItem = ic;
                break;
            }
        }
    }
    private bool CanPickUp(Collider c)
    {
        Vector3 p = c.transform.position - transform.position;
        return Mathf.Tan(Mathf.Deg2Rad * DesignSettings.Instance.MinPickupAngle) <= p.y / p.x
               && Mathf.Tan(Mathf.Deg2Rad * DesignSettings.Instance.MaxPickupAngle) >= p.y / p.x
               && p.x * transform.forward.x >= 0;
    }
}
