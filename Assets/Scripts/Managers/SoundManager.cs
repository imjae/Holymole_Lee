using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[System.Serializable]
public class sound
{
    public string name;
    public AudioClip clip;
}
public class SoundManager : Singleton<SoundManager>
{
    public sound[] effectsounds;
    public AudioSource[] effectPlayer;

    public sound bgmSound;
    public AudioSource bgmPlayer;

    void Start()
    {
           
    }
    public void BGMPlay()
    {
        bgmPlayer.clip = bgmSound.clip;
        bgmPlayer.Play();
    }
    public void BGMPause()
    {
        bgmPlayer.Pause();
    }
    public void BGMUnPause()
    {
        bgmPlayer.UnPause();
    }

    public void EffectPlay(string sName)
    {
        for(int i = 0; i < effectsounds.Length; i++)
        {
            if(sName == effectsounds[i].name)
            {
                for(int j = 0; j <effectPlayer.Length; j++)
                {
                    if(!effectPlayer[j].isPlaying)
                    {
                        effectPlayer[j].clip = effectsounds[i].clip;
                        effectPlayer[j].Play();
                        return;
                    }
                }
            }
        }
    }
}
