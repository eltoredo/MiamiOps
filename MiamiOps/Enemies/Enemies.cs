using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace MiamiOps
{
    public class Enemies
    {
        Game _context;
        readonly int _name;
        Vector2f _place;
        double _life;
        float _speed;
        double _attack;
        bool _isDead;
        public Enemies(Game context, int name, Vector2f place, double life = 100, float speed = 100, double attack = 100)
        {
            this._context = context;
            this._name = name;
            this._place = place;
            this._life = life;
            this._speed = speed;
            this._attack = attack;
            this._isDead = false;
        }

        // Function called when an enemy have less tran 1 life point
        internal void Dead()
        {
            this._isDead = true;
        }

        // When a enemi is touched by the player he loose life point
        public void Hit(double pv)
        {
             this._life -= pv;
             if (this._life <= 0)
             {
                 this.Dead();
             }
        }

        // The move of enemy
        internal void Move(Vector2f target)
        {
            // Buld a vector in the direction of the player
            Vector2f direction = this._place - target;
            //Buld a unit vector int the direction of the player
            float unit_vectorX = direction.X * (1/direction.X);
            float unit_vectorY = direction.Y * (1/direction.Y);
            Vector2f unit_vector = new Vector2f(unit_vectorX, unit_vectorY);
            // Change the position of the enemy
            this._place.X += unit_vector.X * this._speed;
            this._place.Y += unit_vector.Y * this._speed;
        }

        public double Life
        {
            get {return this._life;}
        }
    }
}
