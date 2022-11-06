using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace TopDownShooterFinal
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static Camera2D camera;

        /*
        MAP
        https://www.gamedeveloper.com/programming/procedural-dungeon-generation-algorithm
        
        https://community.monogame.net/t/fast-tilemap/13363/4       <------------- UDĚLAT
         
        https://ansiware.com/tutorial-part-6-create-a-map-and-map-generator/
        https://www.freecodecamp.org/news/how-to-make-your-own-procedural-dungeon-map-generator-using-the-random-walk-algorithm-e0085c8aa9a/
         */


        /*
         A * algo
        https://dotnetcoretutorials.com/2020/07/25/a-search-pathfinding-algorithm-in-c/
        https://gigi.nullneuron.net/gigilabs/a-pathfinding-example-in-c/
        source
        https://community.monogame.net/t/pathfinding-with-aabb-collision-detection/10237/5
        https://gamedev.stackexchange.com/questions/58963/pathfinding-with-2d-non-grid-based-movement-over-uniform-terrain
         ------------------> https://www.geeksforgeeks.org/a-search-algorithm/
         
         */
        //checknout https://www.youtube.com/watch?v=aRInA1P4-fc a jeho kanál pro další užitečný tipy

        //cíl hry -> najít na mapě 10 itemů (jako ve slendermanovi), po zabití zombie se charguje bar a můžeš pak použít nápovědu na to, kde se itemy nachází, v multiplayeru padnou z enemy playera když ho zabiješ + jinak barevná šipka při nápovědě
        //výpočty úhlů ve funkcni v utils

        //naučit se pointers
        //https://www.youtube.com/watch?v=USjZcfj8yxE
        //https://www.youtube.com/watch?v=HkdAHXoRtos
        //pohyb vody
        //různý baráky z různejch materiálů
        /*
        ide "analyzovat" (vyzkoušet coco dělá)
         */
        //player se v keři skryje
        //celkově hru zrychlit, night vision
        //předělat child classy aby to bylo podle asteroidsV2 (víc fancy)
        //udělat všude private x public u proměných
        //ryhclost pohybu hráče a zombie podle povrchu
        //controls v menu (návod)
        //okomentovat úplně všechno
        //pokud se bude animation bugovat, tak to nestíhá logiku ve Draw (poté nutno dát do update)
        //udělat hodinky (asset někde najít)
        //vyskakovací oznámení pro hráče že jsou zombies pomalejší nebo rychlejší (záleží na hodinách)

        //fixnout když zombie umře u zdi -> skrz zeď padne
        //vrstvy pro draw (jen jeden spritebatch begin a end)
        //shake obrazovky při výbuchu (udělat v camera2D)
        //zvuky
        //bullet náraz do objektu
        //gametime přidat do pohybu
        //multiplayer, tiles, drop ammunitionu při zabití enemy
        //při nárazu bullet do něčeho jiskry, itemy různý
        //vlastní buttony, highscore tabulka (v multiplayeru)
        //empty chambers (od bullet na zemi) (fade všech po nějaký době)
        //světlo (lampy, světlo na zbrani)
        //stavění barikád
        //open world
        //GameTime poštelovat pro zastavení hry
        //multiplayer pvp 
        //fade na konci hry
        //dialog na začátku hry se zombie (ikona), písmo se postupně píše na obrazovku (font je jako hodiny (to zelený))
        //všude daynight color zkontrolovat kde má být
      
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            
            Utils.SetUpScreen(_graphics);

            camera = new Camera2D();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Textures.Load(this.Content);
            Utils.SetUpTracks();
            Map.InitializeWater();
            Map.InitializeGround();
            Map.CreateHouses();
        }

        protected override void Update(GameTime gameTime)
        {
            switch (Manager.gameState)
            {
                case GameState.MainMenu:
                    UpdateMainMenu();
                    break;

                case GameState.Gameplay:
                    UpdateGameplay(gameTime, _spriteBatch.GraphicsDevice);
                    break;

                case GameState.EndOfGame:
                    UpdateEndOfGame();
                    break;
            }

            //base.Update(gameTime);
        }

        void UpdateMainMenu()
        {

        }

        void UpdateGameplay(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            Input.Update(gameTime);
            Player.Update(gameTime);
            Utils.CheckLure();
            Manager.UpdateBullets(gameTime);
            Manager.DeleteBullets();
            Manager.UpdateZombies(gameTime, graphicsDevice);
            Manager.DeleteZombies();
            Utils.CheckCollisionBetweenZombies(gameTime);
            Utils.CheckCollisionBetweenPlayerAndZombies(gameTime);
            Utils.CheckCollisionBetweenZombiesAndBullets(graphicsDevice);
            Utils.MeleeattackCheck(graphicsDevice);
            Manager.UpdateListOfAllObjects();
            if(!Player.onHitFlash) DayNight.Update();
            Manager.UpdateDeadZombies(gameTime, graphicsDevice);
            Manager.DeleteZombieBodies();

            /*
            If the users computer cannot finish a Update in the allotted frame slice how can you expect it to have any time left to draw. IsRunningSlowly is always being set here every frame so somehow it’s still drawing on emergency lifesupport.
            https://community.monogame.net/t/lag-if-update-takes-longer-than-frametime/12054/5
            */
            /*if (!gameTime.IsRunningSlowly)
            {
                
            }*/
            Manager.UpdateTracks();
            Manager.RemoveTracks();
            /*if (!gameTime.IsRunningSlowly)
            {
                
            }*/
            Manager.UpdateBlood();
            Manager.RemoveBlood();
            Utils.ScreenFlashOnHit();
            MuzzleFlashAnimation.Update();
            Map.Update(gameTime);
            

            camera.Follow(gameTime);
        }

        void UpdateEndOfGame()
        {

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            
            switch (Manager.gameState)
            {
                case GameState.MainMenu:
                    DrawMainMenu();
                    break;

                case GameState.Gameplay:
                    DrawGameplay(gameTime);
                    break;

                case GameState.EndOfGame:
                    DrawEndOfGame();
                    break;
            }

            //base.Draw(gameTime);
        }

        void DrawMainMenu()
        {

        }

        void DrawGameplay(GameTime gameTime)
        {
            //pokud chci něco vykreslit fixovaně tak použít novej spritebatch begin bez transformu
            

            _spriteBatch.Begin(transformMatrix: camera.Transform);


            Map.DrawGround(_spriteBatch);

            //tiles první (kvůli vrstvám) -> pak vše ostatní -> pak shadows -> pak zdi a věci co dělaj stíny
            Manager.DrawTracks(_spriteBatch);
            Manager.DrawBlood(_spriteBatch);
            Manager.DrawDeadZombies(_spriteBatch);
            Manager.DrawBullets(_spriteBatch);
            Manager.DrawZombies(_spriteBatch, gameTime);
            DayNight.Draw(_spriteBatch);            //pak odstranit
            Player.Draw(_spriteBatch, gameTime); //aby byl poslední (kvůli vrstvám)
            MuzzleFlashAnimation.Draw(_spriteBatch);

            _spriteBatch.DrawString(Textures.debug, Player.position + "", Player.position, Color.White);
            _spriteBatch.Draw(Textures.exp, Player.position, Color.White);
            Map.DrawWalls(_spriteBatch);
            //zčervenání obrazovky při hitu hráče
            _spriteBatch.Draw(Textures._1x1, new Rectangle((int)Player.position.X - 1300, (int)Player.position.Y - 750, Utils.screenWidth + 1300, Utils.screenHeigth + 750), null, DayNight.flashColor, 0, Vector2.Zero, SpriteEffects.None, 1);

            _spriteBatch.End();
            
            _spriteBatch.Begin();
            WeaponIcons.Draw(_spriteBatch);
            _spriteBatch.End();
        }

        void DrawEndOfGame()
        {

        }
    }
}
