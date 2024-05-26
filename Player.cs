using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Final_Game___Space_Conquest
{
        public class Player
        {
            public Vector2 Position;
            private Texture2D _texture;
            private float _speed;
            private float _rotation;

            public Rectangle BoundingBox;

            public Player(Texture2D texture)
            {
                _texture = texture;
                Position = Vector2.Zero;
                _speed = 4f;
                _rotation = 0f;
                UpdateBoundingBox();
            }

            public void Update(GameTime gameTime, List<Rectangle> walls)
            {
                KeyboardState state = Keyboard.GetState();

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

                    if (!IsCollidingWithWalls(newBoundingBox, walls))
                    {
                        Position = newPosition;
                        _rotation = (float)Math.Atan2(direction.Y, direction.X);
                        UpdateBoundingBox();
                    }
                }
            }

        private void UpdateBoundingBox()
        {
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
        }

        private bool IsCollidingWithWalls(Rectangle newBoundingBox, List<Rectangle> walls)
        {
            foreach (Rectangle wall in walls)
            {
                if (newBoundingBox.Intersects(wall))
                {
                    return true;
                }
            }
            return false;
        }

            public void Draw(SpriteBatch spriteBatch)
            {
                Vector2 origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
                spriteBatch.Draw(_texture, Position, null, Color.White, _rotation, origin, 1.0f, SpriteEffects.None, 0f);
            }

        }
    }

