using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    [SerializeField] private int m_sceneToLoad;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.PlayerFall);
            LoadScene();
        } 
    }

    private void LoadScene() 
        => SceneManager.LoadScene(m_sceneToLoad);
}
