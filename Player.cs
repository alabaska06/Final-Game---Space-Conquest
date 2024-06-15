using Microsoft.Xna.Framework.Graphics;
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
            private float _speed;
            private float _rotation;
            Rectangle exitRect, exitRect2;
            
            List<Rectangle> ExitRects;


            public Rectangle BoundingBox;

            public Player(Texture2D texture, Texture2D exitTexture, Texture2D exitTexture2)
            {
                _texture = texture;
                _texture2 = exitTexture;
                _texture3 = exitTexture2;
                Position = new Vector2(1034, 319);
                _speed = 6f;
                _rotation = 0f;
               
                UpdateBoundingBox();
            }

            public void Update(GameTime gameTime, List<Rectangle> walls, List<Rectangle> wallsUp, List<door> doors, List<VerticalDoor> verticalDoors, List<GameObjects> gameObjects)
            {
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

                    if (!IsCollidingWithWalls(newBoundingBox, walls, wallsUp) && !IsCollidingWithDoors(newBoundingBox, doors, verticalDoors) && !IsCollidingWithGameObjects(newBoundingBox, gameObjects))
                    {
                        Position = newPosition;
                        _rotation = (float)Math.Atan2(direction.Y, direction.X);
                        UpdateBoundingBox();
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
                
            spriteBatch.Draw(_texture, new Vector2(Position.X + origin.X, Position.Y + origin.Y), null, Color.White, _rotation, origin, 1.0f, SpriteEffects.None, 0f);
            
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

        }
    }

