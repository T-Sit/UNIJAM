using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [Header("Type")]
    [SerializeField] private VolumeType volumeType;

    private enum VolumeType
    {
        Master,
        Music,
        Ambience,
        VFX
    }

    private Slider volumeSlider;

    private void Awake()
    {
        volumeSlider = GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        switch (volumeType)
        {
            case VolumeType.Master:
                volumeSlider.value = AudioManager.Instance.masterVolume;
                break;
            case VolumeType.Music:
                volumeSlider.value = AudioManager.Instance.musicVolume;
                break;
            case VolumeType.Ambience:
                volumeSlider.value = AudioManager.Instance.ambienceVolume;
                break;
            case VolumeType.VFX:
                volumeSlider.value = AudioManager.Instance.vfxVolume;
                break;
            default:
                Debug.Log("Not supported");
                break;
        }

    }

    public void OnSliderValueChange()
    {
        switch (volumeType)
        {
            case VolumeType.Master:
                AudioManager.Instance.masterVolume = volumeSlider.value;
                break;
            case VolumeType.Music:
                AudioManager.Instance.musicVolume = volumeSlider.value;
                break;
            case VolumeType.Ambience:
                AudioManager.Instance.ambienceVolume = volumeSlider.value;
                break;
            case VolumeType.VFX:
                AudioManager.Instance.vfxVolume = volumeSlider.value;
                break;
            default:
                Debug.Log("Not supported");
                break;
        }
    }
}
