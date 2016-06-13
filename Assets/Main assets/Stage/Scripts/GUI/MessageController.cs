﻿using UnityEngine;
using System.Collections;

public class MessageController : MonoBehaviour
{
    private float ShowMessageStart_Time = 0;
    private float TimerShow = 0;

    void Update()
    {
        if (this.gameObject.activeSelf && TimerShow > 0 && 
            GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetTime() - ShowMessageStart_Time > TimerShow)
        {
            Hide();
        }
    }

    public void SetMessage(string message)
    {
        this.gameObject.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = message;
    }

    public void Hide()
    {
        TimerShow = 0;
        this.gameObject.SetActive(false);
    }

    public void Show(float time)
    {
        TimerShow = time;
        ShowMessageStart_Time = GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetTime();
        this.gameObject.SetActive(true);
    }
}
