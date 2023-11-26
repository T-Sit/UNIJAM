using TMPro;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private string m_textToSpeach;
    private TMP_Text _tmpText;


    private void Awake()
    {
        _tmpText = UIManager.Instance.DialogueWindow.GetComponentInChildren<TMP_Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.DialogueWindow.SetActive(true);
            _tmpText.text = m_textToSpeach;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.DialogueWindow.SetActive(false);
            _tmpText.text = null;
        }
    }
}
