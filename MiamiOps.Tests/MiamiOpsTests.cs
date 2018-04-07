using System;

using NUnit.Framework;

using MiamiOps.Class;

namespace MiamiOps.Tests
{
    [TestFixture]
    public class MiamiOpsTests
    {
        [TestCase(-4)]
        [TestCase(-25)]
        public void Create_round_check_how_may_enemies_is_in_the_round(int nb_of_enemies)
        {
            Assert.Throws<ArgumentException>( () => new Round(nb_of_enemies, new Vector(0, 0), new Vector(0, 0)));
        }

        [Test]
        public void Create_round_with_enemies_spawn_out_of_the_map()
        {
            Vector[] vectors = new Vector[8]{new Vector(0, 2), new Vector(0, 2), new Vector(0, 2), new Vector(0, 2), new Vector(0, 2), new Vector(0, 2), new Vector(0, 2), new Vector(0, 2)};
            foreach (Vector spawnLocation in vectors) Assert.Throws<ArgumentException>( () => new Round(10, spawnLocation, new Vector(0, 0)));
        }
        public void Create_round_with_player_place_out_of_map()
        {
            Vector[] vectors = new Vector[8]{new Vector(0, 2), new Vector(0, 2), new Vector(0, 2), new Vector(0, 2), new Vector(0, 2), new Vector(0, 2), new Vector(0, 2), new Vector(0, 2)};
            foreach (Vector spawnLocation in vectors) Assert.Throws<ArgumentException>( () => new Round(10, new Vector(0, 0), spawnLocation));
        }

        [Test]
        public void Look_if_enemies_go_to_the_player__go_down()
        {
            Round play = new Round(1, new Vector(0, 1), new Vector(0, 0));
            Assert.That(Math.Round(play.Enemies[0].Place.Y, 2), Is.EqualTo(Math.Round(1.0, 2)));
            play.Update(new Vector(0, 0));
            Assert.That(Math.Round(play.Enemies[0].Place.Y, 2), Is.EqualTo(Math.Round(.95, 2)));
            play.Update(new Vector(0, 0));
            Assert.That(Math.Round(play.Enemies[0].Place.Y, 2), Is.EqualTo(Math.Round(.90f, 2)));
            play.Update(new Vector(0, 0));
            play.Update(new Vector(0, 0));
            Assert.That(Math.Round(play.Enemies[0].Place.Y, 2), Is.EqualTo(Math.Round(.80f, 2)));
            for (int idx = 0; idx < 8; idx += 1)
            {
                play.Update(new Vector(0, 0));
                play.Update(new Vector(0, 0));
            }
            Assert.That(Math.Round(play.Enemies[0].Place.Y, 2), Is.EqualTo(Math.Round(0.0, 2)));
        }
    }
}
