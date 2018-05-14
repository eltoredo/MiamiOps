using System;
using System.Collections.Generic;

namespace MiamiOps
{
    public class Weapon
    {
        Player _owner;

        private List<Shoot> _bullets;

        float _attack;
        float _radius;    // rayon d'action
        float _range;    // la portée
        uint _ammo;    // le nombre de fois où tu peux attaquer
        uint _maxAmmo;    // le nombre maximum de munition

        public Weapon(Player owner, float attack, float radius, float range, uint _maxAmmo)
        {
            float[] stats = new float[3]{attack, radius, range};
            foreach (float nb in stats) {if (nb < 0 || nb > 1) {throw new ArgumentException("The parameters can't be lower than 0 or upper than 1.");}}
            if (_maxAmmo < 0) {throw new ArgumentException("The max ammo can't be lower than zero.");}

            _bullets = new List<Shoot>();

            this._owner = owner;
            this._attack = attack;
            this._radius = radius;
            this._range = range;
            this._ammo = _maxAmmo;
            this._maxAmmo = _maxAmmo;
        }

        public void Shoot(Vector playerPosition, Vector mousePlace)
        {
            // Player - Context - Monsters -> If X or Y of mousePlace (direction of bullet) is touching the bounding box of an enemy, he looses life
            // Position de départ et d'arrivée de la balle, vitesse / quand est-ce que j'ai tiré
            // Faire la différence entre le moment où la balle a été tirée et le temps qui s'est écoulé
            // Supprimer la balle après un certain temps

            Shoot shoot = new Shoot(1f, TimeSpan.FromMilliseconds(500), 1f);
            _bullets.Add(shoot);

            _ammo -= 1;
            if (_ammo <= 0) Reload();
        }

        public void Reload()
        {
            _ammo = _maxAmmo;
        }

        public void Update()
        {
            List<Shoot> toRemove = new List<Shoot>();
            foreach(Shoot s in _bullets)
            {
                if (!s.IsAlive) toRemove.Add(s);
            }

            foreach (Shoot s in toRemove) _bullets.Remove(s);
        }

        public List<Shoot> Bullets => _bullets;
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
