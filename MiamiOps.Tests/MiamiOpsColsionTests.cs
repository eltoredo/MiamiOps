using System;

using NUnit.Framework;

namespace MiamiOps.Tests
{

    [TestFixture]
    public class MiamiOpsColsionTests
    {
        [TestCase(0, 1, 0.0, .1f, .2f, .4f, 1.0)]
        [TestCase(0, -1, 0.0, -.1f, -.2f, -.4f, -0.8)]
        public void Look_if_the_player_move_verticaly(float xDirection, float yDirection, double t0, double t1, double t2, double t3, double t4)
        {
            Round play = new Round(0, new Vector(0, 0), playerLargeur : 0.1f, playerHauteur : 0.2f);
            Assert.That(Math.Round(play.Player.Place.Y, 2), Is.EqualTo(Math.Round(t0, 2)));
            play.Player.Move(new Vector(xDirection, yDirection));
            Assert.That(Math.Round(play.Player.Place.Y, 2), Is.EqualTo(Math.Round(t1, 2)));
            play.Player.Move(new Vector(xDirection, yDirection));
            Assert.That(Math.Round(play.Player.Place.Y, 2), Is.EqualTo(Math.Round(t2, 2)));
            play.Player.Move(new Vector(xDirection, yDirection));
            play.Player.Move(new Vector(xDirection, yDirection));
            Assert.That(Math.Round(play.Player.Place.Y, 2), Is.EqualTo(Math.Round(t3, 2)));
            for (int idx = 0; idx < 10; idx += 1)
            {
                play.Player.Move(new Vector(xDirection, yDirection));
            }
            Assert.That(Math.Round(play.Player.Place.Y, 2), Is.EqualTo(Math.Round(t4, 2)));
        }

        [TestCase(-1, 0, -0.0, -.1f, -.2f, -.4f, -1.0)]
        [TestCase(1, 0, 0.0, .1f, .2f, .4f, .9f)]
        public void Look_if_the_player_move_horizontaly(float xDirection, float yDirection, double t0, double t1, double t2, double t3, double t4)
        {
            Round play = new Round(0, new Vector(0, 0), playerLargeur : 0.1f, playerHauteur : 0.2f);
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

        [TestCase(1, -1, 0, 2f, 2f, -1, .2f)]
        [TestCase(-.6f, -1, 1, 2f, 1f, 1, 0f)]
        public void The_player_can_not_go_in_a_wall_verticaly(float spawn_y, float x_obstacle, float y_obstacle, float largeur_obstacle, float hauteur_obstacle, float direction_y, float arrive_y)
        {
            Round play = new Round(0, new Vector(0, spawn_y), playerLargeur : 0.1f, playerHauteur : 0.2f);
            play.AddObstacle(x_obstacle, y_obstacle, largeur_obstacle, hauteur_obstacle);
            for (int idx = 0; idx < 15; idx += 1) {play.Player.Move(new Vector (0, direction_y));}
            Assert.That(Math.Round(play.Player.Place.Y, 2), Is.EqualTo(Math.Round(arrive_y, 2)));
        }

        [TestCase(-1f, 0, 1, 1, 2, 1, -.1f)]
        [TestCase(1f, -1, 1, 1, 2, -1f, 0f)]
        public void The_player_can_not_go_in_a_wall_horizontaly(float spawn_x, float x_obstacle, float y_obstacle, float largeur_obstacle, float hauteur_obstacle, float direction_x, float arrive_x)
        {
             Round play = new Round(0, new Vector(spawn_x, 0), playerLargeur : 0.1f, playerHauteur : 0.2f);
             play.AddObstacle(x_obstacle, y_obstacle, largeur_obstacle, hauteur_obstacle);
             for (int idx = 0; idx < 15; idx += 1) {play.Player.Move(new Vector (direction_x, 0));}
             Assert.That(Math.Round(play.Player.Place.X, 2), Is.EqualTo(Math.Round(arrive_x, 2)));
        }
    }
}
