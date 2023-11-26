using UnityEngine;

public class TrueTrap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerControl>().StopFootsteps();
            Destroy(other.gameObject);
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.PlayerDeath);
        }
    }
}
