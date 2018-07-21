using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Text pauseText;

    private bool isPaused = false;

    private void Update()
    {
        if (!GameManager.hasStarted || GameManager.hasFinished)
            return;

        if (!isPaused && Input.GetKeyDown(KeyCode.Space))
            Pause();
        else if (isPaused && Input.GetKeyDown(KeyCode.Space))
            UnPause();
    }

    void Pause()
    {
        //GetComponent<AudioSource>().Play();
        isPaused = true;
        pauseText.enabled = true;
        Time.timeScale = 0;
    }

    void UnPause()
    {
        //GetComponent<AudioSource>().Play();
        isPaused = false;
        pauseText.enabled = false;
        Time.timeScale = 1;
    }
}
