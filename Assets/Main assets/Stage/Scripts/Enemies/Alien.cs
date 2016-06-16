using UnityEngine;
using System.Collections;

public class Alien : EnemyController
{
    public void Init(float Speed, float _Size, float _Score, float _Damage, float _Hp)
    {
        Size = _Size;
        Score = _Score;
        Damage = _Damage;
        Hp = _Hp;
        MovSpeed = Speed;
        Initiate = true;
	}

    protected override void Move()
    {
        if (GameObject.FindGameObjectWithTag("PlayerShip"))
            this.transform.LookAt(GameObject.FindGameObjectWithTag("PlayerShip").transform);
        this.transform.position += new Vector3(this.transform.forward.x, this.transform.forward.y, this.transform.forward.z) * MovSpeed;
        if (GameObject.Find("TopPanel").transform.Find("SettingsWindow").Find("GraphMode").GetComponent<UnityEngine.UI.Dropdown>().captionText.text.Equals("2D"))
            this.transform.Rotate(90, 0, 0);
    }
    protected override void TakeDamage(float dmg)
    {
        Hp -= dmg;
        if (Hp <= 0)
        {
            if (GameObject.Find("Ship"))
            {
                GameObject.Find("Ship").GetComponent<Shooting>().GiveBombAmmo(1);
                GameObject.Find("Player").GetComponent<PlayerController>().AddScore(Score);
                GameObject.Find("Quest").SendMessage("KillAlien");
                GameObject.Find("Bomb").GetComponent<AudioSource>().Play();
            }
            Destroy(this.gameObject);
        }
    }
}
