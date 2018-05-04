using System;
using System.Collections.Generic;
using System.Linq;

namespace MiamiOps
{
    public class Round
    {
        private Player _player;
        private Enemies[] _enemies;
        private float _enemiesLife;
        private float _enemiesSpeed;
        private float _enemiesAttack;
        private Random random = new Random();
        private List<float[]> _obstacles;
        private float _enemiesLargeur;
        private float _enemiesHauteur;
        private int _count;
        private Dictionary<int, float[]> _spawn;


        public Round(
            int nb_enemies,
            Dictionary<int, float[]> enemySpawn,
            Vector? playerSpawn = null, Vector? enemieSpawn = null,
            float enemiesLife = .1f, float enemiesSpeed = .05f, float enemiesAttack = .75f,
            float playerLife = 1, float playerSpeed = .1f, Vector? playerDirection = null,
            float playerLargeur = 0, float playerHauteur = 0,
            float enemiesLargeur = 0, float enemiesHauteur = 0 
        )

        {
            Vector player = playerSpawn ?? new Vector(-0.7, -0.7);

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
            this._enemiesLargeur = enemiesLargeur;
            this._enemiesHauteur = enemiesHauteur;
            this._spawn = enemySpawn;
            this._count = this._spawn.Count - 1;
            // Create the player and the array of enemies
            Vector playerDir = playerDirection ?? new Vector(1, 0);
            this._player = new Player(this, player, playerLife, playerSpeed, playerDir, playerLargeur, playerHauteur);
            this._enemies = new Enemies[nb_enemies];
            // If the enemies spawn is null (not renseigned) each enemies have a random location
            Func<Vector> createPositionv1;    // This variable is type "Func" and that return a "Vector"
            if( enemieSpawn == null) createPositionv1 = createPositionV2;
            else createPositionv1 = () => enemieSpawn.Value;
            // Put enemies in the array
            for (int idx = 0; idx < nb_enemies; idx += 1) {this._enemies[idx] = new Enemies(this, idx, createPositionv1(), this._enemiesLife, this._enemiesSpeed, this._enemiesAttack, this._enemiesLargeur, this._enemiesHauteur);}

            this._obstacles = new List<float[]>();
            
        }

        internal float GetNextRandomFloat()
        {
            return ((float)this.random.NextDouble() * 2) -1;
        }

        Vector createPositionV2()
        {
            Vector Position = new Vector(this._spawn.Values.ElementAt(_count).ElementAt(0), this._spawn.Values.ElementAt(_count).ElementAt(1));
           this._count--;
            if(_count < 0)
            {
                this._count = _spawn.Count -1;
            }
            return Position;
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

        public void AddObstacle(float x, float y, float largeur, float hauteur)
        {
            this._obstacles.Add(new float[]{x, y, largeur, hauteur});
        }
        
        public Enemies[] Enemies => this._enemies;
        public float EnemiesLife => _enemiesLife;
        public float EnemiesSpeed => _enemiesSpeed;
        public float EnemiesAttack => _enemiesAttack;
        public Player Player => this._player;
        public List<float[]> Obstacles => this._obstacles;
    }
}
