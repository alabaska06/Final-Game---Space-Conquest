﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Final_Game___Space_Conquest
{
    public class Player
    {
        public Vector2 Position;
        private Texture2D _texture;
        private Texture2D _texture2;//takes exit for left
        private Texture2D _texture3;//takes exit for right
        private Texture2D _deadTexture;
        private Texture2D _healthBarTexture;
        private float _speed;
        private float _rotation;
        Rectangle exitRect, exitRect2;
        private double _maxHealth;
        private double _currentHealth;
        private bool _isDead;
        private Texture2D _playerProjectileTexture;

        public int Damage { get; private set; } = 1;
        private int defaultDamage = 1;
        private int poweredUpDamage = 5;
        private double powerUpDuration = 10;
        private double powerUpTimer = 0;
        private bool isPoweredUp = false;

        private float defaultSpeed = 5f;
        private float poweredUpSpeed = 8f;
        private double speedPowerUpDuration = 10;
        private double speedPowerUpTimer;
        private bool isSpeedPoweredUp = false;

        private List<PlayerProjectile> _playerProjectiles;
        public List<PlayerProjectile> Projectiles => _playerProjectiles;

        private TimeSpan _shootCoolDown;
        private TimeSpan _lastShootTime;

        private Camera _camera;

        private GameOverScreen _gameOverScreen;

        List<Rectangle> ExitRects;
 
      
        public bool IsDead => _isDead;

        private double gameOverTimer;
        private bool gameOverDisplayed;


        private bool _mousePressed = false;
        public Rectangle BoundingBox;

        public Player(Texture2D texture, Texture2D deadTexture, Texture2D healthBarTexture, Texture2D playerProjectileTexture, Camera camera, SpriteFont font, Texture2D gameOverBackgroundTexture, Texture2D exitTexture, Texture2D exitTexture2, int maxHealth = 5)
        {
            _texture = texture;
            _deadTexture = deadTexture;
            _healthBarTexture = healthBarTexture;
            _texture2 = exitTexture;
            _texture3 = exitTexture2;
            Position = new Vector2(1034, 319);
            _speed = 6f;
            _rotation = 0f;
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
            _isDead = false;
            _gameOverScreen = new GameOverScreen(font, gameOverBackgroundTexture);
            _camera = camera;
            _shootCoolDown = TimeSpan.FromSeconds(0.01);
            _lastShootTime = TimeSpan.Zero;
            _playerProjectiles = new List<PlayerProjectile>();
            _playerProjectileTexture = playerProjectileTexture;

            gameOverTimer = 0;
            gameOverDisplayed = false;
            UpdateBoundingBox();
        }

        public void Update(GameTime gameTime, List<Rectangle> walls, List<Rectangle> wallsUp, List<door> doors, List<VerticalDoor> verticalDoors, List<GameObjects> gameObjects, List<Bot> bots, List<Projectile> _projectiles, Texture2D projectileTexture)
        {
            if (_isDead)
            {
                gameOverTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (gameOverTimer >= 2 && !gameOverDisplayed)
                {
                    gameOverDisplayed = true;
                }
                return;
            }
            if (gameTime.TotalGameTime - _lastShootTime > _shootCoolDown)
            {
                _lastShootTime = gameTime.TotalGameTime;
                MouseState mouseState = Mouse.GetState();
                if (mouseState.LeftButton == ButtonState.Pressed && !_mousePressed)
                {
                    ShootAtMouse();

                    _mousePressed = true;
                }
                else if (mouseState.LeftButton == ButtonState.Released)
                {
                    _mousePressed = false;
                }
            }
            foreach (var projectile in _playerProjectiles)
            {
                projectile.Update(gameTime, walls, wallsUp, doors, verticalDoors, gameObjects, bots);
            }
            for (int i = _playerProjectiles.Count - 1; i >= 0; i--)
            {
                if (_playerProjectiles[i].Update(gameTime, walls, wallsUp, doors, verticalDoors, gameObjects, bots))
                {
                    _playerProjectiles.RemoveAt(i);
                }
            }
            KeyboardState state = Keyboard.GetState();

            exitRect = new Rectangle(170, 500, 65, 165);

            ExitRects = new List<Rectangle>
                {
                    new Rectangle(-40, 1136, 65, 165),
                    new Rectangle(1978, 500, 65, 165),
                    new Rectangle(2099, 1400, 65, 165),
                };

            Vector2 direction = Vector2.Zero;

            if (state.IsKeyDown(Keys.W))
                direction.Y -= _speed;
            if (state.IsKeyDown(Keys.S))
                direction.Y += _speed;
            if (state.IsKeyDown(Keys.A))
                direction.X -= _speed;
            if (state.IsKeyDown(Keys.D))
                direction.X += _speed;

            if (direction != Vector2.Zero)
            {
                direction.Normalize();
                Vector2 newPosition = Position + direction * _speed;
                Rectangle newBoundingBox = new Rectangle((int)newPosition.X, (int)newPosition.Y, BoundingBox.Width, BoundingBox.Height);

                if (!IsCollidingWithWalls(newBoundingBox, walls, wallsUp) && !IsCollidingWithDoors(newBoundingBox, doors, verticalDoors) && !IsCollidingWithGameObjects(newBoundingBox, gameObjects) /*&& !IsCollidingWithBots(newBoundingBox, bots)*/)
                {
                    Position = newPosition;
                    _rotation = (float)Math.Atan2(direction.Y, direction.X);
                    UpdateBoundingBox();

                }
            }

            if (isPoweredUp)
            {
                powerUpTimer -= gameTime.ElapsedGameTime.TotalSeconds;
                if (powerUpTimer <= 0)
                {
                    Damage = defaultDamage;
                    isPoweredUp = false;
                }
            }

            if (isSpeedPoweredUp)
            {
                speedPowerUpTimer -= gameTime.ElapsedGameTime.TotalSeconds;
                if (speedPowerUpTimer <= 0)
                {
                    _speed = defaultSpeed;
                    isSpeedPoweredUp = false;
                }
            }

            if (exitRect.Contains(BoundingBox))
            {
                Game1.self.Exit();
            }
            for (int i = 0; i < ExitRects.Count; i++)
            {
                if (ExitRects[i].Contains(BoundingBox))
                {
                    Game1.self.Exit();
                }
            }
        }
        private void ShootAtMouse()
        {
            Vector2 mouseScreenPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            Vector2 mouseWorldPosition = _camera.ScreenToWorld(mouseScreenPosition);
            Vector2 direction = mouseWorldPosition - Position;
            if (direction.Length() > 0)
            {
                direction.Normalize();
            }
            Vector2 velocity = direction * 5f;
            PlayerProjectile playerProjectile = new PlayerProjectile(_playerProjectileTexture, Position, velocity);
            _playerProjectiles.Add(playerProjectile);
        }

        private bool IsCollidingWithWalls(Rectangle newBoundingBox, List<Rectangle> walls, List<Rectangle> wallsUp)
        {
            foreach (Rectangle wall in walls)
            {
                if (newBoundingBox.Intersects(wall))
                {
                    return true;
                }
            }
            foreach (Rectangle wall in wallsUp)
            {
                if (newBoundingBox.Intersects(wall))
                {
                    return true;
                }
            }
            return false;
        }
        private bool IsCollidingWithDoors(Rectangle newBoundingBox, List<door> doors, List<VerticalDoor> verticalDoors)
        {
            foreach (door door in doors)
            {
                if (newBoundingBox.Intersects(door.BoundingBox))
                {
                    return true;
                }
            }
            foreach (VerticalDoor verticalDoor in verticalDoors)
            {
                if (newBoundingBox.Intersects(verticalDoor.BoundingBox))
                {
                    return true;
                }
            }
            return false;
        }
        private void UpdateBoundingBox()
        {
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
        }
        private bool IsCollidingWithGameObjects(Rectangle newBoundingBox, List<GameObjects> gameObjects)
        {
            foreach (GameObjects gameObject in gameObjects)
            {
                if (newBoundingBox.Intersects(gameObject.BoundingBox))
                {
                    return true;
                }
            }
            return false;
        }
   
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            Texture2D textureToDraw = _isDead ? _deadTexture : _texture;
            spriteBatch.Draw(textureToDraw, new Vector2(Position.X + origin.X, Position.Y + origin.Y), null, Color.White, _rotation, origin, 1.0f, SpriteEffects.None, 0f);
            DrawHealthBar(spriteBatch);

            if (gameOverDisplayed)
            {
                _gameOverScreen.Draw(spriteBatch, _camera);
            }
            foreach (var projectile in _playerProjectiles)
            {
                projectile.Draw(spriteBatch);
            }
            DrawHealthBar(spriteBatch);
        

        }
        public void DrawTexture(SpriteBatch spriteBatch, SpriteBatch spriteBatch2)
        {
            spriteBatch.Draw(_texture2, exitRect, Color.White);
            spriteBatch2.Draw(_texture2, exitRect2, Color.White);

        }
        public void DrawTextureRight(SpriteBatch spriteBatch)
        {
            foreach (Rectangle exit in ExitRects)
            {
                spriteBatch.Draw(_texture3, exit, Color.White);
            }
        }
        private void DrawHealthBar(SpriteBatch spriteBatch)
        {
            if (_isDead) return;

            int healthBarWidth = 50;
            int healthBarHeight = 5;
            Vector2 healthBarPosition = new Vector2(Position.X + (_texture.Width / 2) - (healthBarHeight / 2), Position.Y - 10);

            Rectangle healthBarRectangle = new Rectangle((int)healthBarPosition.X, (int)healthBarPosition.Y, healthBarWidth, healthBarHeight);
            spriteBatch.Draw(_healthBarTexture, healthBarRectangle, Color.White);

            int healthWidth = (int)((_currentHealth / (float)_maxHealth) * healthBarWidth);
            Rectangle currentHealthRectangle = new Rectangle((int)healthBarPosition.X, (int)healthBarPosition.Y, healthWidth, healthBarHeight);
            spriteBatch.Draw(_healthBarTexture, currentHealthRectangle, Color.Green);
        }
        public void TakeDamage(double amount)
        {
            if (_isDead) return;

            _currentHealth -= amount;
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                _isDead = true;

            }
        }
        public void CollectPowerUp()
        {
            isPoweredUp = true;
            Damage = poweredUpDamage;
            powerUpTimer = powerUpDuration;
        }
        public void CollectSpeedPowerUp()
        {
            isSpeedPoweredUp = true;
            _speed = poweredUpSpeed;
            speedPowerUpTimer = speedPowerUpDuration;
        }
    }
}
