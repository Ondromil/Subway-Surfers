using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseMenu;
    public TMP_Text countdownText;
    public TMP_Text resumeButtonText;
    private bool countdownActive = false;

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
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public IEnumerator Countdown()
    {
        countdownActive = true;
        countdownText.gameObject.SetActive(true);
        resumeButtonText.gameObject.SetActive(false);

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
}
