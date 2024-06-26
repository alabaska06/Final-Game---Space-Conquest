﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace Final_Game___Space_Conquest
{
    public class Game1 : Game
    {
        Texture2D floor, wall, wallUp;
        Texture2D carpet;

        public static Game1 self;

        public List<Bot> _bots;
        private Texture2D _botTexture;
        private Texture2D _projectileTexture;

        private Player _player;
        private Camera _camera;

        private Texture2D _playerTexture;
        private Texture2D _exitTexture;
        private Texture2D _exitTexture2;

        private Texture2D _speedBoost;
        private Texture2D _instaKill;

        private Texture2D _deadPlayerTexture;
        private Texture2D _deadAlienTexture;
        private Texture2D greyhealthTexture;
        private Texture2D greenHealthTexture;
        private Texture2D gameOverTexture;

        private Texture2D _doorTexture;
        private Texture2D _verticalDoorTexture;

        private Texture2D introScreen;
        private Rectangle introSize;

        private Texture2D youWon;

        MouseState mouseState;
        SpriteFont _font;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        List<Rectangle> walls;

        List<Rectangle> wallsUp;

        List<door> doors;
        List<VerticalDoor> verticalDoors;

        List<Projectile> projectiles;
        List<PlayerProjectile> playerProjectile;
        List<Rectangle> InstaKills;
        List<Rectangle> SpeedBoosts;
        private List<GameObjects> _gameObjects;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            self = this;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
            projectiles = new List<Projectile>();
            playerProjectile = new List<PlayerProjectile>();

            introSize = new Rectangle(684, 119, 800, 500);

            base.Initialize();

            _camera = new Camera(GraphicsDevice.Viewport);
            _player = new Player(_playerTexture, _deadPlayerTexture, greyhealthTexture, _projectileTexture, _camera, _font, gameOverTexture, _exitTexture, _exitTexture2);

            _bots = new List<Bot>
            {
                new Bot(_botTexture, _deadAlienTexture, greyhealthTexture, new Vector2(249, 142), _player, _projectileTexture),
                new Bot(_botTexture, _deadAlienTexture, greyhealthTexture, new Vector2(322, 308), _player, _projectileTexture),
                new Bot(_botTexture, _deadAlienTexture, greyhealthTexture, new Vector2(643, 267), _player, _projectileTexture),
                new Bot(_botTexture, _deadAlienTexture, greyhealthTexture, new Vector2(1589, 167), _player, _projectileTexture),
                new Bot(_botTexture, _deadAlienTexture, greyhealthTexture, new Vector2(586, 529), _player, _projectileTexture),
                new Bot(_botTexture, _deadAlienTexture, greyhealthTexture, new Vector2(1825, 75), _player, _projectileTexture),
                new Bot(_botTexture, _deadAlienTexture, greyhealthTexture, new Vector2(1825, 301), _player, _projectileTexture),
                new Bot(_botTexture, _deadAlienTexture, greyhealthTexture, new Vector2(1919, 1133), _player, _projectileTexture),
                new Bot(_botTexture, _deadAlienTexture, greyhealthTexture, new Vector2(1624, 754), _player, _projectileTexture),
                new Bot(_botTexture, _deadAlienTexture, greyhealthTexture, new Vector2(1965, 1624), _player, _projectileTexture),
                new Bot(_botTexture, _deadAlienTexture, greyhealthTexture, new Vector2(748, 1822), _player, _projectileTexture),
                new Bot(_botTexture, _deadAlienTexture, greyhealthTexture, new Vector2(1269, 1679), _player, _projectileTexture),
                new Bot(_botTexture, _deadAlienTexture, greyhealthTexture, new Vector2(870, 829), _player, _projectileTexture),
                new Bot(_botTexture, _deadAlienTexture, greyhealthTexture, new Vector2(93, 851), _player, _projectileTexture),
                new Bot(_botTexture, _deadAlienTexture, greyhealthTexture, new Vector2(887, 1137), _player, _projectileTexture),
                new Bot(_botTexture, _deadAlienTexture, greyhealthTexture, new Vector2(450, 1865), _player, _projectileTexture),
                new Bot(_botTexture, _deadAlienTexture, greyhealthTexture, new Vector2(367, 1585), _player, _projectileTexture),
                new Bot(_botTexture, _deadAlienTexture, greyhealthTexture, new Vector2(113, 956), _player, _projectileTexture),
                new Bot(_botTexture, _deadAlienTexture, greyhealthTexture, new Vector2(450, 1189), _player, _projectileTexture),
            };

            InstaKills = new List<Rectangle>
            {
                new Rectangle(527, 748, 45, 45),
                new Rectangle(1229, 1164, 45, 45),
                new Rectangle(245, 1911, 45, 45),
                new Rectangle(2039, 1458, 45, 45),
                new Rectangle(673, 1909, 45, 45),
                new Rectangle(1626, 1300, 45, 45),
                new Rectangle(1404, 46, 45, 45),
                new Rectangle(300, 300, 45, 45),


            };
            SpeedBoosts = new List<Rectangle>
            {
                new Rectangle(400, 400, 45, 45),
                new Rectangle(75, 1202, 45, 45),
                new Rectangle(352, 1574, 45, 45),
                new Rectangle(2009, 1762, 45, 45),
                new Rectangle(1437, 1035, 45, 45),
                new Rectangle(1899, 552, 45, 45),
                new Rectangle(1694, 378, 45, 45),
            };

            _gameObjects = new List<GameObjects>
            {
                new GameObjects(Content.Load<Texture2D>("chair"), new Vector2(977, 961)),
                new GameObjects(Content.Load<Texture2D>("chair"), new Vector2(1126, 940)),
                new GameObjects(Content.Load<Texture2D>("chair"), new Vector2(1048, 1060)),
                new GameObjects(Content.Load<Texture2D>("chair"), new Vector2(1126, 1028)),
                new GameObjects(Content.Load<Texture2D>("chair"), new Vector2(981, 1020)),
                new GameObjects(Content.Load<Texture2D>("chair"), new Vector2(1040, 917)),
                new GameObjects(Content.Load<Texture2D>("table"), new Vector2(1015, 936)),
                new GameObjects(Content.Load<Texture2D>("shower"), new Vector2(72, 1835)),
                new GameObjects(Content.Load<Texture2D>("kitStove"), new Vector2(646, 1661)),
                new GameObjects(Content.Load<Texture2D>("kitfridge"), new Vector2(646, 1586)),
                new GameObjects(Content.Load<Texture2D>("kitTable"), new Vector2(885, 1758)),
                new GameObjects(Content.Load<Texture2D>("sink"), new Vector2(313, 1832)),
                new GameObjects(Content.Load<Texture2D>("toilet"), new Vector2(169, 1832)),
                new GameObjects(Content.Load<Texture2D>("bench"), new Vector2 (967, 768)),
                new GameObjects(Content.Load<Texture2D>("barChair"), new Vector2(1527, 1800)),
                new GameObjects(Content.Load<Texture2D>("barChair"), new Vector2(1636, 1846)),
                new GameObjects(Content.Load<Texture2D>("barChair"), new Vector2(1733, 1846)),
                new GameObjects(Content.Load<Texture2D>("barChair"), new Vector2(1816, 1827)),
                new GameObjects(Content.Load<Texture2D>("barChair"), new Vector2(1930, 1816)),
                new GameObjects(Content.Load<Texture2D>("barChair"), new Vector2(2008, 1846)),
                new GameObjects(Content.Load<Texture2D>("bar"), new Vector2(1494, 1863)),
                new GameObjects(Content.Load<Texture2D>("console"), new Vector2(933, 30)),
                new GameObjects(Content.Load<Texture2D>("consoleChairs"), new Vector2 (1022, 127)),
                new GameObjects(Content.Load<Texture2D>("consoleChair"), new Vector2 (840, 152)),
                new GameObjects(Content.Load<Texture2D>("consoleChair"), new Vector2 (970, 245)),
                new GameObjects(Content.Load<Texture2D>("consoleChair"), new Vector2 (1099, 245)),
                new GameObjects(Content.Load<Texture2D>("consoleChair"), new Vector2 (1240, 152)),
                new GameObjects(Content.Load<Texture2D>("bed"), new Vector2 (71, 1358)),
                new GameObjects(Content.Load<Texture2D>("bed"), new Vector2 (216, 1358)),
                new GameObjects(Content.Load<Texture2D>("bed"), new Vector2 (347, 1358)),
                new GameObjects(Content.Load<Texture2D>("bedDown"), new Vector2 (71, 1591)),
                new GameObjects(Content.Load<Texture2D>("bedDown"), new Vector2 (216, 1591)),
                new GameObjects(Content.Load<Texture2D>("rover"), new Vector2(1726, 825)),

            };

            doors = new List<door>//horizontal
            {
                new door(_doorTexture, new Vector2(995, 485), Vector2.UnitX, 70f),
                new door(_doorTexture, new Vector2(1570, 443), Vector2.UnitX, 70f),
                new door(_doorTexture, new Vector2(88, 1088), Vector2.UnitX, 70f),
                new door(_doorTexture, new Vector2(1034, 1249), Vector2.UnitX, 70f),
                new door(_doorTexture, new Vector2(1509, 1563), Vector2.UnitX, 70f),
                new door(_doorTexture, new Vector2(448, 1774), Vector2.UnitX, 70f),
            };

            verticalDoors = new List<VerticalDoor>//vertical
            {
                new VerticalDoor(_verticalDoorTexture, new Vector2(793, 307), Vector2.UnitY, 70f),
                new VerticalDoor(_verticalDoorTexture, new Vector2(630, 1361), Vector2.UnitY, 70f),
                new VerticalDoor(_verticalDoorTexture, new Vector2(1549, 1088), Vector2.UnitY, 70f),
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
                 new Rectangle(17, 1774, 432, 57),
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
                 new Rectangle(590, 1506, 57, 505),
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
            carpet = Content.Load<Texture2D>("carpet");
            _exitTexture = Content.Load<Texture2D>("EXIT");
            _exitTexture2 = Content.Load<Texture2D>("EXIT2");
            _botTexture = Content.Load<Texture2D>("alienTest");
            _projectileTexture = Content.Load<Texture2D>("projectile");
            _deadPlayerTexture = Content.Load<Texture2D>("deadAstronaut");
            _deadAlienTexture = Content.Load<Texture2D>("deadAlien");
            greyhealthTexture = Content.Load<Texture2D>("healthBarWhite");
            greenHealthTexture = Content.Load<Texture2D>("healthBarGreen");
            gameOverTexture = Content.Load<Texture2D>("gameOver");
            _font = Content.Load<SpriteFont>("_font");
            _speedBoost = Content.Load<Texture2D>("speedBoost");
            _instaKill = Content.Load<Texture2D>("instaKill");
            introScreen = Content.Load<Texture2D>("introToGame");
            youWon = Content.Load<Texture2D>("youWon");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _player.Update(gameTime, walls, wallsUp, doors, verticalDoors, _gameObjects, _bots, projectiles, _projectileTexture);
            _camera.Update(_player.Position);

            for (int i = 0; i < InstaKills.Count; i++)
            {
                if (InstaKills[i].Intersects(_player.BoundingBox))
                {
                    InstaKills.RemoveAt(i);
                    _player.CollectPowerUp();
                    break;
                }

            }
            for (int i = 0; i < SpeedBoosts.Count; i++)
            {
                if (SpeedBoosts[i].Intersects(_player.BoundingBox))
                {
                    SpeedBoosts.RemoveAt(i);
                    _player.CollectSpeedPowerUp();
                    break;
                }

            }
            foreach (var bot in _bots)
            {
                bot.Update(gameTime, _camera, walls, wallsUp, doors, verticalDoors, _bots, _gameObjects);

                foreach (var projectile in bot.Projectiles)
                {
                    if (projectile.BoundingBox.Intersects(_player.BoundingBox))
                    {
                        _player.TakeDamage(0.1);
                        bot.Projectiles.Remove(projectile);
                        break;
                    }
                }
            }
            for (int i = _player.Projectiles.Count - 1; i >= 0; i--)
            {
                var projectile = _player.Projectiles[i];

                foreach (var bot in _bots)
                {
                    if (projectile.BoundingBox.Intersects(bot.BoundingBox))
                    {
                        bot.TakeDamage(_player.Damage);
                        _player.Projectiles.RemoveAt(i);
                        break;
                    }
                }
            }

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

            foreach (var gameObject in _gameObjects)
            {
                gameObject.Update(gameTime);
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(transformMatrix: _camera.Transform);
            _spriteBatch.Draw(floor, new Rectangle(0, 0, 2100, 2100), Color.White);
            _spriteBatch.Draw(carpet, new Rectangle(157, 1870, 280, 90), Color.White);

            foreach (var bot in _bots)
            {
                bot.Draw(_spriteBatch);
            }
            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw(_spriteBatch);
            }
            foreach (PlayerProjectile projectile in playerProjectile)
            {
                projectile.Draw(_spriteBatch);
            }
            foreach (var door in doors)
            {
                door.Draw(_spriteBatch);
            }

            foreach (var door in verticalDoors)
            {
                door.Draw(_spriteBatch);
            }

            foreach (var gameObject in _gameObjects)
            {
                gameObject.Draw(_spriteBatch);
            }

            foreach (var instaKill in InstaKills)
            {
                _spriteBatch.Draw(_instaKill, instaKill, Color.White);
            }
            foreach (var speedBoost in SpeedBoosts)
            {
                _spriteBatch.Draw(_speedBoost, speedBoost, Color.White);
            }

            foreach (Rectangle walls in walls)
                _spriteBatch.Draw(wall, walls, Color.White);

            foreach (Rectangle wall in wallsUp)
                _spriteBatch.Draw(wallUp, wall, Color.White);

            _player.Draw(_spriteBatch);
            _player.DrawTexture(_spriteBatch, _spriteBatch);
            _player.DrawTextureRight(_spriteBatch);

            _spriteBatch.Draw(introScreen, introSize, Color.White);
            
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                introSize = new Rectangle(-1000, -1000, 2, 2);
            }

            if (_bots.TrueForAll(Bot => Bot.IsDead))
            {
                _spriteBatch.Draw(youWon, new Rectangle((int)(_camera._center.X), (int)(_camera._center.Y), _camera.ViewportWidth, _camera.ViewportHeight), Color.White);
            }

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
