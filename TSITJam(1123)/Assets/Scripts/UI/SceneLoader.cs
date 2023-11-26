using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int _currentLevel;

    private void Awake()
    {
        _currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    public void RestartLevel()
    {
        DG.Tweening.Sequence s = DOTween.Sequence();
        s.Append(transform.DOScale(DesignSettings.Instance.ButtonClickedScale, DesignSettings.Instance.ButtonClickScalingTime / 2));
        s.Append(transform.DOScale(1, DesignSettings.Instance.ButtonClickScalingTime / 2));
        s.AppendCallback(() => { SceneManager.LoadScene(_currentLevel); });
    }
}
