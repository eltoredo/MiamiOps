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

        // Method called when an enemy has less than 1 life point
        internal void Dead()
        {
            this._isDead = true;
        }

        // When a enemy is touched by the player, he loses some life point
        public void Hit(float pv)
        {
             this._life -= pv;
             if (this._life <= 0)
             {
                 this.Dead();
             }
        }

        // The movements of the enemy
        public void Move(Vector target)
        {
            // Builds a vector in the direction of the player
            Vector direction = target - this._place;
            // Builds a unit vector in the direction of the player
            double diviseur = direction.Magnitude;
            if (direction.Magnitude == 0) diviseur = 1;    // In case if the player is in (0, 0) the magnitude is 0 and we can't devided by 0
            Vector unit_vector = direction * (1.0 / diviseur);
            Vector move = unit_vector * this._speed;
            // Changes the position of the enemy
            this._place += move;
        }

        public void Attack(float attack, float distance)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            Hit(.01f);
        }

        public double Life => this._life;
        public bool IsDead => this._isDead;
        public Vector Place => this._place;
    }
}
