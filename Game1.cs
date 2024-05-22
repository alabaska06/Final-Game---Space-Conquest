using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Final_Game___Space_Conquest
{
    public class Game1 : Game
    {
        Texture2D floor, wall3;

        private Player _player;
        private Camera _camera;

        private Texture2D _playerTexture;


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        List<Rectangle> walls;
      

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 800; 
            _graphics.PreferredBackBufferHeight = 500; 
            _graphics.ApplyChanges();

            base.Initialize();

            walls = new List<Rectangle>();
            walls.Add(new Rectangle(0, 100, 300, 75));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            floor = Content.Load<Texture2D>("floor21");
            _playerTexture = Content.Load<Texture2D>("player");
            wall3 = Content.Load<Texture2D>("wall3");

            _player = new Player(_playerTexture);
            _camera = new Camera(GraphicsDevice.Viewport);


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _player.Update(gameTime);
            _camera.Update(_player.Position);


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: _camera.Transform);
            _spriteBatch.Draw(floor, new Rectangle(0, 0, 2100, 2100), Color.White);
            _player.Draw(_spriteBatch);


            _spriteBatch.End();



            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}