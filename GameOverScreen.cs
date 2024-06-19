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
        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Draw(_backgroundTexture, new Rectangle((int)(camera._center.X), (int)(camera._center.Y), camera.ViewportWidth, camera.ViewportHeight), Color.White);
        }
    }
}
