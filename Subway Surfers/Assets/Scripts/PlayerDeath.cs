using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public CharacterController controller;
    private bool touches;
    public static float checkRadius = 1.5f;
    public LayerMask colliderMask;
    public static bool isDead;

    public GameObject dieMenu;

    void Start()
    {
       dieMenu.SetActive(false);
    }

    private void Update()
    {
        touches = Physics.CheckSphere(controller.transform.position, checkRadius, colliderMask);
        if (touches && !isDead)
        {
            isDead = true;
            PlayerMovement.animator.Play("");
            FindObjectOfType<AudioManager>().Play("Die");
            StartCoroutine(Timer());
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(2f);
        dieMenu.SetActive(true);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("Main");
    }
}

