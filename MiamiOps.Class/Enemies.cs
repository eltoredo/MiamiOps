using System;

namespace MiamiOps
{
    public class Enemies
    {
        Round _context;
        readonly int _name;
        Vector _place;
        float _life;
        float _speed;
        float _attack;
        bool _isDead;

        public Enemies(Round context, int name, Vector place, float life = .1f, float speed = .05f, float attack = .75f)
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
        public void Hit(float pv)
        {
             this._life -= pv;
             if (this._life <= 0)
             {
                 this.Dead();
             }
        }

        // The move of enemy
        public void Move(Vector target)
        {
            // Buld a vector in the direction of the player
            Vector direction = target.Sub(this._place);
            //Buld a unit vector in the direction of the player
            Vector unit_vector = direction.Mul(1.0 / direction.Magnitude);
            // The vector of the move
            Vector move = unit_vector.Mul(this._speed);
            // Change the position of the enemy
            this._place = this._place.Add(move);
        }

        public void Attack(float attack, float distance)
        {
            throw new NotImplementedException();
        }

        public double Life => this._life;
        public Vector Place => this._place;
    }
}
