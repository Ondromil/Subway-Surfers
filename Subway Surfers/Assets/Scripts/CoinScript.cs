using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public TMP_Text coinText;
    public static int coins;
    public AudioSource collectSound;

    private void OnTriggerEnter(Collider other)
    {
        coins++;
        FindObjectOfType<AudioManager>().Play("CoinCollect");
        Destroy(gameObject);
        coinText.text = "Coins: " + coins;
    }

}
