using System;

namespace MiamiOps
{
    public class Round
    {
        private Player _player;
        private Enemies[] _enemies;
        private Random random = new Random();

        public Round(int nb_enemies, Vector? playerSpawn=null, Vector? enemieSpawn=null, float? playerSpeed=null)
        {
            Vector player = playerSpawn ?? new Vector(GetNextRandomFloat(), GetNextRandomFloat());

            if (nb_enemies < 0) throw new ArgumentException("The number of enemies can't be null or negative.", nameof(nb_enemies));
            if (
                player.X < -1 ||
                player.X > 1 ||
                player.Y < -1 ||
                player.Y > 1
            ) throw new ArgumentException("The spawn loaction of enemies or the place of the player can't be out of the map (map (x ; y) coordonate: [-1 ~ 1; -1 ~ 1])");

            // Create the player and the array of enemies
            this._player = new Player(this, player, speed: playerSpeed);
            this._enemies = new Enemies[nb_enemies];
            // Put enemies in the array
            // If the enemies spawn is null (not renseigned) each enemies have a random location
            if (enemieSpawn == null) 
            {
                for (int idx = 0; idx < nb_enemies; idx += 1)
                {
                    this._enemies[idx] = new Enemies(this, idx, new Vector(GetNextRandomFloat(), GetNextRandomFloat()));
                }
            }
            else
            {
                if (
                    enemieSpawn.Value.X < -1 ||
                    enemieSpawn.Value.X > 1 ||
                    enemieSpawn.Value.Y < -1 ||
                    enemieSpawn.Value.Y > 1
                ) throw new ArgumentException("The spawn loaction of enemies or the place of the player can't be out of the map (map (x ; y) coordonate: [-1 ~ 1; -1 ~ 1])");
                // If the enemies spawn is not null each enemies have the location renseigned
                Vector spawn = new Vector(enemieSpawn.Value.X, enemieSpawn.Value.Y);
                for (int idx = 0; idx < nb_enemies; idx += 1) {this._enemies[idx] = new Enemies(this, idx, spawn);}
            }
        }

        private float GetNextRandomFloat()
        {
            return ((float)this.random.NextDouble() * 2) -1;

        }

        // Method to update the player and all the enemies
        public void Update() {foreach (Enemies enemy in this._enemies) enemy.Move(this._player.Place);}

        public Enemies[] Enemies => this._enemies;
        public Player Player => this._player;
    }
}
