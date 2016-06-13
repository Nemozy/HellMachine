using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    public Structs.ShottingSettings ShootingSettings;

    void Start()
    {
        ShootingSettings = new Structs.ShottingSettings();
        ShootingSettings.BulletWeaponSetting = new Structs.ShootBullet();
        ShootingSettings.BulletWeaponSetting.CountMiddleWeapon = 1;
        ShootingSettings.LaserAmmo = 5;
        ShootingSettings.StartShooting_Bullet = false;
        ShootingSettings.StartShooting_Laser = false;
        ShootingSettings.ShootingSpeed_Bullet = 0.7f;
        ShootingSettings.ShootingSpeed_Laser = 15f;
        ShootingSettings.AddingSpeed_Laser = 35f;
        ShootingSettings.LastShooting_Bullet = 0;
        ShootingSettings.LastShooting_Laser = 0;
        ShootingSettings.LastAdding_Laser = 0;
    }
	
	void Update ()
    {
        if (!GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetPauseState() && !GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetGameOverState())
        {
            float time = GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetTime();

            if (ShootingSettings.StartShooting_Bullet && ShootingSettings.LastShooting_Bullet + ShootingSettings.ShootingSpeed_Bullet < time)
            {
                if (ShootingSettings.BulletWeaponSetting.CountMiddleWeapon > 0)
                {
                    GameObject bullet = Instantiate((GameObject)Resources.Load("Bullets/Bullet/" +
                            GameObject.Find("TopPanel").transform.Find("SettingsWindow").Find("GraphMode").GetComponent<UnityEngine.UI.Dropdown>().captionText.text + "/Model/Bullet")) as GameObject;
                    bullet.transform.SetParent(GameObject.Find("Player").transform.Find("Bullets"));
                    bullet.name = "Bullet";
                    bullet.transform.position = GameObject.Find("Player").transform.Find("Ship").transform.position;
                    bullet.transform.rotation = GameObject.Find("Player").transform.Find("Ship").transform.rotation;
                }
                if (ShootingSettings.BulletWeaponSetting.CountLeftWeapon > 0)
                {
                    GameObject bullet = Instantiate((GameObject)Resources.Load("Bullets/Bullet/" +
                            GameObject.Find("TopPanel").transform.Find("SettingsWindow").Find("GraphMode").GetComponent<UnityEngine.UI.Dropdown>().captionText.text + "/Model/Bullet")) as GameObject;
                    bullet.transform.SetParent(GameObject.Find("Player").transform.Find("Bullets"));
                    bullet.name = "Bullet";
                    bullet.transform.position = GameObject.Find("Player").transform.Find("Ship").transform.position;
                    bullet.transform.localPosition = new Vector3(bullet.transform.localPosition.x - 2, bullet.transform.localPosition.y, bullet.transform.localPosition.z);
                    bullet.transform.rotation = GameObject.Find("Player").transform.Find("Ship").transform.rotation;
                }
                if (ShootingSettings.BulletWeaponSetting.CountRightWeapon > 0)
                {
                    GameObject bullet = Instantiate((GameObject)Resources.Load("Bullets/Bullet/" +
                            GameObject.Find("TopPanel").transform.Find("SettingsWindow").Find("GraphMode").GetComponent<UnityEngine.UI.Dropdown>().captionText.text + "/Model/Bullet")) as GameObject;
                    bullet.transform.SetParent(GameObject.Find("Player").transform.Find("Bullets"));
                    bullet.name = "Bullet";
                    bullet.transform.position = GameObject.Find("Player").transform.Find("Ship").transform.position;
                    bullet.transform.localPosition = new Vector3(bullet.transform.localPosition.x + 2, bullet.transform.localPosition.y, bullet.transform.localPosition.z);
                    bullet.transform.rotation = GameObject.Find("Player").transform.Find("Ship").transform.rotation;
                }

                ShootingSettings.LastShooting_Bullet = time;
            }
            if (ShootingSettings.StartShooting_Laser && ShootingSettings.LastShooting_Laser + ShootingSettings.ShootingSpeed_Laser < time && ShootingSettings.LaserAmmo > 0)
            {
                ShootingSettings.LaserAmmo -= 1;
                GameObject enemy = Instantiate((GameObject)Resources.Load("Bullets/Laser/" +
                        GameObject.Find("TopPanel").transform.Find("SettingsWindow").Find("GraphMode").GetComponent<UnityEngine.UI.Dropdown>().captionText.text + "/Model/Laser")) as GameObject;
                enemy.transform.SetParent(GameObject.Find("Player").transform.Find("Bullets"));
                enemy.name = "Laser";
                enemy.transform.position = GameObject.Find("Player").transform.Find("Ship").transform.position + GameObject.Find("Player").transform.Find("Ship").transform.up*30;
                enemy.transform.rotation = GameObject.Find("Player").transform.Find("Ship").transform.rotation;

                ShootingSettings.LastShooting_Laser = time;
            }
            if (ShootingSettings.LastAdding_Laser + ShootingSettings.AddingSpeed_Laser < time)
            {
                GameObject.Find("Interface").transform.Find("Message").GetComponent<MessageController>().SetMessage("+1 Laser ammo");
                GameObject.Find("Interface").transform.Find("Message").GetComponent<MessageController>().Show(2);
                ShootingSettings.LaserAmmo++;
                ShootingSettings.LastAdding_Laser = time;
            }
        }
	}

    public int GetLaserAmmo()
    {
        return ShootingSettings.LaserAmmo;
    }

    public void GiveLaserAmmo(int value)
    {
        ShootingSettings.LaserAmmo += value;
    }

    public float BulletCooldown()
    {
        return (GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetTime() - ShootingSettings.LastShooting_Bullet) / ShootingSettings.ShootingSpeed_Bullet;
    }

    public float LaserCooldown()
    {
        return (GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetTime() - ShootingSettings.LastShooting_Laser) / ShootingSettings.ShootingSpeed_Laser;
    }

    public float LaserAddingProgress()
    {
        return (GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetTime() - ShootingSettings.LastAdding_Laser) / ShootingSettings.AddingSpeed_Laser;
    }

    public void StartShoot(Unions.BulletType type)
    {
        if (type == Unions.BulletType.Bullet)
        {
            ShootingSettings.StartShooting_Bullet = true;
        }
        if (type == Unions.BulletType.Laser)
        {
            ShootingSettings.StartShooting_Laser = true;
        }
    }

    public void StopShoot(Unions.BulletType type)
    {
        if (type == Unions.BulletType.Bullet)
        {
            ShootingSettings.StartShooting_Bullet = false;
        }
        if (type == Unions.BulletType.Laser)
        {
            ShootingSettings.StartShooting_Laser = false;
        }
    }
}
