using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance{get;private set;}

    private List<EventInstance> eventInstances;
    private EventInstance musicEventInstance;
    private void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateInstance(musicEventReference);
        musicEventInstance.start();
    }
    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
        
    }

    private void Start()
    {
        InitializeMusic(FMODEvents.instance.music);
        DontDestroyOnLoad(this);
    }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one Audio Manager");
        }

        instance = this;
    }

    public void PlayOneShot(EventReference sound,Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound,worldPos);
    }

    public void StopMusic()
    {
        var result = musicEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        if (result != FMOD.RESULT.OK)
        {
            Debug.Log($"Failed to stop event with result: {result}");
        }
        result = musicEventInstance.release();
        if (result != FMOD.RESULT.OK)
        {
            Debug.Log($"Failed to stop event with result: {result}");
        }
    }

}
