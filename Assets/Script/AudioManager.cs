using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioSource audio;
    private bool isNext = true;

    public AudioClip playerDeath;
    public AudioClip levelComplete;
    public AudioClip buttonClick;
    public AudioClip bonusCollected;

    [Space]
    public AudioMixerGroup soundMixer;
    public AudioMixerGroup masterMixer;
    public AudioMixerGroup musicMixer;

    [Space]
    public AudioClip[] backSound;

    void Start()
    {
        instance = this;
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isNext)
        {
            int randomMusicNumber = Random.Range(0, backSound.Length);
            AudioClip randomBackAudio = backSound[randomMusicNumber];
            StartCoroutine(PlayMusic(randomBackAudio));
        }
    }

    public void PlayBonusCollected()
    {
        StartCoroutine(PlaySound(bonusCollected));
    }
    public void PlayPlayerDeath()
    {
        StartCoroutine(PlaySound(playerDeath));
    }
    public void PlayLevelComplete()
    {
        StartCoroutine(PlaySound(levelComplete));
    }
    public void PlayButtonClick()
    {
        StartCoroutine(PlaySound(buttonClick));
    }

    public void Volume(Slider slider)
    {
        if (slider.gameObject.name == "MasterSlider") masterMixer.audioMixer.SetFloat("MasterVolume", slider.value);
        else if(slider.gameObject.name == "SoundSlider") soundMixer.audioMixer.SetFloat("SoundVolume", slider.value);
        else if(slider.gameObject.name == "MusicSlider") musicMixer.audioMixer.SetFloat("MusicVolume", slider.value);
    }

    IEnumerator PlaySound(AudioClip audioClip)
    {
        float time = audioClip.length;
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.outputAudioMixerGroup = soundMixer;
        audio.clip = audioClip;
        audio.pitch = Random.Range(0.9f, 1.1f);
        audio.Play();
        yield return new WaitForSeconds(time);
        Destroy(audio);
    }

    IEnumerator PlayMusic(AudioClip audioClip)
    {
        float time = audioClip.length;
        //audio.pitch = Random.Range(0.9f, 1.1f);
        isNext = false;
        audio.clip = audioClip;
        audio.Play();
        yield return new WaitForSeconds(time);
        isNext = true;
    }
}
