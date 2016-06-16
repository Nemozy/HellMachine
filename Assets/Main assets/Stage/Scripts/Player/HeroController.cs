using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour
{
    public float Hp = 30;
    public float Gasoline = 1000;
    public float Regeneration = 1;
    private float LastAutoRegeneration = 0;
    private float LastMoving = 0;
    const float MAX_GASOLINE = 1000;
    const float MAX_HP = 30;
    const float RegenerationTick = 5f;

    void OnGUI()
    {
        Vector3 crd = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
        GUI.DrawTexture(new Rect(crd.x - (Hp / MAX_HP * 50 / 2), Screen.height - crd.y - 45, Hp / MAX_HP * 50, 5), Resources.Load("Hero/Textures/HPBar") as Texture);
    }

    void Update()
    {
        if (LastAutoRegeneration + RegenerationTick < GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetTime())
        {
            LastAutoRegeneration = GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetTime();
            GetHp(Regeneration);
        }
    }

    public void AddGas(float value)
    {
        Gasoline += value;
        if (Gasoline > MAX_GASOLINE)
            Gasoline = MAX_GASOLINE;
    }

    public void RegenerationGas(float value)
    {
        if (GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetTime() - LastMoving > 5)
        {
            Gasoline += value;
            if (Gasoline > MAX_GASOLINE)
                Gasoline = MAX_GASOLINE;
        }
    }


    public bool RemoveGas(float value)
    {
        if (value > 0)
            LastMoving = GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetTime();

        Gasoline -= value;
        if (Gasoline <= 0)
        {
            Gasoline = 0;
            return false;
        }

        return true;
    }

    public float GetGas()
    {
        return Gasoline;
    }

    public void SetGas(float value)
    {
        Gasoline = value;
    }

    public float GetHp(float value)
    {
        Hp += value;
        if (Hp > MAX_HP)
            Hp = MAX_HP;

        return Hp;
    }

    public float TakeDamage(float dmg)
    {
        Hp -= dmg;
        if (Hp <= 0)
        {
            for (int i = 0; i < 6; i++)
            {
                float rot = Random.Range(0f, 360f);
                GameObject enemy = Instantiate((GameObject)Resources.Load("Enemies/Meteoroids/Meteoroid_1/" +
                        GameObject.Find("TopPanel").transform.Find("SettingsWindow").Find("GraphMode").GetComponent<UnityEngine.UI.Dropdown>().captionText.text + "/Model/Meteoroid")) as GameObject;
                enemy.transform.SetParent(GameObject.Find("Enemies").transform);
                enemy.name = "Meteoroid_Crash";
                float sze = Random.Range(0.30f, 0.70f);
                enemy.transform.localScale *= sze;
                enemy.transform.position = this.gameObject.transform.position;
                if (GameObject.Find("TopPanel").transform.Find("SettingsWindow").Find("GraphMode").GetComponent<UnityEngine.UI.Dropdown>().captionText.text.Equals("3D"))
                    enemy.transform.Rotate(90, rot, 0);
                else
                    enemy.transform.Rotate(rot, 0, 0);
                enemy.transform.GetComponent<Meteoroid>().Init(0.05f, sze, Random.Range(1, 5), Random.Range(1, 5), Random.Range(1, 5));
            }
            GameObject.Find("Terrain").GetComponent<StageEnvironment>().GameOver();
            this.gameObject.SetActive(false);
        }

        return Hp;
    }
}
