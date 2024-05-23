using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Final_Game___Space_Conquest
{
    public class Game1 : Game
    {
        Texture2D floor, wallmc;

        private Player _player;
        private Camera _camera;

        private Texture2D _playerTexture;

        MouseState mouseState;


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
            walls.Add(new Rectangle(210, 63, 432, 57));//mech
            walls.Add(new Rectangle(373, 63, 432, 57));//pil
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            floor = Content.Load<Texture2D>("floor21");
            _playerTexture = Content.Load<Texture2D>("pilot");
            wallmc = Content.Load<Texture2D>("wallmc2");

            _player = new Player(_playerTexture);
            _camera = new Camera(GraphicsDevice.Viewport);


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            mouseState = Mouse.GetState();
           

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _player.Update(gameTime);
            _camera.Update(_player.Position);

            Vector2 worldMousePosition = ScreenToWorld(new Vector2(mouseState.X, mouseState.Y));

            Window.Title = $"Mouse Position: {worldMousePosition.X}, {worldMousePosition.Y}";

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: _camera.Transform);
            _spriteBatch.Draw(floor, new Rectangle(0, 0, 2100, 2100), Color.White);
            _player.Draw(_spriteBatch);

            foreach (Rectangle wall in walls)
                _spriteBatch.Draw(wallmc, wall, Color.White);
            

            _spriteBatch.End();



            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, Matrix.Invert(_camera.Transform));
        }


    }
}
