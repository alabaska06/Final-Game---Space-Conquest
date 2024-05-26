using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Threading;

namespace Final_Game___Space_Conquest
{
    public class Game1 : Game
    {
        Texture2D floor, wall;

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

            walls = new List<Rectangle>
            {
                 new Rectangle(228, 60, 432, 57), // mech
                 new Rectangle(373, 60, 432, 57),
                 new Rectangle(228, 444, 432, 57),
                 new Rectangle(373, 444, 432, 57),
                 new Rectangle(805, -29, 432, 57), // pilot
                 new Rectangle(1237, -29, 432, 57),
                 new Rectangle(1549, -29, 432, 57), // coms
                 new Rectangle(565, 444, 432, 57), // pilot
                 new Rectangle(1140, 444, 432, 57),
                 new Rectangle(1715, 444, 432, 57), // coms
                 new Rectangle(1548, 657, 432, 57), // gar
                 new Rectangle(1668, 657, 432, 57),
                 new Rectangle(1548, 1363, 432, 57),
                 new Rectangle(1668, 1363, 432, 57),
                 new Rectangle(647, 1530, 432, 57), // kit
                 new Rectangle(1079, 1530, 432, 57)
            };

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            floor = Content.Load<Texture2D>("floor21");
            _playerTexture = Content.Load<Texture2D>("pilot");
            wall = Content.Load<Texture2D>("bluewall");

            _player = new Player(_playerTexture);
            _camera = new Camera(GraphicsDevice.Viewport);


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _player.Update(gameTime, walls);
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

            foreach (Rectangle walls in walls)
                _spriteBatch.Draw(wall, walls, Color.White);


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
