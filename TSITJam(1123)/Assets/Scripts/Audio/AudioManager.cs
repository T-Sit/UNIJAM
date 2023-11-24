using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [Header("Volume")]
    [Range(0, 1)]
    public float masterVolume = 1;

    [Range(0, 1)]
    public float musicVolume = 1;

    [Range(0, 1)]
    public float ambienceVolume = 1;

    [Range(0, 1)]
    public float vfxVolume = 1;

    private Bus masterBus;
    private Bus musicBus;
    private Bus ambienceBus;
    private Bus VFXBus;

    public static AudioManager Instance;

    private List<EventInstance> eventInstances;

    private EventInstance ambienceEventInstance;

    private EventInstance musicEventInstance;

    private void Awake()
    {
        if (Instance != null)
            Debug.Log("shrek");
        else
        {
            Instance = this;
        }

        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
        VFXBus = RuntimeManager.GetBus("bus:/VFX");
        eventInstances = new List<EventInstance>();
            
    }

    private void Start()
    {
        InitilizeAmbience(FMODEvents.Instance.WindAmbience);
        InitilizeMusic(FMODEvents.Instance.Lvl1Music);
    }

    private void Update()
    {
        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolume);
        ambienceBus.setVolume(ambienceVolume);
        VFXBus.setVolume(vfxVolume);
    }

    private void OnDestroy()
    {
        CleanEventInstances();
    }

    private void InitilizeAmbience(EventReference ambienceEventReference)
    {
        ambienceEventInstance = CreateEventInstance(ambienceEventReference);
        ambienceEventInstance.start();
    }

    private void InitilizeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateEventInstance(musicEventReference);
        musicEventInstance.start();
    }

    public void SetAmbienceParameter(string parametrName, float parametrValue)
    {
        ambienceEventInstance.setParameterByName(parametrName, parametrValue);
    }

    public void SetMusicArea(MusicArea musicArea)
    {
        musicEventInstance.setParameterByName("area", (float)musicArea);
    }

    public void PlayOneShot(EventReference sound, Vector3 playFrom)
    {
        RuntimeManager.PlayOneShot(sound, playFrom);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    private void CleanEventInstances()
    {
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }
}
