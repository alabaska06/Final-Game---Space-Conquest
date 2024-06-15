using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace Final_Game___Space_Conquest
{
    internal class Bot
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
        private double _shootInterval = 1.0;//in seconds
        
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
        public void Update(GameTime gameTime, Camera camera)
        {
            if (IsPlayerinViewport(camera))
            {
                MoveTowardsPlayer(gameTime);
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
        private void MoveTowardsPlayer(GameTime gameTime)
        {
            Vector2 direction = _player.Position - Position;
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
                Position += direction * _speed;
                _rotation = (float)Math.Atan2(direction.Y, direction.X);
            }
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
