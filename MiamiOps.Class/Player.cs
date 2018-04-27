using System;
using System.Threading;



namespace MiamiOps
{
    public class Player
    {
        Weapon _weapon;
        Round _context;
        Vector _place;
        Vector _oldPlace;
        float _life;
        float _speed;
        bool _collide;
        public Player(Round context, Vector place, float life, float speed)
        {
            this._context = context;
            this._place = place;
            this._life = life;
            this._speed = speed;
            _collide = false;
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
        }

        // When the player attacks the enemies
        public void Damage(float attack, float distance)
        {
            throw new NotImplementedException();
        }

        public Vector Place {
            get { return this._place; }
            set { this._place = value; }
        }

        public Vector OldPlace
        {
            get { return this._oldPlace; }
            set { this._oldPlace = value; }
        }

        public bool Collide
        {
            get { return this._collide; }
            set { this._collide = value; }
        }

    }
}
