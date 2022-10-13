using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using Microsoft.Xna.Framework;

namespace TopDownShooterFinal
{
    internal class Laser
    {
        private Rectangle destinationRectangle;
        private double angle;
        private Circle collisionCircle;

        private List<Circle> circlesToDeleteAFter;
        //private int num;

        public Laser()
        {
            //num = index;
            //nefunguje
        }

        public void Update(float pX, float pY, double angle)
        {
            float dist = 0;
            bool con = true;
            this.angle = angle;
            collisionCircle = new Circle(Player.position, 1);

            List<Circle> listOfCircles = new List<Circle>();
            //
            angle = MathHelper.ToDegrees((float)angle);
            float posX = ((float)(Math.Cos(angle / 360.0 * 2 * Math.PI) * 13));
            float posY = ((float)(Math.Sin(angle / 360.0 * 2 * Math.PI) * 13));
            //

            double radian2 = Math.Atan2((Player.playerAnimation.originBullet.Y + Player.position.Y - (Cursor.posY + posY * 40)), (Player.playerAnimation.originBullet.X + Player.position.X - (Cursor.posX + posX * 40)));
            float degrees = (float)(radian2 * 180 / Math.PI);
            float positionX = ((float)(Math.Cos(degrees / 360.0 * 2 * Math.PI) * 1000));
            float positionY = ((float)(Math.Sin(degrees / 360.0 * 2 * Math.PI) * 1000));

            for (int i = 0; i < 200; i++)
            {
                collisionCircle.Center.X -= positionX;
                collisionCircle.Center.Y -= positionY;

                Circle c = new Circle(collisionCircle.Center, 1);
                listOfCircles.Add(c);
            }

            for (int i = 1; i <= listOfCircles.Count; i++)
            {
                listOfCircles[i - 1].Center.X += positionX * 0.99f * i;
                listOfCircles[i - 1].Center.Y += positionY * 0.99f * i;
            }

            foreach (var k in listOfCircles)
            {
                if (con)
                {
                    foreach (var o in Manager.zombieList)
                    {
                        if (Utils.IntersectCircles(k, o.hitboxCircle))
                        {
                            dist = Vector2.Distance(Player.playerAnimation.originBullet + Player.position, k.Center);
                            con = false;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            if (con)
            {
                destinationRectangle = new Rectangle((int)Player.playerAnimation.originBullet.X + (int)Player.position.X, (int)Player.playerAnimation.originBullet.Y + (int)Player.position.Y, 2500, 1);
            }
            else
            {
                destinationRectangle = new Rectangle((int)Player.playerAnimation.originBullet.X + (int)Player.position.X, (int)Player.playerAnimation.originBullet.Y + (int)Player.position.Y, (int)dist + 3, 1);
            }
            circlesToDeleteAFter = listOfCircles;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var k in circlesToDeleteAFter)
            {
                spriteBatch.Draw(Textures.exp, k.Center, Color.White);
            }
            spriteBatch.Draw(Textures.Laser, destinationRectangle, null, Input.laserColor, (float)angle, Vector2.Zero, SpriteEffects.None, 1);
        }
    }
}
