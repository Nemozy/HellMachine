using UnityEngine;
using System.Collections;

public class Meteoroid : EnemyController
{
    private Animator anim;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        if(this.GetComponent<Renderer>())
            this.GetComponent<Renderer>().material.mainTexture = Resources.Load("Enemies/Meteoroids/Meteoroid_1/3D/Textures/Meteoroid_"+ Random.Range(1,6).ToString()) as Texture;
    }

    //Инициализация
    public void Init(float Speed, float _Size, float _Score, float _Damage, float _Hp)
    {
        Size = _Size;
        Score = _Score;
        Damage = _Damage;
        Hp = _Hp;
        MovSpeed = Speed;
        Initiate = true;
	}

    //Движение
    protected override void Move()
    {
        Vector3 vecPos = new Vector3(this.transform.up.x, this.transform.up.y, this.transform.up.z);
        this.transform.position += vecPos * MovSpeed;
        DestroyWhenOut();
    }

    //Уничтожить объект после вылета за пределы видимости экрана
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

    //Получить урон
    protected override void TakeDamage(float dmg)
    {
        Hp -= dmg;
        if (Hp <= 0)
        {
            GameObject.Find("Player").GetComponent<PlayerController>().AddScore(Score);
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
                    enemy.transform.GetComponent<Meteoroid>().Init(0.1f / 0.5f, Size * 0.5f, Score * 0.5f, Damage * 0.5f, Hp * 0.5f);
                }
            }
            GameObject.Find("Exploy").GetComponent<AudioSource>().Play();
            this.transform.GetComponent<SphereCollider>().isTrigger = true;
            if(anim) 
                anim.SetBool("Exploy", true);
            Destroy(this.gameObject,2);
        }
        GameObject.Find("Bomb").GetComponent<AudioSource>().Play();
    }
}
