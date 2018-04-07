using System;
using MiamiOps;
using NUnit.Framework;
using SFML.System;


namespace MiamiOpsEnemies.Tests
{
    [TestFixture]
    public class EnemiesTests
    {
        [Test]
        public void Creating_enemy_and_he_get_hit()
        {
            Enemies enemi = new Enemies(null, 0, new Vector2f(0, 0));
            enemi.Hit(50);
            Assert.That( enemi.Life, Is.EqualTo(50) );
        }

    }
}
