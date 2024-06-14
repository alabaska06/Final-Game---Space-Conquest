using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Final_Game___Space_Conquest
{
    public class Sprite
{
        protected readonly Texture2D texture;
        protected readonly Vector2 origin;
        public Vector2 Position { get; set; }
        public int Speed { get; set; }
        public Sprite(Texture2D tex, Vector2 pos)
        {
            texture = tex;
            Position = pos;
            Speed = 300;
            origin = new(tex.Width / 2, tex.Height /2);
        }

        public virtual void draw()
        {
            Globals.SpriteBatch.Draw(texture, Position, null, Color.White, 0, origin, 1, SpriteEffects.None, 1);
        }

}
}
