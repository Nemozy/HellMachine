using UnityEngine;
using System.Collections;

public class Laser : Bullet
{
    private float StartTime;
    private float LifeTime = 0.5f;

	void Start () 
    {
        StartTime = GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetTime();
	}

    protected override void Move()
    {
        if (GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetTime() - StartTime > LifeTime)
        {
            Destroy(this.gameObject);
        }
	}

    protected override void Damage(GameObject obj)
    {
        if (obj.name.ToUpper().Contains("METEOROID") || obj.name.ToUpper().Contains("ALIEN") || obj.name.ToUpper().Contains("BOSS"))
        {
            object parameter = Dmg;
            obj.SendMessage("TakeDamage", parameter);
        }
    }
}
