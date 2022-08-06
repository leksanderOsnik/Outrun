using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sound class responsible for storing the sound variables.

[System.Serializable]
public class Sound 
{
    public string name;
    public AudioClip clip;

    public float volume;
    public bool loop;
    public AudioSource source;
}
