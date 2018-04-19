using System;
using System.Collections.Generic;

namespace MiamiOps
{
    public class Player
    {
        List<Weapon> _weapons;
        Round _context;
        Vector _place;
        float _life;
        float _speed;
        Weapon _currentWeapon;

        public Player(Round context, Vector place, float life, float speed)
        {
            this._context = context;
            this._place = place;
            this._life = life;
            this._speed = speed;
            this._weapons = new List<Weapon>();
        }

        public Player(List<Weapon> weapons, Round context, Vector place, float life = 1, float speed = .1f) : this(context, place, life, speed)
        {
            this._weapons = weapons;
            //this._currentWeapon = this._weapons[0];
        }

        // Method to handle the player's movements
        public void Move(Vector direction)
        {
            // Builds a unit vector in the direction where the player will go
            double diviseur = direction.Magnitude;
            if (direction.Magnitude == 0) diviseur = 1;    // In case if the player is in (0, 0), the magnitude is 0 and we can't divide by 0
            Vector unit_vector = direction * (1.0 / diviseur);
            // The vector of the movements
            Vector move = unit_vector * this._speed;
            // Changes the position of the player
            this._place += move;

            // Checks if the player doesn't go out of the map
            if (this._place.X > 1) this._place = new Vector(1, this._place.Y);
            if (this._place.Y > 1) this._place = new Vector(this._place.X, 1);
            if (this._place.X < -1) this._place = new Vector(-1, this._place.Y);
            if (this._place.Y < -1) this._place = new Vector(this._place.X, -1);
        }

        // When the player attacks the enemies
        public void Damage(float attack, float distance)
        {
            throw new NotImplementedException();
        }

        public void GetNewWeapon(Weapon weapon)
        {
            this._weapons.Add(weapon);
        }

        public void ChangeWeapon(int shift)
        {
            if (this._weapons.Count != 0)
            {
                if (this._currentWeapon == null) {this._currentWeapon = this._weapons.OtherElem(this._weapons[0], shift);}
                this._currentWeapon = this._weapons.OtherElem(this._currentWeapon, shift);
            }
        }

        public Vector Place => this._place;
        public List<Weapon> Weapons => this._weapons;
        public Weapon CurrentWeapon => this._currentWeapon;
    }
}
