using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    private float Score;

	void Update ()
    {
        if (!GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetPauseState())
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                this.transform.Find("Ship").GetComponent<Shooting>().StartShoot(Unions.BulletType.Bullet);
            }
            if (Input.GetKeyUp(KeyCode.K))
            {
                this.transform.Find("Ship").GetComponent<Shooting>().StopShoot(Unions.BulletType.Bullet);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                this.transform.Find("Ship").GetComponent<Shooting>().StartShoot(Unions.BulletType.Laser);
            }
            if (Input.GetKeyUp(KeyCode.L))
            {
                this.transform.Find("Ship").GetComponent<Shooting>().StopShoot(Unions.BulletType.Laser);
            }
        }
	}

    public void AddScore(float value)
    {
        Score += value;
    }

    public float GetScore()
    {
        return Score;
    }
}
