using UnityEngine;
using System.Collections;

public class BossController : EnemyController
{
    public Structs.ShottingSettings ShootingSettings;

    public void Init(float Speed, float _Size, float _Score, float _Damage, float _Hp)
    {
        Size = _Size;
        Score = _Score;
        Damage = _Damage;
        Hp = _Hp;
        MovSpeed = Speed;
        Initiate = true;

        ShootingSettings = new Structs.ShottingSettings();
        ShootingSettings.BulletWeaponSetting = new Structs.ShootBullet();
        ShootingSettings.BulletWeaponSetting.CountMiddleWeapon = Mathf.RoundToInt(1 * _Size);
        ShootingSettings.BulletWeaponSetting.CountLeftWeapon = Mathf.RoundToInt(0.3f * _Size);
        ShootingSettings.BulletWeaponSetting.CountRightWeapon = Mathf.RoundToInt(0.3f * _Size);
        ShootingSettings.LaserAmmo = 0;
        ShootingSettings.StartShooting_Bullet = false;
        ShootingSettings.StartShooting_Laser = false;
        ShootingSettings.StartShooting_Bomb = false;
        ShootingSettings.ShootingSpeed_Bullet = 1.7f - 0.2f * _Size;
        ShootingSettings.ShootingSpeed_Laser = 30f;
        ShootingSettings.ShootingSpeed_Bomb = 60f;
        ShootingSettings.AddingSpeed_Laser = 35f;
        ShootingSettings.LastShooting_Bullet = 0;
        ShootingSettings.LastShooting_Laser = 0;
        ShootingSettings.LastShooting_Bomb = 0;
        ShootingSettings.LastAdding_Laser = 0;
    }

    private void Shooting()
    {
        if (Hp > 0 && !GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetGameOverState())
        {
            float time = GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetTime();

            if (ShootingSettings.StartShooting_Bullet && ShootingSettings.LastShooting_Bullet + ShootingSettings.ShootingSpeed_Bullet < time)
            {
                if (ShootingSettings.BulletWeaponSetting.CountMiddleWeapon > 0)
                {
                    GameObject bullet = Instantiate((GameObject)Resources.Load("Enemies/Bullets/Bullet/3D/Model/Bullet")) as GameObject;
                    bullet.transform.SetParent(GameObject.Find("Enemies").transform);
                    bullet.name = "Bullet";
                    bullet.transform.position = this.transform.position;
                    bullet.transform.rotation = this.transform.rotation;
                    bullet.transform.Rotate(90, 0, 0);
                    bullet.GetComponent<BulletEnemy>().Dmg = ShootingSettings.BulletWeaponSetting.CountMiddleWeapon;
                }
                if (ShootingSettings.BulletWeaponSetting.CountLeftWeapon > 0)
                {
                    GameObject bullet = Instantiate((GameObject)Resources.Load("Enemies/Bullets/Bullet/3D/Model/Bullet")) as GameObject;
                    bullet.transform.SetParent(GameObject.Find("Enemies").transform);
                    bullet.name = "Bullet";
                    bullet.transform.position = this.transform.position;
                    bullet.transform.localPosition = this.transform.position + this.transform.right * -4;
                    bullet.transform.rotation = this.transform.rotation;
                    bullet.transform.Rotate(90, 0, 0);
                    bullet.GetComponent<BulletEnemy>().Dmg = ShootingSettings.BulletWeaponSetting.CountMiddleWeapon;
                }
                if (ShootingSettings.BulletWeaponSetting.CountRightWeapon > 0)
                {
                    GameObject bullet = Instantiate((GameObject)Resources.Load("Enemies/Bullets/Bullet/3D/Model/Bullet")) as GameObject;
                    bullet.transform.SetParent(GameObject.Find("Enemies").transform);
                    bullet.name = "Bullet";
                    bullet.transform.position = this.transform.position;
                    bullet.transform.localPosition = this.transform.position + this.transform.right * 4;
                    bullet.transform.rotation = this.transform.rotation;
                    bullet.transform.Rotate(90, 0, 0);
                    bullet.GetComponent<BulletEnemy>().Dmg = ShootingSettings.BulletWeaponSetting.CountMiddleWeapon;
                }
                GameObject.Find("Shoot").GetComponent<AudioSource>().Play();
                ShootingSettings.LastShooting_Bullet = time;
            }
        }
    }

    protected override void Move()
    {
        if (GameObject.FindGameObjectWithTag("PlayerShip"))
            this.transform.LookAt(GameObject.FindGameObjectWithTag("PlayerShip").transform);
        if (Vector3.Distance(this.gameObject.transform.position, GameObject.Find("Player").transform.Find("Ship").transform.position) > 40)
        {
            ShootingSettings.StartShooting_Bullet = false;
            this.transform.position += new Vector3(this.transform.forward.x, this.transform.forward.y, this.transform.forward.z) * MovSpeed;
        }
        else
        {
            ShootingSettings.StartShooting_Bullet = true;
            Shooting();
        }
    }

    protected override void TakeDamage(float dmg)
    {
        Hp -= dmg;
        if (Hp <= 0)
        {
            if (GameObject.Find("Ship"))
            {
                GameObject.Find("Player").GetComponent<PlayerController>().AddScore(Score);
                GameObject.Find("Exploy").GetComponent<AudioSource>().Play();
            }
            Destroy(this.gameObject);
        }
    }
}
