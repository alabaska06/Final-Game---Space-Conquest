using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace Final_Game___Space_Conquest
{
    public class Bot
    {

        public Vector2 Position;
        private Texture2D _texture;
        private Texture2D _deadtexture;
        private Texture2D _healthBarTexture;
        private int _maxHealth;
        private int _currentHealth;

        private float _speed;
        private float _rotation;
        private Rectangle _boundingBox;

        //projectiles
        private List<Projectile> _projectiles;
        private Texture2D _projectileTexture;
        private TimeSpan _shootCoolDown;
        private TimeSpan _lastShootTime;
        private float _timeSinceDeath;

        private bool _isDead;
        private bool _isDeactivated = false;

        public List<Projectile> Projectiles => _projectiles;
        public Rectangle BoundingBox => _boundingBox;
        public bool IsDead => _isDead;

        private Player _player;

        public Bot(Texture2D texture, Texture2D deadTexture, Texture2D healthBartexture, Vector2 position, Player player, Texture2D projectileTexture, int maxHealth = 5)
        {
            _texture = texture;
            _deadtexture = deadTexture;
            _healthBarTexture = healthBartexture;
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
            Position = position;
            _speed = 2f;
            _rotation = 0f;
            _player = player;
            _isDead = false;
            //projectiles
            _projectileTexture = projectileTexture;

            _projectiles = new List<Projectile>();
            _shootCoolDown = TimeSpan.FromSeconds(2);
            _lastShootTime = TimeSpan.Zero;

            UpdateBoundingBox();
        }

        public void Update(GameTime gameTime, Camera camera, List<Rectangle> walls, List<Rectangle> wallsUp, List<door> doors, List<VerticalDoor> verticalDoors, List<Bot> bots, List<GameObjects> gameObjects)
        {
            if (_isDead)
            {
                _timeSinceDeath += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_timeSinceDeath >= 2f)
                {
                    _isDeactivated = true;
                }
            }

            if (_isDead) return;

            if (!_isDead)
            {
                _timeSinceDeath = 0f;
                _isDeactivated = false;
            }

            // check if bot is within the players viewport
            if (IsPlayerinViewport(camera))
            {
                //follow the player
                MoveTowardsPlayer(gameTime, walls, wallsUp, doors, verticalDoors, bots, gameObjects);

                if (gameTime.TotalGameTime - _lastShootTime > _shootCoolDown)
                {
                    ShootAtPlayer();
                    _lastShootTime = gameTime.TotalGameTime;
                }
            }

            foreach (var projectile in _projectiles)
            {
                projectile.Update(gameTime, walls, wallsUp, doors, verticalDoors, gameObjects, bots);
            }


            for (int i = _projectiles.Count - 1; i >= 0; i--)
            {
                if (_projectiles[i].Update(gameTime, walls, wallsUp, doors, verticalDoors, gameObjects, bots))
                {
                    _projectiles.RemoveAt(i);
                }
            }

            UpdateBoundingBox();
        }
        private bool IsPlayerinViewport(Camera camera)
        {
            Rectangle viewport = new Rectangle(
                (int)(-camera.Transform.Translation.X),
                (int)(-camera.Transform.Translation.Y),
                camera.ViewportWidth,
                camera.ViewportHeight);

            return viewport.Contains(_player.Position);
        }
        private void MoveTowardsPlayer(GameTime gameTime, List<Rectangle> walls, List<Rectangle> wallsUp, List<door> doors, List<VerticalDoor> verticalDoors, List<Bot> bots, List<GameObjects> gameObjects)
        {
            if (_isDead) return;

            Vector2 direction = _player.Position - Position;
            if (direction != Vector2.Zero)
            {
                direction.Normalize();

                // horiz
                Vector2 newPosition = new Vector2(Position.X + direction.X * _speed, Position.Y);
                Rectangle newBoundingBox = new Rectangle((int)newPosition.X, (int)newPosition.Y, _boundingBox.Width, _boundingBox.Height);

                if (!IsCollidingWithWalls(newBoundingBox, walls, wallsUp) && !IsCollidingWithBots(newBoundingBox, bots) && !IsCollidingWithGameObjects(newBoundingBox, gameObjects) && !IsCollidingWithPlayer(newBoundingBox) && !IsCollidingWithDoors(newBoundingBox, doors, verticalDoors))
                {
                    Position = newPosition;
                    _rotation = (float)Math.Atan2(direction.Y, direction.X);
                }


                // vert
                newPosition = new Vector2(Position.X, Position.Y + direction.Y * _speed);
                newBoundingBox = new Rectangle((int)newPosition.X, (int)newPosition.Y, _boundingBox.Width, _boundingBox.Height);

                if (!IsCollidingWithWalls(newBoundingBox, walls, wallsUp) && !IsCollidingWithBots(newBoundingBox, bots) && !IsCollidingWithGameObjects(newBoundingBox, gameObjects) && !IsCollidingWithPlayer(newBoundingBox) && !IsCollidingWithDoors(newBoundingBox, doors, verticalDoors))
                {
                    Position = newPosition;
                    _rotation = (float)Math.Atan2(direction.Y, direction.X);
                }

            }
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
        private bool IsCollidingWithBots(Rectangle newBoundingBox, List<Bot> bots)
        {
            if (_isDeactivated) return false;
            foreach (Bot bot in bots)
            {
                if (bot != this && newBoundingBox.Intersects(bot.BoundingBox))
                {
                    return true;
                }
            }
            return false;
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
        private bool IsCollidingWithPlayer(Rectangle newBoundingBox)
        {
            if (_isDeactivated) return false;

            return newBoundingBox.Intersects(_player.BoundingBox);
        }
        private void UpdateBoundingBox()
        {
            _boundingBox = new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_isDeactivated) return;

            Vector2 origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            Texture2D textureToDraw = _isDead ? _deadtexture : _texture;

            if (!_isDead || _timeSinceDeath < 2f)
            {
                spriteBatch.Draw(textureToDraw, new Vector2(Position.X + origin.X, Position.Y + origin.Y), null, Color.White, _rotation, origin, 1.0f, SpriteEffects.None, 0f);
            }

            foreach (var projectile in _projectiles)
            {
                projectile.Draw(spriteBatch);
            }
            DrawHealthBar(spriteBatch);
        }
        private void DrawHealthBar(SpriteBatch spriteBatch)
        {

            int healthBarWidth = 50;
            int healthBarHeight = 5;
            Vector2 healthBarPosition = new Vector2(Position.X + (_texture.Width / 2) - (healthBarHeight / 2), Position.Y - 10);

            Rectangle healthBarRectangle = new Rectangle((int)healthBarPosition.X, (int)healthBarPosition.Y, healthBarWidth, healthBarHeight);
            spriteBatch.Draw(_healthBarTexture, healthBarRectangle, Color.White);

            int healthWidth = (int)((_currentHealth / (float)_maxHealth) * healthBarWidth);
            Rectangle currentHealthRectangle = new Rectangle((int)healthBarPosition.X, (int)healthBarPosition.Y, healthWidth, healthBarHeight);
            spriteBatch.Draw(_healthBarTexture, currentHealthRectangle, Color.Green);
        }

        //projectiles
        private void ShootAtPlayer()
        {
            if (_isDead) return;

            Vector2 direction = _player.Position - Position;
            direction.Normalize();
            Vector2 velocity = direction * 5f;

            Projectile projectile = new Projectile(_projectileTexture, Position, velocity);
            _projectiles.Add(projectile);
        }

        public void TakeDamage(int amount)
        {
            _currentHealth -= amount;
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                _isDead = true;
            }
        }
    }
}
