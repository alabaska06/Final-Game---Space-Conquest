using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Final_Game___Space_Conquest
{
    public class GameOverScreen
{

        private SpriteFont _font;
        private Texture2D _backgroundTexture;

        public GameOverScreen(SpriteFont font, Texture2D backgroundTexture)
        {
            _font = font;
            _backgroundTexture = backgroundTexture;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, 800, 600), Color.Black * 0.5f);
            string message = "You Died, exit the game to try again";
            Vector2 messageSize = _font.MeasureString(message);
            Vector2 messagePosition = new Vector2(400 - messageSize.X / 2, 300 - messageSize.Y / 2);
            spriteBatch.DrawString(_font, message, messagePosition, Color.White);
        }
    }
}
