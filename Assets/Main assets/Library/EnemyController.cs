using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public float Hp;
    public float Damage;
    public float Score;
    public float Size;
    public float MovSpeed;
    public bool Initiate = false;

	void Update () 
    {
        if (Initiate && !GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetPauseState())
            Move();
	}

    protected virtual void Move()
    {
    }

    protected virtual void TakeDamage(float dmg)
    {
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.ToUpper().Contains("SHIP") && col.gameObject.GetComponent<HeroController>())
        {
            col.gameObject.GetComponent<HeroController>().TakeDamage(Damage);
            TakeDamage(Hp);
        }
    }
}
