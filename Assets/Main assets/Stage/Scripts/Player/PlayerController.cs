using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    private float Score;
    public bool autoShooting = false;
    public bool autoLaser = false;
    public bool autoBomb = false;
    public bool MouseOverGUI = false;

    void Start()
    {
        autoShooting = true;
        GameObject.Find("Player").transform.Find("Ship").GetComponent<Shooting>().StartShoot(Unions.BulletType.Bullet);
        GameObject.Find("Interface").transform.Find("BottomPanel").Find("IcoShoot").Find("AutoOn").gameObject.SetActive(autoShooting);
    }

	void Update ()
    {
        if (!GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetPauseState())
        {
            if (!autoShooting && Input.GetKeyDown(KeyCode.K))
            {
                this.transform.Find("Ship").GetComponent<Shooting>().StartShoot(Unions.BulletType.Bullet);
            }
            if (!autoShooting && Input.GetKeyUp(KeyCode.K))
            {
                this.transform.Find("Ship").GetComponent<Shooting>().StopShoot(Unions.BulletType.Bullet);
            }
            if (!autoLaser && Input.GetKeyDown(KeyCode.L))
            {
                this.transform.Find("Ship").GetComponent<Shooting>().StartShoot(Unions.BulletType.Laser);
            }
            if (!autoLaser && Input.GetKeyUp(KeyCode.L))
            {
                this.transform.Find("Ship").GetComponent<Shooting>().StopShoot(Unions.BulletType.Laser);
            }
            if (!autoBomb && Input.GetKeyDown(KeyCode.J))
            {
                this.transform.Find("Ship").GetComponent<Shooting>().StartShoot(Unions.BulletType.Bomb);
            }
            if (!autoBomb && Input.GetKeyUp(KeyCode.J))
            {
                this.transform.Find("Ship").GetComponent<Shooting>().StopShoot(Unions.BulletType.Bomb);
            }
        }
	}

    public void AddScore(float value)
    {
        Score += Mathf.CeilToInt(value);
    }

    public float GetScore()
    {
        return Score;
    }
}
