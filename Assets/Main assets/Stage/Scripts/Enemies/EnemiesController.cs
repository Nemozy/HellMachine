using UnityEngine;
using System.Collections;

public class EnemiesController : MonoBehaviour
{
    private float SpawnSpeed_Alien = 40;
    private float SpawnSpeed_METEOROID = 10;
    private float SpawnSpeed_AlienBoss = 302;
    private float LastSpawn_Alien = 0;
    private float LastSpawn_AlienBoss = 0;
    private float LastSpawn_METEOROID = 0;
    private float MAX_SPAWN_SPEED_ALIEN = 10;
    private float MAX_SPAWN_SPEED_METEOROID = 2;
    private float SPAWN_COUNT_ALIENS = 1;
    private float SPAWN_COUNT_METEOROIDS = 3;

	void Update () 
    {
        if(!GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetPauseState() && 
            !GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetGameOverState())
        {
            float time = GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetTime();

            if (time > LastSpawn_METEOROID + SpawnSpeed_METEOROID)
            {
                LastSpawn_METEOROID = time;
                SpawnSpeed_METEOROID = SpawnSpeed_METEOROID - 0.5f < MAX_SPAWN_SPEED_METEOROID ? MAX_SPAWN_SPEED_METEOROID : SpawnSpeed_METEOROID - 0.5f;
                Vector3 vec = new Vector3(0, 0, 0);
                float rot = 0;
                if (time % 60 == 0)
                {
                    SPAWN_COUNT_METEOROIDS += 1;
                }
                for (int i = 0; i < SPAWN_COUNT_METEOROIDS; i++)
                {
                    GeneratePosAndRotation(ref vec, ref rot);
                    GameObject enemy = Instantiate((GameObject)Resources.Load("Enemies/Meteoroids/Meteoroid_1/" +
                        GameObject.Find("TopPanel").transform.Find("SettingsWindow").Find("GraphMode").GetComponent<UnityEngine.UI.Dropdown>().captionText.text + "/Model/Meteoroid")) as GameObject;
                    enemy.transform.SetParent(GameObject.Find("Enemies").transform);
                    enemy.name = "Meteoroid_Big";
                    enemy.transform.position = vec;
                    if (GameObject.Find("TopPanel").transform.Find("SettingsWindow").Find("GraphMode").GetComponent<UnityEngine.UI.Dropdown>().captionText.text.Equals("3D"))
                        enemy.transform.Rotate(90, rot, 0);
                    else
                        enemy.transform.Rotate(0, 0, rot);

                    float sze = Random.Range(0.6f, 3.0f);
                    int delta = Mathf.RoundToInt(sze);
                    enemy.transform.localScale *= sze;
                    enemy.transform.GetComponent<Meteoroid>().Init(0.3f / sze, sze, delta, delta * 2, delta * 3);
                }
            }

            if (time > LastSpawn_Alien + SpawnSpeed_Alien)
            {
                if (time % 120 == 0)
                {
                    SPAWN_COUNT_ALIENS += 1;
                }
                LastSpawn_Alien = time;
                SpawnSpeed_Alien = SpawnSpeed_Alien - 3f < MAX_SPAWN_SPEED_ALIEN ? MAX_SPAWN_SPEED_ALIEN : SpawnSpeed_Alien - 3f;
                Vector3 vec = new Vector3(0, 0, 0);
                float rot = 0;
                for (int i = 0; i < SPAWN_COUNT_ALIENS; i++)
                {
                    GeneratePosAndRotation(ref vec, ref rot);
                    GameObject enemy = Instantiate((GameObject)Resources.Load("Enemies/Alien/" +
                        GameObject.Find("TopPanel").transform.Find("SettingsWindow").Find("GraphMode").GetComponent<UnityEngine.UI.Dropdown>().captionText.text + "/Model/Alien")) as GameObject;
                    enemy.transform.SetParent(GameObject.Find("Enemies").transform);
                    enemy.name = "Alien";
                    enemy.transform.position = vec;
                    int delta = Mathf.RoundToInt(time / 60);
                    enemy.transform.GetComponent<Alien>().Init(0.15f, 1, Random.Range(10, 15), Random.Range(5, 10) + delta, Random.Range(5, 10) + delta);

                    GameObject.Find("Interface").transform.Find("Message").GetComponent<MessageController>().SetMessage("Alien is here!");
                    GameObject.Find("Interface").transform.Find("Message").GetComponent<MessageController>().Show(2);
                }
            }

            if (time > LastSpawn_AlienBoss + SpawnSpeed_AlienBoss)
            {
                LastSpawn_AlienBoss = time;
                Vector3 vec = new Vector3(0, 0, 0);
                float rot = 0;
                GeneratePosAndRotation(ref vec, ref rot);
                GameObject enemy = Instantiate((GameObject)Resources.Load("Enemies/Boss/3D/Model/Boss")) as GameObject;
                enemy.transform.SetParent(GameObject.Find("Enemies").transform);
                enemy.name = "Boss";
                enemy.transform.position = vec;
                int delta = Mathf.RoundToInt(time / 300);
                enemy.transform.GetComponent<BossController>().Init(0.1f, delta, 700 * delta, 700 * delta, 700 * delta);

                GameObject.Find("Interface").transform.Find("Message").GetComponent<MessageController>().SetMessage("BOSS IS COMING!!!");
                GameObject.Find("Interface").transform.Find("Message").GetComponent<MessageController>().Show(2);
            }
        }
	}

    private void GeneratePosAndRotation(ref Vector3 vec, ref float rot)
    {
        Map map = GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetMap();
        switch (Random.Range(0, 4))
        {
            // x = 0
            case 0:
                vec = new Vector3(map.Get_MIN_WIDTH(), 10.0f, Random.Range(map.Get_MIN_HEIGHT(), map.Get_MAX_HEIGHT()));
                rot = Random.Range(40f, 140f);
                break;
            // x = max
            case 1:
                vec = new Vector3(map.Get_MAX_WIDTH(), 10.0f, Random.Range(map.Get_MIN_HEIGHT(), map.Get_MAX_HEIGHT()));
                rot = Random.Range(220f, 320f);
                break;
            // y = 0
            case 2:
                vec = new Vector3(Random.Range(map.Get_MIN_WIDTH(), map.Get_MAX_WIDTH()), 10.0f, map.Get_MIN_HEIGHT());
                rot = Random.Range(-40f, 40f);
                break;
            // y = max
            case 3:
                vec = new Vector3(Random.Range(map.Get_MIN_WIDTH(), map.Get_MAX_WIDTH()), 10.0f, map.Get_MAX_HEIGHT());
                rot = Random.Range(140f, 220f);
                break;

            default:
                vec = new Vector3(map.Get_MIN_WIDTH(), 10.0f, Random.Range(map.Get_MIN_HEIGHT(), map.Get_MAX_HEIGHT()));
                rot = Random.Range(40f, 140f);
                break;
        }
    }
}
