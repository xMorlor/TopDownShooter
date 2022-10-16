﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TopDownShooterFinal
{

    static class Input
    {
        static KeyboardState currentKeyState;
        static KeyboardState previousKeyState;
        static MouseState previousMouseState;

        public static bool alreadyMoving = false;
        public static bool alreadyIdle = true;

        public static int ammoToBeLoaded = 0;
        public static int ammoToBeDeleted = 0;

        public static Color laserColor = Color.LimeGreen;
        private static readonly Random rnd1 = new Random();
        private static readonly Random rnd2 = new Random();
        private static readonly Random rnd3 = new Random();

        private static bool wDown = false;
        private static bool aDown = false;
        private static bool sDown = false;
        private static bool dDown = false;

        public static bool holdingLeftButton;

        public static KeyboardState GetState()
        {
            previousKeyState = currentKeyState;
            currentKeyState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
            return currentKeyState;
        }

        public static bool HasBeenPressed(Keys key)
        {
            return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
        }

        private static bool CheckKeys()
        {
            int num = 0;
            if (wDown) num++;
            if (aDown) num++;
            if (sDown) num++;
            if (dDown) num++;

            if (num >= 2) return true;
            else return false;
        }

        public static void Update(GameTime gameTime)
        {
            Cursor.Update(gameTime);
            GetState();
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.W))
            {
                wDown = true;
            }
            else wDown = false;
            if (kstate.IsKeyDown(Keys.A))
            {
                aDown = true;
            }
            else aDown = false;
            if (kstate.IsKeyDown(Keys.S))
            {
                sDown = true;
            }
            else sDown = false;
            if (kstate.IsKeyDown(Keys.D))
            {
                dDown = true;
            }
            else dDown = false;

            //více kláves zmáčknuto najednou

            if (CheckKeys())
            {
                Player.moving = true;
                if (kstate.IsKeyDown(Keys.W) && kstate.IsKeyDown(Keys.A))
                {
                    if (Player.knifeEquiped)
                    {
                        Player.position.Y -= 176.78f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        Player.position.X -= 176.78f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (Player.handgunEquiped)
                    {
                        Player.position.Y -= 155.56f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        Player.position.X -= 155.56f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (Player.rifleEquiped || Player.shotgunEquiped)
                    {
                        Player.position.Y -= 113.14f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        Player.position.X -= 113.14f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }
                else if (kstate.IsKeyDown(Keys.W) && kstate.IsKeyDown(Keys.D))
                {
                    if (Player.knifeEquiped)
                    {
                        Player.position.Y -= 176.78f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        Player.position.X += 176.78f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (Player.handgunEquiped)
                    {
                        Player.position.Y -= 155.56f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        Player.position.X += 155.56f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (Player.rifleEquiped || Player.shotgunEquiped)
                    {
                        Player.position.Y -= 113.14f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        Player.position.X += 113.14f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }
                else if (kstate.IsKeyDown(Keys.S) && kstate.IsKeyDown(Keys.A))
                {
                    if (Player.knifeEquiped)
                    {
                        Player.position.Y += 176.78f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        Player.position.X -= 176.78f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (Player.handgunEquiped)
                    {
                        Player.position.Y += 155.56f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        Player.position.X -= 155.56f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (Player.rifleEquiped || Player.shotgunEquiped)
                    {
                        Player.position.Y += 113.14f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        Player.position.X -= 113.14f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }
                else if (kstate.IsKeyDown(Keys.S) && kstate.IsKeyDown(Keys.D))
                {
                    if (Player.knifeEquiped)
                    {
                        Player.position.Y += 176.78f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        Player.position.X += 176.78f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (Player.handgunEquiped)
                    {
                        Player.position.Y += 155.56f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        Player.position.X += 155.56f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (Player.rifleEquiped || Player.shotgunEquiped)
                    {
                        Player.position.Y += 113.14f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        Player.position.X += 113.14f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }
            }
            else
            {
                if (kstate.IsKeyDown(Keys.W))
                {
                    Player.moving = true;
                    if (Player.knifeEquiped)
                    {
                        Player.position.Y -= 250 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (Player.handgunEquiped)
                    {
                        Player.position.Y -= 220 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (Player.rifleEquiped || Player.shotgunEquiped)
                    {
                        Player.position.Y -= 160 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }
                else if (kstate.IsKeyDown(Keys.A))
                {
                    Player.moving = true;
                    if (Player.knifeEquiped)
                    {
                        Player.position.X -= 250 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (Player.handgunEquiped)
                    {
                        Player.position.X -= 220 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (Player.rifleEquiped || Player.shotgunEquiped)
                    {
                        Player.position.X -= 160 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }
                else if (kstate.IsKeyDown(Keys.S))
                {
                    Player.moving = true;
                    if (Player.knifeEquiped)
                    {
                        Player.position.Y += 250 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (Player.handgunEquiped)
                    {
                        Player.position.Y += 220 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (Player.rifleEquiped || Player.shotgunEquiped)
                    {
                        Player.position.Y += 160 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }
                else if (kstate.IsKeyDown(Keys.D))
                {
                    Player.moving = true;
                    if (Player.knifeEquiped)
                    {
                        Player.position.X += 250 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (Player.handgunEquiped)
                    {
                        Player.position.X += 220 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (Player.rifleEquiped || Player.shotgunEquiped)
                    {
                        Player.position.X += 160 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }
            }

            if (!kstate.IsKeyDown(Keys.W) && !kstate.IsKeyDown(Keys.A) && !kstate.IsKeyDown(Keys.S) && !kstate.IsKeyDown(Keys.D))
            {
                Player.moving = false;
                alreadyMoving = false;
            }

            //4 - knife
            //3 - handgun
            //2 - shotgun
            //1 - rifle

            if (HasBeenPressed(Keys.D1) && !Player.rifleEquiped && !Player.knifeAttacking && !Player.handgunShooting && !Player.handgunReloading && !Player.handgunMeleeAttacking && !Player.shotgunMeleeAttacking && !Player.shotgunReloading && !Player.shotgunShooting)
            {
                Utils.MakePlayerHoldRifle();

                if (Player.moving)
                {
                    Utils.MakePlayerRifleMove();
                }
                else
                {
                    Utils.MakePlayerRifleIdle();
                }
            }
            if (HasBeenPressed(Keys.D2) && !Player.shotgunEquiped && !Player.knifeAttacking && !Player.handgunShooting && !Player.handgunReloading && !Player.handgunMeleeAttacking && !Player.rifleReloading && !Player.rifleMeleeAttacking && !Player.rifleShooting)
            {
                Utils.MakePlayerHoldShotgun();
                if (Player.moving)
                {
                    Utils.MakePlayerShotgunMove();
                }
                else
                {
                    Utils.MakePlayerShotgunIdle();
                }
            }
            if (HasBeenPressed(Keys.D3) && !Player.handgunEquiped && !Player.knifeAttacking && !Player.rifleReloading && !Player.rifleMeleeAttacking && !Player.rifleShooting && !Player.shotgunMeleeAttacking && !Player.shotgunReloading && !Player.shotgunShooting)
            {
                Utils.MakePlayerHoldHandgun();
                if (Player.moving)
                {
                    Utils.MakePlayerHandgunMove();
                }
                else
                {
                    Utils.MakePlayerHandgunIdle();
                }
            }
            if (HasBeenPressed(Keys.D4) && !Player.knifeEquiped && !Player.handgunShooting && !Player.handgunReloading && !Player.handgunMeleeAttacking && !Player.rifleReloading && !Player.rifleMeleeAttacking && !Player.rifleShooting && !Player.shotgunMeleeAttacking && !Player.shotgunReloading && !Player.shotgunShooting)
            {
                Utils.MakePlayerHoldKnife();
                if (Player.moving)
                {
                    Utils.MakePlayerKnifeMove();
                }
                else
                {
                    Utils.MakePlayerKnifeIdle();
                }
            }

            if (Player.knifeEquiped)
            {
                if (Player.moving == false && Player.knifeAttacking == false && alreadyIdle == false)
                {
                    Utils.MakePlayerKnifeIdle();
                }

                if (Player.moving && alreadyMoving == false && Player.knifeAttacking == false)
                {
                    Utils.MakePlayerKnifeMove();
                }

                if (!Player.knifeAttacking && previousMouseState.LeftButton == ButtonState.Released && Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    Utils.MakePlayerKnifeAttacking();
                }
            }

            else if (Player.handgunEquiped)
            {
                if (!Player.moving && !Player.handgunShooting && !Player.handgunMeleeAttacking && !Player.handgunReloading && !alreadyIdle)
                {
                    Utils.MakePlayerHandgunIdle();
                }

                if (Player.moving && !Player.handgunShooting && !Player.handgunMeleeAttacking && !Player.handgunReloading && !alreadyMoving)
                {
                    Utils.MakePlayerHandgunMove();
                }

                if (Player.stamina >= 10 && !Player.handgunMeleeAttacking && !Player.handgunReloading && !Player.handgunShooting && previousMouseState.RightButton == ButtonState.Released && Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    Utils.MakePlayerHandgunMeleeattack();
                }

                if (!Player.handgunMeleeAttacking && Player.handgunAmmoLoaded > 0 && !Player.handgunReloading && !Player.handgunShooting && previousMouseState.LeftButton == ButtonState.Released && Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    Utils.MakePlayerHandgunShoot(gameTime);
                }

                if (HasBeenPressed(Keys.R) && !Player.handgunMeleeAttacking && !Player.handgunReloading && !Player.handgunShooting && Player.handgunAmmo > 0 && Player.handgunAmmoLoaded < Player.handgunMagazineSize)
                {
                    ammoToBeLoaded = Player.handgunMagazineSize - Player.handgunAmmoLoaded;
                    if (ammoToBeLoaded > Player.handgunAmmo)
                    {
                        ammoToBeLoaded = Player.handgunAmmo;
                        ammoToBeDeleted = Player.handgunAmmo;
                    }
                    else
                    {
                        ammoToBeDeleted = ammoToBeLoaded;
                    }
                    Utils.MakePlayerHandgunReload();
                }
            }

            else if (Player.rifleEquiped)
            {
                if (!Player.moving && !Player.rifleShooting && !Player.rifleMeleeAttacking && !Player.rifleReloading && !alreadyIdle)
                {
                    Utils.MakePlayerRifleIdle();
                }
                if (Player.moving && !Player.rifleShooting && !Player.rifleMeleeAttacking && !Player.rifleReloading && !alreadyMoving)
                {
                    Utils.MakePlayerRifleMove();
                }
                if (Player.stamina >= 20 && !Player.rifleMeleeAttacking && !Player.rifleReloading && !Player.rifleShooting && previousMouseState.RightButton == ButtonState.Released && Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    Utils.MakePlayerRifleMeleeattack();
                }
                //if (!Player.rifle_meleeAttacking && Player.rifle_ammo_loaded > 0 && !Player.rifleReloading && !Player.rifle_shooting && previousMouseState.LeftButton == ButtonState.Released && Mouse.GetState().LeftButton == ButtonState.Pressed)
                if (!Player.rifleMeleeAttacking && Player.rifleAmmoLoaded > 0 && !Player.rifleReloading && !Player.rifleShooting && Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    Utils.MakePlayerRifleShoot(gameTime);
                }
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    holdingLeftButton = true;
                }
                else
                {
                    holdingLeftButton = false;
                }
                if (HasBeenPressed(Keys.R) && !Player.rifleMeleeAttacking && !Player.rifleReloading && !Player.rifleShooting && Player.rifleAmmo > 0 && Player.rifleAmmoLoaded < Player.rifleMagazineSize)
                {
                    ammoToBeLoaded = Player.rifleMagazineSize - Player.rifleAmmoLoaded;
                    if (ammoToBeLoaded > Player.rifleAmmo)
                    {
                        ammoToBeLoaded = Player.rifleAmmo;
                        ammoToBeDeleted = Player.rifleAmmo;
                    }
                    else
                    {
                        ammoToBeDeleted = ammoToBeLoaded;
                    }
                    Utils.MakePlayerRifleReload();
                }
            }

            else if (Player.shotgunEquiped)
            {
                if (!Player.moving && !Player.shotgunShooting && !Player.shotgunMeleeAttacking && !Player.shotgunReloading && !alreadyIdle)
                {
                    Utils.MakePlayerShotgunIdle();
                }
                if (Player.moving && !Player.shotgunShooting && !Player.shotgunMeleeAttacking && !Player.shotgunReloading && !alreadyMoving)
                {
                    Utils.MakePlayerShotgunMove();
                }
                if (Player.stamina >= 20 && !Player.shotgunMeleeAttacking && !Player.shotgunReloading && !Player.shotgunShooting && previousMouseState.RightButton == ButtonState.Released && Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    Utils.MakePlayerShotgunMeleeattack();
                }
                if (!Player.shotgunReloading && Player.shotgunAmmoLoaded > 0 && !Player.shotgunMeleeAttacking && !Player.shotgunShooting && previousMouseState.LeftButton == ButtonState.Released && Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    Utils.MakePlayerShotgunShoot(gameTime);
                }
                if (HasBeenPressed(Keys.R) && !Player.shotgunMeleeAttacking && !Player.shotgunReloading && !Player.shotgunShooting && Player.shotgunAmmo > 0 && Player.shotgunAmmoLoaded < Player.shotgunMagazineSize)
                {
                    Utils.MakePlayerShotgunReload();
                }
            }

            if (Input.HasBeenPressed(Keys.N) && (Player.handgunEquiped || Player.rifleEquiped || Player.shotgunEquiped))
            {
                Player.laserOn = !Player.laserOn;
            }
            else if (HasBeenPressed(Keys.C) && (Player.handgunEquiped || Player.rifleEquiped || Player.shotgunEquiped))
            {
                laserColor = new Color(rnd1.Next(1, 256), rnd2.Next(1, 256), rnd3.Next(1, 256));
            }


            //debug (pak odstranit)
            if (HasBeenPressed(Keys.B))
            {
                for (int i = 0; i < 5; i++)
                {
                    Zombie z = new Zombie();
                    Manager.zombieList.Add(z);
                }
            }

            previousMouseState = Mouse.GetState();
        }
    }
}
