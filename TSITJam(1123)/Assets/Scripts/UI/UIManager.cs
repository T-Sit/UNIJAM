using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject UICanvas;

    public GameObject DialogueWindow;

    public GameObject RestartButton;

    public GameObject PauseButton;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);
    }

}
