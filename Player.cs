using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Game___Space_Conquest
{
    public class Player
{
        public Vector2 Position;
        private Texture2D _texture;
        private float _speed;
        private float _rotation;

        public Player(Texture2D texture)
        {
            _texture = texture;
            Position = Vector2.Zero;
            _speed = 2f;
            _rotation = 0f;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            Vector2 direction = Vector2.Zero;

            if (state.IsKeyDown(Keys.W))
                Position.Y -= _speed;
            if (state.IsKeyDown(Keys.S))
                Position.Y += _speed;
            if (state.IsKeyDown(Keys.A))
                Position.X -= _speed;
            if (state.IsKeyDown(Keys.D))
                Position.X += _speed;

            if (direction != Vector2.Zero)
            {
                direction.Normalize();
                Position += direction * _speed;
                _rotation = (float)Math.Atan2(direction.Y, direction.X);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            spriteBatch.Draw(_texture, Position, null, Color.White, _rotation, origin, 1.0f, SpriteEffects.None, 0f);
        }

    }
}
