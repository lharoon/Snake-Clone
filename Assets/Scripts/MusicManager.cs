using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager musicManager = null;

    private void Awake()
    {
        // Prevent multiple instances of music manager from being created on restart
        if (musicManager != null && musicManager != this)
        {
            Destroy(gameObject);
            return;
        }
        musicManager = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GetComponent<AudioSource>().Play();
    }
}
