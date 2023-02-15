using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.VisualBasic;

namespace TopDownShooterFinal
{
    enum NotLuredBehavior
    {
        eating,
        idle,
        saunter,
    }

    class Zombie
    {
        public Vector2 position;
        private int numberToNextChangeOfBehavior, indexToNearestBody, updateSight;
        public int health;
        public float movementSpeed;
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

        private Circle hitboxForPathFinder;
        
        bool sightIsBlocked;
        bool followNextPoint;

        GridTile startTile = new GridTile();
        GridTile endTile = new GridTile();
        GridTile currentTile;
        List<GridTile> pathList = new List<GridTile>();
        List<GridTile> successors = new List<GridTile>();
        List<GridTile> path = new List<GridTile>();
        List<GridTile> pathTilesToDelete = new List<GridTile>();
        List<Rectangle> nearestWalls = new List<Rectangle>();
        List<Rectangle> walls = new List<Rectangle>();

        public Zombie()
        {
            hitboxForPathFinder = new Circle(position, 10);
            updateSight = 10;
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


            //udělat takovouhle optimalizaci výkonu všude kde je to možný
            //pro bullets to udělat přes player's nearest walls
            /*walls.Clear();
            foreach(var k in Map.walls)
            {
                walls.Add(k.hitboxRectangle1);
                walls.Add(k.hitboxRectangle2);
            }
            nearestWalls = walls.FindAll(x => Vector2.Distance(position, x.Center.ToVector2()) < 3000);*/



            switch (isDead)
            {
                case true:
                    UpdateDeadZombie();

                    break;

                case false:
                    UpdateLiveZombie(gameTime, posX, posY);
                    speed = speed * 0.9f;
                    break;
            }
            zombieAnimation.Update(this);
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

                if (Math.Abs(Math.Abs(position.X) - Math.Abs(hitboxCircle.Center.X)) > 1000)
                {
                    zombieAnimation.fadeOut = true;
                }
                if (Math.Abs(Math.Abs(position.Y) - Math.Abs(hitboxCircle.Center.Y)) > 1000)
                {
                    zombieAnimation.fadeOut = true;
                }
            }
        }

        Vector2 pos1;
        Vector2 pos2;
        Vector2 pos3;
        Vector2 pos4;

        private void HandleMovement(GameTime gameTime)
        {
            position += direction * movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            /*foreach (var k in nearestWalls)
            {
                if (k.Intersects(new Rectangle((int)(hitboxCircle.Center.X - hitboxCircle.Radius), (int)(hitboxCircle.Center.Y - hitboxCircle.Radius), (int)(hitboxCircle.Radius * 2), (int)(hitboxCircle.Radius * 2))))
                {
                    pos1 = hitboxCircle.Center; //up
                    pos2 = hitboxCircle.Center; //right
                    pos3 = hitboxCircle.Center; //down
                    pos4 = hitboxCircle.Center; //left

                    for (int i = 0; i < 50; i++)
                    {
                        pos1.Y--;
                        pos2.X++;
                        pos3.Y++;
                        pos4.X--;

                        if (k.Intersects(new Rectangle((int)pos1.X, (int)pos1.Y, 5, 5)))
                        {
                            direction = position - pos1;
                            position += direction * 0.15f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                            speed += 3000 * (direction / (direction.LengthSquared() + 1)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        if (k.Intersects(new Rectangle((int)pos2.X, (int)pos2.Y, 5, 5)))
                        {
                            direction = position - pos2;
                            position += direction * 0.15f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                            speed += 3000 * (direction / (direction.LengthSquared() + 1)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        if (k.Intersects(new Rectangle((int)pos3.X, (int)pos3.Y, 5, 5)))
                        {
                            direction = position - pos3;
                            position += direction * 0.15f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                            speed += 3000 * (direction / (direction.LengthSquared() + 1)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        if (k.Intersects(new Rectangle((int)pos4.X, (int)pos4.Y, 5, 5)))
                        {
                            direction = position - pos4;
                            position += direction * 0.15f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                            speed += 3000 * (direction / (direction.LengthSquared() + 1)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                    }
                }
            }*/

            foreach (var k in Map.walls)
            {
                if (k.hitboxRectangle1.Intersects(new Rectangle((int)(hitboxCircle.Center.X - hitboxCircle.Radius), (int)(hitboxCircle.Center.Y - hitboxCircle.Radius), (int)(hitboxCircle.Radius * 2), (int)(hitboxCircle.Radius * 2))) || k.hitboxRectangle2.Intersects(new Rectangle((int)(hitboxCircle.Center.X - hitboxCircle.Radius), (int)(hitboxCircle.Center.Y - hitboxCircle.Radius), (int)(hitboxCircle.Radius * 2), (int)(hitboxCircle.Radius * 2))))
                {
                    Vector2 pos1 = hitboxCircle.Center; //up
                    Vector2 pos2 = hitboxCircle.Center; //right
                    Vector2 pos3 = hitboxCircle.Center; //down
                    Vector2 pos4 = hitboxCircle.Center; //left

                    for (int i = 0; i < 50; i++)
                    {
                        pos1.Y--;
                        pos2.X++;
                        pos3.Y++;
                        pos4.X--;

                        if (k.hitboxRectangle1.Intersects(new Rectangle((int)pos1.X, (int)pos1.Y, 5, 5)) || k.hitboxRectangle2.Intersects(new Rectangle((int)pos1.X, (int)pos1.Y, 5, 5)))
                        {
                            direction = position - pos1;
                            position += direction * 0.15f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                            speed += 3000 * (direction / (direction.LengthSquared() + 1)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        if (k.hitboxRectangle1.Intersects(new Rectangle((int)pos2.X, (int)pos2.Y, 5, 5)) || k.hitboxRectangle2.Intersects(new Rectangle((int)pos2.X, (int)pos2.Y, 5, 5)))
                        {
                            direction = position - pos2;
                            position += direction * 0.15f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                            speed += 3000 * (direction / (direction.LengthSquared() + 1)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        if (k.hitboxRectangle1.Intersects(new Rectangle((int)pos3.X, (int)pos3.Y, 5, 5)) || k.hitboxRectangle2.Intersects(new Rectangle((int)pos3.X, (int)pos3.Y, 5, 5)))
                        {
                            direction = position - pos3;
                            position += direction * 0.15f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                            speed += 3000 * (direction / (direction.LengthSquared() + 1)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        if (k.hitboxRectangle1.Intersects(new Rectangle((int)pos4.X, (int)pos4.Y, 5, 5)) || k.hitboxRectangle2.Intersects(new Rectangle((int)pos4.X, (int)pos4.Y, 5, 5)))
                        {
                            direction = position - pos4;
                            position += direction * 0.15f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                            speed += 3000 * (direction / (direction.LengthSquared() + 1)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                    }
                }
            }
        }

        Random random = new Random();

        private void UpdateLiveZombie(GameTime gameTime, int posX, int posY)
        {
            hitboxForPathFinder.Update(position);

            if (updateSight == 10)
            {
                updateSight = 0;
                sightIsBlocked = SightIsBlocked();
            }
            else updateSight++;


            if (lured && !sightIsBlocked)
            {
                if (path.Count > 0) path.Clear();

                radian = Math.Atan2((position.Y - posY), (position.X - posX));
                angle = (radian * (180 / Math.PI) + 360) % 360;
                direction = new Vector2((float)-Math.Cos(MathHelper.ToRadians((float)angle)), (float)-Math.Sin(MathHelper.ToRadians((float)angle)));
                direction.Normalize();

                HandleMovement(gameTime);


                if (Vector2.Distance(position, Player.position) < 70 && !attacking)
                {
                    MakeZombieAttack();
                }
            }
            else if (lured && sightIsBlocked)
            {
                FollowPath(gameTime);
            }
            else
            {
                foreach (var k in Manager.zombieList)
                {
                    if (k != this)
                    {
                        if (Utils.IntersectCircles(hitboxCircle, k.hitboxCircle))
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
                    
                    behavior = (NotLuredBehavior)behaviors.GetValue(random.Next(behaviors.Length));

                    if (behavior != behaviorBefore)
                    {
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
                            //Random rnd = new Random();
                            angle = random.Next(1, 361);
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
                    else if (Utils.IntersectCircles(hitboxCircle, Manager.deadZombieBodies[indexToNearestBody].hitboxCircle) && zombieAnimation.Texture != Textures.ZombieEating)
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

        private void UpdateDeadZombie()
        {
            if (((zombieAnimation.Texture == Textures.ZombieDeath1 || zombieAnimation.Texture == Textures.ZombieDeath2) && zombieAnimation.currentFrame == zombieAnimation.totalFrames - 2))
            {
                if (!Manager.deadZombieBodies.Contains(this))
                {
                    if (rnd1.Next(1, 3) == 1)
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

        List<GridTile> openList = new List<GridTile>();
        List<GridTile> closedList = new List<GridTile>();
        int smallest;
        bool b2;
        bool b4;
        bool b5;
        bool b7;
        Rectangle rec2;
        Rectangle rec4;
        Rectangle rec5;
        Rectangle rec7;


        private List<GridTile> MakePath()
        {
            
            endTile = new GridTile();
            endTile.position = Player.position;
            endTile.collisionRectangle = new Rectangle((int)endTile.position.X - 50, (int)endTile.position.Y - 50, 100, 100);
            endTile.h = 0;
            
            

            startTile = new GridTile();
            startTile.position = position;
            startTile.collisionRectangle = new Rectangle((int)startTile.position.X, (int)startTile.position.Y, 20, 20);
            startTile.h = Int32.MaxValue;
            currentTile = startTile;

            currentTile.g = 0;
            currentTile.h = (int)Math.Sqrt((currentTile.position.X - endTile.position.X) * (currentTile.position.X - endTile.position.X) + (currentTile.position.Y - endTile.position.Y) * (currentTile.position.Y - endTile.position.Y));
            currentTile.f = currentTile.h;
            openList.Clear();
            closedList.Clear();

            openList.Add(currentTile);

            while (openList.Count > 0)
            {
                smallest = Int32.MaxValue;
                foreach (var k in openList)
                {
                    if (k.f < smallest)
                    {
                        smallest = k.f;
                        currentTile = k;
                    }
                }
                if (currentTile.collisionRectangle.Intersects(endTile.collisionRectangle))
                {
                    break;
                }

                closedList.Add(currentTile);
                openList.Remove(currentTile);

                successors = new List<GridTile>();

                //bool b1 = true;
                b2 = true;
                //bool b3 = true;
                b4 = true; //přejmenovat správně a vyhodit zbytečný //nevyhazovat, udělat všechny (movement v 8 směrech)
                b5 = true;
                //bool b6 = true;
                b7 = true;
                //bool b8 = true;

                //Rectangle rec1 = new Rectangle((int)currentTile.position.X - 20, (int)currentTile.position.Y - 20, 20, 20);
                rec2 = new Rectangle((int)currentTile.position.X, (int)currentTile.position.Y - 20, 20, 20);
                //Rectangle rec3 = new Rectangle((int)currentTile.position.X + 20, (int)currentTile.position.Y - 20, 20, 20);
                rec4 = new Rectangle((int)currentTile.position.X - 20, (int)currentTile.position.Y, 20, 20);
                rec5 = new Rectangle((int)currentTile.position.X + 20, (int)currentTile.position.Y, 20, 20);
                //Rectangle rec6 = new Rectangle((int)currentTile.position.X - 20, (int)currentTile.position.Y + 20, 20, 20);
                rec7 = new Rectangle((int)currentTile.position.X, (int)currentTile.position.Y + 20, 20, 20);
                //Rectangle rec8 = new Rectangle((int)currentTile.position.X + 20, (int)currentTile.position.Y + 20, 20, 20);
                /*foreach (var k in nearestWalls)//pokud bude zombie při followpath chodit do zdí tak je chyba v tom že nejsou ty zdi v nearest walls
                {
                    //if (k.hitboxRectangle1.Intersects(rec1) || k.hitboxRectangle2.Intersects(rec1))
                    //{
                    //    b1 = false;
                    //}
                    if (k.Intersects(rec2))
                    {
                        b2 = false;
                    }
                    //if (k.hitboxRectangle1.Intersects(rec3) || k.hitboxRectangle2.Intersects(rec3))
                    //{
                    //    b3 = false;
                    //}
                    if (k.Intersects(rec4))
                    {
                        b4 = false;
                    }
                    if (k.Intersects(rec5))
                    {
                        b5 = false;
                    }
                    //if (k.hitboxRectangle1.Intersects(rec6) || k.hitboxRectangle2.Intersects(rec6))
                    //{
                    //    b6 = false;
                    //}
                    if (k.Intersects(rec7))
                    {
                        b7 = false;
                    }
                    //if (k.hitboxRectangle1.Intersects(rec8) || k.hitboxRectangle2.Intersects(rec8))
                    //{
                    //    b8 = false;
                    //}
                }*/
                foreach (var k in Map.walls)
                {
                    /*if (k.hitboxRectangle1.Intersects(rec1) || k.hitboxRectangle2.Intersects(rec1))
                    {
                        b1 = false;
                    }*/
                    if (k.hitboxRectangle1.Intersects(rec2) || k.hitboxRectangle2.Intersects(rec2))
                    {
                        b2 = false;
                    }
                    /*if (k.hitboxRectangle1.Intersects(rec3) || k.hitboxRectangle2.Intersects(rec3))
                    {
                        b3 = false;
                    }*/
                    if (k.hitboxRectangle1.Intersects(rec4) || k.hitboxRectangle2.Intersects(rec4))
                    {
                        b4 = false;
                    }
                    if (k.hitboxRectangle1.Intersects(rec5) || k.hitboxRectangle2.Intersects(rec5))
                    {
                        b5 = false;
                    }
                    /*if (k.hitboxRectangle1.Intersects(rec6) || k.hitboxRectangle2.Intersects(rec6))
                    {
                        b6 = false;
                    }*/
                    if (k.hitboxRectangle1.Intersects(rec7) || k.hitboxRectangle2.Intersects(rec7))
                    {
                        b7 = false;
                    }
                    /*if (k.hitboxRectangle1.Intersects(rec8) || k.hitboxRectangle2.Intersects(rec8))
                    {
                        b8 = false;
                    }*/
                }

                foreach (var k in openList)
                {
                    /*if (k.collisionRectangle.Intersects(rec1))
                    {
                        b1 = false;
                    }*/
                    if (k.collisionRectangle.Intersects(rec2))
                    {
                        b2 = false;
                    }
                    /*if (k.collisionRectangle.Intersects(rec3))
                    {
                        b3 = false;
                    }*/
                    if (k.collisionRectangle.Intersects(rec4))
                    {
                        b4 = false;
                    }
                    if (k.collisionRectangle.Intersects(rec5))
                    {
                        b5 = false;
                    }
                    /*if (k.collisionRectangle.Intersects(rec6))
                    {
                        b6 = false;
                    }*/
                    if (k.collisionRectangle.Intersects(rec7))
                    {
                        b7 = false;
                    }
                    /*if (k.collisionRectangle.Intersects(rec8))
                    {
                        b8 = false;
                    }*/
                }
                foreach (var k in closedList)
                {
                    /*if (k.collisionRectangle.Intersects(rec1))
                    {
                        b1 = false;
                    }*/
                    if (k.collisionRectangle.Intersects(rec2))
                    {
                        b2 = false;
                    }
                    /*if (k.collisionRectangle.Intersects(rec3))
                    {
                        b3 = false;
                    }*/
                    if (k.collisionRectangle.Intersects(rec4))
                    {
                        b4 = false;
                    }
                    if (k.collisionRectangle.Intersects(rec5))
                    {
                        b5 = false;
                    }
                    /*if (k.collisionRectangle.Intersects(rec6))
                    {
                        b6 = false;
                    }*/
                    if (k.collisionRectangle.Intersects(rec7))
                    {
                        b7 = false;
                    }
                    /*if (k.collisionRectangle.Intersects(rec8))
                    {
                        b8 = false;
                    }*/
                }
                if (b2)
                {
                    GridTile tile2 = new GridTile();
                    tile2.position = new Vector2(currentTile.position.X, currentTile.position.Y - 20);
                    tile2.h = (int)Math.Sqrt((tile2.position.X - endTile.position.X) * (tile2.position.X - endTile.position.X) + (tile2.position.Y - endTile.position.Y) * (tile2.position.Y - endTile.position.Y));
                    tile2.collisionRectangle = new Rectangle((int)tile2.position.X, (int)tile2.position.Y, 20, 20);
                    successors.Add(tile2);
                }
                if (b4)
                {
                    GridTile tile4 = new GridTile();
                    tile4.position = new Vector2(currentTile.position.X - 20, currentTile.position.Y);
                    tile4.h = (int)Math.Sqrt((tile4.position.X - endTile.position.X) * (tile4.position.X - endTile.position.X) + (tile4.position.Y - endTile.position.Y) * (tile4.position.Y - endTile.position.Y));
                    tile4.collisionRectangle = new Rectangle((int)tile4.position.X, (int)tile4.position.Y, 20, 20);
                    successors.Add(tile4);
                }
                if (b5)
                {
                    GridTile tile5 = new GridTile();
                    tile5.position = new Vector2(currentTile.position.X + 20, currentTile.position.Y);
                    tile5.h = (int)Math.Sqrt((tile5.position.X - endTile.position.X) * (tile5.position.X - endTile.position.X) + (tile5.position.Y - endTile.position.Y) * (tile5.position.Y - endTile.position.Y));
                    tile5.collisionRectangle = new Rectangle((int)tile5.position.X, (int)tile5.position.Y, 20, 20);
                    successors.Add(tile5);
                }
                if (b7)
                {
                    GridTile tile7 = new GridTile();
                    tile7.position = new Vector2(currentTile.position.X, currentTile.position.Y + 20);
                    tile7.h = (int)Math.Sqrt((tile7.position.X - endTile.position.X) * (tile7.position.X - endTile.position.X) + (tile7.position.Y - endTile.position.Y) * (tile7.position.Y - endTile.position.Y));
                    tile7.collisionRectangle = new Rectangle((int)tile7.position.X, (int)tile7.position.Y, 20, 20);
                    successors.Add(tile7);
                }

                foreach (var y in successors)
                {
                    int tentativeG = currentTile.g + 1;
                    if (tentativeG > y.g)
                    {
                        y.parent = currentTile;
                        y.g = tentativeG;
                        y.f = tentativeG + (int)Math.Sqrt((y.position.X - endTile.position.X) * (y.position.X - endTile.position.X) + (y.position.Y - endTile.position.Y) * (y.position.Y - endTile.position.Y));
                        openList.Add(y);
                    }
                }
            }
            pathList = new List<GridTile>();

            MakePathList(currentTile, pathList);

            foreach (var k in pathList)
            {
                k.collisionCircle = new Circle(k.collisionRectangle.Center.ToVector2(), 5);
            }

            
            /*foreach(var k in pathList)
            {
                foreach(var o in Map.rectanglesAroundHouses)
                //foreach(var o in Map.rectanglesAroundHouses.FindAll(x => Vector2.Distance(x.Center.ToVector2(), position) < 3000))
                {
                    if (k.collisionRectangle.Intersects(o))
                    {
                        k.collisionCircle.Center = o.Center.ToVector2();
                        Rectangle r = k.collisionRectangle;
                        k.collisionRectangle = new Rectangle(o.Center.X, o.Center.Y, r.Width, r.Height);
                        k.position = o.Center.ToVector2();
                    }
                }
            }*/

            return pathList;
        }

        private void MakePathList(GridTile tile, List<GridTile> list)
        {
            if (tile.parent != null)
            {
                list.Add(tile);
                MakePathList(tile.parent, list);
            }
        }
        
        private void FollowPath(GameTime gameTime)
        {
            foreach(var k in path)
            {
                if (Utils.IntersectCircles(hitboxForPathFinder, k.collisionCircle))
                {
                    pathTilesToDelete.Add(k);
                    followNextPoint = true;
                }
            }
            foreach(var k in pathTilesToDelete)
            {
                path.Remove(k);
            }
            pathTilesToDelete.Clear();

            if(path.Count == 0)
            {
                path = MakePath();
            }

            radian = Math.Atan2((hitboxForPathFinder.Center.Y - path[path.Count - 1].collisionCircle.Center.Y), (hitboxForPathFinder.Center.X - path[path.Count - 1].collisionCircle.Center.X));
            //radian = Math.Atan2((position.Y - path[path.Count - 1].position.Y), (position.X - path[path.Count - 1].position.X));
            angle = (radian * (180 / Math.PI) + 360) % 360;
            direction = new Vector2((float)-Math.Cos(MathHelper.ToRadians((float)angle)), (float)-Math.Sin(MathHelper.ToRadians((float)angle)));
            direction.Normalize();

            HandleMovement(gameTime);
        }

        public void MakeZombieAttack()
        {
            movementSpeed = 80;
            attacking = true;
            //Random rnd = new Random();
            int x = random.Next(1, 4);
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
        List<Zombie> listOfBodies = new List<Zombie>();
        int num;
        private void CheckNearestDeadBody()
        {
            indexToNearestBody = 0;
            listOfBodies.Clear();
            foreach (var o in Manager.deadZombieBodies)
            {
                num = 0;
                foreach (var k in Manager.zombieList)
                {
                    if (k.indexToNearestBody == Manager.deadZombieBodies.IndexOf(o))
                    {
                        num++;
                    }
                }
                if (num < 2)
                {
                    listOfBodies.Add(o);
                }
            }
            if (listOfBodies.Count == 0)
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
            foreach (var k in nearestWalls)
            {
                if(k.Intersects(new Rectangle((int)(hitboxCircle.Center.X - hitboxCircle.Radius), (int)(hitboxCircle.Center.Y - hitboxCircle.Radius), (int)(hitboxCircle.Radius * 2), (int)(hitboxCircle.Radius * 2))))
                {
                    angle -= 180;
                    direction = new Vector2((float)-Math.Cos(MathHelper.ToRadians((float)angle)), (float)-Math.Sin(MathHelper.ToRadians((float)angle)));
                    direction.Normalize();
                    position += direction * movementSpeed * 10 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    position += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                }
            }
            //HandleMovement(gameTime);
        }

        public void MakeZombieDie(GraphicsDevice graphicsDevice)
        {
            isDead = true;
            if (random.Next(1, 3) == 1)
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

            zombieAnimation.Columns = 8;
            zombieAnimation.Rows = 4;
            zombieAnimation.Texture = Textures.ZombieRun;
            zombieAnimation.currentFrame = 0;
            zombieAnimation.totalFrames = 4 * 8;
            movementSpeed = random.Next(240, 251);
        }

        public void MakeZombieWalk()
        {

            walking = true;
            zombieAnimation.Columns = 8;
            zombieAnimation.Rows = 4;
            zombieAnimation.Texture = Textures.ZombieWalk;
            zombieAnimation.currentFrame = 0;
            zombieAnimation.totalFrames = 4 * 8;
            movementSpeed = random.Next(120, 141);
        }

        public void MakeZombieRunOrWalk()
        {
            if (random.Next(1, 3) == 1)
            {
                MakeZombieRun();
            }
            else
            {
                MakeZombieWalk();
            }
        }

        bool condition;
        Circle collisionCircle;
        List<Circle> listOfCircles = new List<Circle>();
        List<Rectangle> listOfRectangles = new List<Rectangle>();
        double radian2;
        float degrees;
        float positionX;
        float positionY;
        private bool SightIsBlocked()
        {
            condition = true;
            collisionCircle = new Circle(this.position, 1);

            listOfCircles.Clear();
            listOfRectangles.Clear();

            radian2 = Math.Atan2((position.Y - (Player.position.Y)), (position.X - (Player.position.X)));
            degrees = (float)(radian2 * 180 / Math.PI);
            positionX = ((float)(Math.Cos(degrees / 360.0 * 2 * Math.PI) * 1000));
            positionY = ((float)(Math.Sin(degrees / 360.0 * 2 * Math.PI) * 1000));

            for (int i = 0; i < 200; i++)
            {
                collisionCircle.Center.X -= positionX;
                collisionCircle.Center.Y -= positionY;

                //Circle c = new Circle(collisionCircle.Center, 1);
                listOfCircles.Add(new Circle(collisionCircle.Center, 1));
            }

            for (int i = 1; i <= listOfCircles.Count; i++)
            {
                listOfCircles[i - 1].Center.X += positionX * 0.99f * i;
                listOfCircles[i - 1].Center.Y += positionY * 0.99f * i;

                listOfRectangles.Add(new Rectangle((int)listOfCircles[i - 1].Center.X, (int)listOfCircles[i - 1].Center.Y, 1, 1));
            }

            foreach (var k in listOfRectangles)
            {
                if (condition)
                {
                    if (k.Intersects(Player.hitboxRectangle))
                    {
                        return false;
                    }
                    foreach (var o in Map.drawListWalls)
                    {
                        if (k.Intersects(o.hitboxRectangle1) || k.Intersects(o.hitboxRectangle2))
                        {
                            listOfRectangles = listOfRectangles.FindAll(i => listOfRectangles.IndexOf(i) < listOfRectangles.IndexOf(k));
                            return true;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Textures.ball2, hitboxCircle.Center - new Vector2(25, 25), Color.White);
            zombieAnimation.Draw(spriteBatch, angle, this);
            /*if(path.Count > 0)
            {
                foreach(var k in path)
                {
                    spriteBatch.Draw(Textures.exp, k.collisionRectangle.Center.ToVector2(), Color.Wheat);
                }
            }
            spriteBatch.Draw(Textures.exp, position, Color.White);*/
            /*for(int i = 0; i < listOfRectangles.Count; i++)
            {
                spriteBatch.Draw(Textures.exp, listOfRectangles[i].Location.ToVector2(), Color.White);
            }*/
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
