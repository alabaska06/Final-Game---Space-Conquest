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
        public bool IsActive;
        //private double _timeSinceLastShot;//taken
        public Rectangle BoundingBox => _boundingBox;

        //private List<Projectile> _projectiles;

        private double _shootInterval;

        public Projectile(Texture2D texture, Vector2 position, Vector2 direction)
        {
            _texture = texture;
            Position = position;
            _direction = direction;
            _speed = 1f;
            IsActive = true;
            //_projectiles = new List<Projectile>();

            //_timeSinceLastShot = 0.0;
            _shootInterval = 2.5;
            UpdateBoundingBox();
        }
        //public double TimeSinceLastShot
        //{
        //    get { return _timeSinceLastShot; }
        //    set { _timeSinceLastShot = value; }
        //}
        //public double ShootInterval
        //{
        //    get { return _shootInterval; }
        //    set { _shootInterval = value; }
        //}
        public void Update(GameTime gameTime, List<Rectangle> walls, List<Rectangle> wallsUp, List<door> doors, List<VerticalDoor> verticalDoors, List<GameObjects> gameObjects, List<Bot> bots)
        {
            if (!IsActive) return;

            Position += _direction * _speed;

            if (IsCollidingWithDoors(doors, verticalDoors) || IsCollidingWithWalls(walls, wallsUp) || IsCollidingWithGameObjects(gameObjects))
            {
                IsActive = false;
            }

            //UpdateProjectiles(gameTime, walls, wallsUp, doors, verticalDoors, gameObjects, bots);//p
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
        //private void UpdateProjectiles(GameTime gameTime, List<Rectangle> walls, List<Rectangle> wallsUp, List<door> doors, List<VerticalDoor> verticalDoors, List<GameObjects> gameObjects, List<Bot> bots)
        //{
        //    foreach (var projectile in _projectiles)
        //    {
        //        projectile.Update(gameTime, walls, wallsUp, doors, verticalDoors, gameObjects, bots);
        //    }
        //    _projectiles.RemoveAll(p => !p.IsActive);
        //}//p
        //public List<Projectile> Projectiles => _projectiles;//taken
        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                spriteBatch.Draw(_texture, Position, Color.White);
            }


        }
}
}
