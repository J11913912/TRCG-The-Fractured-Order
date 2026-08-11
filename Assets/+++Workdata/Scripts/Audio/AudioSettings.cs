using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour {

    //FMOD.Studio.Bus Music;
    //FMOD.Studio.Bus SFX;
   // FMOD.Studio.Bus Ambience;
  //  FMOD.Studio.Bus Master;
    float MusicVolume = 0.5f;
    float SFXVolume = 0.5f;
    float AmbienceVolume = 0.5f;
    float MasterVolume = 1f;

    void Awake ()
    {
      //  Music = FMODUnity.RuntimeManager.GetBus ("bus:/MusicTracks");
       // SFX = FMODUnity.RuntimeManager.GetBus ("bus:/SFX");
        // Ambience = FMODUnity.RuntimeManager.GetBus ("bus:/Ambience");
        //Master = FMODUnity.RuntimeManager.GetBus ("bus:/");
        
        MusicVolume = PlayerPrefs.GetFloat ("MusicVolume");
        SFXVolume = PlayerPrefs.GetFloat ("SFXVolume");
    }

    void Update () 
    {
       // Music.setVolume (MusicVolume);
       // SFX.setVolume (SFXVolume);
        //  Ambience.setVolume (AmbienceVolume);
       // Master.setVolume (MasterVolume);
        
        PlayerPrefs.SetFloat ("SFXVolume", SFXVolume);
        PlayerPrefs.SetFloat ("MusicVolume", MusicVolume);

    }

    public void MasterVolumeLevel (float newMasterVolume)
    {
        MasterVolume = newMasterVolume;
    }

    public void MusicVolumeLevel (float newMusicVolume)
    {
        MusicVolume = newMusicVolume;
    }
     
    public void AmbienceVolumeLevel (float newAmbienceVolume)
    {
        AmbienceVolume = newAmbienceVolume;
    }

    public void SFXVolumeLevel (float newSFXVolume)
    {
        SFXVolume = newSFXVolume;
    }
    
}