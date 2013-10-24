using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ADGTerri
{
    public class Timer
    {
        private float startCount;
        private float endCount;
        public String displayValue { get; private set; }
        
        public Boolean isActive { get; private set; }
        public Boolean isComplete { get; private set; }

        public Timer()
        {
            this.isActive = false;
            this.isComplete = false;
            this.displayValue = "None";
            this.startCount = 0;
            this.endCount = 0;
        }

        public void Set(GameTime gameTime, float seconds)
        {
            this.startCount = gameTime.TotalGameTime.Seconds;
            this.endCount = this.startCount + seconds;
            this.isActive = true;
            this.displayValue = this.endCount.ToString();
        }

        public Boolean checkTimer(GameTime gameTime)
        {
            if (this.isComplete == false)
            {
                if (gameTime.TotalGameTime.Seconds > this.startCount)
                {
                    this.startCount = gameTime.TotalGameTime.Seconds;
                    this.endCount = (this.endCount - 1f);
                    this.displayValue = this.endCount.ToString();
                    if (this.endCount < 0)
                    {
                        this.endCount = 0;
                        this.isComplete = true;
                        this.displayValue = "Fail";

                    }
                }
            }
            else
            {
                this.displayValue = "Game Over";
            }

            return this.isComplete;
        }

        public void Reset()
        {
            this.isComplete = false;
            this.isActive = false;
            this.displayValue = "none";
            this.startCount = 0;
            this.endCount = 0;
        }
    }
}
