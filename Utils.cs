using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TopDownShooterFinal
{
    static class Utils
    {
        public static int screenWidth;
        public static int screenHeigth;

        

        public static void SetUpTracks()
        {
            Manager.trackTextures.Add(Textures.Run0);
            Manager.trackTextures.Add(Textures.Run1);
            Manager.trackTextures.Add(Textures.Run2);
            Manager.trackTextures.Add(Textures.Run3);
            Manager.trackTextures.Add(Textures.Run4);
            Manager.trackTextures.Add(Textures.Run5);
            Manager.trackTextures.Add(Textures.Run6);
            Manager.trackTextures.Add(Textures.Run7);
            Manager.trackTextures.Add(Textures.Run8);
            Manager.trackTextures.Add(Textures.Run9);
            Manager.trackTextures.Add(Textures.Run10);
            Manager.trackTextures.Add(Textures.Run11);
            Manager.trackTextures.Add(Textures.Run12);
            Manager.trackTextures.Add(Textures.Run13);
            Manager.trackTextures.Add(Textures.Run14);
            Manager.trackTextures.Add(Textures.Run15);
            Manager.trackTextures.Add(Textures.Run16);
            Manager.trackTextures.Add(Textures.Run17);
            Manager.trackTextures.Add(Textures.Run18);
            Manager.trackTextures.Add(Textures.Run19);
        }

        public static void SetUpScreen(GraphicsDeviceManager graphics)
        {
            GraphicsDevice device = graphics.GraphicsDevice;
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = device.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = device.DisplayMode.Height;
            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeigth = graphics.PreferredBackBufferHeight;
            graphics.ApplyChanges();
        }

        public static bool IntersectCircles(Circle a, Circle b)
        {
            float dx = b.Center.X - a.Center.X;
            float dy = b.Center.Y - a.Center.Y;
            float dist = MathF.Sqrt(dx * dx + dy * dy);
            float r2 = a.Radius + b.Radius;

            if (dist >= r2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void ScreenFlashOnHit()
        {
            if (Player.onHitFlash)
            {
                if(DayNight.flashColor.R < 60 && !Player.rev)
                {
                    DayNight.flashColor.R += 6;
                }
                if(DayNight.flashColor.R == 60 && !Player.rev)
                {
                    Player.rev = true;
                }
                if (Player.rev)
                {
                    DayNight.flashColor.R -= 6;
                }
                if(Player.rev && DayNight.flashColor.R == 0)
                {
                    Player.onHitFlash = false;
                    Player.rev = false;
                }
            }
        }

        public static void MeleeattackCheck(GraphicsDevice graphicsDevice)
        {
            if (Player.handgunMeleeAttacking)
            {
                foreach (var k in Manager.zombieList)
                {
                    if (IntersectCircles(k.hitboxCircle, Player.playerAnimation.meleeattackCircle) && !k.meleeAttacked)
                    {
                        k.health -= 30;
                        k.meleeAttacked = true;
                        if(k.health > 0)
                        {
                            Blood b = new Blood(true, k.position, k, 0, graphicsDevice);
                            Manager.bloodList.Add(b);
                        }
                    }
                }
            }
            else if (Player.rifleMeleeAttacking)
            {
                foreach (var k in Manager.zombieList)
                {
                    if (IntersectCircles(k.hitboxCircle, Player.playerAnimation.meleeattackCircle) && !k.meleeAttacked)
                    {
                        k.health -= 40;
                        k.meleeAttacked = true;
                        if (k.health > 0)
                        {
                            Blood b = new Blood(true, k.position, k, 0, graphicsDevice);
                            Manager.bloodList.Add(b);
                        }
                    }
                }
            }
            else if (Player.shotgunMeleeAttacking)
            {
                foreach (var k in Manager.zombieList)
                {
                    if (IntersectCircles(k.hitboxCircle, Player.playerAnimation.meleeattackCircle) && !k.meleeAttacked)
                    {
                        k.health -= 40;
                        k.meleeAttacked = true;
                        if (k.health > 0)
                        {
                            Blood b = new Blood(true, k.position, k, 0, graphicsDevice);
                            Manager.bloodList.Add(b);
                        }
                    }
                }
            }
            else if (Player.knifeAttacking)
            {
                foreach (var k in Manager.zombieList)
                {
                    if (IntersectCircles(k.hitboxCircle, Player.playerAnimation.meleeattackCircle) && !k.meleeAttacked)
                    {
                        k.health -= 60;
                        k.meleeAttacked = true;
                        if (k.health > 0)
                        {
                            Blood b = new Blood(true, k.position, k, 0, graphicsDevice);
                            Manager.bloodList.Add(b);
                        }
                    }
                }
            }
            if(!Player.knifeAttacking && !Player.handgunMeleeAttacking && !Player.rifleMeleeAttacking && !Player.shotgunMeleeAttacking)
            {
                foreach(var k in Manager.zombieList)
                {
                    k.meleeAttacked = false;
                }
            }
        }

        public static void CheckCollisionBetweenZombies(GameTime gameTime)
        {
            for (int i = 0; i < Manager.zombieList.Count; i++)
            {
                for (int j = 0; j < Manager.zombieList.Count; j++)
                {
                    if (Manager.zombieList[i] != Manager.zombieList[j] && IntersectCircles(Manager.zombieList[i].hitboxCircle, Manager.zombieList[j].hitboxCircle) && (Manager.zombieList[i].lured == false || Manager.zombieList[j].lured == false))
                    {
                        if (Manager.zombieList[i].lured == false && Manager.zombieList[j].lured == false)
                        {
                            Manager.zombieList[i].direction = Manager.zombieList[i].position - Manager.zombieList[j].position;
                            Manager.zombieList[i].position += Manager.zombieList[i].direction * 0.0001f * (float)gameTime.ElapsedGameTime.TotalSeconds;//tady a dole upravovat
                            Manager.zombieList[i].speed += 80000 * (Manager.zombieList[i].direction / (Manager.zombieList[i].direction.LengthSquared() + 1)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                            /*
                            Manager.zombieList[i].CollisionBetweenAnotherZombie(gameTime, (int)Manager.zombieList[j].position.X, (int)Manager.zombieList[j].position.Y);
                            Manager.zombieList[j].direction = Manager.zombieList[j].position - Manager.zombieList[i].position;
                            Manager.zombieList[j].position += Manager.zombieList[j].direction * 0.0001f * (float)gameTime.ElapsedGameTime.TotalSeconds;//tady a dole upravovat
                            Manager.zombieList[j].speed += 80000 * (Manager.zombieList[j].direction / (Manager.zombieList[j].direction.LengthSquared() + 1)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                            Manager.zombieList[j].CollisionBetweenAnotherZombie(gameTime, (int)Manager.zombieList[j].position.X, (int)Manager.zombieList[j].position.Y);
                            */
                        }
                        if (Manager.zombieList[i].lured)
                        {
                            Manager.zombieList[j].lured = true;
                            Manager.zombieList[j].MakeZombieRunOrWalk();
                        }
                        if (Manager.zombieList[j].lured)
                        {
                            Manager.zombieList[i].lured = true;
                            Manager.zombieList[i].MakeZombieRunOrWalk();
                        }
                    }
                    else if (Manager.zombieList[i] != Manager.zombieList[j] && IntersectCircles(Manager.zombieList[i].hitboxCircle, Manager.zombieList[j].hitboxCircle))
                    {
                        Manager.zombieList[i].direction = Manager.zombieList[i].position - Manager.zombieList[j].position;
                        Manager.zombieList[i].position += Manager.zombieList[i].direction * 0.0001f * (float)gameTime.ElapsedGameTime.TotalSeconds;//tady a dole upravovat
                        Manager.zombieList[i].speed += 80000 * (Manager.zombieList[i].direction / (Manager.zombieList[i].direction.LengthSquared() + 1)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }
            }
        }

        public static void CheckCollisionBetweenPlayerAndZombies(GameTime gameTime)
        {
            foreach(var k in Manager.zombieList)
            {
                if (IntersectCircles(k.hitboxCircle, Player.hitboxCircle))
                {
                    k.direction = k.position - (Player.position + Player.playerAnimation.originPlayer);
                    k.position += k.direction * 0.0001f * (float)gameTime.ElapsedGameTime.TotalSeconds;//tady a dole upravovat
                    k.speed += 250000 * (k.direction / (k.direction.LengthSquared() + 1)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
        }

        public static void CheckCollisionBetweenZombiesAndBullets(GraphicsDevice graphicsDevice)
        {
            foreach(var k in Manager.zombieList)
            {
                foreach(var o in Manager.bulletList)
                {
                    if(k.health > 0)
                    {
                        if (IntersectCircles(k.hitboxCircle, o.hitboxCircle))
                        {
                            if (!k.lured)
                            {
                                k.lured = true;
                                k.MakeZombieRunOrWalk();
                            }

                            Manager.bulletsToDelete.Add(o);
                            if (o.shotFromRifle) k.health -= 70;
                            else k.health -= 50;

                            if(k.health > 0)
                            {
                                Blood b = new Blood(true, k.position, k, 0, graphicsDevice); //tady se doplňovat num na konci nemusí
                                Manager.bloodList.Add(b);
                            }
                        }
                    }
                }
            }
        }

        public static void CreateBlood(Zombie zombie, int num, GraphicsDevice graphicsDevice)
        {
            Blood b = new Blood(false, zombie.position, zombie, num, graphicsDevice);
            Manager.bloodList.Add(b);
        }

        public static void CheckLure()
        {
            foreach (var k in Manager.zombieList)
            {
                if(!k.lured)
                {
                    if (IntersectCircles(k.lureCircle, Player.lureCircle))
                    {
                        k.lured = true;
                        k.MakeZombieRunOrWalk();
                    }
                }
            }
        }

        public static void CreateBulletHandgun(GameTime gameTime)//zkusit udělat private
        {
            var angle = Player.playerAnimation.angle;
            Vector2 bulletOrigin = Player.playerAnimation.originBullet;
            bulletOrigin.Y += (float)Math.Cos(angle) * 3.5f;

            angle = MathHelper.ToDegrees((float)angle);

            Vector2 pos = Player.position + bulletOrigin;

            Bullet bullet = new Bullet(pos, Textures.bul, angle, gameTime);
            Manager.bulletList.Add(bullet);
            Player.handgunAmmoLoaded--;
            bullet.shotFromHandgun = true;

            MuzzleFlashAnimation.texture = Textures.MuzzleFlash1;
            MuzzleFlashAnimation.draw = true;
        }

        public static void CreateBulletRifle(GameTime gameTime)//zkusit udělat private
        {
            var angle = Player.playerAnimation.angle;
            angle = MathHelper.ToDegrees((float)angle);

            Vector2 pos = Player.position + Player.playerAnimation.originBullet;
            
            Bullet bullet = new Bullet(pos, Textures.bul, angle, gameTime);
            Manager.bulletList.Add(bullet);
            Player.rifleAmmoLoaded--;
            bullet.shotFromRifle = true;

            MuzzleFlashAnimation.texture = Textures.MuzzleFlash1;
            MuzzleFlashAnimation.draw = true;
        }

        public static void CreateBulletsShotgun(GameTime gameTime)//zkusit udělat private
        {
            for(int i = 0; i < 8; i++)
            {
                var angle = Player.playerAnimation.angle;
                angle = MathHelper.ToDegrees((float)angle);

                Random rnd = new Random();
                angle -= rnd.Next(-15, 16);

                Vector2 pos = Player.position + Player.playerAnimation.originBullet;

                Bullet bullet = new Bullet(pos, Textures.bul, angle, gameTime);
                Manager.bulletList.Add(bullet);
                
                bullet.shotFromShotgun = true;
            }
            Player.shotgunAmmoLoaded--;

            MuzzleFlashAnimation.texture = Textures.MuzzleFlash1;
            MuzzleFlashAnimation.draw = true;
        }

        public static void MakePlayerKnifeIdle()
        {
            Player.texture = Textures.PlayerKnifeIdle;
            Player.playerAnimation.Texture = Player.texture;
            Player.playerAnimation.Rows = 3;
            Player.playerAnimation.Columns = 4;
            Player.playerAnimation.currentFrame = 0;
            Player.playerAnimation.totalFrames = Player.playerAnimation.Rows * Player.playerAnimation.Columns;
            Player.playerAnimation.reversed = false;
            Input.alreadyIdle = true;
            Player.knifeAttacking = false;
        }

        public static void MakePlayerKnifeMove()
        {
            Player.texture = Textures.PlayerKnifeMove;
            Player.playerAnimation.Texture = Player.texture;
            Player.playerAnimation.Rows = 7;
            Player.playerAnimation.Columns = 3;
            Player.playerAnimation.currentFrame = 0;
            Player.playerAnimation.totalFrames = Player.playerAnimation.Rows * Player.playerAnimation.Columns;
            Player.playerAnimation.reversed = false;
            Input.alreadyMoving = true;
            Player.knifeAttacking = false;
            Input.alreadyIdle = false;
        }

        public static void MakePlayerKnifeAttacking()
        {
            Player.texture = Textures.PlayerKnifeMeleeattack;
            Player.playerAnimation.Texture = Player.texture;
            Player.playerAnimation.Rows = 5;
            Player.playerAnimation.Columns = 3;
            Player.playerAnimation.currentFrame = 0;
            Player.playerAnimation.totalFrames = Player.playerAnimation.Rows * Player.playerAnimation.Columns;
            Player.playerAnimation.reversed = false;
            Player.knifeAttacking = true;
            Input.alreadyIdle = false;
        }

        public static void MakePlayerHandgunIdle()
        {
            Player.texture = Textures.PlayerHandgunIdle;
            Player.playerAnimation.Texture = Player.texture;
            Player.playerAnimation.Rows = 2;
            Player.playerAnimation.Columns = 5;
            Player.playerAnimation.currentFrame = 0;
            Player.playerAnimation.totalFrames = Player.playerAnimation.Rows * Player.playerAnimation.Columns;
            //Player.handgun_equiped = true;
            Player.playerAnimation.reversed = false;
            Input.alreadyIdle = true;
            Player.handgunShooting = false;
            Player.handgunMeleeAttacking = false;
        }

        public static void MakePlayerHandgunMove()
        {
            Player.texture = Textures.PlayerHandgunMove;
            Player.playerAnimation.Texture = Player.texture;
            Player.playerAnimation.Rows = 2;
            Player.playerAnimation.Columns = 6;
            Player.playerAnimation.currentFrame = 0;
            Player.playerAnimation.totalFrames = Player.playerAnimation.Rows * Player.playerAnimation.Columns;
            Player.playerAnimation.reversed = false;
            Input.alreadyIdle = false;
            Input.alreadyMoving = true;
            Player.handgunShooting = false;
            //Player.handgun_equiped = true;
            Player.handgunMeleeAttacking = false;
        }

        public static void MakePlayerHandgunMeleeattack()
        {
            //Player.handgun_equiped = true;
            Player.texture = Textures.PlayerHandgunMeleeattack;
            Player.playerAnimation.Texture = Player.texture;
            Player.playerAnimation.Rows = 3;
            Player.playerAnimation.Columns = 5;
            Player.playerAnimation.currentFrame = 0;
            Player.playerAnimation.totalFrames = Player.playerAnimation.Rows * Player.playerAnimation.Columns;
            Player.playerAnimation.reversed = false;
            Input.alreadyIdle = false;
            Player.handgunMeleeAttacking = true;
            Player.handgunShooting = false;
            Player.stamina -= 10;
        }

        public static void MakePlayerHandgunReload()
        {
            Player.texture = Textures.PlayerHandgunReload;
            Player.playerAnimation.Texture = Player.texture;
            Player.playerAnimation.Rows = 5;
            Player.playerAnimation.Columns = 3;
            Player.playerAnimation.currentFrame = 0;
            Player.playerAnimation.totalFrames = Player.playerAnimation.Rows * Player.playerAnimation.Columns;
            Player.playerAnimation.reversed = false;
            Input.alreadyIdle = false;
            //Player.handgun_equiped = true;
            Player.handgunMeleeAttacking = false;
            Player.handgunShooting = false;
            Player.handgunReloading = true;
        }

        public static void MakePlayerHandgunShoot(GameTime gameTime)
        {
            Player.texture = Textures.PlayerHandgunShoot;
            Player.playerAnimation.Texture = Player.texture;
            Player.playerAnimation.Rows = 1;
            Player.playerAnimation.Columns = 3;
            Player.playerAnimation.currentFrame = 0;
            Player.playerAnimation.totalFrames = Player.playerAnimation.Rows * Player.playerAnimation.Columns;
            Player.playerAnimation.reversed = false;
            //Player.handgun_equiped = true;
            Player.handgunMeleeAttacking = false;
            Player.handgunShooting = true;
            Input.alreadyIdle = false;

            CreateBulletHandgun(gameTime);
        }

        public static void MakePlayerRifleIdle()
        {
            Player.texture = Textures.PlayerRifleIdle;
            Player.playerAnimation.Texture = Player.texture;
            Player.playerAnimation.Rows = 4;
            Player.playerAnimation.Columns = 3;
            Player.playerAnimation.currentFrame = 0;
            Player.playerAnimation.totalFrames = Player.playerAnimation.Rows * Player.playerAnimation.Columns;
            Player.playerAnimation.reversed = false;
            Input.alreadyIdle = true;
            Player.rifleShooting = false;
            Player.rifleMeleeAttacking = false;
            Input.alreadyMoving = false;
        }

        public static void MakePlayerRifleMove()
        {
            Player.texture = Textures.PlayerRifleMove;
            Player.playerAnimation.Texture = Player.texture;
            Player.playerAnimation.Rows = 5;
            Player.playerAnimation.Columns = 4;
            Player.playerAnimation.currentFrame = 0;
            Player.playerAnimation.totalFrames = Player.playerAnimation.Rows * Player.playerAnimation.Columns;
            Player.playerAnimation.reversed = false;
            Input.alreadyIdle = false;
            Input.alreadyMoving = true;
            Player.rifleShooting = false;
            Player.rifleMeleeAttacking = false;
        }

        public static void MakePlayerRifleMeleeattack()
        {
            Player.texture = Textures.PlayerRifleMeleeattack;
            Player.playerAnimation.Texture = Player.texture;
            Player.playerAnimation.Rows = 3;
            Player.playerAnimation.Columns = 5;
            Player.playerAnimation.currentFrame = 0;
            Player.playerAnimation.totalFrames = Player.playerAnimation.Rows * Player.playerAnimation.Columns;
            Player.playerAnimation.reversed = false;
            Input.alreadyIdle = false;
            Player.rifleMeleeAttacking = true;
            Player.rifleShooting = false;
            Player.stamina -= 20;
        }

        public static void MakePlayerRifleReload()
        {
            Player.texture = Textures.PlayerRifleReload;
            Player.playerAnimation.Texture = Player.texture;
            Player.playerAnimation.Rows = 4;
            Player.playerAnimation.Columns = 5;
            Player.playerAnimation.currentFrame = 0;
            Player.playerAnimation.totalFrames = Player.playerAnimation.Rows * Player.playerAnimation.Columns;
            Player.playerAnimation.reversed = false;
            Input.alreadyIdle = false;
            Player.rifleMeleeAttacking = false;
            Player.rifleShooting = false;
            Player.rifleReloading = true;
        }

        public static void MakePlayerRifleShoot(GameTime gameTime)
        {
            Player.texture = Textures.PlayerRifleShoot;
            Player.playerAnimation.Texture = Player.texture;
            Player.playerAnimation.Rows = 3;
            Player.playerAnimation.Columns = 1;
            Player.playerAnimation.currentFrame = 0;
            Player.playerAnimation.totalFrames = Player.playerAnimation.Rows * Player.playerAnimation.Columns;
            Player.playerAnimation.reversed = false;
            Player.rifleReloading = false;
            Player.rifleMeleeAttacking = false;
            Player.rifleShooting = true;
            Input.alreadyIdle = false;

            CreateBulletRifle(gameTime);
        }

        public static void MakePlayerShotgunIdle()
        {
            Player.texture = Textures.PlayerShotgunIdle;
            Player.playerAnimation.Texture = Player.texture;
            Player.playerAnimation.Rows = 4;
            Player.playerAnimation.Columns = 5;
            Player.playerAnimation.currentFrame = 0;
            Player.playerAnimation.totalFrames = 4 * 5;
            Player.playerAnimation.reversed = false;
            Input.alreadyIdle = true;
            Player.shotgunShooting = false;
            Player.shotgunMeleeAttacking = false;
            Input.alreadyMoving = false;
        }

        public static void MakePlayerShotgunMove()
        {
            Player.texture = Textures.PlayerShotgunMove;
            Player.playerAnimation.Texture = Player.texture;
            Player.playerAnimation.Rows = 4;
            Player.playerAnimation.Columns = 5;
            Player.playerAnimation.currentFrame = 0;
            Player.playerAnimation.totalFrames = 4 * 5;
            Player.playerAnimation.reversed = false;
            Input.alreadyIdle = false;
            Input.alreadyMoving = true;
            Player.shotgunShooting = false;
            Player.shotgunMeleeAttacking = false;
        }

        public static void MakePlayerShotgunMeleeattack()
        {
            Player.texture = Textures.PlayerShotgunMeleeattack;
            Player.playerAnimation.Texture = Player.texture;
            Player.playerAnimation.Rows = 3;
            Player.playerAnimation.Columns = 5;
            Player.playerAnimation.currentFrame = 0;
            Player.playerAnimation.totalFrames = 3 * 5;
            Player.playerAnimation.reversed = false;
            Input.alreadyIdle = false;
            Player.shotgunMeleeAttacking = true;
            Player.shotgunShooting = false;
            Player.stamina -= 20;
        }

        public static void MakePlayerShotgunReload()
        {
            Player.texture = Textures.PlayerShotgunReload;
            Player.playerAnimation.Texture = Player.texture;
            Player.playerAnimation.Rows = 4;
            Player.playerAnimation.Columns = 5;
            Player.playerAnimation.currentFrame = 0;
            Player.playerAnimation.totalFrames = 4 * 5;
            Player.playerAnimation.reversed = false;
            Input.alreadyIdle = false;
            Player.shotgunMeleeAttacking = false;
            Player.shotgunShooting = false;
            Player.shotgunReloading = true;
        }

        public static void MakePlayerShotgunShoot(GameTime gameTime)
        {
            Player.texture = Textures.PlayerShotgunShoot;
            Player.playerAnimation.Texture = Player.texture;
            Player.playerAnimation.Rows = 1;
            Player.playerAnimation.Columns = 3;
            Player.playerAnimation.currentFrame = 0;
            Player.playerAnimation.totalFrames = 3;
            Player.playerAnimation.reversed = false;
            Player.shotgunReloading = false;
            Player.shotgunMeleeAttacking = false;
            Player.shotgunShooting = true;
            Input.alreadyIdle = false;

            CreateBulletsShotgun(gameTime);
        }

        public static void MakePlayerHoldKnife()
        {
            Player.knifeEquiped = true;
            Player.handgunEquiped = false;
            Player.rifleEquiped = false;
            Player.shotgunEquiped = false;
        }

        public static void MakePlayerHoldHandgun()
        {
            Player.knifeEquiped = false;
            Player.handgunEquiped = true;
            Player.rifleEquiped = false;
            Player.shotgunEquiped = false;
        }

        public static void MakePlayerHoldRifle()
        {
            Player.knifeEquiped = false;
            Player.handgunEquiped = false;
            Player.rifleEquiped = true;
            Player.shotgunEquiped = false;
        }

        public static void MakePlayerHoldShotgun()
        {
            Player.shotgunEquiped = true;
            Player.knifeEquiped = false;
            Player.handgunEquiped = false;
            Player.rifleEquiped = false;
        }
    }
}
