using System;
using System.Collections.Generic;

namespace MiamiOps
{
    public class Round
    {
        private Player _player;
        private Enemies[] _enemies;
        private List<Weapon> _weapons = new List<Weapon>();
        private float _enemiesLife;
        private float _enemiesSpeed;
        private float _enemiesAttack;
        private Random random = new Random();

        public Round(int nb_enemies, Vector? playerSpawn = null, Vector? enemieSpawn = null, float enemiesLife = .1f, float enemiesSpeed = .05f, float enemiesAttack = .75f, float playerLife = 1, float playerSpeed = .1f, Vector? playerDirection = null)
        {
            Vector player = playerSpawn ?? new Vector(GetNextRandomFloat(), GetNextRandomFloat());

            if (nb_enemies < 0) throw new ArgumentException("The number of enemies can't be null or negative.", nameof(nb_enemies));
            if (
                player.X < -1 ||
                player.X > 1 ||
                player.Y < -1 ||
                player.Y > 1
            ) throw new ArgumentException("The spawn loaction of enemies or the place of the player can't be out of the map (map (x ; y) coordonate: [-1 ~ 1; -1 ~ 1])");
            if (
                enemieSpawn != null && (
                    enemieSpawn.Value.X < -1 ||
                    enemieSpawn.Value.X > 1 ||
                    enemieSpawn.Value.Y < -1 ||
                    enemieSpawn.Value.Y > 1
                )
            ) throw new ArgumentException("The spawn loaction of enemies or the place of the player can't be out of the map (map (x ; y) coordonate: [-1 ~ 1; -1 ~ 1])");

            float[] values = new float[5]{enemiesLife, enemiesSpeed, enemiesAttack, playerLife, playerSpeed};
            foreach (float elem in values) {if (elem < 0 || elem > 1) throw new ArgumentException("Somethings is wrong, you can't have value bigger than 1 and lower than 0.");}
            
            // Save the enemies parametres
            this._enemiesLife = enemiesLife;
            this._enemiesSpeed = enemiesSpeed;
            this._enemiesAttack = enemiesAttack;
            // Create the player and the array of enemies
            Vector playerDir = playerDirection ?? new Vector(1, 0);
            this._player = new Player(_weapons, this, player, playerLife, playerSpeed, playerDir);
            this._enemies = new Enemies[nb_enemies];
            // If the enemies spawn is null (not renseigned) each enemies have a random location
            Func<Vector> createPosition;    // This variable is type "Func" and that return a "Vector"
            if( enemieSpawn == null) createPosition = CreateRandomPosition;
            else createPosition = () => enemieSpawn.Value;
            // Put enemies in the array
            for (int idx = 0; idx < nb_enemies; idx += 1) {this._enemies[idx] = new Enemies(this, idx, createPosition(), this._enemiesLife, this._enemiesSpeed, this._enemiesAttack);}
        }

        internal float GetNextRandomFloat()
        {
            return ((float)this.random.NextDouble() * 2) -1;
        }

        Vector CreateRandomPosition()
        {
            return new Vector(GetNextRandomFloat(), GetNextRandomFloat());
        }

        // Method to update the player and all the enemies
        public void Update()
        {
            foreach (Enemies enemy in this._enemies) enemy.Move(this._player.Place);
        }

        public Enemies[] Enemies => this._enemies;
        public float EnemiesLife => _enemiesLife;
        public float EnemiesSpeed => _enemiesSpeed;
        public float EnemiesAttack => _enemiesAttack;

        public Player Player => this._player;
    }
}
