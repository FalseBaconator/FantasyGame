using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgm;
    public AudioSource sfx;

    public Slider bgSlider;
    public Slider sfxSlider;

    public enum ClipToPlay { Upgrade, MenuClick, Heal, Hurt, Die, Block, RaiseShield }
    public enum BGM { Menu, Goblin, Necromancer };
    BGM currentTrack;

    [Header("BGM Tracks")]
    public AudioClip menu;
    public AudioClip goblin;
    public AudioClip necromancer;

    [Header("SFX")]
    public AudioClip upgrade;
    public AudioClip menuClick;
    public AudioClip heal;
    public AudioClip hurt;
    public AudioClip die;
    public AudioClip block;
    public AudioClip raiseShield;

    private void Start()
    {
        bgm.volume = PlayerPrefs.GetFloat("BGVolume", 1);
        sfx.volume = PlayerPrefs.GetFloat("SFXVolume", 1);
        bgSlider.value = bgm.volume;
        sfxSlider.value = sfx.volume;
    }

    public void SwitchTrack(BGM track)
    {
        if (currentTrack == track) return;

        currentTrack = track;

        bgm.Stop();
        switch(track){
            case BGM.Menu:
                bgm.clip = menu;
                break;
            case BGM.Goblin:
                bgm.clip = goblin;
                break;
            case BGM.Necromancer:
                bgm.clip = necromancer;
                break;
        }
        bgm.Play();
    }

    public void PlaySFX(ClipToPlay clipToPlay)
    {
        switch (clipToPlay)
        {
            case ClipToPlay.Upgrade:
                sfx.clip = upgrade;
                break;
            case ClipToPlay.MenuClick:
                sfx.clip = menuClick;
                break;
            case ClipToPlay.Heal:
                sfx.clip = heal;
                break;
            case ClipToPlay.Hurt:
                sfx.clip = hurt;
                break;
            case ClipToPlay.Die:
                sfx.clip = die;
                break;
            case ClipToPlay.Block:
                sfx.clip = block;
                break;
            case ClipToPlay.RaiseShield:
                sfx.clip = raiseShield;
                break;
        }
        sfx.Play();
    }

    public void PauseAllAudio()
    {
        bgm.Pause();
        sfx.Pause();
    }

    public void UnpauseAllAudio()
    {
        bgm.UnPause();
        sfx.UnPause();

    }

    public void ChangeBGVolume(Slider slider)
    {
        bgm.volume = slider.value;
        PlayerPrefs.SetFloat("BGVolume", slider.value);
    }

    public void ChangeSFXVolume(Slider slider)
    {
        sfx.volume = slider.value;
        PlayerPrefs.SetFloat("SFXVolume", slider.value);
    }

}
