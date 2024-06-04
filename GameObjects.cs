using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final_Game___Space_Conquest
{
    public class GameObjects
{
        protected Texture2D _texture;
        public Vector2 Position;
        public Rectangle BoundingBox { get; protected set; }

        public GameObjects(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            Position = position;
            UpdateBoundingBox();
        }

        protected virtual void UpdateBoundingBox()
        {
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
        }

        public virtual void Update(GameTime gameTime)
        {
            UpdateBoundingBox();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }
}
}
