using TMPro;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
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
            UIManager.Instance.DialogueWindow.transform.DOKill();
            UIManager.Instance.DialogueWindow.transform.localScale = new(0, 0, 0);
            UIManager.Instance.DialogueWindow.SetActive(true);
            UIManager.Instance.DialogueWindow.transform.DOScale(1, DesignSettings.Instance.DialogueWindowAppearingTime)
                .SetEase(Ease.OutQuart);
            _tmpText.text = m_textToSpeach;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.DialogueWindow.transform.DOKill();
            UIManager.Instance.DialogueWindow.transform.DOScale(0, DesignSettings.Instance.DialogueWindowAppearingTime)
                .SetEase(Ease.InQuart)
                .OnComplete(() =>
                {
                    UIManager.Instance.DialogueWindow.SetActive(false);
                    _tmpText.text = null;
                });
        }
    }
}
