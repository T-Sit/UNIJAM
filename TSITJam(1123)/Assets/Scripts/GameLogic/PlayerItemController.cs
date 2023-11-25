using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
            if (ic is not null)
            {
                ic.PickUp(gameObject);
                _pickedItem = ic;
                break;
            }
        }
    }
}
