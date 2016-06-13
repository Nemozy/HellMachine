using UnityEngine;
using System.Collections;

public class HeroMovingController : MonoBehaviour
{
    private float Gasoline = 1000;
    private float MAX_SPEED = 0.4f;
    private float Speed = 0f;
    const float DeltaSpeed = 0.15f;
    const float RotationSpeed = 85f;
    private Animator anim;

	void Start () 
    {
        anim = this.GetComponent<Animator>();
	}

	void FixedUpdate ()
    {
        if (!GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetPauseState())
        {
            #region Управление
            if (Input.GetKey(KeyCode.W) && Gasoline > 0)
            {
                if (Speed < MAX_SPEED)
                {
                    Speed = Speed + DeltaSpeed * Time.deltaTime > MAX_SPEED ? MAX_SPEED : Speed + DeltaSpeed * Time.deltaTime;
                }
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.forward * RotationSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.forward * -RotationSpeed * Time.deltaTime);
            }

            if ((Speed > 0 && !Input.GetKey(KeyCode.W)) || Gasoline <= 0)
            {
                Speed = Speed - DeltaSpeed * Time.deltaTime < 0 ? 0 : Speed - DeltaSpeed * Time.deltaTime;
            }
            #endregion Управление

            anim.SetFloat("Speed", Speed);
            Gasoline -= Speed;
            this.transform.position += new Vector3(this.transform.up.x, this.transform.up.y, this.transform.up.z) * Speed;

            #region Вылеты за экран
            Map map = GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetMap();

            if (this.transform.position.x < map.Get_MIN_WIDTH() + 50)
            {
                this.transform.position = new Vector3(map.Get_MIN_WIDTH() + 50, this.transform.position.y, this.transform.position.z);
            }
            if (this.transform.position.x > map.Get_MAX_WIDTH() - 50)
            {
                this.transform.position = new Vector3(map.Get_MAX_WIDTH() - 50, this.transform.position.y, this.transform.position.z);
            }
            if (this.transform.position.z < map.Get_MIN_HEIGHT() + 20)
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, map.Get_MIN_HEIGHT() + 20);
            }
            if (this.transform.position.z > map.Get_MAX_HEIGHT() - 20)
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, map.Get_MAX_HEIGHT() - 20);
            }
            #endregion Вылеты за экран
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.ToUpper().Contains("METEOROID") || col.gameObject.name.ToUpper().Contains("ALIEN"))
        {
            this.gameObject.SetActive(false);

            for (int i = 0; i < 6; i++)
            {
                float rot = Random.Range(0f, 360f);
                GameObject enemy = Instantiate((GameObject)Resources.Load("Enemies/Meteoroids/Meteoroid_1/" +
                        GameObject.Find("TopPanel").transform.Find("SettingsWindow").Find("GraphMode").GetComponent<UnityEngine.UI.Dropdown>().captionText.text + "/Model/Meteoroid")) as GameObject;
                enemy.transform.SetParent(GameObject.Find("Enemies").transform);
                enemy.name = "Meteoroid_Crash";
                enemy.transform.localScale *= Random.Range(0.30f, 0.70f);
                enemy.transform.position = this.gameObject.transform.position;
                if (GameObject.Find("TopPanel").transform.Find("SettingsWindow").Find("GraphMode").GetComponent<UnityEngine.UI.Dropdown>().captionText.text.Equals("3D"))
                    enemy.transform.Rotate(90, rot, 0);
                else
                    enemy.transform.Rotate(rot, 0, 0);
                enemy.transform.GetComponent<Meteoroid>().Init(0.05f);
            }

            GameObject.Find("Terrain").GetComponent<StageEnvironment>().GameOver();
        }
    }

    public float GetGasoline()
    {
        return Gasoline;
    }

    public void AddGasoline(float value)
    {
        Gasoline += value;
    }

    public void SetFixGasoline(float value)
    {
        Gasoline = value;
    }
}
