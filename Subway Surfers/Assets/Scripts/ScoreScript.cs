using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public int score;
    public TMP_Text scoreText;
    private bool scoreAdded = false;
    void Update()
    {
        if (!scoreAdded)
        {
            scoreAdded = true;
            StartCoroutine(AddScore());
        }
    }

    private IEnumerator AddScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
        yield return new WaitForSeconds(0.1f);
        scoreAdded = false;
    }
}
