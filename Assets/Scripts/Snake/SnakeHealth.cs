using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHealth : MonoBehaviour
{
    public int maxHealth = 1;

    public int Health // Lives
    {
        get;
        set;
    }

    private void Awake()
    {
        Health = maxHealth;
    }
}
