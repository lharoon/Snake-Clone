using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExtents : MonoBehaviour
{
    public static int XMin { get; private set; }
    public static int YMin { get; private set; }
    public static int XMax { get; private set; }
    public static int YMax { get; private set; }

    private void Start()
    {
        XMin = -getHalfWidth();
        YMin = -getHalfHeight();
        XMax = getHalfWidth();
        YMax = getHalfHeight();
    }

    int getHalfWidth()
    {
        return (int)transform.localScale.x / 2;
    }

    int getHalfHeight()
    {
        return (int)transform.localScale.y / 2;
    }
}
