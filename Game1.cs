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
            Utils.SetUpListOfGroundCracks();
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
            Map.Update(gameTime);
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
            Utils.CheckCollisionBetweenBulletsAndWalls();
            Utils.MeleeattackCheck(graphicsDevice);
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
            _spriteBatch.Begin(transformMatrix: camera.Transform);
            Map.DrawGround(_spriteBatch);
            Manager.DrawTracks(_spriteBatch);
            Manager.DrawBlood(_spriteBatch);
            Manager.DrawDeadZombies(_spriteBatch);
            Manager.DrawBullets(_spriteBatch);
            Manager.DrawZombies(_spriteBatch, gameTime);
            DayNight.Draw(_spriteBatch);            
            Player.Draw(_spriteBatch, gameTime); 
            MuzzleFlashAnimation.Draw(_spriteBatch);
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
