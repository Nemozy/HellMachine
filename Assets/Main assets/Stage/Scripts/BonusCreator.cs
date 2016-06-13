using UnityEngine;
using System.Collections;

public class BonusCreator : MonoBehaviour
{
    private float LastSpawn = 0f;
    private float ReuseSpawn = 30f;

	void Update () 
    {
        if (!GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetPauseState() &&
                !GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetGameOverState())
        {
            float time = GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetTime();

            if (time > LastSpawn + ReuseSpawn)
            {
                LastSpawn = time;

                GameObject bonus = Instantiate((GameObject)Resources.Load("Bonuses/3D/Model/Bonus")) as GameObject;
                bonus.transform.SetParent(GameObject.Find("Enemies").transform);
                bonus.name = "Bonus";
                bonus.transform.position = new Vector3 (GameObject.Find("Player").transform.Find("Ship").transform.position.x + Random.Range(-20,20),
                                                        GameObject.Find("Player").transform.Find("Ship").transform.position.y,
                                                        GameObject.Find("Player").transform.Find("Ship").transform.position.z + Random.Range(-20,20));

                int val = Random.Range(0,3);
                Debug.Log(val);
                GameObject.Find("Interface").transform.Find("Message").GetComponent<MessageController>().SetMessage("Bonus spawn");
                GameObject.Find("Interface").transform.Find("Message").GetComponent<MessageController>().Show(2);
                bonus.transform.GetComponent<BonusController>().Gasoline = Random.Range(100, 500);
                switch (val)
                {
                    case 0:
                        bonus.transform.GetComponent<BonusController>().WeaponLeft = Random.Range(0, 2);
                        bonus.transform.GetComponent<BonusController>().WeaponMiddle = Random.Range(0, 2);
                        bonus.transform.GetComponent<BonusController>().WeaponRight = Random.Range(0, 2);
                        break;
                    case 1:
                        bonus.transform.GetComponent<BonusController>().LaserAmmo = Random.Range(0, 4);
                        break;
                    case 2:
                        bonus.transform.GetComponent<BonusController>().BulletSpeed = Random.Range(0.01f, 0.15f);
                        break;
                    default:
                        bonus.transform.GetComponent<BonusController>().WeaponLeft = Random.Range(0, 2);
                        bonus.transform.GetComponent<BonusController>().WeaponMiddle = Random.Range(0, 2);
                        bonus.transform.GetComponent<BonusController>().WeaponRight = Random.Range(0, 2);
                        break;
                }
            }
        }
	}
}
