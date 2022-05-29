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

    private void Start()
    {
        coins = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        coins++;
        FindObjectOfType<AudioManager>().Play("CoinCollect");
        Destroy(gameObject);
        coinText.text = "Coins: " + coins;
    }

}
