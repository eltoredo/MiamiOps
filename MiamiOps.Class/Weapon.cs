using System;
using System.Collections.Generic;

namespace MiamiOps
{
    public class Weapon : IStuff
    {
        Player _owner;

        private List<Shoot> _bullets;
        private List<Shoot> toRemove = new List<Shoot>();

        float _attack;
        float _radius;    // rayon d'action
        float _range;    // la portée
        uint _ammo;    // le nombre de fois où tu peux attaquer
        uint _maxAmmo;    // le nombre maximum de munition

        int _time;


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

            Shoot shoot = new Shoot(1f, TimeSpan.FromMilliseconds(5000), 0.005f, playerPosition, mousePlace);
            _bullets.Add(shoot);

            _ammo -= 1;
            if (_ammo <= 0) Reload();
        }

        public Vector BulletMove(Shoot bullet, float speed)
        {
            Vector bulletPlace;

            // Builds a vector in the direction of the mouse
            Vector direction = bullet.MousePosition - bullet.StartPosition;
            // Builds a unit vector in the direction of the mouse
            double diviseur = direction.Magnitude;
            if (direction.Magnitude == 0) diviseur = 1;    // In case if the enemie is in (0, 0) the magnitude is 0 and we can't devided by 0
            Vector unit_vector = direction * (1.0 / diviseur);
            Vector move = unit_vector * speed;
            if(bullet.CountBullet == 0)
            {
                bulletPlace = bullet.StartPosition + move;
                bullet.CountBullet++;
            }
            else
            {
                bulletPlace = bullet.BulletPosition + move;
            }

            bullet.BulletPosition = bulletPlace;
            return bullet.BulletPosition;
        }

        public void Reload()
        {
            _ammo = _maxAmmo;
        }

        public void Update()
        {
            if (_bullets.Count > 0)
            {
                foreach (Shoot s in _bullets)
                {
                    BulletMove(s, s.SpeedBullet);
                }
            }

            foreach (Shoot s in _bullets)
            {
                if (!s.IsAlive) toRemove.Add(s);
            }

            foreach (Shoot s in toRemove) _bullets.Remove(s);

        }

        public void WalkOn()
        {
            throw new NotImplementedException();
        }

        public List<Shoot> Bullets => _bullets;
    }

    public class WeaponFactory : IStuffFactory
    {
        readonly float _attack;
        readonly float _radius;
        readonly float _range;
        readonly uint _maxAmmo;



        public WeaponFactory(float attack, float radius, float range, uint maxAmmo)
        {
            _attack = attack;
            _radius = radius;
            _range = range;
            _maxAmmo = maxAmmo;
        }

        public IStuff Create()
        {
            return new Weapon(_owner, _attack, _radius, _range, _maxAmmo);
        }

        public void WalkOn()
        {
            throw new NotImplementedException();
        }
    }
}
