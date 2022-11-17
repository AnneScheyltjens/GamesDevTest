using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{

    enum Richting {Up, Left, Down, Right, Idle}

    internal class Animation
    {
        public AnimationFrame CurrentFrame { get; set; }
        private List<AnimationFrame> frames;

        //fps stuff
        private int counter;
        private double secondCounter = 0;
        private int FPS = 15;

        //frames stuff
        private int currentFrameNr;
        private Richting currentRichting;


        public Animation()
        {
            frames = new List<AnimationFrame>();
            currentFrameNr = 9;
            currentRichting = Richting.Idle;
        }

        public void AddFrame(AnimationFrame frame)
        {
            frames.Add(frame);
            CurrentFrame = frames[0];
        }

        public void Update(GameTime gameTime, Richting richting)
        {
            
            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
            

            if (secondCounter >= 1d/FPS)    //1 as double 
            {
                //counter++;    //wanneer we gewoon naar de volgende frame wilden gaan
                ChangeFrame(richting);
                secondCounter = 0;
            }

            if (counter >= frames.Count)
            {
                counter = 0;
            }
        }

        private void ChangeFrame(Richting richting)
        {
            int richtingNr = (int)richting;
            //if idle, zet frame op 9
            if (richting == Richting.Idle)
            {
                currentFrameNr = 9;
            }
            else if (richting != currentRichting)
            {
                //andere richting, switch van richting
                currentRichting = richting;
                //zet frame op 1ste frame van die richting
                currentFrameNr = richtingNr * 4;
            }
            else
            {
                //zelfde richting, maar volgende frame
                currentFrameNr += 1;
                //check dat je niet naar een frame van een andere richting gaat
                if (currentFrameNr > (richtingNr * 4 + 3))
                {
                    //terugzetten op 1ste frame van de richting
                    currentFrameNr = richtingNr * 4;
                }
            }



            CurrentFrame = frames[currentFrameNr];

            //Debug.WriteLine($"Richting: {richting}, currentframeNR: {currentFrameNr}");

        }
    }
}
