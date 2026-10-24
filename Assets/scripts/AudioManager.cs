using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }
    }

    public void Play (string audioName)
    {
        Sound sound = Array.Find(sounds, x => x.name == audioName);
        sound.source.Play();
    }
}
[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0, 1f)]
    public float volume;
    [Range(0, 1f)]
    public float pitch;

    public AudioSource source;

}
