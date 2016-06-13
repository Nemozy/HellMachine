using UnityEngine;
using System.Collections;

public class Meteoroid : EnemyController
{
	public void Init(float Speed) 
    {
        MovSpeed = Speed;
        Initiate = true;
	}

    protected override void Move()
    {
        Vector3 vecPos = new Vector3(this.transform.up.x, this.transform.up.y, this.transform.up.z);
        this.transform.position += vecPos * MovSpeed;
        DestroyWhenOut();
    }

    private void DestroyWhenOut()
    {
        Map map = GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetMap();

        if (this.transform.position.z < map.Get_MIN_WIDTH() - 10 ||
            this.transform.position.z > map.Get_MAX_WIDTH() + 10 ||
            this.transform.position.x < map.Get_MIN_HEIGHT() - 10 ||
            this.transform.position.x > map.Get_MAX_HEIGHT() + 10)
        {
            Destroy(this.gameObject);
        }
    }

    protected override void TakeDamage()
    {
        if (this.gameObject.name.Equals("Meteoroid_Big"))
        {
            for (int i = 0; i < 3; i++)
            {
                float rot = Random.Range(0f, 360f);
                GameObject enemy = Instantiate((GameObject)Resources.Load("Enemies/Meteoroids/Meteoroid_1/" +
                        GameObject.Find("TopPanel").transform.Find("SettingsWindow").Find("GraphMode").GetComponent<UnityEngine.UI.Dropdown>().captionText.text + "/Model/Meteoroid")) as GameObject;
                enemy.transform.SetParent(GameObject.Find("Enemies").transform);
                enemy.name = "Meteoroid_Small";
                enemy.transform.localScale *= 0.5f;
                enemy.transform.position = this.gameObject.transform.position;
                if (GameObject.Find("TopPanel").transform.Find("SettingsWindow").Find("GraphMode").GetComponent<UnityEngine.UI.Dropdown>().captionText.text.Equals("3D"))
                    enemy.transform.Rotate(90, rot, 0);
                else
                    enemy.transform.Rotate(0, 0, rot);
                enemy.transform.GetComponent<Meteoroid>().Init(0.1f);
            }
        }
    }
}
