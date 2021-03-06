using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sounds
{
     public string name;
     public AudioClip clip;
     public AudioMixerGroup mixer;
     [Range(0f, 1f)]
     public float volume;
     public bool loop;

     [HideInInspector]
     public AudioSource source;
}
