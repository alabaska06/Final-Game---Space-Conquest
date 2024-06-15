using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


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

        public int ViewportWidth => _viewport.Width;
        public int ViewportHeight => _viewport.Height;

    }
}

