using UnityEngine;
using System.Collections;

public class Bomb : Bullet
{
    private float LifeTime;

	void Start ()
    {
        MovSpeed = 0.3f;
	    LifeTime = GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetTime();
	}
	
    protected override void Move()
    {
        if (GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetTime() - LifeTime < 2)
        {
            this.transform.position += new Vector3(this.transform.up.x, this.transform.up.y, this.transform.up.z) * MovSpeed;
            this.transform.localScale *= 1.01f;
            if (this.transform.GetComponent<SpriteRenderer>())
            {
                Color colr = this.transform.GetComponent<SpriteRenderer>().color;
                colr.g -= 0.5f * Time.deltaTime;
                this.transform.GetComponent<SpriteRenderer>().color = colr;
            }
            else if (this.transform.GetComponent<Renderer>())
            {
                Color colr = this.transform.GetComponent<Renderer>().material.color;
                colr.g -= 0.5f * Time.deltaTime;
                this.transform.GetComponent<Renderer>().material.color = colr;
            }
        }
        else
            Exploy();
    }

    //Взрыв
    private void Exploy()
    {
        GameObject.Find("Exploy").GetComponent<AudioSource>().Play();
        Destroy(this.gameObject);
    }

    //Нанести урон
    protected override void Damage(GameObject obj)
    {
        if (obj.name.ToUpper().Contains("METEOROID") || obj.name.ToUpper().Contains("ALIEN") || obj.name.ToUpper().Contains("BOSS"))
        {
            object parameter = Dmg;
            obj.SendMessage("TakeDamage", parameter);
        }
    }
}
