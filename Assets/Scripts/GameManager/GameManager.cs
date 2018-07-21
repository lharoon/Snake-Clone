using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text[] scoreTexts = new Text[2];
    public Text startText;

    public static bool hasStarted;
    public static bool hasFinished;

    private void Awake()
    {
        hasStarted = false;
        hasFinished = false;
    }

    void Update()
    {
        if (StartGame())
        {
            startText.enabled = false;
            foreach (Text t in scoreTexts) t.enabled = true;
            hasStarted = true;
        }

        // Restart
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);
    }

    bool StartGame()
    {
        return Input.GetKeyDown(KeyCode.W) ||
           Input.GetKeyDown(KeyCode.A) ||
           Input.GetKeyDown(KeyCode.S) ||
           Input.GetKeyDown(KeyCode.D) ||
           Input.GetKeyDown(KeyCode.UpArrow) ||
           Input.GetKeyDown(KeyCode.LeftArrow) ||
           Input.GetKeyDown(KeyCode.DownArrow) ||
                    Input.GetKeyDown(KeyCode.RightArrow);
    }
}
