using System;

namespace MiamiOps
{
    public class Round
    {
        private Player _player;
        private Enemies[] _enemies;
        public Round(int nb_enemies, Vector enemiesSpawn, Vector playerPlace)
        {
            if (nb_enemies < 0) throw new ArgumentException("The number of enemies can't be null or negative.", nameof(nb_enemies));
            Vector[] vectors = new Vector[2]{enemiesSpawn, playerPlace};
            foreach (Vector vector in vectors)
            {
                if (
                    vector.X < -1 ||
                    vector.X > 1 ||
                    vector.Y < -1 ||
                    vector.Y > 1
                ) throw new ArgumentException("The spawn loaction of enemies or the place of the player can't be out of the map (map (x ; y) coordonate: [-1 ~ 1; -1 ~ 1])");
            }

            // Create the player and the array of enemies
            this._player = new Player(this, playerPlace);
            this._enemies = new Enemies[nb_enemies];
            // Put enemies in the array
            for (int idx = 0; idx < nb_enemies; idx += 1) {this._enemies[idx] = new Enemies(this, idx, enemiesSpawn);}
        }

        // Method to update the player and all the enemies
        public void Update() {foreach (Enemies enemy in this._enemies) enemy.Move(this._player.Place);}

        public Enemies[] Enemies => this._enemies;
        public Player Player => this._player;
    }
}
