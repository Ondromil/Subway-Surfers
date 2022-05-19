using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinScript : MonoBehaviour
{
    public TMP_Text coinText;
    public static int coins;
    public AudioSource collectSound;

    private void OnTriggerEnter(Collider other)
    {
        coins++;
        collectSound.Play();
        Destroy(gameObject);
    }

    private void Update()
    {
        coinText.text = "Score: " + coins;
    }
}
