using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerDeath : MonoBehaviour
{
    public CharacterController controller;
    private bool touches;
    public static float checkRadius = 1.5f;
    public LayerMask colliderMask;
    public static bool isDead;

    public GameObject dieMenu;
    public TMP_Text highScoreText;
    
    private static int highScore = 0;

    void Start()
    {
       dieMenu.SetActive(false);
    }

    private void Update()
    {
        touches = Physics.CheckSphere(controller.transform.position, checkRadius, colliderMask);    // Pokud se controller dotkne Ground Masky "Collider", hráč "umře"
        if (touches && !isDead)
        {
            isDead = true;
            PlayerMovement.animator.Play("MaxFall-Back");
            FindObjectOfType<AudioManager>().Play("Die");
            StartCoroutine(Timer());
        }
    }

    private IEnumerator Timer()               // Hra počká po smrti 2 sekundy, aby se mohla ukázat animace, až pak se objeví game over screen
    {
        yield return new WaitForSeconds(2f);
        dieMenu.SetActive(true);
        if (ScoreScript.score > highScore)
        {
            highScore = ScoreScript.score;
            highScoreText.text = "" + highScore;
        }
        else
        {
            highScoreText.text = "" + highScore;
        }
    }

    public void RestartScene()            
    {
        SceneManager.LoadScene("Main");
    }
}

