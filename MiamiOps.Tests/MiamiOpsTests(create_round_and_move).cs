using System;

using NUnit.Framework;


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
            Vector[] vectors = new Vector[4]{new Vector(0, 2), new Vector(0, -2), new Vector(2, 0), new Vector(-2, 0)};
            foreach (Vector spawnLocation in vectors) Assert.Throws<ArgumentException>( () => new Round(10, spawnLocation, new Vector(0, 0)));
        }

        [Test]
        public void Create_round_with_player_place_out_of_map()
        {
            Vector[] vectors = new Vector[4]{new Vector(0, 2), new Vector(0, -2), new Vector(2, 0), new Vector(-2, 0)};
            foreach (Vector spawnLocation in vectors) Assert.Throws<ArgumentException>( () => new Round(10, new Vector(0, 0), spawnLocation));
        }

        [Test]
        public void Look_if_enemies_go_to_the_player__go_down()
        {
            Round play = new Round(1, new Vector(0, 1), new Vector(0, 0));
            Assert.That(Math.Round(play.Enemies[0].Place.Y, 2), Is.EqualTo(Math.Round(1.0, 2)));
            play.Update();
            Assert.That(Math.Round(play.Enemies[0].Place.Y, 2), Is.EqualTo(Math.Round(.95, 2)));
            play.Update();
            Assert.That(Math.Round(play.Enemies[0].Place.Y, 2), Is.EqualTo(Math.Round(.90f, 2)));
            play.Update();
            play.Update();
            Assert.That(Math.Round(play.Enemies[0].Place.Y, 2), Is.EqualTo(Math.Round(.80f, 2)));
            for (int idx = 0; idx < 8; idx += 1)
            {
                play.Update();
                play.Update();
            }
            Assert.That(Math.Round(play.Enemies[0].Place.Y, 2), Is.EqualTo(Math.Round(0.0, 2)));
        }

        [Test]
        public void Look_if_enemies_go_to_the_player__go_up()
        {
            Round play = new Round(1, new Vector(0, -1), new Vector(0, 0));
            Assert.That(Math.Round(play.Enemies[0].Place.Y, 2), Is.EqualTo(Math.Round(-1.0, 2)));
            play.Update();
            Assert.That(Math.Round(play.Enemies[0].Place.Y, 2), Is.EqualTo(Math.Round(-.95, 2)));
            play.Update();
            Assert.That(Math.Round(play.Enemies[0].Place.Y, 2), Is.EqualTo(Math.Round(-.90f, 2)));
            play.Update();
            play.Update();
            Assert.That(Math.Round(play.Enemies[0].Place.Y, 2), Is.EqualTo(Math.Round(-.80f, 2)));
            for (int idx = 0; idx < 8; idx += 1)
            {
                play.Update();
                play.Update();
            }
            Assert.That(Math.Round(play.Enemies[0].Place.Y, 2), Is.EqualTo(Math.Round(-0.0, 2)));
        }

        [Test]
        public void Look_if_enemies_go_to_the_player__go_left()
        {
            Round play = new Round(1, new Vector(1, 0), new Vector(0, 0));
            Assert.That(Math.Round(play.Enemies[0].Place.X, 2), Is.EqualTo(Math.Round(1.0, 2)));
            play.Update();
            Assert.That(Math.Round(play.Enemies[0].Place.X, 2), Is.EqualTo(Math.Round(.95, 2)));
            play.Update();
            Assert.That(Math.Round(play.Enemies[0].Place.X, 2), Is.EqualTo(Math.Round(.90f, 2)));
            play.Update();
            play.Update();
            Assert.That(Math.Round(play.Enemies[0].Place.X, 2), Is.EqualTo(Math.Round(.80f, 2)));
            for (int idx = 0; idx < 8; idx += 1)
            {
                play.Update();
                play.Update();
            }
            Assert.That(Math.Round(play.Enemies[0].Place.X, 2), Is.EqualTo(Math.Round(0.0, 2)));
        }

        [Test]
        public void Look_if_enemies_go_to_the_player__go_rigth()
        {
            Round play = new Round(1, new Vector(-1, 0), new Vector(0, 0));
            Assert.That(Math.Round(play.Enemies[0].Place.X, 2), Is.EqualTo(Math.Round(-1.0, 2)));
            play.Update();
            Assert.That(Math.Round(play.Enemies[0].Place.X, 2), Is.EqualTo(Math.Round(-.95, 2)));
            play.Update();
            Assert.That(Math.Round(play.Enemies[0].Place.X, 2), Is.EqualTo(Math.Round(-.90f, 2)));
            play.Update();
            play.Update();
            Assert.That(Math.Round(play.Enemies[0].Place.X, 2), Is.EqualTo(Math.Round(-.80f, 2)));
            for (int idx = 0; idx < 8; idx += 1)
            {
                play.Update();
                play.Update();
            }
            Assert.That(Math.Round(play.Enemies[0].Place.X, 2), Is.EqualTo(Math.Round(-0.0, 2)));
        }

        [TestCase(0, 1, 0.0, .1f, .2f, .4f, 1.0)]
        [TestCase(0, -1, 0.0, -.1f, -.2f, -.4f, -1)]
        public void Look_if_the_player_move_verticaly(float xDirection, float yDirection, double t0, double t1, double t2, double t3, double t4)
        {
            Round play = new Round(0, new Vector(0, 0), new Vector(0, 0));
            Assert.That(Math.Round(play.Player.Place.Y, 2), Is.EqualTo(Math.Round(t0, 2)));
            play.Player.Move(new Vector(xDirection, yDirection));
            Assert.That(Math.Round(play.Player.Place.Y, 2), Is.EqualTo(Math.Round(t1, 2)));
            play.Player.Move(new Vector(xDirection, yDirection));
            Assert.That(Math.Round(play.Player.Place.Y, 2), Is.EqualTo(Math.Round(t2, 2)));
            play.Player.Move(new Vector(xDirection, yDirection));
            play.Player.Move(new Vector(xDirection, yDirection));
            Assert.That(Math.Round(play.Player.Place.Y, 2), Is.EqualTo(Math.Round(t3, 2)));
            for (int idx = 0; idx < 6; idx += 1)
            {
                play.Player.Move(new Vector(xDirection, yDirection));
            }
            Assert.That(Math.Round(play.Player.Place.Y, 2), Is.EqualTo(Math.Round(t4, 2)));
        }

        [TestCase(-1, 0, -0.0, -.1f, -.2f, -.4f, -1)]
        [TestCase(1, 0, 0.0, .1f, .2f, .4f, 1)]
        public void Look_if_the_player_move_horizontaly(float xDirection, float yDirection, double t0, double t1, double t2, double t3, double t4)
        {
            Round play = new Round(0, new Vector(0, 0), new Vector(0, 0));
            Assert.That(Math.Round(play.Player.Place.X, 2), Is.EqualTo(Math.Round(t0, 2)));
            play.Player.Move(new Vector(xDirection, yDirection));
            Assert.That(Math.Round(play.Player.Place.X, 2), Is.EqualTo(Math.Round(t1, 2)));
            play.Player.Move(new Vector(xDirection, yDirection));
            Assert.That(Math.Round(play.Player.Place.X, 2), Is.EqualTo(Math.Round(t2, 2)));
            play.Player.Move(new Vector(xDirection, yDirection));
            play.Player.Move(new Vector(xDirection, yDirection));
            Assert.That(Math.Round(play.Player.Place.X, 2), Is.EqualTo(Math.Round(t3, 2)));
            for (int idx = 0; idx < 6; idx += 1)
            {
                play.Player.Move(new Vector(xDirection, yDirection));
            }
            Assert.That(Math.Round(play.Player.Place.X, 2), Is.EqualTo(Math.Round(t4, 2)));
        }

        [TestCase(0, 1, 0, 1)]    // In the top
        [TestCase(0, -1, 0, -1)]    // In the bottom
        [TestCase(-1, 0, -1, 0)]    // In the left
        [TestCase(1, 0, 1, 0)]    // In the rigth
        public void The_player_can_not_go_out_of_the_map(float xBegin, float yBegin, float xDirection, float yDirection)
        {
            Round play = new Round(0, new Vector(0, 0), new Vector(xBegin, yBegin));
            play.Player.Move(new Vector(xDirection, yDirection));
            play.Update();
            Assert.That(Math.Round(play.Player.Place.X, 2), Is.EqualTo(Math.Round(xBegin, 2)));
            Assert.That(Math.Round(play.Player.Place.Y, 2), Is.EqualTo(Math.Round(yBegin, 2)));

        }
    }
}