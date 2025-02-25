using System.Collections.Generic;
using UnityEngine;

public enum Sounds
{
    Click,
    Error,
    UIClick,
    NewRecord,
    Transition,
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource _audioSource;

    private Dictionary<Sounds, AudioClip> AudioList = new Dictionary<Sounds, AudioClip>();

    private float _volume = 1.0f;

    private void Awake()
    {
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;

            TryGetComponent(out _audioSource);

            AudioList.Add(Sounds.Click, Resources.Load<AudioClip>("Audio/bong_001"));
            AudioList.Add(Sounds.UIClick, Resources.Load<AudioClip>("Audio/select_005"));
            AudioList.Add(Sounds.Error, Resources.Load<AudioClip>("Audio/error_005"));
            AudioList.Add(Sounds.NewRecord, Resources.Load<AudioClip>("Audio/578572__nomiqbomi__congrats-2"));
            AudioList.Add(Sounds.Transition, Resources.Load<AudioClip>("Audio/325261__deleted_user_2104797__whoosh"));
        }
    }

    public void PlayAudio(Sounds sound)
    {
        _audioSource.clip = AudioList[sound];
        _audioSource.volume = _volume;
        _audioSource.Play();
    }
}
