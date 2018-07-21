using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text hiScoreText;

    private int score = 0;
    private string scoreDescriptor;

    private int hiScore = 0;
    private string hiScoreDescriptor;

    private string ppHiScore = "HiScore"; // Key

    private void Awake()
    {
        scoreDescriptor = scoreText.text;
        hiScoreDescriptor = hiScoreText.text;
    }

    private void Start()
    {
        scoreText.text = scoreDescriptor + score.ToString();

        if (PlayerPrefs.HasKey(ppHiScore))
            hiScore = PlayerPrefs.GetInt(ppHiScore);
        PlayerPrefs.SetInt(ppHiScore, hiScore);
        hiScoreText.text = hiScoreDescriptor + hiScore.ToString();
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = scoreDescriptor + score.ToString();
    }

    public void UpdateHiScore()
    {
        if (score > hiScore)
        {
            hiScore = score;
            PlayerPrefs.SetInt(ppHiScore, hiScore);
        }
        hiScoreText.text = hiScoreDescriptor + hiScore.ToString();
    }
}
