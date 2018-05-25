﻿using System;

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
        float _width;
        float _height;
        bool _isDead;

        public Enemies(Round context, int name, Vector place, float life, float speed, float attack,float width=0, float height=0)
        {
            this._context = context;
            this._name = name;
            this._place = place;
            this._life = life;
            this._speed = speed;
            this._attack = attack;
            this._isDead = false;
            this._height = height;
            this._width = width;
        }

        // Method called when an enemy has less than 1 life point
        internal void Dead()
        {
            this._isDead = true;
            // We put a new enemy in place of the old one
            this._context.Enemies[this._name] = new Enemies(this._context, this._name, new Vector(this._context.GetNextRandomFloat(), this._context.GetNextRandomFloat()), _context.EnemiesLife, _context.EnemiesSpeed, _context.EnemiesAttack);
            _context.Player.Experience += 10;
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
            (bool, Vector) CanMoveInformation = CanMove(target);
            if (CanMoveInformation.Item1)
            {
                this._place = CanMoveInformation.Item2;
            }
        }
        
        private (bool, Vector) CanMove(Vector target)
        {
            bool canMove = true;

            Vector nextPlace = SimulateMove(target);

            // Check the next place of the enemie (don't in a wall or out of the map)
            if (Math.Round(nextPlace.X + this._width, 2) > 1 || Math.Round(nextPlace.Y, 2) > 1 || Math.Round(nextPlace.X, 2) < -1 || Math.Round(nextPlace.Y - this._height, 2) < -1)
            {
                canMove = false;
            }

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

        private Vector SimulateMove(Vector target)
        {
            // Builds a vector in the direction of the enemie
            Vector direction = target - this._place;
            // Builds a unit vector in the direction of the enemie
            double diviseur = direction.Magnitude;
            if (direction.Magnitude == 0) diviseur = 1;    // In case if the enemie is in (0, 0) the magnitude is 0 and we can't devided by 0
            Vector unit_vector = direction * (1.0 / diviseur);
            Vector move = unit_vector * this._speed;
            Vector enemiePlace = this._place + move;
            return enemiePlace;
        }

        public void Attack(float attack, float distance)
        {
            throw new NotImplementedException();
        }

        public double Life => this._life;
        public bool IsDead => this._isDead;
        public Vector Place => this._place;
    }
}
