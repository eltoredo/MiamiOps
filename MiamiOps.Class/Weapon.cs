﻿using System;
using System.Collections.Generic;

namespace MiamiOps
{
    public class Weapon : IStuff
    {
        Player _owner;

        private List<Shoot> _bullets;
        private List<Shoot> toRemove = new List<Shoot>();
        List<int> _listCount = new List<int>();

        private Random random = new Random();
        Vector _place;
        bool _life;
        string _name;
        float _attack;
        float _radius;    // rayon d'action
        float _range;    // la portée
        uint _ammo;    // le nombre de fois où tu peux attaquer
        uint _maxAmmo;    // le nombre maximum de munition
        TimeSpan _lifeSpan;
        DateTime _creationDate;
        Round _context;
        bool verifWeaponInList;
        int _time;
        int count;

        public Weapon(Player owner, float attack, float radius, float range, uint _maxAmmo)
        {
            float[] stats = new float[3]{attack, radius, range};
         //   foreach (float nb in stats) {if (nb < 0 || nb > 1) {throw new ArgumentException("The parameters can't be lower than 0 or upper than 1.");}}
            if (_maxAmmo < 0) {throw new ArgumentException("The max ammo can't be lower than zero.");}

            _bullets = new List<Shoot>();

            this._owner = owner;
            this._attack = attack;
            this._radius = radius;
            this._range = range;
            this._ammo = _maxAmmo;
            this._maxAmmo = _maxAmmo;
            _life = true;
        }

        public Weapon(Round context, string name, float attack, float radius, float range, uint _maxAmmo,TimeSpan lifeSpan) : this(new Player(null, new Vector(), 0, 0, new Vector()), attack, radius, range, _maxAmmo)
        {
            this._context = context;
            this._name = name;
            this._place = CreateRandomPosition();
            this._lifeSpan = lifeSpan;
            this._creationDate = DateTime.UtcNow;
            _life = true;
        }

        public void Shoot(Vector playerPosition, Vector mousePlace)
        {
            // Player - Context - Monsters -> If X or Y of mousePlace (direction of bullet) is touching the bounding box of an enemy, he looses life
            // Position de départ et d'arrivée de la balle, vitesse / quand est-ce que j'ai tiré
            // Faire la différence entre le moment où la balle a été tirée et le temps qui s'est écoulé
            // Supprimer la balle après un certain temps

            Shoot shoot = new Shoot(1f, TimeSpan.FromSeconds(5), 0.005f, playerPosition, mousePlace);
            _context.ListBullet.Add(shoot);

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

        internal float GetNextRandomFloat()
        {
            return ((float)this.random.NextDouble() * 2) - 1;
        }

        Vector CreateRandomPosition()
        {
            return new Vector(GetNextRandomFloat(), GetNextRandomFloat());
        }

        public void Update()
        {
            if (_context.ListBullet.Count > 0)
            {
                foreach (Shoot s in _context.ListBullet)
                {
                    BulletMove(s, s.SpeedBullet);
                }
            }
            count = 0;
            foreach (Shoot s in _context.ListBullet)
            {
                count++;
                if (!s.IsAlive || s.LifeBullet == false)
                {
                    toRemove.Add(s);
                    _listCount.Add(count);
                }
            }

            foreach(int count in _listCount)
            {
               _context.ListBullet.RemoveAt(count-1);
            }
            _listCount.Clear();
            foreach (Shoot s in toRemove) _bullets.Remove(s);

        }

        public void WalkOn(Round Ctx)
        {
            int count = 0;
            foreach (var item in Ctx.Player.Weapons)
            {
                count++;
                if(item.Name == this.Name)
                {
                    verifWeaponInList = true;
                }
            }

            if (verifWeaponInList == false)
            {
                this.LifeSpan = TimeSpan.FromSeconds(30);
                this.CreationDate = DateTime.UtcNow;
                Ctx.Player.Weapons.Add(this);
                Ctx.Player.CurrentWeapon = this;
            }
            else
            {
                Ctx.Player.Weapons[count-1].LifeSpan = TimeSpan.FromSeconds(30);
                Ctx.Player.Weapons[count-1].CreationDate = DateTime.UtcNow;
            }
            Ctx.Player.BlockWeapon = true;
            Ctx.StuffList.Remove(this);

        }

        public List<Shoot> Bullets => _bullets;

        public string Name => _name;
        public float Attack => _attack;

        public Vector Position => _place;

        public uint Ammo => this._ammo;
        public uint MaxAmmo => this._maxAmmo;
        public TimeSpan LifeSpan
        {
            get { return _lifeSpan; }
            set { _lifeSpan = value; }
        }

        public bool IsAlive
        {
            get
            {
                TimeSpan span = DateTime.UtcNow - _creationDate;
                return span < _lifeSpan;
            }

            set
            {
                _life = false;
            }
        }

        public DateTime CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }
        public bool Life
        {
            get { return this._life ; }
            set { this._life = value ; }
        }
    }

    public class WeaponFactory : IStuffFactory
    {
        readonly string _name;
        readonly float _attack;
        readonly float _radius;
        readonly float _range;
        readonly uint _maxAmmo;
        readonly TimeSpan _lifeSpan;
        readonly DateTime _creationDate;

        Round _roundCtx;

        public WeaponFactory(Round roundCtx, string name, float attack, float radius, float range, uint maxAmmo, TimeSpan lifeSpan)
        {
            _roundCtx = roundCtx;

            _name = name;
            _attack = attack;
            _radius = radius;
            _range = range;
            _maxAmmo = maxAmmo;
            _lifeSpan = lifeSpan;
        }

        public IStuff Create()
        {
            return new Weapon(_roundCtx,_name, _attack, _radius, _range, _maxAmmo,_lifeSpan);
        }


    }
}
