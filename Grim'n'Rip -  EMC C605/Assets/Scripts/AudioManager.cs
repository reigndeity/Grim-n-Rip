using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Music Properties")]
    public AudioSource musicSource;
    public AudioClip[] musicClips; // 0:MainMenuSound |
    [Header("SFX Properties")]
    public AudioSource playerSource;
    public AudioSource sfxSource;
    public AudioClip[] sfxClips; // 0:ButtonSound | 1:WaveCountSound  | 2:GrimmyDeath

    [Header("Sliders")]
    public Slider bgmSlider;
    public Slider sfxSlider;

    [Header("Global Parameters")]
    public int playOnce;

    void Start()
    {
        // Load saved volume settings if they exist, otherwise use default values (1f)
        float savedBgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        float savedSfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // Set the sliders to the saved volume values
        bgmSlider.value = savedBgmVolume;
        sfxSlider.value = savedSfxVolume;

        // Apply the saved values to the audio sources
        musicSource.volume = savedBgmVolume;
        sfxSource.volume = savedSfxVolume;
        playerSource.volume = savedSfxVolume;
    }

    void Update()
    {
        musicSource.volume = bgmSlider.value;
        sfxSource.volume = sfxSlider.value;
        playerSource.volume = sfxSlider.value;

        // Save the updated volume settings
        PlayerPrefs.SetFloat("BGMVolume", bgmSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);

        // Persist the changes in PlayerPrefs
        PlayerPrefs.Save();
    }
    public void PlayMainMenuMusic()
    {
        musicSource.Stop();
        musicSource.PlayOneShot(musicClips[0]);
    }

    public void PlayInGameMusic()
    {
        musicSource.Stop();
        musicSource.PlayOneShot(musicClips[1]);
    }

    public void PlayButtonClickSound()
    {
        float randomPitch = Random.Range(0.5f, 1f);
        sfxSource.pitch = randomPitch;
        sfxSource.PlayOneShot(sfxClips[0]);
    }

    public void PlayWavecountSound()
    {
        sfxSource.PlayOneShot(sfxClips[1]);
    }

    public void PlayPlayerDeathSound()
    {
        musicSource.Stop();
        sfxSource.PlayOneShot(sfxClips[2]);
    }
    public void PlayPlayerAttack()
    {
        float randomPitch = Random.Range(0.8f, 1f);
        playerSource.pitch = randomPitch;
        playerSource.PlayOneShot(sfxClips[3]);
    }
    public void PlayProjectileHitEnemySound()
    {
        float randomPitch = Random.Range(0.8f, 1f);
        sfxSource.pitch = randomPitch;
        sfxSource.PlayOneShot(sfxClips[4]);
    }
    public void PlayEnemyDeathSound()
    {
        float randomPitch = Random.Range(0.9f, 1f);
        sfxSource.pitch = randomPitch;
        sfxSource.PlayOneShot(sfxClips[5]);
    }
    public void PlayPlayerHurtSound()
    {
        float randomPitch = Random.Range(0.9f, 1f);
        sfxSource.pitch = randomPitch;
        sfxSource.PlayOneShot(sfxClips[6]);
    }

    public void PlayBlazeAttackSound()
    {
        sfxSource.PlayOneShot(sfxClips[7]);
    }
    public void PlaySinisterSeerAttackSound()
    {
        sfxSource.PlayOneShot(sfxClips[8]);
    }
        public void PlayVainedAttackSound()
    {
        sfxSource.PlayOneShot(sfxClips[9]);
    }
        public void PlayTormentorAttackSound()
    {
        sfxSource.PlayOneShot(sfxClips[10]);
    }
    // ===================================================================

}
