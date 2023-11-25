using UnityEngine;

public class trap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            Destroy(other.gameObject);
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.PlayerDeath);
        }
    }
}
