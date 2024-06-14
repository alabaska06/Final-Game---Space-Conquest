using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Game___Space_Conquest
{
    public class AlienManager
{
        private readonly List<Bot> _bots = new();

        public AlienManager()
        {
            var bottexture = Globals.Content.Load<Texture2D>("alienTest");

            var ai = new PatrolMovementAI();
            ai.AddWayPoint(new(100,100));
            ai.AddWayPoint(new(400, 100));
            ai.AddWayPoint(new(400, 400));
            ai.AddWayPoint(new(100, 400));

            _bots.Add(new(bottexture, new(50, 50))
            {
                MoveAI = ai
            });
        }
        public void Update()
        {
            foreach (var bot in _bots)
            {
                bot.Update();
            }
        }
        public void Draw()
        {
            foreach(var bot in _bots)
            {
                bot.draw();
            }
        }
}
}
