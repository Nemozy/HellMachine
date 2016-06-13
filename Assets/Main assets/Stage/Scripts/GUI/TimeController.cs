using UnityEngine;
using System.Collections;

public class TimeController : MonoBehaviour
{
	void Update ()
    {
        if (!GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetPauseState() &&
            !GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetGameOverState())
        {
            string time = Formatter.timeFormatMMSS(GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetTime());
            this.transform.Find("TimeText").GetComponent<UnityEngine.UI.Text>().text = time;
        }
	}
}
