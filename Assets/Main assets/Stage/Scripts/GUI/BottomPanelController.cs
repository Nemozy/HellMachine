using UnityEngine;
using System.Collections;

public class BottomPanelController : MonoBehaviour 
{
	void Start () 
    {
        UpdateLaserState();
	}
	
	void Update ()
    {
        if (!GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetPauseState() &&
            !GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetGameOverState())
        {
            UpdateLaserState();
            this.transform.Find("IcoLaser").Find("Reuse").GetComponent<UnityEngine.UI.Image>().fillAmount =
                        1 - GameObject.Find("Player").transform.Find("Ship").GetComponent<Shooting>().LaserCooldown();
            this.transform.Find("IcoShoot").Find("Reuse").GetComponent<UnityEngine.UI.Image>().fillAmount =
                        1 - GameObject.Find("Player").transform.Find("Ship").GetComponent<Shooting>().BulletCooldown();
            this.transform.Find("IcoLaser").Find("Adding").Find("Progress").GetComponent<UnityEngine.UI.Image>().fillAmount =
                        GameObject.Find("Player").transform.Find("Ship").GetComponent<Shooting>().LaserAddingProgress();
        }
	}

    public void UpdateLaserState()
    {
        this.transform.Find("IcoLaser").Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Laser\n" + "x" +
            GameObject.Find("Player").transform.Find("Ship").GetComponent<Shooting>().GetLaserAmmo().ToString();
    }
}
