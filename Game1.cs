using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Final_Game___Space_Conquest
{
    public class Game1 : Game
    {
        Texture2D floor, wall9;

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
            walls.Add(new Rectangle(285, 103, 486, 54));//mech
            walls.Add(new Rectangle(427, 61, 486, 54));//pil
            walls.Add(new Rectangle(395, 61, 486, 54));//coms
            walls.Add(new Rectangle(267, 269, 486, 54));
            walls.Add(new Rectangle(443, 387, 486, 54));
            walls.Add(new Rectangle(114, 148, 486, 54));
            walls.Add(new Rectangle(337, 416, 486, 54));
            walls.Add(new Rectangle(201, 70, 486, 54));
            walls.Add(new Rectangle(61, 312, 486, 54));
            walls.Add(new Rectangle(97, 258, 486, 54));
            walls.Add(new Rectangle(97, 341, 486, 54));
            walls.Add(new Rectangle(217, 403, 486, 54));
            walls.Add(new Rectangle(460, 312, 486, 54));
            walls.Add(new Rectangle(324, 82, 486, 54));
            walls.Add(new Rectangle(217, 403, 486, 54));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            floor = Content.Load<Texture2D>("floor21");
            _playerTexture = Content.Load<Texture2D>("pilot");
            wall9 = Content.Load<Texture2D>("wall9");

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
                _spriteBatch.Draw(wall9, wall, Color.White);
            

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
