using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;


public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseMenu;
    public TMP_Text countdownText;
    public TMP_Text resumeButtonText;
    public GameObject musicSlider;
    public GameObject sfxSlider;
    private bool countdownActive = false;

    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused && !countdownActive)
            {
                gamePaused = false;
                StartCoroutine(Countdown());
            }
            else
            {
                if (!countdownActive && !PlayerDeath.isDead)
                {
                    Pause();
                }
            }
        }
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        resumeButtonText.gameObject.SetActive(true);
        musicSlider.gameObject.SetActive(true);
        sfxSlider.gameObject.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public IEnumerator Countdown()                         // Po stisknuti "Resume" začne odpočet 3 sekundy, aby se hráč mohl připravit
    {
        countdownActive = true;
        countdownText.gameObject.SetActive(true);
        resumeButtonText.gameObject.SetActive(false);
        musicSlider.gameObject.SetActive(false);
        sfxSlider.gameObject.SetActive(false);

        countdownText.text = "3";
        yield return new WaitForSecondsRealtime(0.5f);
        countdownText.text = "2";
        yield return new WaitForSecondsRealtime(0.5f);
        countdownText.text = "1";
        yield return new WaitForSecondsRealtime(0.5f);

        countdownText.gameObject.SetActive(false);
        countdownActive = false;
        gamePaused = false;

        Resume();
    }

    public void StartCountdown()
    {
        StartCoroutine(Countdown());
    }

    public void SetMusicVolume(float musicVolume)
    {
        musicMixer.SetFloat("Music volume", musicVolume);
    }

    public void SetSFXVolume(float SFXvolume)
    {
        sfxMixer.SetFloat("SFX volume", SFXvolume);
    }
}
