﻿using System;

namespace MiamiOps
{
    public class Player
    {
        Weapon _weapon;
        Round _context;
        Vector _place;
        float _life;
        float _speed;
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
            //Build a unit vector in the direction where to go
            double diviseur = direction.Magnitude;
            if (direction.Magnitude == 0) diviseur = 1;    // In case if the player is in (0, 0) the magnitude is 0 and we can't devided by 0
            Vector unit_vector = direction * (1.0 / diviseur);
            // The vector of the move
            Vector move = unit_vector * this._speed;
            // Change the position of the enemy
            this._place += move;

            // Check if the player don't go out of the map
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

        public Vector Place => this._place;
    }
}
