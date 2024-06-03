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
    internal class VerticalDoor
{
        private Texture2D _texture;
        public Vector2 Position;
        private Vector2 _originalPosition;//to reset the door after closing
        private float _speed;
        private Vector2 _direction;//direction of doors movement
        private bool _isOpening;//flag to check if the door is opening
        private bool _isClosing;//flag to check if door is closing 
        private float _movementDistance; //total distance door should move
        private float _movedDistance; //distance the door has moved 
        private Rectangle _boundingBox; //for collision detection

        public VerticalDoor(Texture2D texture, Vector2 position, Vector2 direction, float speed)
        {
            _texture = texture;
            Position = position;
            _originalPosition = position;
            _direction = direction;
            _speed = speed;
            _isClosing = false;
            _isOpening = false;
            _movementDistance = direction == Vector2.UnitY ? texture.Height : texture.Width; //determine movement distance based on direction
            _movedDistance = 0f;
            UpdateBaoundingBox();
        }

        public void Update(GameTime gameTime, MouseState mousestate, Vector2 worldMousePosition)
        {
            //check if player clicked on the door
            if (mousestate.LeftButton == ButtonState.Pressed && _boundingBox.Contains(worldMousePosition))
            {
                if (!_isOpening && !_isClosing)
                {
                    _isOpening = true;
                }
            }

            //handle door opening
            if (_isOpening)
            {
                float movement = _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Position += _direction * movement;
                _movedDistance += movement;

                if (_movedDistance >= _movementDistance)
                {
                    Position = _originalPosition + _direction * _movementDistance;
                    _isOpening = false;
                    _isClosing = true;
                }

            }

            //handle door closing
            if (_isClosing)
            {
                float movement = _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Position -= _direction * movement;
                _movedDistance -= movement;

                if (_movedDistance <= 0)
                {
                    Position = _originalPosition;
                    _isClosing = false;
                }
            }
            UpdateBaoundingBox();
        }

        private void UpdateBaoundingBox()
        {
            _boundingBox = new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }

        public Rectangle BoundingBox => _boundingBox;
    }
}

