using UnityEngine;
using System.Collections;

public class BonusController : MonoBehaviour
{
    public int WeaponLeft = 0;
    public int WeaponMiddle = 0;
    public int WeaponRight = 0;
    public int LaserAmmo = 0;
    public float BulletSpeed = 0;
    public float Gasoline = 0;

    private Vector3 RotationAni;
    private bool NonActive = false;

    void Start()
    {
        RotationAni = new Vector3(Random.Range(0, 20), Random.Range(0, 20), Random.Range(0, 20));
    }

    void Update()
    {
        #region only 3D
        this.transform.Rotate(RotationAni * Time.deltaTime);
        #endregion only 3D
    }

    void OnCollisionEnter(Collision col)
    {
        if (!NonActive && !GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetGameOverState() && col.gameObject.name.ToUpper().Equals("SHIP"))
        {
            NonActive = true;
            if (LaserAmmo > 0)
            {
                GameObject.Find("Player").transform.Find("Ship").transform.GetComponent<Shooting>().ShootingSettings.LaserAmmo += LaserAmmo;

                GameObject.Find("Interface").transform.Find("Message").GetComponent<MessageController>().SetMessage("Laser ammo +" + LaserAmmo.ToString());
                GameObject.Find("Interface").transform.Find("Message").GetComponent<MessageController>().Show(2);
            }
            if (BulletSpeed > 0)
            {
                GameObject.Find("Player").transform.Find("Ship").transform.GetComponent<Shooting>().ShootingSettings.ShootingSpeed_Bullet -= BulletSpeed;
                if (GameObject.Find("Player").transform.Find("Ship").transform.GetComponent<Shooting>().ShootingSettings.ShootingSpeed_Bullet < 0.2)
                    GameObject.Find("Player").transform.Find("Ship").transform.GetComponent<Shooting>().ShootingSettings.ShootingSpeed_Bullet = 0.2f;
                GameObject.Find("Interface").transform.Find("Message").GetComponent<MessageController>().SetMessage("Increase shooting speed");
                GameObject.Find("Interface").transform.Find("Message").GetComponent<MessageController>().Show(2);
            }
            else
            {
                GameObject.Find("Player").transform.Find("Ship").transform.GetComponent<Shooting>().ShootingSettings.BulletWeaponSetting.CountLeftWeapon += WeaponLeft;
                GameObject.Find("Player").transform.Find("Ship").transform.GetComponent<Shooting>().ShootingSettings.BulletWeaponSetting.CountMiddleWeapon += WeaponMiddle;
                GameObject.Find("Player").transform.Find("Ship").transform.GetComponent<Shooting>().ShootingSettings.BulletWeaponSetting.CountRightWeapon += WeaponRight;
                GameObject.Find("Interface").transform.Find("Message").GetComponent<MessageController>().SetMessage("Increase shooting power");
                GameObject.Find("Interface").transform.Find("Message").GetComponent<MessageController>().Show(2);
            }
            GameObject.Find("Player").transform.Find("Ship").GetComponent<HeroMovingController>().AddGasoline(Gasoline);
            Destroy(this.gameObject);
        }
    }
}
