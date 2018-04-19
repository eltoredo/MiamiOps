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
<<<<<<< HEAD
        uint _maxAmmo;    // le nombre maximum de munition

        public Weapon(Player owner, float attack, float radius, float range, uint ammo, uint maxAmmo)
=======
        uint _maxAmmo;

        public Weapon(Player owner, float attack, float radius, float range, uint _maxAmmo)
>>>>>>> f67726aca704eec3e76e92c1a858ebfb8141e04f
        {
            float[] stats = new float[3]{attack, radius, range};
            foreach (float nb in stats) {if (nb < 0 || nb > 1) {throw new ArgumentException("The parameters can't be lower than 0 or upper than 1.");}}
            if (_maxAmmo < 0) {throw new ArgumentException("The max ammo can't be lower than zero.");}

            this._owner = owner;
            this._attack = attack;
            this._radius = radius;
            this._range = range;
<<<<<<< HEAD
            this._ammo = ammo;
            this._maxAmmo = maxAmmo;
=======
            this._ammo = _maxAmmo;
            this._maxAmmo = _maxAmmo;
>>>>>>> f67726aca704eec3e76e92c1a858ebfb8141e04f
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
<<<<<<< HEAD
            return new Weapon(owner, 0, 0, 0, 0, 0);
=======
            return new Weapon(owner, 0, 0, 0, 30);
>>>>>>> f67726aca704eec3e76e92c1a858ebfb8141e04f
        }

        public Weapon CreateShotgun(Player owner)
        {
<<<<<<< HEAD
            return new Weapon(owner, 0, 0, 0, 0, 0);
=======
            return new Weapon(owner, 0, 0, 0, 8);
>>>>>>> f67726aca704eec3e76e92c1a858ebfb8141e04f
        }

        public Weapon CreatePistol(Player owner)
        {
<<<<<<< HEAD
            return new Weapon(owner, 0, 0, 0, 0, 0);
=======
            return new Weapon(owner, 0, 0, 0, 12);
>>>>>>> f67726aca704eec3e76e92c1a858ebfb8141e04f
        }

        public Weapon CreateBaseballBat(Player owner)
        {
            return new Weapon(owner, 0, 0, 0, 0, 0);
        }

        public Weapon CreateSoulcalibur(Player owner)
        {
            return new Weapon(owner, 0, 0, 0, 0, 0);
        }

        public Weapon CreateChaosBlade(Player owner)
        {
            return new Weapon(owner, 0, 0, 0, 0, 0);
        }

        public Weapon CreateGodBlade(Player owner)
        {
            return new Weapon(owner, 0, 0, 0, 0, 0);
        }

        public Weapon CreateCompanion()
        {
            throw new NotImplementedException();
        }
    }
}
