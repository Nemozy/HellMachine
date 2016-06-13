using System;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    const float MAX_WIDTH = 300;
    const float MAX_HEIGHT = 300;
    const float MIN_WIDTH = 100;
    const float MIN_HEIGHT = 100;

    public float Get_MAX_WIDTH()
    {
        return MAX_WIDTH;
    }

    public float Get_MAX_HEIGHT()
    {
        return MAX_HEIGHT;
    }

    public float Get_MIN_WIDTH()
    {
        return MIN_WIDTH;
    }

    public float Get_MIN_HEIGHT()
    {
        return MIN_HEIGHT;
    }
}
