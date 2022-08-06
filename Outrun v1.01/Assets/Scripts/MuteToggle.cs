using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[RequireComponent(typeof(Toggle))]
public class MuteToggle : MonoBehaviour
{   
    Toggle myToggle;
    
    void Start()
    {
        //if audio listener is off the toggle is set to false
        myToggle = GetComponent<Toggle>();
        if (AudioListener.volume == 0)
        {
            myToggle.isOn = false;
        }
    }

    // Update is called once per frame
    public void ToggleAudioOnValueChange(bool audioIn) 
    {   //toggles between mute and sound
        if (audioIn)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }
}
