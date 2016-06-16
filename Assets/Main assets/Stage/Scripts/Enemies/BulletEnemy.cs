using UnityEngine;
using System.Collections;

public class BulletEnemy : Bullet 
{
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

    protected override void Damage(GameObject obj)
    {
        if (obj.gameObject.name.ToUpper().Contains("SHIP") && obj.gameObject.GetComponent<HeroController>())
        {
            NonActive = true;
            obj.gameObject.GetComponent<HeroController>().TakeDamage(Dmg);
            Destroy(this.gameObject);
        }
    }
}
