using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerItemController : MonoBehaviour
{
    private ItemController _pickedItem;
    private void Update()
    {
        ParsePickDropButton();
    }

    private void ParsePickDropButton()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_pickedItem is null)
            {
                Collider[] col = Physics.OverlapSphere(transform.position, DesignSettings.Instance.PickUpDistance, DesignSettings.Instance.ItemsLayer);
                foreach (Collider c in col)
                {
                    Debug.Log("Check");
                    ItemController ic = c.GetComponent<ItemController>();
                    if (ic is not null)
                    {
                        ic.PickUp(gameObject);
                        _pickedItem = ic;
                        break;
                    }
                }
            }
            else
            {
                _pickedItem.DropDown();
                _pickedItem = null;
            }
        }
    }
}
