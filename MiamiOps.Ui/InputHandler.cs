﻿using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Threading;

namespace MiamiOps
{
    public class InputHandler
    {
        RoundUI _roundUIContext;

        Texture _bulletTexture;
        Music _bulletSound;
        Music _zawarudo;

        int i = 0;
        int _timerNextWeapon = 4;
        int _timerPreviousWeapon = 4;
        double x = 0.03f;
        double y = 0.03f;

        public InputHandler(RoundUI roundUIContext)
        {
            _roundUIContext = roundUIContext;

            _bulletTexture = new Texture("../../../../Images/fireball.png");
            _bulletSound = new Music("../../../Menu/bullet_sound.ogg");
            _zawarudo = new Music("../../../../Images/ZA WARUDO.ogg");

        }

        public void Handle()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Z))
            {
                _roundUIContext.RoundContext.Player.Move(new Vector(0, 1));
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                _roundUIContext.RoundContext.Player.Move(new Vector(0, -1));
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
            {
                _roundUIContext.RoundContext.Player.Move(new Vector(-1, 0));
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                _roundUIContext.RoundContext.Player.Move(new Vector(1, 0));
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.P)&&_roundUIContext.RoundContext.Player.BlockWeapon == false)
            {
                if (_timerNextWeapon == 5)
                {
                    _roundUIContext.RoundContext.Player.ChangeWeapon(1);
                    _timerNextWeapon = 0;
                }
                _timerNextWeapon++;
               
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.O)&&_roundUIContext.RoundContext.Player.BlockWeapon == false)
            {
                if (_timerPreviousWeapon == 4)
                {
                    _roundUIContext.RoundContext.Player.ChangeWeapon(-1);
                    _timerPreviousWeapon = 0;
                }
                _timerPreviousWeapon++;

            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                              
                if (i >= 30 && _roundUIContext.RoundContext.Player.CurrentWeapon.Name == "USP")
                {
                        _bulletSound.Play();
                        _roundUIContext.RoundContext.Player.Attack(CalculMouseVector());
                    
                    i = 0;
                }else if(i >= 10 && _roundUIContext.RoundContext.Player.CurrentWeapon.Name == "ak47")
                {
                    _bulletSound.Play();
                    _roundUIContext.RoundContext.Player.Attack(CalculMouseVector());
                    i = 0;

                }else if(i >= 30 && _roundUIContext.RoundContext.Player.CurrentWeapon.Name == "shotgun") {
                    _bulletSound.Play();
                    Vector shotgun_shoot = CalculMouseVector();
                    _roundUIContext.RoundContext.Player.Attack(shotgun_shoot);
                    int count = 0;
                    for (int i = 0; i < 4; i++)
                    {
                       
                        Vector new_shotgun_shoot = new Vector(shotgun_shoot.X + x, shotgun_shoot.Y + y);
                        _roundUIContext.RoundContext.Player.Attack(new_shotgun_shoot);
                        if (count < 2)
                        {
                            x += 0.03f;
                            y += 0.03f;
                        }
                        else
                        {
                            x -= 0.03f;
                            y -= 0.03f;
                        }

                        count++;
                        if(count == 2)
                        {
                            x = -0.01f;
                            y = -0.01f;
                        }
                    }

                    i = 0;
                    x = 0.03f;
                    y = 0.03f;

                }else if(_roundUIContext.RoundContext.Player.CurrentWeapon.Name == "soulcalibur")
                {
                    TimeSpan TimeBegin = TimeSpan.FromSeconds(5);
                    DateTime DateBegin = DateTime.UtcNow;
                    TimeSpan span = TimeSpan.FromSeconds(0);
                    _roundUIContext.GameCtx.MusicMain.Pause();
                    _zawarudo.Play();
                    while (span < TimeBegin && Keyboard.IsKeyPressed(Keyboard.Key.Space)) {
                         span = DateTime.UtcNow - DateBegin;
                        _bulletSound.Play();
                        _roundUIContext.RoundContext.Player.Attack(CalculMouseVector());
                    }
                    _roundUIContext.RoundContext.Player.CurrentWeapon.LifeSpan = TimeSpan.FromSeconds(0);
                    _roundUIContext.RoundContext.Player.CurrentWeapon.CreationDate = DateTime.UtcNow;
                    _roundUIContext.GameCtx.MusicMain.Play();
                    i = 0;
                }

                i++;
            }
        }

        public Vector CalculMouseVector()
        {
            Vector2f viewPos = _roundUIContext.GameCtx.MyView.GetPosition();
            Vector viewPosition = new Vector(viewPos.X, viewPos.Y);

            Vector2i mouseVector2i = Mouse.GetPosition(_roundUIContext.GameCtx.Window);
            Vector mouseVector = new Vector(mouseVector2i.X, mouseVector2i.Y);

            Vector mouseAim = new Vector(viewPosition.X + mouseVector.X, viewPosition.Y + mouseVector.Y);

            Vector finalMousePosition = new Vector(mouseAim.X * (1.0 / (_roundUIContext.MapWidth / 2.0)) - 1.0, -(mouseAim.Y * (1.0 / (_roundUIContext.MapHeight / 2.0)) - 1.0)); // Vraie coordonnées de la souris en -1 / 1

            return finalMousePosition;
        }
    }
}
