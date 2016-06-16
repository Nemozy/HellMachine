using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float Dmg = 1;
    protected float MovSpeed = 1.1f;
    protected bool NonActive = false;

	void Update () 
    {
        if (!GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetPauseState())
            Move();
	}

    protected virtual void Move()
    {
        this.transform.position += new Vector3(this.transform.up.x, this.transform.up.y, this.transform.up.z) * MovSpeed;
        DestroyWhenOut();
    }

    //Уничтожить после вылета за пределы видимости 
    private void DestroyWhenOut()
    {
        Map map = GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetMap();

        if (this.transform.position.z < map.Get_MIN_WIDTH() - 30 ||
            this.transform.position.z > map.Get_MAX_WIDTH() + 30 ||
            this.transform.position.x < map.Get_MIN_HEIGHT() - 30 ||
            this.transform.position.x > map.Get_MAX_HEIGHT() + 30)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (!NonActive && !GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetGameOverState())
            Damage(col.gameObject);
    }

    //Нанести урон
    protected virtual void Damage(GameObject obj)
    {
        if (obj.name.ToUpper().Contains("METEOROID") || obj.name.ToUpper().Contains("ALIEN") || obj.name.ToUpper().Contains("BOSS"))
        {
            NonActive = true;
            object parameter = Dmg;
            obj.SendMessage("TakeDamage", parameter);
            Destroy(this.gameObject);
        }
    }
}
