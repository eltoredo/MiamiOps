using System;

namespace MiamiOps
{
    public class Player
    {
        Round _context;

        Vector _place;
        float _life;
        float _speed;
        Weapon _weapon;
        Weapon default_weapon = new Weapon();
        public Player(Round context, Vector place, float life = 1, float speed = .05f)
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

        // Method to handle the player's movements
        public void Move(Vector direction)
        {
            // Builds a unit vector in the direction where the player will go
            double diviseur = direction.Magnitude;
            if (direction.Magnitude == 0) diviseur = 1;    // In case if the player is in (0, 0), the magnitude is 0 and we can't divide by 0
            Vector unit_vector = direction.Mul(1.0 / diviseur);
            // The vector of the movements
            Vector move = unit_vector.Mul(this._speed);
            // Changes the position of the player
            this._place = this._place.Add(move);

            // Checks if the player doesn't go out of the map
            if (this._place.X > 1) this._place.X = 1;
            if (this._place.Y > 1) this._place.Y = 1;
            if (this._place.X < -1) this._place.X = -1;
            if (this._place.Y < -1) this._place.Y = -1;
        }

        // When the player attacks the enemies
        public void Damage(float attack, float distance)
        {
            throw new NotImplementedException();
        }

        public Vector Place => this._place;
    }
}
