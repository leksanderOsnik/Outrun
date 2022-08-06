using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] audioSound;
    void Start()
    {   //for every sound in the array assign the variables
        foreach (Sound s in audioSound) 
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
        }
        //plays the song upon instantiation
        PlaySound("Soundtrack");
        
    }

    //PlaySound method plays the sound based on the determined name
    public void PlaySound(string name) 
    {   

        foreach (Sound s in audioSound)
        {
            if (s.name == name)
                s.source.Play();
        }
    }
}
