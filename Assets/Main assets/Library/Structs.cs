﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Structs
{
    public struct ShottingSettings
    {
        public int LaserAmmo;
        public bool StartShooting_Bullet;
        public bool StartShooting_Laser;
        public float ShootingSpeed_Bullet;
        public float ShootingSpeed_Laser;
        public float AddingSpeed_Laser;
        public float LastShooting_Bullet;
        public float LastShooting_Laser;
        public float LastAdding_Laser;
        public ShootBullet BulletWeaponSetting;
    }

    public struct ShootBullet
    {
        public int CountLeftWeapon;
        public int CountMiddleWeapon;
        public int CountRightWeapon;
    }
}
