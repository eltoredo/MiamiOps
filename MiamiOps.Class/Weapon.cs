﻿using System;

namespace MiamiOps
{
    public class Weapon
    {
        Player _owner;
        float _attack;
        float _radius;    // rayon d'action
        float _range;    // la portée
        uint _ammo;    // le nombre de fois où tu peux attaquer

        public Weapon(Player owner, float attack, float radius, float range, uint ammo)
        {
            float[] stats = new float[3]{attack, radius, range};
            foreach (float nb in stats) {if (nb < 0 || nb > 1) {throw new ArgumentException("The parameters can't be lower than 0 or upper than 1.");}}
            if (ammo < 0) {throw new ArgumentException("The ammo can't be lower than zero.");}

            this._owner = owner;
            this._attack = attack;
            this._radius = radius;
            this._range = range;
            this._ammo = ammo;
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }

        public void Reload()
        {
            throw new NotImplementedException();
        }
    }


    public class WeaponFactory
    {
        public Weapon CreateAssaultRifle()
        {
            throw new NotImplementedException();
        }

        public Weapon CreateShotgun()
        {
            throw new NotImplementedException();
        }

        public Weapon CreatePistol()
        {
            throw new NotImplementedException();
        }

        public Weapon CreateBaseballBat()
        {
            throw new NotImplementedException();
        }

        public Weapon CreateSoulcalibur()
        {
            throw new NotImplementedException();
        }

        public Weapon CreateChaosBlade()
        {
            throw new NotImplementedException();
        }

        public Weapon CreateGodBlade()
        {
            throw new NotImplementedException();
        }

        public Weapon CreateCompanion()
        {
            throw new NotImplementedException();
        }
    }
}
