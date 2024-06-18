using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Final_Game___Space_Conquest
{
    public class Projectile
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
            _speed = 1f;

            UpdateBoundingBox();
        }
    
        public void Update(GameTime gameTime, List<Rectangle> walls, List<Rectangle> wallsUp, List<door> doors, List<VerticalDoor> verticalDoors, List<GameObjects> gameObjects, List<Bot> bots)
        {
            Vector2 newPosition = Position += _direction * _speed;

            if (IsCollidingWithDoors(doors, verticalDoors) || IsCollidingWithWalls(walls, wallsUp) || IsCollidingWithGameObjects(gameObjects))
            {
                Position = newPosition;
                
            }
            UpdateBoundingBox();
        }
        private void UpdateBoundingBox()
        {
            _boundingBox = new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);

        }
        private bool IsCollidingWithWalls( List<Rectangle> walls, List<Rectangle> wallsUp)
        {
            foreach (Rectangle wall in walls)
            {
                if (BoundingBox.Intersects(wall))
                {
                    return true;
                }
            }
            foreach (Rectangle wall in wallsUp)
            {
                if (BoundingBox.Intersects(wall))
                {
                    return true;
                }
            }
            return false;
        }
        private bool IsCollidingWithDoors(List<door> doors, List<VerticalDoor> verticalDoors)
        {
            foreach (door door in doors)
            {
                if (BoundingBox.Intersects(door.BoundingBox))
                {
                    return true;
                }
            }
            foreach (VerticalDoor verticalDoor in verticalDoors)
            {
                if (BoundingBox.Intersects(verticalDoor.BoundingBox))
                {
                    return true;
                }
            }
            return false;
        }
        private bool IsCollidingWithGameObjects(List<GameObjects> gameObjects)
        {
            foreach (GameObjects gameObject in gameObjects)
            {
                if (BoundingBox.Intersects(gameObject.BoundingBox))
                {
                    return true;
                }
            }
            return false;
        }
        public void Draw(SpriteBatch spriteBatch)
        { 
            spriteBatch.Draw(_texture, Position, Color.White);
        }
}
}
