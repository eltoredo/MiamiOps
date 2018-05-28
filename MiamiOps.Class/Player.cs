using System;
using System.Collections.Generic;
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
        float _width;
        float _height;
        Vector _direction;
        Weapon _currentWeapon;

        public Player(Round context, Vector place, float life, float speed, Vector direction, float width=0 , float height=0)
        {
            this._context = context;
            this._place = place;
            this._life = life;
            this._speed = speed;
            this._direction = direction;
            this._weapons = new List<Weapon>();
            this._height = height;
            this._width = width;
        }

        public Player(List<Weapon> weapons, Round context, Vector place, float life, float speed, Vector direction, float width = 0, float height = 0) : this(context, place, life, speed, direction,width,height)
        {
            this._weapons = weapons;
            if (context != null)
            {
                _currentWeapon = new Weapon("Gun", 0.5f, 0.1f, 0.05f, 30);
                GetNewWeapon(_currentWeapon);
            }
        }

        // Method to handle the player's movements
        public void Move(Vector direction)
        {
            (bool, Vector) CanMoveInformation = CanMove(direction);
            if (CanMoveInformation.Item1) {this._place = CanMoveInformation.Item2;}
        }

        // When the player attacks the enemies
        public void Attack(Vector mousePlace)
        {
            _currentWeapon.Shoot(this.Place, mousePlace);
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

        private (bool, Vector) CanMove(Vector direction)
        {
            bool canMove = true;

            Vector nextPlace = SimulationMove(direction);

            // Checks if the player doesn't go out of the map
            if (Math.Round(nextPlace.X + this._width, 2) > 1 || Math.Round(nextPlace.Y, 2) > 1 || Math.Round(nextPlace.X, 2) < -1 || Math.Round(nextPlace.Y - this._height, 2) < -1)
            {
                canMove = false;
            }

            // Checks if the player don't go in a wall
            foreach (float[] wall in this._context.Obstacles)
            {
                if (
                    Math.Round(nextPlace.Y - this._height, 2) < wall[1] && wall[1] - wall[3] < Math.Round(nextPlace.Y, 2) && 
                    Math.Round(nextPlace.X, 2) < wall[0] + wall[2] && Math.Round(nextPlace.X + this._width, 2) > wall[0]
                )
                {
                    canMove = false;
                }
            }
            
            return (canMove, nextPlace);
        }

        private Vector SimulationMove(Vector direction)
        {
            double diviseur = direction.Magnitude;
            if (direction.Magnitude == 0) diviseur = 1;
            Vector unit_vector = direction * (1.0 / diviseur);
            Vector move = unit_vector * this._speed;
            Vector playerPlace = this._place + move;
            return playerPlace;
        }

        public Vector Direction => this._direction;
        public List<Weapon> Weapons => this._weapons;
        public Weapon CurrentWeapon => this._currentWeapon;
        public Vector Place
        {
            get { return this._place; }
        }
    }
}
