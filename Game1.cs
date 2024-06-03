using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Threading;

namespace Final_Game___Space_Conquest
{
    public class Game1 : Game
    {
        Texture2D floor, wall, wallUp;

        private Player _player;
        private Camera _camera;

        private Texture2D _playerTexture;

        private Texture2D _doorTexture;
        private Texture2D _verticalDoorTexture;

        MouseState mouseState;


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        List<Rectangle> walls;

        List<Rectangle> wallsUp;

        List<door> doors;
        List<VerticalDoor> verticalDoors;


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

            doors = new List<door>//horizontal
            {
                new door(_doorTexture, new Vector2(0, 20), Vector2.UnitX, 50f),
                new door(_doorTexture, new Vector2(0, 0), Vector2.UnitX, 50f),
            };

            verticalDoors = new List<VerticalDoor>//vertical
            {
                new VerticalDoor(_verticalDoorTexture, new Vector2(0, 40), Vector2.UnitY, 50f)
            };

            walls = new List<Rectangle>
            {
                 new Rectangle(228, 60, 432, 57), // mech
                 new Rectangle(373, 60, 432, 57),
                 new Rectangle(171, 444, 432, 57),
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
                 new Rectangle(1079, 1530, 432, 57),
                 new Rectangle(1652, 1530, 432, 57),
                 new Rectangle(1665, 1530, 432, 57),
                 new Rectangle(647, 1979, 432, 57),
                 new Rectangle(1079, 1979, 432, 57),
                 new Rectangle(1511, 1979, 432, 57),
                 new Rectangle(1665, 1979, 487, 57),
                 new Rectangle(90, 657, 432, 57),//furn
                 new Rectangle(198, 657, 451, 57),
                 new Rectangle(-341, 1080, 432, 57),
                 new Rectangle(198, 1080, 451, 57),
                 new Rectangle(-1, 1305, 432, 57),//bed
                 new Rectangle(215, 1305, 432, 57),
                 new Rectangle(15, 1979, 432, 57),
                 new Rectangle(217, 1979, 432, 57),
                 new Rectangle(792, 713, 432, 57),//meet
                 new Rectangle(894, 713, 432, 57),
                 new Rectangle(792, 1248, 245, 57),
                 new Rectangle(1180, 1248, 200, 57),
            };

            wallsUp = new List<Rectangle>
            {
                 new Rectangle(173, 60, 57, 437),//mech
                 new Rectangle(774, -29, 57, 337),//pil/mech
                 new Rectangle(1330, -29, 57, 437),//coms
                 new Rectangle(1330, 62, 57, 436),
                 new Rectangle(1980, -29, 57, 437),
                 new Rectangle(1980, 62, 57, 436),
                 new Rectangle(33, 657, 57, 432),//furn
                 new Rectangle(33, 702, 57, 432),
                 new Rectangle(594, 657, 57, 432),
                 new Rectangle(594, 702, 57, 432),
                 new Rectangle(792, 712, 57, 432),//meet
                 new Rectangle(792, 869, 57, 432),
                 new Rectangle(1324, 712, 57, 432),
                 new Rectangle(1324, 869, 57, 432),
                 new Rectangle(1549, 657, 57, 432),//gar
                 new Rectangle(1549, 1231, 57, 186),
                 new Rectangle(15, 1304, 57, 432),//bed
                 new Rectangle(15, 1736, 57, 297),
                 new Rectangle(590, 1530, 57, 505),
                 new Rectangle(2094, 1530, 57, 505),//kitch
            };

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            floor = Content.Load<Texture2D>("floor21");
            _playerTexture = Content.Load<Texture2D>("pilot");
            wall = Content.Load<Texture2D>("bluewall");
            wallUp = Content.Load<Texture2D>("wallUp");
            _doorTexture = Content.Load<Texture2D>("doorIn");
            _verticalDoorTexture = Content.Load<Texture2D>("verticalDoor");

            _player = new Player(_playerTexture);
            _camera = new Camera(GraphicsDevice.Viewport);


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            _player.Update(gameTime, walls, wallsUp, doors);
            _camera.Update(_player.Position);

            Vector2 worldMousePosition = ScreenToWorld(new Vector2(mouseState.X, mouseState.Y));

            Window.Title = $"Mouse Position: {worldMousePosition.X}, {worldMousePosition.Y}";

            foreach (var door in doors)
            {
                door.Update(gameTime, mouseState, worldMousePosition);
            }

            foreach (var door in verticalDoors)
            {
                door.Update(gameTime, mouseState, worldMousePosition);
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: _camera.Transform);
            _spriteBatch.Draw(floor, new Rectangle(0, 0, 2100, 2100), Color.White);
            _player.Draw(_spriteBatch);

            foreach (var door in doors)
            {
                door.Draw(_spriteBatch);
            }

            foreach (var door in verticalDoors)
            {
                door.Draw(_spriteBatch);
            }

            foreach (Rectangle walls in walls)
                _spriteBatch.Draw(wall, walls, Color.White);

            foreach (Rectangle wall in wallsUp)
                _spriteBatch.Draw(wallUp, wall, Color.White);


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
