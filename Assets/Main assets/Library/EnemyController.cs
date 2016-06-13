using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
    public float MovSpeed;
    public bool Initiate = false;

	void Start ()
    {
	
	}
	
	void Update () 
    {
        if(Initiate && !GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetPauseState())
            Move();
	}

    protected virtual void Move()
    {
    }

    protected virtual void TakeDamage()
    {
    }
}
