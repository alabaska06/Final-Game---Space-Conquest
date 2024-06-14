using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Final_Game___Space_Conquest
{
    public class Bot : Sprite
{
        public MovementAI MoveAI { get; set; }

        public Bot(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            Speed = 250;
        }
        public void Update()
        {
            MoveAI.Move(this);
        }
}
}
