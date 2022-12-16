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


        public Animation(int startNr)
        {
            frames = new List<AnimationFrame>();
            currentFrameNr = startNr;
            currentRichting = Richting.Idle;
        }

        public void AddFrame(AnimationFrame frame)
        {
            frames.Add(frame);
            CurrentFrame = frames[0];
        }

        public void Update(GameTime gameTime, Richting richting, int nrPerRij, int startNr)
        {
            
            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
            

            if (secondCounter >= 1d/FPS)    //1 as double 
            {
                //counter++;    //wanneer we gewoon naar de volgende frame wilden gaan
                ChangeFrame(richting, nrPerRij, startNr);
                //CurrentFrame = frames[0];
                secondCounter = 0;
            }

            if (counter >= frames.Count)
            {
                counter = 0;
            }
        }

        private void ChangeFrame(Richting richting, int nrPerRij, int startNr)
        {
            int richtingNr = (int)richting;
            //if idle, zet frame op 9
            if (richting == Richting.Idle)
            {
                //currentFrameNr = 9;
                currentFrameNr = startNr;
            }
            else if (richting != currentRichting)
            {
                //andere richting, switch van richting
                currentRichting = richting;
                //zet frame op 1ste frame van die richting
                //currentFrameNr = richtingNr * 4;
                currentFrameNr = richtingNr * nrPerRij;
            }
            else
            {
                //zelfde richting, maar volgende frame
                currentFrameNr += 1;
                //check dat je niet naar een frame van een andere richting gaat
                if (currentFrameNr > (richtingNr * nrPerRij + (nrPerRij-1)))
                {
                    //terugzetten op 1ste frame van de richting
                    currentFrameNr = richtingNr * nrPerRij;
                }
            }

            CurrentFrame = frames[currentFrameNr];

            //Debug.WriteLine($"Richting: {richting}, currentframeNR: {currentFrameNr}");

        }

        /*public void InitialUpdate(Richting richting)
        {
            ChangeFrame(richting);
        }*/
    }
}
