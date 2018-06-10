using System;
using System.Collections.Generic;
//using System.Linq;

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
        private List<float[]> _obstacles;
        private float _enemiesLargeur;
        private float _enemiesHauteur;
        private int _count;
        private Dictionary<int, Vector> _spawn;
        private Vector _enemiesSpawn;
        private int _time;
        private int _timeForWeaponSpawn = 299;
        private int _passOut = 0;
        int _countSpawn;
        bool _gameState;

        Random _random;
        private List<IStuffFactory> _stuffFactories;
        private List<IStuff> _stuffList;

        private Dictionary<int, WeaponFactory> _listWeaponFactory = new Dictionary<int, WeaponFactory>();
        
        public Round(
            int nb_enemies,
            Vector? playerSpawn = null, Vector? enemieSpawn = null,
            float enemiesLife = .1f, float enemiesSpeed = .05f, float enemiesAttack = .75f,
            float playerLife = 1, float playerSpeed = .1f, Vector? playerDirection = null,
            float playerLargeur = 0, float playerHauteur = 0,
            float enemiesLargeur = 0, float enemiesHauteur = 0,
            Dictionary<int, Vector> enemySpawn = null
        )
        {
            _countSpawn = 1 ;
            _random = new Random();
            _stuffFactories = new List<IStuffFactory>();
            _stuffList = new List<IStuff>();
            _stuffFactories.Add(new PackageFactory(this, "health", TimeSpan.FromMinutes(2), 1)); // indice de rareté
            _stuffFactories.Add(new WeaponFactory(this, "USP", 0.5f, 0.1f, 0.05f, 30));

            Vector player = playerSpawn ?? new Vector(-0.7, 0.7);

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
           // foreach (float elem in values) {if (elem < 0 || elem > 1) throw new ArgumentException("Somethings is wrong, you can't have value bigger than 1 and lower than 0.");}
            
            // Save the enemies parametres
            this._enemiesLife = enemiesLife;
            this._enemiesSpeed = enemiesSpeed;
            this._enemiesAttack = enemiesAttack;
            this._enemiesLargeur = enemiesLargeur;
            this._enemiesHauteur = enemiesHauteur;
            this._spawn = enemySpawn;
            if (this._spawn == null) {
                this._count = nb_enemies;
            }
            else
            {
                this._count = _spawn.Count;
            }

            // Create the player and the array of enemies
            Vector playerDir = playerDirection ?? new Vector(1, 0);

            this._player = new Player(_weapons, this, player, playerLife, playerSpeed, playerDir,playerLargeur,playerHauteur);
            this._player.GetNewWeapon(new Weapon("shotgun", 0f, 0, 0f, 60));
          //  this._player.GetNewWeapon(new Weapon("shotgun", 0f, 0, 0f, 20));
            this._enemies = new Enemies[nb_enemies];
            // If the enemies spawn is null (not renseigned) each enemies have a random location
            Func<Vector> createPosition;    // This variable is type "Func" and that return a "Vector"
            if (enemieSpawn == null) createPosition = CreateRandomPosition;
            else createPosition = () => CreatePositionOnSpawn(enemieSpawn.Value);            // Put enemies in the array
            for (int idx = 0; idx < nb_enemies; idx += 1) {this._enemies[idx] = new Enemies(this, idx, createPosition(), this._enemiesLife, this._enemiesSpeed, this._enemiesAttack, this._enemiesLargeur, this._enemiesHauteur);}
            if (this._count > this._enemies.Length)
            {
                this._count = this._enemies.Length;
            }
            this._obstacles = new List<float[]>();

         
        }

        internal float GetNextRandomFloat()
        {
            return ((float)this.random.NextDouble() * 2) -1;
        }

      public  Vector CreatePositionOnSpawn(Vector enemieSpawn)
        {
            Vector Position;
            if (_spawn != null)
            {
                 Position = _spawn[_count];
                this._count--;
                if (_count < 1)
                {
                    this._count = _spawn.Count;
                }
                return Position;
            }
            Position = enemieSpawn;
            return Position;
        }

        Vector CreateRandomPosition()
        {
            return new Vector(GetNextRandomFloat(), GetNextRandomFloat());
        }

        // Method to update the player and all the enemies
        public void Update()
        {
            _player.CurrentWeapon.Update();
            _player.Update();
            UpdateList();
            UpdatePackage();

            for (int i = 0 ; i < _count; i++)
            {
                this._enemies[i].Move(this._player.Place);
                
            }

            _time++;
            _timeForWeaponSpawn++;
            if (_time == 120 && this._spawn != null)
            {
                _count += _spawn.Count;
                if (_count > this._enemies.Length)
                {
                    _count = this.Enemies.Length;
                }
            }

            if (_timeForWeaponSpawn == 300)
            {
                int factoryIndex = _random.Next(0, _stuffFactories.Count);
                IStuffFactory randomStuffFactory = _stuffFactories[factoryIndex];
                IStuff stuff = randomStuffFactory.Create();
                _stuffList.Add(stuff);

                _timeForWeaponSpawn = 0;
            }

        }

        public void UpdateList()
        {
          
            if (this.Player.Level == 5&& _passOut == 0)
            {
                this._player.GetNewWeapon(new Weapon("ak47", 0, 0, 0, 30));
                this._stuffFactories.Add(new PackageFactory(this, "speed", TimeSpan.FromMinutes(2), 1));
                _passOut++;
            }else if(this.Player.Level == 10 && _passOut == 1)
            {
                this._player.GetNewWeapon(new Weapon("shotgun", 0, 0, 0, 10));
                this._stuffFactories.Add(new PackageFactory(this, "speed", TimeSpan.FromMinutes(2), 1));
                this._stuffFactories.Add(new WeaponFactory(this, "chaos_blade", 0.5f, 0.1f, 0.05f, 30));
                _passOut++;
            }
            else if (this.Player.Level == 15 && _passOut == 2)
            {
                this._stuffFactories.Add(new PackageFactory(this, "point", TimeSpan.FromMinutes(2), 1));
                this._stuffFactories.Add(new WeaponFactory(this, "soulcalibur", 0.5f, 0.1f, 0.05f, 30));
                _passOut++;
            }

        }

        public void UpdatePackage()
        {
            
            foreach (Weapon weapon in _weapons)
            {
                if (!weapon.IsAlive)
                {
                    if (this.Player.CurrentWeapon == weapon) this.Player.CurrentWeapon = this._weapons[this._weapons.Count - 2];
                    _weapons.Remove(weapon);
                    break;
                }
            }

        }

        public void AddObstacle(float x, float y, float largeur, float hauteur)
        {
            this._obstacles.Add(new float[]{x, y, largeur, hauteur});
        }

        public List<IStuff> StuffList => _stuffList;

        public Enemies[] Enemies
        {
            get { return this._enemies; }
            set { this._enemies = value; }
        }
        public float EnemiesLife => _enemiesLife;
        public float EnemiesSpeed => _enemiesSpeed;
        public float EnemiesAttack => _enemiesAttack;
        public Player Player => this._player;
        public List<float[]> Obstacles => this._obstacles;
        public int SpawnCount => this._spawn.Count;
        public int CountEnnemi
        {
            get { return this._count; }
            set { this._count = value; }
        }
        public int CountSpawnDead
        {
            get { return this._countSpawn; }
            set { this._countSpawn = value; }
        }
        public int Time {
            get { return this._time; }
            set
            {
                this._time = value;
            }
        }
        public bool GameState
        {
            get { return this._gameState; }
            set { this._gameState = value; }
        }

    }
}
