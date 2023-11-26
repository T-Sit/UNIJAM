using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Music")]
    [field: SerializeField] public EventReference LvlMusic { get; private set; }

    [field: Header("SFX")]
    [field: SerializeField] public EventReference LvlRotation { get; private set; }
    [field: SerializeField] public EventReference FootSteps { get; private set; }
    [field: SerializeField] public EventReference PlayerDeath { get; private set; }
    [field: SerializeField] public EventReference PlayerFall { get; private set; }

    public static FMODEvents Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Debug.Log("Shrek");
    }
}
