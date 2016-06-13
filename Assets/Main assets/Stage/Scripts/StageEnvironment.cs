using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageEnvironment : MonoBehaviour
{
    private bool _START = false;                        // начать играть
    private bool GAME_OVER = false;                     // проиграл?
    private bool _PAUSE = false;                        // пауза?
    private float PauseTime = 0;                        // pause time (общее время всех пауз)
    private float PauseCurrentTime = 0;                 // pause time (время текущей паузы)
    private Map MainMap;

	void Start ()
    {
        MainMap = new Map();
        Pause();
	}
	
	void Update ()
    {
        if (!GAME_OVER)
        {
            if (!_START && (Input.GetKey("enter") || Input.GetKey(KeyCode.Return)))
            {
                _START = true;
                GameObject.Find("Interface").transform.Find("Message").GetComponent<MessageController>().Hide();
                GameObject.Find("Interface").transform.Find("TopPanel").Find("HelpMessage").gameObject.SetActive(false);
                Pause();
            }
            if (_START && Input.GetKey(KeyCode.P))
            {
                Pause();
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Stage");
        }
        if (Input.GetKey(KeyCode.Q))
        {
            Application.Quit();
        }
	}

    /// <summary>
    /// Установить паузу или убрать паузу
    /// </summary>
    public void Pause()
    {
        if (!_PAUSE)
        {
            _PAUSE = true;
            PauseCurrentTime = Time.timeSinceLevelLoad;
            if (_START)
            {
                GameObject.Find("Interface").transform.Find("Message").GetComponent<MessageController>().SetMessage("Pause");
                GameObject.Find("Interface").transform.Find("Message").GetComponent<MessageController>().Show(0);
            }
        }
        else if (_START && _PAUSE)
        {
            _PAUSE = false;
            PauseTime += Time.timeSinceLevelLoad - PauseCurrentTime;
            PauseCurrentTime = 0;
            GameObject.Find("Interface").transform.Find("Message").GetComponent<MessageController>().Hide();
        }
    }

    /// <summary>
    /// Получить настройки карты уровня
    /// </summary>
    public Map GetMap()
    {
        return MainMap;
    }

    /// <summary>
    /// Поражение
    /// </summary>
    public void GameOver()
    {
        //Pause();
        GAME_OVER = true;

        GameObject.Find("Interface").transform.Find("Message").GetComponent<MessageController>().SetMessage(
            "GAME OVER"+'\n' + "Score: " + GameObject.Find("Player").GetComponent<PlayerController>().GetScore().ToString()+'\n' +
            "Press R to repeat");
        GameObject.Find("Interface").transform.Find("Message").GetComponent<MessageController>().Show(0);
    }

    /// <summary>
    /// Получить время игры
    /// </summary>
    public float GetTime()
    {
        return Time.timeSinceLevelLoad - GetSecPause();
    }

    /// <summary>
    /// Получить время жизненного цикла программы, в состоянии паузы
    /// </summary>
    public int GetSecPause()
    {
        return Mathf.RoundToInt(PauseTime);
    }

    /// <summary>
    /// Получить состояние паузы
    /// </summary>
    public bool GetPauseState()
    {
        return _PAUSE;
    }

    /// <summary>
    /// Получить состояние конца игры
    /// </summary>
    public bool GetGameOverState()
    {
        return GAME_OVER;
    }

    /// <summary>
    /// Установить 3D графический набор
    /// </summary>
    public void VisualSet_3D()
    {
        this.transform.GetComponent<GraphicsController>().Set3D();
    }

    /// <summary>
    /// Установить 2D графический набор
    /// </summary>
    public void VisualSet_2D()
    {
        this.transform.GetComponent<GraphicsController>().Set2D();
    }
}
