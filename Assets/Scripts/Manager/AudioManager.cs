using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] sounds;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
            
        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        PlayAudio(Constants.Bgm);
    }

    public void PlayAudio(string name)
    {
        var s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
    public void StopAudio(string name)
    {
        var s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
