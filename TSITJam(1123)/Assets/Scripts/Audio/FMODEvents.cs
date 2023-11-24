using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Ambience")]
    [field: SerializeField] public EventReference WindAmbience { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference Lvl1Music { get; private set; }

    [field: Header("VFX")]
    [field: SerializeField] public EventReference LaserShoot { get; private set; }
    [field: SerializeField] public EventReference FootSteps { get; private set; }

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
