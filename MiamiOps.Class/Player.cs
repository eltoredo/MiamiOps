using System;

namespace MiamiOps.Class
{
    class Player
    {
        Round _context;
        Vector _place;
        float _life;
        float _speed;
        Weapon _weapon;
        Weapon default_weapon = new Weapon();
        public Player(Round context, Vector place, float life = 1, float speed = .1f)
        {
            this._context = context;
            this._place = place;
            this._life = life;
            this._speed = speed;

        }
        public Player(Weapon weapon, Round context, Vector place, float life = 1, float speed = .1f) : this(context, place, life, speed)
        {
            this._weapon = weapon;
        }

        // To move the player; direction is the direction of player
        public void Move(Vector direction)
        {
            this._place.Add(direction);
        }

        // When the player attack the enemies
        public void Damage(float attack, float distance)
        {
            throw new NotImplementedException();
        }

        public Vector Place => this._place;
    }
}
