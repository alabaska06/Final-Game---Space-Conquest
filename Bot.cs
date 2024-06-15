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
        private float _speed;
        private float _rotation;
        private Rectangle _boundingBox;

        public Rectangle BoundingBox => _boundingBox;

        private Player _player;
        private List<Projectile> _projectiles;
        private Texture2D _projectileTexture;
        private double _shootTimer;
        private double _shootInterval = 1.5;//in seconds
        
        public Bot(Texture2D texture, Vector2 position, Texture2D projectileTexture, Player player)
        {
            _texture = texture;
            Position = position;
            _speed = 2f;
            _rotation = 0f;
            _player = player;
            _projectiles = new List<Projectile>();
            _projectileTexture = projectileTexture;
            UpdateBoundingBox();
        }
        public void Update(GameTime gameTime, Camera camera, List<Rectangle> walls, List<Rectangle> wallsUp, List<door> doors, List<VerticalDoor> verticalDoors, List<Bot> bots, List<GameObjects> gameObjects)
        {
            // check if bot is within the players viewport
            if (IsPlayerinViewport(camera))
            {
                //follow the player
                MoveTowardsPlayer(gameTime, walls, wallsUp, doors, verticalDoors, bots, gameObjects);
                Shoot(gameTime);
            }
            UpdateProjectiles(gameTime);
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
            Vector2 direction = _player.Position - Position;
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
                Vector2 newPosition = Position + direction * _speed;
                Rectangle newBoundingBox = new Rectangle((int)newPosition.X, (int)newPosition.Y, _boundingBox.Width, _boundingBox.Height);

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
            return newBoundingBox.Intersects(_player.BoundingBox);
        }
        private void Shoot(GameTime gameTime)
        {
            _shootTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (_shootTimer >= _shootInterval)
            {
                _shootTimer = 0;
                Vector2 direction = _player.Position - Position;
                direction.Normalize();
                _projectiles.Add(new Projectile(_projectileTexture, Position, direction));

            }
        }
        private void UpdateProjectiles(GameTime gameTime)
        {
            foreach (var projectile in _projectiles)
            {
                projectile.Update(gameTime);
            }
        }
        private void UpdateBoundingBox()
        {
            _boundingBox = new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            spriteBatch.Draw(_texture, new Vector2(Position.X + origin.X, Position.Y + origin.Y), null, Color.White, _rotation, origin, 1.0f, SpriteEffects.None, 0f);

            foreach (var projectile in _projectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }
}
}
