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

        Camera2D camera;

        /*
        MAP
        https://www.gamedeveloper.com/programming/procedural-dungeon-generation-algorithm

         

         
         */
        //předělat cameru... místo hejbání všeho se hejbe jen player... kamera vycentrovaná na něj

        /*
         A * algo
        https://www.youtube.com/watch?v=FflEY83irJo
        https://www.youtube.com/watch?v=j3iNy36dKxY
        https://www.youtube.com/watch?v=dlVwzKnV6FM
         
         
         
         */
        //checknout https://www.youtube.com/watch?v=aRInA1P4-fc a jeho kanál pro další užitečný tipy

        //cíl hry -> najít na mapě 10 itemů (jako ve slendermanovi), po zabití zombie se charguje bar a můžeš pak použít nápovědu na to, kde se itemy nachází, v multiplayeru padnou z enemy playera když ho zabiješ + jinak barevná šipka při nápovědě
        //výpočty úhlů ve funkcni v utils

        //naučit se pointers
        //https://www.youtube.com/watch?v=USjZcfj8yxE
        //https://www.youtube.com/watch?v=HkdAHXoRtos


        /*
        ide "analyzovat" (vyzkoušet coco dělá)
         */
        //celkově hru zrychlit
        //předělat child classy aby to bylo podle asteroidsV2 (víc fancy)
        //udělat všude private x public u proměných
        //ryhclost pohybu hráče a zombie podle povrchu
        //controls v menu (návod)
        //okomentovat úplně všechno
        //pokud se bude animation bugovat, tak to nestíhá logiku ve Draw (poté nutno dát do update)
        //udělat hodinky (asset někde najít)
        //vyskakovací oznámení pro hráče že jsou zombies pomalejší nebo rychlejší (záleží na hodinách)


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
        //tiles
        //fade na konci hry
        //dialog na začátku hry se zombie (ikona), písmo se postupně píše na obrazovku (font je jako hodiny (to zelený))
        //headshoty (one shot one kill do zombies)

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
            Manager.UpdateTracks();
            Manager.RemoveTracks();
            Manager.UpdateBlood();
            Manager.RemoveBlood();
            Utils.ScreenFlashOnHit();
            MuzzleFlashAnimation.Update();

            camera.Follow();
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

            
            //tiles první (kvůli vrstvám) -> pak vše ostatní -> pak shadows -> pak zdi a věci co dělaj stíny
            Manager.DrawTracks(_spriteBatch);
            Manager.DrawBlood(_spriteBatch);
            Manager.DrawDeadZombies(_spriteBatch);
            Manager.DrawBullets(_spriteBatch);
            Manager.DrawZombies(_spriteBatch, gameTime);
            DayNight.Draw(_spriteBatch);            //pak odstranit
            Player.Draw(_spriteBatch, gameTime); //aby byl poslední (kvůli vrstvám)
            MuzzleFlashAnimation.Draw(_spriteBatch);

            
            //zčervenání obrazovky při hitu hráče
            _spriteBatch.Draw(Textures._1x1, new Rectangle(-495, -280, Utils.screenWidth + 990, Utils.screenHeigth + 560), null, DayNight.flashColor, 0, Vector2.Zero, SpriteEffects.None, 1);
            
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
