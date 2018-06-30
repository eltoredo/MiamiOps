using SFML.Audio;
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
        bool _shoot;
        int a = 0;
        

        public InputHandler(RoundUI roundUIContext)
        {
            _roundUIContext = roundUIContext;

            _bulletTexture = new Texture("../../../../Images/fireball.png");
            _bulletSound = new Music("../../../../Images/" + _roundUIContext.RoundHandlerContext.RoundObject.Player.CurrentWeapon.Name + "bullet.ogg");
            _zawarudo = new Music("../../../../Images/ZA WARUDO.ogg");
            _shoot = false;

        }

        public void Handle()
        {

            if (Keyboard.IsKeyPressed(Keyboard.Key.Z))
            {
                if (_roundUIContext.RoundHandlerContext.RoundObject.Player.Effect == "Reverse")
                {
                    _roundUIContext.RoundHandlerContext.RoundObject.Player.Move(new Vector(0, -1));
                }
                else
                {
                    _roundUIContext.RoundHandlerContext.RoundObject.Player.Move(new Vector(0, 1));
                }
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                if (_roundUIContext.RoundHandlerContext.RoundObject.Player.Effect == "Reverse")
                {
                    _roundUIContext.RoundHandlerContext.RoundObject.Player.Move(new Vector(0, 1));
                }
                else
                {
                    _roundUIContext.RoundHandlerContext.RoundObject.Player.Move(new Vector(0, -1));
                }
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
            {
                if (_roundUIContext.RoundHandlerContext.RoundObject.Player.Effect == "Reverse")
                {
                    _roundUIContext.RoundHandlerContext.RoundObject.Player.Move(new Vector(1, 0));
                }
                else
                {
                    _roundUIContext.RoundHandlerContext.RoundObject.Player.Move(new Vector(-1, 0));
                }
            }
           else  if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                if (_roundUIContext.RoundHandlerContext.RoundObject.Player.Effect == "Reverse")
                {
                    _roundUIContext.RoundHandlerContext.RoundObject.Player.Move(new Vector(-1, 0));
                }
                else
                {
                    _roundUIContext.RoundHandlerContext.RoundObject.Player.Move(new Vector(1, 0));
                }
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.P)&&_roundUIContext.RoundHandlerContext.RoundObject.Player.BlockWeapon == false)
            {
                if (_timerNextWeapon == 5)
                {
                    _roundUIContext.RoundHandlerContext.RoundObject.Player.ChangeWeapon(1);
                    _timerNextWeapon = 0;
                }
                _timerNextWeapon++;
               
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.O)&& _roundUIContext.RoundHandlerContext.RoundObject.Player.BlockWeapon == false)
            {
                if (_timerPreviousWeapon == 4)
                {
                    _roundUIContext.RoundHandlerContext.RoundObject.Player.ChangeWeapon(-1);
                    _timerPreviousWeapon = 0;
                }
                _timerPreviousWeapon++;

            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                              
                if (i >= 30 && _roundUIContext.RoundHandlerContext.RoundObject.Player.CurrentWeapon.Name == "USP")
                {
                    ChangeSound();
                    _bulletSound.Play();
                    _roundUIContext.RoundHandlerContext.RoundObject.Player.Attack(CalculMouseVector());
                    i = 0;
                }else if(i >= 10 && _roundUIContext.RoundHandlerContext.RoundObject.Player.CurrentWeapon.Name == "ak47")
                {
                    ChangeSound();
                    _bulletSound.Play();
                    _roundUIContext.RoundHandlerContext.RoundObject.Player.Attack(CalculMouseVector());
                    i = 0;
                }

            else if (i >= 50 && _roundUIContext.RoundHandlerContext.RoundObject.Player.CurrentWeapon.Name == "shotgun" || i >= 10 && _roundUIContext.RoundHandlerContext.RoundObject.Player.CurrentWeapon.Name == "FreezeGun")
            {
                    ChangeSound();
                    _bulletSound.Play();
                    Vector shotgun_shoot = CalculMouseVector();
                    _roundUIContext.RoundHandlerContext.RoundObject.Player.Attack(shotgun_shoot);
                    int count = 0;
                    for (int i = 0; i < 4; i++)
                    {
                       
                        Vector new_shotgun_shoot = new Vector(shotgun_shoot.X + x, shotgun_shoot.Y + y);
                        _roundUIContext.RoundHandlerContext.RoundObject.Player.Attack(new_shotgun_shoot);
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

                }
                else if(_roundUIContext.RoundHandlerContext.RoundObject.Player.CurrentWeapon.Name == "soulcalibur")
                {
                    TimeSpan TimeBegin = TimeSpan.FromSeconds(5);
                    DateTime DateBegin = DateTime.UtcNow;
                    TimeSpan span = TimeSpan.FromSeconds(0);
                    _roundUIContext.GameCtx.MusicMain.Pause();
                    _roundUIContext.EffectMusic.Pause();
                    _roundUIContext.GameCtx.MusicMain.Pause();
                    _zawarudo.Play();
                    while (span < TimeBegin && Keyboard.IsKeyPressed(Keyboard.Key.Space)) {
                         span = DateTime.UtcNow - DateBegin;
                        ChangeSound();
                        _bulletSound.Play();
                        _roundUIContext.RoundHandlerContext.RoundObject.Player.Attack(CalculMouseVector());
                    }
                    //_roundUIContext.RoundHandlerContext.RoundObject.Player.CurrentWeapon.LifeSpan = TimeSpan.FromSeconds(0);
                    //_roundUIContext.RoundHandlerContext.RoundObject.Player.CurrentWeapon.CreationDate = DateTime.UtcNow;
                    _roundUIContext.RoundHandlerContext.RoundObject.Player.CurrentWeapon.Life = false;
                    _roundUIContext.GameCtx.MusicMain.Play();
                    i = 0;
                }else if (i >= 20 && _roundUIContext.RoundHandlerContext.RoundObject.Player.CurrentWeapon.Name == "SheepGun")
                {
                    ChangeSound();
                    _bulletSound.Play();
                    _roundUIContext.RoundHandlerContext.RoundObject.Player.Attack(CalculMouseVector());
                    i = 0;
                }
                else if (i >= 20 && _roundUIContext.RoundHandlerContext.RoundObject.Player.CurrentWeapon.Name == "HypnoseGun")
                {
                    ChangeSound();
                    _bulletSound.Play();
                    _roundUIContext.RoundHandlerContext.RoundObject.Player.Attack(CalculMouseVector());
                    i = 0;
                }else if(i>=40 && _roundUIContext.RoundHandlerContext.RoundObject.Player.CurrentWeapon.Name == "TpGun")
                {
                    ChangeSound();
                   _bulletSound.Play();
                     if (a == 0) {

                        _roundUIContext.RoundHandlerContext.RoundObject.Player.CurrentWeapon.TpPlace = CalculMouseVector();
                        a++;
                    }
                    else if(a == 1)
                    {
                        _roundUIContext.RoundHandlerContext.RoundObject.Player.Place = _roundUIContext.RoundHandlerContext.RoundObject.Player.CurrentWeapon.TpPlace;
                        a = 0;
                    }
                    i = 0;
                }


                i++;

            }


            if (Keyboard.IsKeyPressed(Keyboard.Key.F1)&& (Keyboard.IsKeyPressed(Keyboard.Key.F2)))
            {
                _roundUIContext.RoundHandlerContext.RoundObject.Player.Experience += 500;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.F2)&& (Keyboard.IsKeyPressed(Keyboard.Key.F3)))
            {
                _roundUIContext.RoundHandlerContext.RoundObject.Player.Points += 1000000;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.F12)&& Keyboard.IsKeyPressed(Keyboard.Key.F11))
            {
                _roundUIContext.RoundHandlerContext.RoundObject.Stage = 5;
                _roundUIContext.RoundHandlerContext.RoundObject.Level = 3;

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

        public void ChangeSound()
        {
            _bulletSound.Dispose();
            if(a == 1)
            {
                _bulletSound = new Music("../../../../Images/" + _roundUIContext.RoundHandlerContext.RoundObject.Player.CurrentWeapon.Name + "2bullet.ogg");

            }
            else
            {
                _bulletSound = new Music("../../../../Images/" + _roundUIContext.RoundHandlerContext.RoundObject.Player.CurrentWeapon.Name + "bullet.ogg");
            }
            _bulletSound.Volume = 150f;
        }
    }
}
