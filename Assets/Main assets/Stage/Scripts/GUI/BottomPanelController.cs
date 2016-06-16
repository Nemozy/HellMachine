using UnityEngine;
using System.Collections;

public class BottomPanelController : MonoBehaviour
{
    StageEnvironment stage;
    Shooting HeroShooting;

	void Start () 
    {
        stage = GameObject.Find("Terrain").GetComponent<StageEnvironment>();
        HeroShooting = GameObject.Find("Player").transform.Find("Ship").GetComponent<Shooting>();
        UpdateLaserState();
	}
	
	void Update ()
    {
        if (!stage.GetPauseState() &&
            !stage.GetGameOverState())
        {
            UpdateLaserState();
            this.transform.Find("IcoLaser").Find("Reuse").GetComponent<UnityEngine.UI.Image>().fillAmount = 1 - HeroShooting.LaserCooldown();
            this.transform.Find("IcoShoot").Find("Reuse").GetComponent<UnityEngine.UI.Image>().fillAmount = 1 - HeroShooting.BulletCooldown();
            this.transform.Find("IcoBomb").Find("Reuse").GetComponent<UnityEngine.UI.Image>().fillAmount = 1 - HeroShooting.BombCooldown();
            this.transform.Find("IcoLaser").Find("Adding").Find("Progress").GetComponent<UnityEngine.UI.Image>().fillAmount = HeroShooting.LaserAddingProgress();
            UpdateBombState();
        }
	}

    public void UpdateLaserState()
    {
        this.transform.Find("IcoLaser").Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Laser\n" + "x" +
            GameObject.Find("Player").transform.Find("Ship").GetComponent<Shooting>().GetLaserAmmo().ToString();
    }

    public void UpdateBombState()
    {
        this.transform.Find("IcoBomb").Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Bomb\n" + "x" +
            GameObject.Find("Player").transform.Find("Ship").GetComponent<Shooting>().GetBombAmmo().ToString();
    }

    public void ClickShooting()
    {
        if (GameObject.Find("Player").GetComponent<PlayerController>().autoShooting = !GameObject.Find("Player").GetComponent<PlayerController>().autoShooting)
            HeroShooting.StartShoot(Unions.BulletType.Bullet);
        else
            HeroShooting.StopShoot(Unions.BulletType.Bullet);

        this.transform.Find("IcoShoot").Find("AutoOn").gameObject.SetActive(GameObject.Find("Player").GetComponent<PlayerController>().autoShooting);
    }

    public void ClickLaserShooting()
    {
        if (GameObject.Find("Player").GetComponent<PlayerController>().autoLaser = !GameObject.Find("Player").GetComponent<PlayerController>().autoLaser)
            HeroShooting.StartShoot(Unions.BulletType.Laser);
        else
            HeroShooting.StopShoot(Unions.BulletType.Laser);

        this.transform.Find("IcoLaser").Find("AutoOn").gameObject.SetActive(GameObject.Find("Player").GetComponent<PlayerController>().autoLaser);
    }

    public void ClickBombShooting()
    {
        if (GameObject.Find("Player").GetComponent<PlayerController>().autoBomb = !GameObject.Find("Player").GetComponent<PlayerController>().autoBomb)
            HeroShooting.StartShoot(Unions.BulletType.Bomb);
        else
            HeroShooting.StopShoot(Unions.BulletType.Bomb);

        this.transform.Find("IcoBomb").Find("AutoOn").gameObject.SetActive(GameObject.Find("Player").GetComponent<PlayerController>().autoBomb);
    }
}
