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

        public Player(Texture2D texture)
        {
            _texture = texture;
            Position = Vector2.Zero;
            _speed = 2f;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.W))
                Position.Y -= _speed;
            if (state.IsKeyDown(Keys.S))
                Position.Y += _speed;
            if (state.IsKeyDown(Keys.A))
                Position.X -= _speed;
            if (state.IsKeyDown(Keys.D))
                Position.X += _speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }

    }
}
