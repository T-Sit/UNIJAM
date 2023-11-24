using UnityEngine;

public class MusicChangeTrigger : MonoBehaviour
{
    [SerializeField] private MusicArea m_area;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioManager.Instance.SetMusicArea(m_area);
        }
    }
}
