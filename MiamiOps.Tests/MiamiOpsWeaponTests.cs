using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;


namespace MiamiOps.Tests
{
    [TestFixture]
    class MiamiOpsWeaponTest
    {
        [TestCase("C", 1, "D")]
        [TestCase("C", -1, "B")]
        [TestCase("A", -1, "F")]
        [TestCase("F", 1, "A")]
        [TestCase("C", -4, "E")]
        [TestCase("E", 3, "B")]
        public void Test_the_list_helpers__the_change_element_class_method(string current, int shift, string expected)
        {
            List<string> letters = new List<string>{"A", "B", "C", "D", "E", "F"};
            string letter = letters.OtherElem(current, shift);
            Assert.That(letter, Is.EqualTo(expected));
        }

        [Test]
        public void The_player_change_his_weapon()
        {
            Player player = new Player(new List<Weapon>(), null, new Vector(0, 0), 1, 1);
            WeaponFactory factory = new WeaponFactory();
            player.GetNewWeapon(factory.CreateAssaultRifle(player));
            player.GetNewWeapon(factory.CreateBaseballBat(player));
            player.GetNewWeapon(factory.CreateSoulcalibur(player));

            player.ChangeWeapon(0);
            Assert.That(player.CurrentWeapon, Is.EqualTo(player.Weapons[0]));
            player.ChangeWeapon(-1);
            Assert.That(player.CurrentWeapon, Is.EqualTo(player.Weapons[2]));
            player.ChangeWeapon(3);
            Assert.That(player.CurrentWeapon, Is.EqualTo(player.Weapons[1]));
        }
    }
}
