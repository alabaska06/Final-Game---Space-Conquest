using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Final_Game___Space_Conquest
{
    internal class Projectile
{
        public Vector2 Position;
        private Texture2D _texture;
        private Vector2 _direction;
        private float _speed;
        private Rectangle _boundingBox;
        public Rectangle BoundingBox => _boundingBox;

        public Projectile(Texture2D texture, Vector2 position, Vector2 direction)
        {
            _texture = texture;
            Position = position;
            _direction = direction;
            _speed = 5f;
            UpdateBoundingBox();
        }
        public void Update(GameTime gameTime)
        {
            Position += _direction * _speed;
            UpdateBoundingBox();
        }
        private void UpdateBoundingBox()
        {
            _boundingBox = new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }
}
}
