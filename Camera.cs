using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Game___Space_Conquest
{
    public class Camera
    {
        public Matrix Transform { get; private set; }
        private Vector2 _center;
        private Viewport _viewport;

        public Camera(Viewport viewport)
        {
            _viewport = viewport;
        }

        public void Update(Vector2 playerPosition)
        {
            _center = new Vector2((playerPosition.X + 50) - _viewport.Width / 2, (playerPosition.Y + 50) - _viewport.Height / 2);
            Transform = Matrix.CreateTranslation(new Vector3(-_center, 0));
        }

    }
}
