using UnityEngine;

public class BouncePlatform : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Items"))
        {
            if (collision.gameObject.GetComponent<Freezable>() != null)
                collision.gameObject.GetComponent<Freezable>().IsBounced = true;
        }

    }
}
