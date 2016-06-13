using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GraphicsController : MonoBehaviour
{
    public void Set3D()
    {
        #region Hero switch
        GameObject OldHeroShip = GameObject.Find("Player").transform.Find("Ship").gameObject;
        GameObject HeroShip = Instantiate((GameObject)Resources.Load("Hero/Model/3D/Model/Ship")) as GameObject;
        HeroShip.transform.SetParent(GameObject.Find("Player").transform);
        HeroShip.name = OldHeroShip.name;
        HeroShip.transform.position = OldHeroShip.transform.position;
        HeroShip.transform.rotation = OldHeroShip.transform.rotation;
        HeroShip.GetComponent<Shooting>().ShootingSettings = OldHeroShip.GetComponent<Shooting>().ShootingSettings;
        HeroShip.GetComponent<HeroMovingController>().SetFixGasoline(OldHeroShip.GetComponent<HeroMovingController>().GetGasoline());
        Destroy(OldHeroShip);
        #endregion

        #region enemies switch=
        int countEnmy = GameObject.Find("Enemies").transform.childCount;
        for (int i = 0; i < countEnmy; i++)
        {
            GameObject OldEnemy = GameObject.Find("Enemies").transform.GetChild(i).gameObject;
            GameObject Enemy = null;
            if (OldEnemy.name.ToUpper().Contains("METEOROID"))
            {
                Enemy = Instantiate((GameObject)Resources.Load("Enemies/Meteoroids/Meteoroid_1/3D/Model/Meteoroid")) as GameObject;
                Enemy.GetComponent<Meteoroid>().Init(OldEnemy.GetComponent<Meteoroid>().MovSpeed);
            }
            if (OldEnemy.name.ToUpper().Contains("ALIEN"))
            {
                Enemy = Instantiate((GameObject)Resources.Load("Enemies/Alien/3D/Model/Alien")) as GameObject;
                Enemy.GetComponent<Alien>().Init(OldEnemy.GetComponent<Alien>().MovSpeed);
            }
            Enemy.transform.SetParent(GameObject.Find("Enemies").transform);
            Enemy.name = OldEnemy.name;
            Enemy.transform.position = OldEnemy.transform.position;
            Enemy.transform.rotation = OldEnemy.transform.rotation;
            if(Enemy.name.ToUpper().Contains("SMALL"))
                Enemy.transform.localScale *= 0.5f;;
            Destroy(GameObject.Find("Enemies").transform.GetChild(i).gameObject);
        }
        #endregion enemies switch

        #region bullets switch
        int countBullets = GameObject.Find("Player").transform.Find("Bullets").transform.childCount;
        for (int i = 0; i < countBullets; i++)
        {
            GameObject OldBullet = GameObject.Find("Player").transform.Find("Bullets").transform.GetChild(i).gameObject;
            GameObject Bullet = null;
            if (OldBullet.name.ToUpper().Contains("BULLET"))
            {
                Bullet = Instantiate((GameObject)Resources.Load("Bullets/Bullet/3D/Model/Bullet")) as GameObject;
            }
            if (OldBullet.name.ToUpper().Contains("LASER"))
            {
                Bullet = Instantiate((GameObject)Resources.Load("Bullets/Laser/3D/Model/Laser")) as GameObject;
            }
            Bullet.transform.SetParent(GameObject.Find("Player").transform.Find("Bullets").transform);
            Bullet.name = OldBullet.name;
            Bullet.transform.position = OldBullet.transform.position;
            Bullet.transform.rotation = OldBullet.transform.rotation;
            Destroy(GameObject.Find("Player").transform.Find("Bullets").transform.GetChild(i).gameObject);
        }
        #endregion bullets switch
    }

    public void Set2D()
    {
        #region Hero switch
        GameObject OldHeroShip = GameObject.Find("Player").transform.Find("Ship").gameObject;
        GameObject HeroShip = Instantiate((GameObject)Resources.Load("Hero/Model/2D/Model/Ship")) as GameObject;
        HeroShip.transform.SetParent(GameObject.Find("Player").transform);
        HeroShip.name = OldHeroShip.name;
        HeroShip.transform.position = OldHeroShip.transform.position;
        HeroShip.transform.rotation = OldHeroShip.transform.rotation;
        HeroShip.GetComponent<Shooting>().ShootingSettings = OldHeroShip.GetComponent<Shooting>().ShootingSettings;
        HeroShip.GetComponent<HeroMovingController>().SetFixGasoline(OldHeroShip.GetComponent<HeroMovingController>().GetGasoline());
        Destroy(OldHeroShip);
        #endregion

        #region enemies switch
        int countEnmy = GameObject.Find("Enemies").transform.childCount;
        for (int i = 0; i < countEnmy; i++)
        {
            GameObject OldEnemy = GameObject.Find("Enemies").transform.GetChild(i).gameObject;
            GameObject Enemy = null;
            if (OldEnemy.name.ToUpper().Contains("METEOROID"))
            {
                Enemy = Instantiate((GameObject)Resources.Load("Enemies/Meteoroids/Meteoroid_1/2D/Model/Meteoroid")) as GameObject;
                Enemy.GetComponent<Meteoroid>().Init(OldEnemy.GetComponent<Meteoroid>().MovSpeed);
            }
            if (OldEnemy.name.ToUpper().Contains("ALIEN"))
            {
                Enemy = Instantiate((GameObject)Resources.Load("Enemies/Alien/2D/Model/Alien")) as GameObject;
                Enemy.GetComponent<Alien>().Init(OldEnemy.GetComponent<Alien>().MovSpeed);
            }
            Enemy.transform.SetParent(GameObject.Find("Enemies").transform);
            Enemy.name = OldEnemy.name;
            Enemy.transform.position = OldEnemy.transform.position;
            Enemy.transform.rotation = OldEnemy.transform.rotation;
            if (Enemy.name.ToUpper().Contains("SMALL"))
                Enemy.transform.localScale *= 0.5f; ;
            Destroy(GameObject.Find("Enemies").transform.GetChild(i).gameObject);
        }
        #endregion enemies switch

        #region bullets switch
        int countBullets = GameObject.Find("Player").transform.Find("Bullets").transform.childCount;
        for (int i = 0; i < countBullets; i++)
        {
            GameObject OldBullet = GameObject.Find("Player").transform.Find("Bullets").transform.GetChild(i).gameObject;
            GameObject Bullet = null;
            if (OldBullet.name.ToUpper().Contains("BULLET"))
            {
                Bullet = Instantiate((GameObject)Resources.Load("Bullets/Bullet/2D/Model/Bullet")) as GameObject;
            }
            if (OldBullet.name.ToUpper().Contains("LASER"))
            {
                Bullet = Instantiate((GameObject)Resources.Load("Bullets/Laser/2D/Model/Laser")) as GameObject;
            }
            Bullet.transform.SetParent(GameObject.Find("Player").transform.Find("Bullets").transform);
            Bullet.name = OldBullet.name;
            Bullet.transform.position = OldBullet.transform.position;
            Bullet.transform.rotation = OldBullet.transform.rotation;
            Destroy(GameObject.Find("Player").transform.Find("Bullets").transform.GetChild(i).gameObject);
        }
        #endregion bullets switch
    }
}
