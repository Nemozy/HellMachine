using UnityEngine;
using System.Collections;

public class GasolineController : MonoBehaviour 
{
	void Update ()
    {
        if (!GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetPauseState() &&
            !GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetGameOverState())
        {
            float countGas = GameObject.Find("Player").transform.Find("Ship").GetComponent<HeroMovingController>().GetGasoline();
            this.transform.Find("Progress").GetComponent<UnityEngine.UI.Image>().fillAmount = countGas/1000;
            this.transform.Find("Count").GetComponent<UnityEngine.UI.Text>().text = "Gasoline: " + Mathf.FloorToInt(countGas).ToString();
        }
	}
}
