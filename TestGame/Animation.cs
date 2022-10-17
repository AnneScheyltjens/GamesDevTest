using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    internal class Animation
    {
        public AnimationFrame CurrentFrame { get; set; }
        private List<AnimationFrame> frames;
        private int counter;
        private double secondCounter = 0;
        private int FPS = 15;


        public Animation()
        {
            frames = new List<AnimationFrame>();
        }

        public void AddFrame(AnimationFrame frame)
        {
            frames.Add(frame);
            CurrentFrame = frames[0];
        }

        public void Update(GameTime gameTime)
        {
            CurrentFrame = frames[counter];

            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
            

            if (secondCounter >= 1d/FPS)    //1 as double 
            {
                counter++;
                secondCounter = 0;
            }

            if (counter >= frames.Count)
            {
                counter = 0;
            }
        }
    }
}
