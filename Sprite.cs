using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Final_Game___Space_Conquest
{
    public class Sprite
{
        protected readonly Texture2D texture;
        protected readonly Vector2 origen;
        public Vector2 Position { get; set; }
        public int Speed { get; set; }
        public Sprite(Texture2D tex, Vector2 pos)
        {
            texture = tex;
            Position = pos;
            Speed = 300;
            origen = new(tex. Width / 2, tex.Height /2);
        }

        public virtual void draw()
        {
            Globals.SpriteBatch.Draw(texture, Position, null, color.White, 0, origen, 1, SpriteEffects.None, 1);
        }

}
}
