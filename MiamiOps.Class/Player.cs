using System;
using System.Collections.Generic;
namespace MiamiOps
{
    public class Player
    {
        SkillsTree _skillsTree;

        List<Weapon> _weapons;
        Round _context;
        Vector _place;
        Vector _oldPlace;
        float _life;
        float _lifeMax;
        float _speed;
        float _width;
        float _height;
        int _level;
        float _points;
        float _experience;
        float _experienceMax;
        Vector _direction;
        Weapon _currentWeapon;

        public Player(Round context, Vector place, float life, float speed, Vector direction, float width=0 , float height=0)
        {
            this._context = context;

            _skillsTree = new SkillsTree(context);

            this._place = place;
            this._life = life;
            this._lifeMax = life;
            this._speed = speed;
            this._direction = direction;
            this._weapons = new List<Weapon>();
            this._height = height;
            this._width = width;
            this._level = 1;
            this._points = 0;
            this._experience = 0;
            this._experienceMax = 100;
        }

        public Player(List<Weapon> weapons, Round context, Vector place, float life, float speed, Vector direction, float width = 0, float height = 0) : this(context, place, life, speed, direction,width,height)
        {
            this._weapons = weapons;
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

        public bool LevelUp()
        {
            if (_experience == _experienceMax)
            {
                this._level++;
                _experienceMax += _level * 100;
                _experience = 0;
                return true;
            }
            else return false;
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

        public void Update()
        {
            LevelUp();
            _skillsTree.Update();
        }

        public float Points
        {
            get { return _points; }
            set { _points = value; }
        }
        public int Level => _level;
        public float Experience
        {
            get { return _experience; }
            set { _experience = value; }
        }

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector Place
        {
            get { return this._place; }
        }

        public float LifePlayer
        {
            get { return this._life; }
            set { this._life = value; }
        }
        public float LifePlayerMax {
            get { return this._lifeMax; }
            set { this._lifeMax = value; }
        }


        public float ExperienceMax => this._experienceMax;
        public Vector Direction => this._direction;
        public List<Weapon> Weapons => this._weapons;
        public Weapon CurrentWeapon => this._currentWeapon;
       



    }
}
