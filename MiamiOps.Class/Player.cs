using System;
using System.Collections.Generic;
using System.Threading;



namespace MiamiOps
{
    public class Player
    {
        List<Weapon> _weapons;
        Round _context;
        Vector _place;
        Vector _oldPlace;
        float _life;
        float _speed;
        Vector _direction;
        Weapon _currentWeapon;

        public Player(Round context, Vector place, float life, float speed, Vector direction)
        {
            this._context = context;
            this._place = place;
            this._life = life;
            this._speed = speed;
            this._direction = direction;
            this._weapons = new List<Weapon>();
        }

        public Player(List<Weapon> weapons, Round context, Vector place, float life, float speed, Vector direction) : this(context, place, life, speed, direction)
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

            
            //Thread.Sleep(85);
            //Console.WriteLine("x_place : " + this._place.X + "  y_place : " + this._place.Y);
            //Console.WriteLine("x_oldPlace : " + this._oldPlace.X + "  y_oldPlace : " + this._oldPlace.Y);


            // Checks if the player doesn't go out of the map
            if (this._place.X > 1) this._place = new Vector(1, this._place.Y);
            if (this._place.Y > 1) this._place = new Vector(this._place.X, 1);
            if (this._place.X < -1) this._place = new Vector(-1, this._place.Y);
            if (this._place.Y < -1) this._place = new Vector(this._place.X, -1);

            this._direction = direction;
        }

        // When the player attacks the enemies
        public void Damage(float attack, float distance)
        {
            throw new NotImplementedException();
        }

        public void GetNewWeapon(Weapon weapon)
        {
            this._weapons.Add(weapon);
            this._currentWeapon = this._weapons[this.Weapons.Count - 1];
        }

        public void ChangeWeapon(int shift)
        {
            if (this._weapons.Count != 0)
            {
                // Dans le cas ou le player n'as pas encore d'arme
                if (this._currentWeapon == null) {this._currentWeapon = this._weapons.OtherElem(this._weapons[0], shift);}
                this._currentWeapon = this._weapons.OtherElem(this._currentWeapon, shift);
            }
        }

        public Vector Direction => this._direction;
        public List<Weapon> Weapons => this._weapons;
        public Weapon CurrentWeapon => this._currentWeapon;
        public Vector Place {
            get { return this._place; }
            set { this._place = value; }
        }

    }
}
