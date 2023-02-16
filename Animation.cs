using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TopDownShooterFinal
{
    class Animation
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int currentFrame { get; set; }
        public int totalFrames { get; set; }
        public bool reversed { get; set; }
        public double angle { get; set; }
        public double angle2 { get; set; }

        public Vector2 originPlayer, originBullet;
        public Rectangle destinationRectangleLaser;
        public Circle meleeattackCircle;
        private Circle collisionCircle = new Circle(Player.position, 1); //tady je radius jedno jaký je
        private bool laserCondition;
        private List<Circle> listOfCircles;
        private double radian2;
        private float degrees, positionX, positionY, laserDist, posX, posY;
        private int width, height, row, column, number = 0;
        private Rectangle destinationRectanglePlayer, sourceRectangle, testRectangle;
        private Vector2 diff, diffVec;

        public Animation(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;

            Vector2 diff = new Vector2(Player.position.X + originBullet.X, Player.position.Y + originBullet.Y);
            var diffVec = new Vector2(Cursor.posX, Cursor.posY) - diff;
            angle = (float)Math.Atan2(diffVec.Y, diffVec.X);
            reversed = false;
            angle2 = angle;
            angle2 = MathHelper.ToDegrees((float)angle2);
        }

        public void Update(GameTime gameTime)
        {
            if (Player.texture == Textures.PlayerKnifeIdle)
            {
                if (!reversed && currentFrame < totalFrames - 1)
                {
                    currentFrame++;
                }
                if (!reversed && currentFrame == totalFrames - 1)
                {
                    reversed = !reversed;
                }

                if (reversed && currentFrame > 0)
                {

                    currentFrame--;
                }

                if (reversed && currentFrame == 0)
                {
                    reversed = !reversed;
                }
            }

            else if (Player.texture == Textures.PlayerKnifeMove)
            {
                currentFrame++;
                if (currentFrame == totalFrames - 1)
                {
                    currentFrame = 0;
                }
            }

            else if (Player.texture == Textures.PlayerKnifeMeleeattack)
            {
                currentFrame++;

                if (currentFrame == totalFrames)
                {
                    if (Player.moving)
                    {
                        Utils.MakePlayerKnifeMove();
                    }
                    else
                    {
                        Utils.MakePlayerKnifeIdle();
                    }
                }
            }

            else if (Player.texture == Textures.PlayerHandgunIdle)
            {
                if (!reversed && currentFrame < totalFrames)
                {
                    currentFrame++;
                }
                if (!reversed && currentFrame == totalFrames)
                {
                    reversed = !reversed;
                }

                if (reversed && currentFrame > 0)
                {

                    currentFrame--;
                }

                if (reversed && currentFrame == 0)
                {
                    reversed = !reversed;
                }
            }

            else if (Player.texture == Textures.PlayerHandgunMove)
            {
                if (!reversed && currentFrame < totalFrames - 1)
                {
                    currentFrame++;
                }
                if (!reversed && currentFrame == totalFrames - 1)
                {
                    reversed = !reversed;
                }

                if (reversed && currentFrame > 0)
                {

                    currentFrame--;
                }

                if (reversed && currentFrame == 0)
                {
                    reversed = !reversed;
                }
            }

            else if (Player.texture == Textures.PlayerHandgunMeleeattack)
            {
                currentFrame++;
                if (currentFrame == totalFrames)
                {
                    if (Player.moving)
                    {
                        Utils.MakePlayerHandgunMove();
                    }
                    else
                    {
                        Utils.MakePlayerHandgunIdle();
                    }
                }
            }

            else if (Player.texture == Textures.PlayerHandgunReload)
            {
                currentFrame++;
                if (currentFrame == totalFrames)
                {
                    Player.handgunReloading = false;
                    Player.handgunAmmoLoaded += Input.ammoToBeLoaded;
                    Player.handgunAmmo -= Input.ammoToBeDeleted;
                    if (Player.moving)
                    {
                        Utils.MakePlayerHandgunMove();
                    }
                    else
                    {
                        Utils.MakePlayerHandgunIdle();
                    }
                }
            }

            else if (Player.texture == Textures.PlayerHandgunShoot)
            {
                currentFrame++;
                if (currentFrame == totalFrames)
                {
                    if (Player.moving)
                    {
                        Utils.MakePlayerHandgunMove();
                    }
                    else
                    {
                        Utils.MakePlayerHandgunIdle();
                    }
                }
            }

            else if (Player.texture == Textures.PlayerRifleIdle)
            {
                if (!reversed && currentFrame < totalFrames - 1)
                {
                    currentFrame++;
                }
                if (!reversed && currentFrame == totalFrames - 1)
                {
                    reversed = !reversed;
                }

                if (reversed && currentFrame > 0)
                {

                    currentFrame--;
                }

                if (reversed && currentFrame == 0)
                {
                    reversed = !reversed;
                }
            }

            else if (Player.texture == Textures.PlayerRifleMeleeattack)
            {
                currentFrame++;
                if (currentFrame == totalFrames)
                {
                    if (Player.moving)
                    {
                        Utils.MakePlayerRifleMove();
                    }
                    else
                    {
                        Utils.MakePlayerRifleIdle();
                    }
                }
            }

            else if (Player.texture == Textures.PlayerRifleMove)
            {
                currentFrame++;
                if (currentFrame == totalFrames)
                {
                    currentFrame = 0;
                }
            }

            else if (Player.texture == Textures.PlayerRifleReload)
            {
                currentFrame++;
                if (currentFrame == totalFrames)
                {
                    Player.rifleReloading = false;
                    Player.rifleAmmoLoaded += Input.ammoToBeLoaded;
                    Player.rifleAmmo -= Input.ammoToBeDeleted;
                    if (Player.moving)
                    {
                        Utils.MakePlayerRifleMove();
                    }
                    else
                    {
                        Utils.MakePlayerRifleIdle();
                    }
                }
            }

            else if (Player.texture == Textures.PlayerRifleShoot)
            {
                if (number == 0) currentFrame++;

                if ((currentFrame == totalFrames && number == 0) || (currentFrame == totalFrames - 1 && number != 0))
                {
                    if (Input.holdingLeftButton && Player.rifleAmmoLoaded > 0)
                    {
                        if (number == 0) currentFrame--;
                        number++;
                        if (number == 3)
                        {
                            Utils.MakePlayerRifleShoot(gameTime);
                            number = 0;
                        }
                    }
                    else
                    {
                        number = 0;
                        if (Player.moving)
                        {
                            Utils.MakePlayerRifleMove();
                        }
                        else
                        {
                            Utils.MakePlayerRifleIdle();
                        }
                    }
                }
            }

            else if (Player.texture == Textures.PlayerShotgunIdle)
            {
                if (!reversed && currentFrame < totalFrames - 1)
                {
                    currentFrame++;
                }
                if (!reversed && currentFrame == totalFrames - 1)
                {
                    reversed = !reversed;
                }

                if (reversed && currentFrame > 0)
                {

                    currentFrame--;
                }

                if (reversed && currentFrame == 0)
                {
                    reversed = !reversed;
                }
            }

            else if (Player.texture == Textures.PlayerShotgunMeleeattack)
            {
                currentFrame++;
                if (currentFrame == totalFrames)
                {
                    if (Player.moving)
                    {
                        Utils.MakePlayerShotgunMove();
                    }
                    else
                    {
                        Utils.MakePlayerShotgunIdle();
                    }
                }
            }

            else if (Player.texture == Textures.PlayerShotgunMove)
            {
                currentFrame++;
                if (currentFrame == totalFrames)
                {
                    currentFrame = 0;
                }
            }

            else if (Player.texture == Textures.PlayerShotgunReload)
            {
                currentFrame++;
                if (currentFrame == totalFrames)
                {
                    Player.shotgunReloading = false;
                    Player.shotgunAmmoLoaded++;
                    Player.shotgunAmmo--;
                    if (Player.moving)
                    {
                        Utils.MakePlayerShotgunMove();
                    }
                    else
                    {
                        Utils.MakePlayerShotgunIdle();
                    }
                }
            }

            else if (Player.texture == Textures.PlayerShotgunShoot)
            {
                currentFrame++;
                if (currentFrame == totalFrames)
                {
                    if (Player.moving)
                    {
                        Utils.MakePlayerShotgunMove();
                    }
                    else
                    {
                        Utils.MakePlayerShotgunIdle();
                    }
                }
            }


        }

        private void HandleLaser(float pX, float pY)
        {
            laserDist = 0;
            laserCondition = true;
            collisionCircle = new Circle(originBullet + Player.position, 1);

            listOfCircles = new List<Circle>();

            radian2 = Math.Atan2((originBullet.Y + Player.position.Y - (Cursor.posY + pY * 40)), (originBullet.X + Player.position.X - (Cursor.posX + pX * 40)));
            degrees = (float)(radian2 * 180 / Math.PI);
            positionX = ((float)(Math.Cos(degrees / 360.0 * 2 * Math.PI) * 1000));
            positionY = ((float)(Math.Sin(degrees / 360.0 * 2 * Math.PI) * 1000));

            for (int i = 0; i < 200; i++)
            {
                collisionCircle.Center.X -= positionX;
                collisionCircle.Center.Y -= positionY;

                listOfCircles.Add(new Circle(collisionCircle.Center, 1));
            }

            for (int i = 1; i <= listOfCircles.Count; i++)
            {
                listOfCircles[i - 1].Center.X += positionX * 0.99f * i;
                listOfCircles[i - 1].Center.Y += positionY * 0.99f * i;
            }

            foreach (var k in listOfCircles)
            {
                if (laserCondition)
                {
                    foreach (var o in Manager.zombieList)
                    {
                        if (Utils.IntersectCircles(k, o.hitboxCircle))
                        {
                            laserDist = Vector2.Distance(originBullet + Player.position, k.Center);
                            laserCondition = false;
                        }
                    }
                    foreach (var o in Map.drawListWalls)
                    {
                        testRectangle = new Rectangle((int)((int)k.Center.X - k.Radius), (int)(k.Center.Y - k.Radius), (int)(k.Radius * 2), (int)(k.Radius * 2));

                        if(testRectangle.Intersects(o.hitboxRectangle1) || testRectangle.Intersects(o.hitboxRectangle2))
                        {
                            laserDist = Vector2.Distance(originBullet + Player.position, testRectangle.Center.ToVector2());
                            laserCondition = false;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            //circlesToDeleteAFter = listOfCircles;    // debug
            if (laserCondition)
            {
                destinationRectangleLaser = new Rectangle((int)originBullet.X + (int)Player.position.X, (int)originBullet.Y + (int)Player.position.Y, 2500, 1);
            }
            else
            {
                destinationRectangleLaser = new Rectangle((int)originBullet.X + (int)Player.position.X, (int)originBullet.Y + (int)Player.position.Y, (int)laserDist + 3, 1);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, GameTime gameTime)
        {
            width = Texture.Width / Columns;
            height = Texture.Height / Rows;
            row = currentFrame / Columns;
            column = currentFrame % Columns;

            destinationRectanglePlayer = new Rectangle();
            sourceRectangle = new Rectangle(width * column, height * row, width, height);
            if (Player.texture == Textures.PlayerShotgunMove)
            {
                width = 539 / Columns;
                height = 284 / Rows;
            }

            if (Player.knifeEquiped)
            {
                destinationRectanglePlayer = new Rectangle((int)location.X, (int)location.Y, width, height);
                originPlayer = new Vector2(44, 35);

                angle2 = angle;
                angle2 = MathHelper.ToDegrees((float)angle2);
                posX = ((float)(Math.Cos(angle2 / 360.0 * 2 * Math.PI) * 13));
                posY = ((float)(Math.Sin(angle2 / 360.0 * 2 * Math.PI) * 13));

                diff = new Vector2(Player.position.X + originBullet.X, Player.position.Y + originBullet.Y);
                diffVec = new Vector2(Cursor.posX + posX * 15, Cursor.posY + posY * 15) - diff;
                angle = (float)Math.Atan2(diffVec.Y, diffVec.X);

                originBullet.X = (float)((Math.Cos(angle) * (Player.texture.Height / Rows) / 2) - (Math.Sin(angle) * 18));
                originBullet.Y = (float)((Math.Sin(angle) * (Player.texture.Height / Rows) / 2) + (Math.Cos(angle) * 20));
            }
            else if (Player.handgunEquiped)
            {
                destinationRectanglePlayer = new Rectangle((int)location.X, (int)location.Y, width, height);

                originPlayer = new Vector2(44, 35);

                angle2 = angle;
                angle2 = MathHelper.ToDegrees((float)angle2);
                posX = ((float)(Math.Cos(angle2 / 360.0 * 2 * Math.PI) * 13));
                posY = ((float)(Math.Sin(angle2 / 360.0 * 2 * Math.PI) * 13));

                diff = new Vector2(Player.position.X + originBullet.X, Player.position.Y + originBullet.Y);
                diffVec = new Vector2(Cursor.posX + posX * 15, Cursor.posY + posY * 15) - diff;
                angle = (float)Math.Atan2(diffVec.Y, diffVec.X);

                if (Player.texture != Textures.PlayerHandgunMeleeattack)
                {
                    originBullet.X = (float)((Math.Cos(angle) * (Player.texture.Height / Rows) / 2) - (Math.Sin(angle) * 18));
                    originBullet.Y = (float)((Math.Sin(angle) * (Player.texture.Height / Rows) / 2) + (Math.Cos(angle) * 20));
                    //originBullet += originPlayer;
                }
                else
                {
                    originBullet.X = (float)((Math.Cos(angle) * (Player.texture.Height / Rows) / 2) - (Math.Sin(angle) * 30));
                    originBullet.Y = (float)((Math.Sin(angle) * (Player.texture.Height / Rows) / 2) + (Math.Cos(angle) * 20));
                    //originBullet += originPlayer;
                }
                if(Player.laserOn) HandleLaser(posX, posY);

            }
            else if (Player.rifleEquiped)
            {
                destinationRectanglePlayer = new Rectangle((int)location.X, (int)location.Y, width, height);

                originPlayer = new Vector2(44, 35);

                if (Player.texture == Textures.PlayerRifleMove)
                {
                    destinationRectanglePlayer.Y -= 1;
                }

                angle2 = angle;
                angle2 = MathHelper.ToDegrees((float)angle2);
                posX = ((float)(Math.Cos(angle2 / 360.0 * 2 * Math.PI) * 13));
                posY = ((float)(Math.Sin(angle2 / 360.0 * 2 * Math.PI) * 13));

                diff = new Vector2(Player.position.X + originBullet.X, Player.position.Y + originBullet.Y);
                diffVec = new Vector2(Cursor.posX + posX * 15, Cursor.posY + posY * 15) - diff;
                angle = (float)Math.Atan2(diffVec.Y, diffVec.X);

                if (Player.texture != Textures.PlayerRifleMeleeattack)
                {
                    originBullet.X = (float)((Math.Cos(angle) * 1.55 * (Player.texture.Height / Rows) / 2) - (Math.Sin(angle) * 18));
                    originBullet.Y = (float)((Math.Sin(angle) * 1.55 * (Player.texture.Height / Rows) / 2) + (Math.Cos(angle) * 20));
                    ///originBullet += originPlayer;
                    /////TOHLE FIXNE ORIGIN BULLET
                }
                else
                {
                    //originPlayer = new Vector2(44, 35);
                    originBullet.X = (float)((Math.Cos(angle) * 1.55 * (Player.texture.Height / Rows) / 2) - (Math.Sin(angle) * 30));
                    originBullet.Y = (float)((Math.Sin(angle) * 1.55 * (Player.texture.Height / Rows) / 2) + (Math.Cos(angle) * 20));
                    //originBullet += originPlayer;
                    originBullet.X -= posX * 2.8f;
                    originBullet.Y -= posY * 2.8f;
                    originPlayer = new Vector2((Player.texture.Width / Columns) / 2, (Player.texture.Height / Rows) / 2);
                    originPlayer.X -= 8;
                }

                if (Player.laserOn) HandleLaser(posX, posY);
            }
            else if (Player.shotgunEquiped)
            {
                //musí to být na dva způsoby, kvůli třesu texture atlasu u shotgunMove
                if (Player.texture == Textures.PlayerShotgunMove)
                {
                    destinationRectanglePlayer = new Rectangle((int)location.X, (int)location.Y, width, height);
                    originPlayer = new Vector2(44, 35);
                    //destinationRectangleLaser = new Rectangle(destinationRectanglePlayer.X, destinationRectanglePlayer.Y, 1500, 1);

                    angle2 = angle;
                    angle2 = MathHelper.ToDegrees((float)angle2);
                    posX = ((float)(Math.Cos(angle2 / 360.0 * 2 * Math.PI) * 13));
                    posY = ((float)(Math.Sin(angle2 / 360.0 * 2 * Math.PI) * 13));

                    diff = new Vector2(Player.position.X + originBullet.X, Player.position.Y + originBullet.Y);
                    diffVec = new Vector2(Cursor.posX + posX * 15, Cursor.posY + posY * 15) - diff;
                    angle = (float)Math.Atan2(diffVec.Y, diffVec.X);

                    originBullet.X = (float)((Math.Cos(angle) * 1.55 * (284 / Rows) / 2) - (Math.Sin(angle) * 18));
                    originBullet.Y = (float)((Math.Sin(angle) * 1.55 * (284 / Rows) / 2) + (Math.Cos(angle) * 20));
                    //originBullet += originPlayer;


                    originPlayer = new Vector2(131, 100); //zaměření na střed
                }
                else
                {
                    destinationRectanglePlayer = new Rectangle((int)location.X, (int)location.Y, width, height);
                    originPlayer = new Vector2(44, 35);
                    //destinationRectangleLaser = new Rectangle(destinationRectanglePlayer.X, destinationRectanglePlayer.Y, 1500, 1);

                    if (Player.texture == Textures.PlayerShotgunMeleeattack)
                    {
                        originPlayer.Y += 28;
                        originPlayer.X += 10;
                    }

                    angle2 = angle;
                    angle2 = MathHelper.ToDegrees((float)angle2);
                    posX = ((float)(Math.Cos(angle2 / 360.0 * 2 * Math.PI) * 13));
                    posY = ((float)(Math.Sin(angle2 / 360.0 * 2 * Math.PI) * 13));

                    diff = new Vector2(Player.position.X + originBullet.X, Player.position.Y + originBullet.Y);
                    diffVec = new Vector2(Cursor.posX + posX * 15, Cursor.posY + posY * 15) - diff;
                    angle = (float)Math.Atan2(diffVec.Y, diffVec.X);



                    if (Player.texture != Textures.PlayerShotgunMeleeattack)
                    {
                        originBullet.X = (float)((Math.Cos(angle) * 1.55 * (Player.texture.Height / Rows) / 2) - (Math.Sin(angle) * 18));
                        originBullet.Y = (float)((Math.Sin(angle) * 1.55 * (Player.texture.Height / Rows) / 2) + (Math.Cos(angle) * 20));
                        //originBullet += originPlayer;
                    }
                    else
                    {
                        originBullet.X = (float)((Math.Cos(angle) * (Player.texture.Height / Rows) / 2) - (Math.Sin(angle) * 30));
                        originBullet.Y = (float)((Math.Sin(angle) * (Player.texture.Height / Rows) / 2) + (Math.Cos(angle) * 20));
                        //originBullet += new Vector2(44, 35);
                    }

                    if (Player.laserOn) HandleLaser(posX, posY);
                }
            }

            meleeattackCircle = new Circle(originBullet + Player.position, 1);

            spriteBatch.Draw(Texture, destinationRectanglePlayer, sourceRectangle, DayNight.color, (float)angle, originPlayer, SpriteEffects.None, 1);

            if ((Player.laserOn && !Player.handgunMeleeAttacking && Player.handgunEquiped && !Player.handgunReloading) || (Player.laserOn && Player.rifleEquiped && !Player.rifleMeleeAttacking && !Player.rifleReloading && !Player.moving) || (Player.laserOn && Player.shotgunEquiped && !Player.shotgunMeleeAttacking && !Player.shotgunReloading && !Player.moving))
            {
                spriteBatch.Draw(Textures.Laser, destinationRectangleLaser, null, Input.laserColor, (float)angle, Vector2.Zero, SpriteEffects.None, 1);
            }
        }
    }
}
