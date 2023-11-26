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
        return Mathf.Tan(Mathf.Deg2Rad * DesignSettings.Instance.MinPickupAngle) <= p.y / p.x       // Min angle check
               && Mathf.Tan(Mathf.Deg2Rad * DesignSettings.Instance.MaxPickupAngle) >= p.y / p.x    // Max angle check
                && p.x * transform.forward.x >= 0; // object position and sight along x are both positive or both negative
    }
}
