using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace TopDownShooterFinal
{
    enum NotLuredBehavior
    {
        eating,
        idle,
        saunter,
    }

    class Zombie : MotherClass
    {
        private int numberToNextChangeOfBehavior, indexToNearestBody;
        public int health;
        private float movementSpeed;
        private double radian;
        public double angle, randomAngle;
        private Array behaviors;
        public NotLuredBehavior behavior;
        public Texture2D texture;
        public Vector2 direction, speed;
        private Random rnd1, rnd2;
        public ZombieAnimation zombieAnimation;
        public Circle hitboxCircle, lureCircle;
        private bool isDead;
        public bool lured, attacking, running, walking, meleeAttacked;

        public Zombie()
        {
            rnd1 = new Random();
            rnd2 = new Random();
            meleeAttacked = false;
            running = false;
            walking = false;
            attacking = false;
            isDead = false;
            behaviors = Enum.GetValues(typeof(NotLuredBehavior));
            health = 100;
            movementSpeed = 100;
            behavior = NotLuredBehavior.idle;
            lured = false;
            Random rnd = new Random();
            randomAngle = rnd.Next(1, 361);
            position = new Vector2(rnd1.Next(0, Utils.screenWidth), rnd2.Next(0, Utils.screenHeigth));
            texture = Textures.ZombieIdle;
            hitboxCircle = new Circle(new Vector2(position.X, position.Y), 25);
            lureCircle = new Circle(new Vector2(position.X, position.Y), 75);
            zombieAnimation = new ZombieAnimation(texture, 4, 8);
            numberToNextChangeOfBehavior = 900;
        }
        
        public void Update(GameTime gameTime, int posX, int posY, GraphicsDevice graphicsDevice)
        {
            if (health < 1 && (zombieAnimation.Texture != Textures.ZombieDeath1 && zombieAnimation.Texture != Textures.ZombieDeath2))
            {
                hitboxCircle.Radius += 7;
                MakeZombieDie(graphicsDevice);
            }

            switch (isDead)
            {
                case true:
                    UpdateDeadZombie(gameTime);
                    
                    break;

                case false:
                    UpdateLiveZombie(gameTime, posX, posY);
                    speed = speed * 0.9f;
                    break;
            }
            zombieAnimation.Update(gameTime, this);
            hitboxCircle.Update(position);
            if (isDead)
            {
                if (zombieAnimation.Texture == Textures.ZombieDeath1)
                {
                    hitboxCircle.Center += direction * 60;
                }
                else if (zombieAnimation.Texture == Textures.ZombieDeath2)
                {
                    hitboxCircle.Center -= direction * 60;
                }

                if(Math.Abs(Math.Abs(position.X) - Math.Abs(hitboxCircle.Center.X)) > 1000)
                {
                    zombieAnimation.fadeOut = true;
                }
                if (Math.Abs(Math.Abs(position.Y) - Math.Abs(hitboxCircle.Center.Y)) > 1000)
                {
                    zombieAnimation.fadeOut = true;
                }
            }
        }

        private void UpdateLiveZombie(GameTime gameTime, int posX, int posY)
        {
            if (lured) //běh za hráčem
            {                
                radian = Math.Atan2((position.Y - posY), (position.X - posX));
                angle = (radian * (180 / Math.PI) + 360) % 360;
                direction = new Vector2((float)-Math.Cos(MathHelper.ToRadians((float)angle)), (float)-Math.Sin(MathHelper.ToRadians((float)angle)));
                direction.Normalize();
                position += direction * movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                position += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                //speed = speed * 0.9f;

                if(Vector2.Distance(position, Player.position + new Vector2(44, 35)) < 70 && !attacking)
                {
                    MakeZombieAttack();
                }
            }
            else
            {
                foreach (var k in Manager.zombieList)
                {
                    if(k != this)
                    {
                        if(Utils.IntersectCircles(hitboxCircle, k.hitboxCircle))
                        {
                            position += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                    }
                }
                numberToNextChangeOfBehavior--;
                if (numberToNextChangeOfBehavior == 0)
                {
                    NotLuredBehavior behaviorBefore = behavior;
                    numberToNextChangeOfBehavior = 900; //udělat random
                    Random random = new Random();
                    behavior = (NotLuredBehavior)behaviors.GetValue(random.Next(behaviors.Length));

                    if (behavior != behaviorBefore)
                    {
                        //dodělat
                        if (behavior == NotLuredBehavior.eating && Manager.deadZombieBodies.Count > 0)
                        {
                            
                            numberToNextChangeOfBehavior = 2700; //udělat random
                            if (behavior != NotLuredBehavior.idle) MakeZombieSaunterToDeadBody();
                        }
                        else if (behavior == NotLuredBehavior.eating && Manager.deadZombieBodies.Count == 0)
                        {
                            behavior = NotLuredBehavior.idle;
                            MakeZombieIdle();
                        }
                        else if (behavior == NotLuredBehavior.idle)
                        {
                            MakeZombieIdle();
                        }
                        else if (behavior == NotLuredBehavior.saunter)
                        {
                            Random rnd = new Random();
                            angle = rnd.Next(1, 361);
                            MakeZombieSaunter();
                        }
                    }
                }
                if (behavior == NotLuredBehavior.eating)
                {
                    CheckNearestDeadBody();
                    if (!Utils.IntersectCircles(hitboxCircle, Manager.deadZombieBodies[indexToNearestBody].hitboxCircle))
                    {
                        SaunterTodeadBodyUpdate(gameTime);
                    }
                    else if(Utils.IntersectCircles(hitboxCircle, Manager.deadZombieBodies[indexToNearestBody].hitboxCircle) && zombieAnimation.Texture != Textures.ZombieEating)
                    {
                        MakeZombieEat();
                    }
                }
                else if (behavior == NotLuredBehavior.saunter)
                {
                    ZombieSaunterUpdate(gameTime);
                }

                lureCircle.Update(position);
            }
        }

        private void UpdateDeadZombie(GameTime gameTime)
        {
            if (((zombieAnimation.Texture == Textures.ZombieDeath1 || zombieAnimation.Texture == Textures.ZombieDeath2) && zombieAnimation.currentFrame == zombieAnimation.totalFrames - 2))
            {
                if (!Manager.deadZombieBodies.Contains(this))
                {
                    if(rnd1.Next(1, 3) == 1)
                    {
                        //fade out
                        zombieAnimation.fadeOut = true;
                        Manager.deadZombieBodies.Add(this);
                        Manager.zombiesToDelete.Add(this);
                    }
                    else
                    {
                        Manager.deadZombieBodies.Add(this);
                        Manager.zombiesToDelete.Add(this);
                    }
                }
            }
        }

        public void MakeZombieAttack()
        {
            movementSpeed = 80;
            attacking = true;
            Random rnd = new Random();
            int x = rnd.Next(1, 4);
            switch (x)
            {
                case 1:
                    zombieAnimation.Texture = Textures.ZombieAttack1;
                    break;
                case 2:
                    zombieAnimation.Texture = Textures.ZombieAttack2;
                    break;
                case 3:
                    zombieAnimation.Texture = Textures.ZombieAttack3;
                    break;
            }
            zombieAnimation.Rows = 4;
            zombieAnimation.Columns = 5;
            zombieAnimation.currentFrame = 0;
            zombieAnimation.totalFrames = 4 * 5;
        }

        private void CheckNearestDeadBody()
        {
            indexToNearestBody = 0;
            List<Zombie> listOfBodies = new List<Zombie>();
            foreach (var o in Manager.deadZombieBodies)
            {
                int num = 0;
                foreach (var k in Manager.zombieList)
                {
                    if (k.indexToNearestBody == Manager.deadZombieBodies.IndexOf(o))
                    {
                        num++;
                    }
                }
                if(num < 2)
                {
                    listOfBodies.Add(o);
                }
            }
            if(listOfBodies.Count == 0)
            {
                behavior = NotLuredBehavior.idle;
                MakeZombieIdle();
            }
            else
            {
                //https://stackoverflow.com/questions/21868842/xna-get-the-nearest-distance-between-a-object-and-a-list-of-objects
                Zombie closestZombieBody = listOfBodies.OrderBy<Zombie, float> 
                    (i => Vector2.Distance(i.position, position)).ToList<Zombie>()[0];
                indexToNearestBody = Manager.deadZombieBodies.IndexOf(closestZombieBody);
            }
        }
        
        private void MakeZombieSaunterToDeadBody()
        {
            movementSpeed = 80;
            zombieAnimation.Texture = Textures.ZombieSaunter;
            zombieAnimation.Rows = 4;
            zombieAnimation.Columns = 8;
            zombieAnimation.currentFrame = 0;
            zombieAnimation.totalFrames = 4 * 8;
        }

        private void SaunterTodeadBodyUpdate(GameTime gameTime)
        {
            radian = Math.Atan2((position.Y - Manager.deadZombieBodies[indexToNearestBody].hitboxCircle.Center.Y), (position.X - Manager.deadZombieBodies[indexToNearestBody].hitboxCircle.Center.X));
            angle = (radian * (180 / Math.PI) + 360) % 360;
            direction = new Vector2((float)-Math.Cos(MathHelper.ToRadians((float)angle)), (float)-Math.Sin(MathHelper.ToRadians((float)angle)));
            direction.Normalize();
            position += direction * movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void MakeZombieEat()
        {
            zombieAnimation.Texture = Textures.ZombieEating;
            zombieAnimation.Rows = 3;
            zombieAnimation.Columns = 8;
            zombieAnimation.currentFrame = 0;
            zombieAnimation.totalFrames = 3 * 8;
        }

        private void MakeZombieIdle()
        {
            zombieAnimation.Texture = Textures.ZombieIdle;
            zombieAnimation.Rows = 4;
            zombieAnimation.Columns = 8;
            zombieAnimation.currentFrame = 0;
            zombieAnimation.totalFrames = 4 * 8;
        }

        private void MakeZombieSaunter()
        {
            movementSpeed = 80;
            zombieAnimation.Texture = Textures.ZombieSaunter;
            zombieAnimation.Rows = 4;
            zombieAnimation.Columns = 8;
            zombieAnimation.currentFrame = 0;
            zombieAnimation.totalFrames = 4 * 8;
        }

        private void ZombieSaunterUpdate(GameTime gameTime)
        {
            direction = new Vector2((float)-Math.Cos(MathHelper.ToRadians((float)angle)), (float)-Math.Sin(MathHelper.ToRadians((float)angle)));
            direction.Normalize();
            position += direction * movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void MakeZombieDie(GraphicsDevice graphicsDevice)
        {
            isDead = true;

            Random random = new Random();
            if(random.Next(1, 3) == 1)
            {
                zombieAnimation.Texture = Textures.ZombieDeath1;
                Utils.CreateBlood(this, 75, graphicsDevice);
            } 
            else
            {
                zombieAnimation.Texture = Textures.ZombieDeath2;
                Utils.CreateBlood(this, -75, graphicsDevice);
            }
                
            zombieAnimation.Rows = 3;
            zombieAnimation.Columns = 6;
            zombieAnimation.currentFrame = 0;
            zombieAnimation.totalFrames = 3 * 6;
        }

        public void MakeZombieRun()
        {
            Random rnd = new Random();
            zombieAnimation.Columns = 8;
            zombieAnimation.Rows = 4;
            zombieAnimation.Texture = Textures.ZombieRun;
            zombieAnimation.currentFrame = 0;
            zombieAnimation.totalFrames = 4 * 8;
            //rnd = new Random();
            movementSpeed = rnd.Next(240, 251);
        }

        public void MakeZombieWalk()
        {
            Random rnd = new Random();
            walking = true;
            zombieAnimation.Columns = 8;
            zombieAnimation.Rows = 4;
            zombieAnimation.Texture = Textures.ZombieWalk;
            zombieAnimation.currentFrame = 0;
            zombieAnimation.totalFrames = 4 * 8;
            //rnd = new Random();
            movementSpeed = rnd.Next(120, 141);
        }

        public void MakeZombieRunOrWalk()
        {
            Random rnd = new Random();
            if(rnd.Next(1, 3) == 1)
            {
                //run
                /*running = true;
                zombieAnimation.Columns = 8;
                zombieAnimation.Rows = 4;
                zombieAnimation.Texture = Textures.ZombieRun;
                rnd = new Random();
                movementSpeed = rnd.Next(240, 251);*/
                MakeZombieRun();
            }
            else
            {
                //walk
                MakeZombieWalk();
                /*walking = true;
                zombieAnimation.Columns = 8;
                zombieAnimation.Rows = 4;
                zombieAnimation.Texture = Textures.ZombieWalk;
                rnd = new Random();
                movementSpeed = rnd.Next(120, 141);*/
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Textures.ball2, hitboxCircle.Center - new Vector2(25, 25), Color.White);
            zombieAnimation.Draw(spriteBatch, position, angle);
            //spriteBatch.DrawString(Textures.debug, Vector2.Distance(position, Player.position + Player.playerAnimation.originPlayer) + "", new Vector2(position.X + 50, position.Y + 50), Color.White);
            /*spriteBatch.DrawString(Textures.debug, running + "", new Vector2(position.X, position.Y + 60),Color.White);
            spriteBatch.DrawString(Textures.debug, walking + "", new Vector2(position.X, position.Y + 80), Color.White);
            spriteBatch.DrawString(Textures.debug, attacking + "", new Vector2(position.X, position.Y + 100), Color.White);
            spriteBatch.DrawString(Textures.debug, isDead + "", new Vector2(position.X, position.Y + 120), Color.White);*/
            //spriteBatch.DrawString(Textures.debug, health + "", new Vector2(position.X, position.Y + 80), Color.White);
            //spriteBatch.DrawString(Textures.debug, behavior + "", new Vector2(position.X, position.Y + 160), Color.White);
            //spriteBatch.DrawString(Textures.debug, lured + "", new Vector2(position.X, position.Y + 180), Color.White);
            //spriteBatch.Draw(Textures.exp, position, Color.White);
            //spriteBatch.Draw(Textures.exp, hitboxCircle.Center, Color.White);
            //spriteBatch.DrawString(Textures.debug, Manager.deadZombieBodies.Count + "", new Vector2(600, 600), Color.White);
            //spriteBatch.DrawString(Textures.debug, position  + "",new Vector2(position.X + 5, position.Y + 5), Color.White);
            //spriteBatch.DrawString(Textures.debug, hitboxCircle.Center + "", new Vector2(position.X + 5, position.Y + 25), Color.White);
        }
    }
}
