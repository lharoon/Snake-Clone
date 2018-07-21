using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public SnakeHealth snakeHealth;
    public UnityEngine.UI.Text gameOverText;
    public UnityEngine.UI.Text retryText;

    public SpriteRenderer snakeHeadSprite;

    public ScoreManager scoreManager;

    void Update()
    {
        if (snakeHealth.Health <= 0)
        {
            GameManager.hasFinished = true;

            gameOverText.enabled = true;
            retryText.enabled = true;
            // Play death anim…
            snakeHeadSprite.color = Color.red;

            scoreManager.UpdateHiScore();
        }
    }

}
