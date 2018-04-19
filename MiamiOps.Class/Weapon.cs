using System;

namespace MiamiOps
{
    public class Weapon
    {
        Player _owner;
        float _attack;
        float _radius;    // rayon d'action
        float _range;    // la portée
        uint _ammo;    // le nombre de fois où tu peux attaquer
        uint _maxAmmo;

        public Weapon(Player owner, float attack, float radius, float range, uint _maxAmmo)
        {
            float[] stats = new float[3]{attack, radius, range};
            foreach (float nb in stats) {if (nb < 0 || nb > 1) {throw new ArgumentException("The parameters can't be lower than 0 or upper than 1.");}}
            if (_maxAmmo < 0) {throw new ArgumentException("The max ammo can't be lower than zero.");}

            this._owner = owner;
            this._attack = attack;
            this._radius = radius;
            this._range = range;
            this._ammo = _maxAmmo;
            this._maxAmmo = _maxAmmo;
        }

        public void Attack()
        {
            //if (_maxAmmo > 0) RemoveFiredBullets();
            if (_ammo <= 0) Reload();
        }

        public void Reload()
        {
            _ammo = _maxAmmo;
        }
    }


    public class WeaponFactory
    {
        public Weapon CreateAssaultRifle(Player owner)
        {
            return new Weapon(owner, 0, 0, 0, 30);
        }

        public Weapon CreateShotgun(Player owner)
        {
            return new Weapon(owner, 0, 0, 0, 8);
        }

        public Weapon CreatePistol(Player owner)
        {
            return new Weapon(owner, 0, 0, 0, 12);
        }

        public Weapon CreateBaseballBat(Player owner)
        {
            return new Weapon(owner, 0, 0, 0, 0);
        }

        public Weapon CreateSoulcalibur(Player owner)
        {
            return new Weapon(owner, 0, 0, 0, 0);
        }

        public Weapon CreateChaosBlade(Player owner)
        {
            return new Weapon(owner, 0, 0, 0, 0);
        }

        public Weapon CreateGodBlade(Player owner)
        {
            return new Weapon(owner, 0, 0, 0, 0);
        }

        public Weapon CreateCompanion()
        {
            throw new NotImplementedException();
        }
    }
}
