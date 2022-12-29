using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TopDownShooterFinal
{
    static class DayNight
    {
        private static int hours = 12;
        private static int minutes = 0;
        private static int number = 0;
        private static string clockHours;
        private static string clockMinutes;

        public static Color flashColor = Color.Transparent;

        public static Color color = new Color();
        public static Color colorBeforeFlash = color;

        private static int value = 255;
        private static int numberDayCycle = 0;
        
        public static void Update()
        {
            //aby byl cyklus den a noc na pět minut, tak bude podmínka pro number 12
            number++;
            if(number == 12)
            {
                minutes++;
                number = 0;
            }
            if(minutes == 60)
            {
                minutes = 0;
                hours++;
            }
            if(hours == 24)
            {
                hours = 0;
            }

            if(minutes.ToString().Length == 1) clockMinutes = "0" + minutes.ToString();
            else clockMinutes = minutes.ToString();
            if (hours.ToString().Length == 1) clockHours = "0" + hours.ToString();
            else clockHours = hours.ToString();

            numberDayCycle++;
            if(hours >= 5 && hours < 18)
            {
                if (numberDayCycle == 40 && value < 255)
                {
                    numberDayCycle = 0;
                    value++;
                }
            }
            else//(hours >= 18 || hours < 6)
            {
                if (numberDayCycle == 60 && value > 190)
                {
                    numberDayCycle = 0;
                    value--;
                }
            }
            if(numberDayCycle > 60) numberDayCycle = 0;

            color = new Color(value, value, value);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            //odstranit
            spriteBatch.DrawString(Textures.debug, clockHours + ":" + clockMinutes, new Vector2(50, 200), Color.White);
            //spriteBatch.DrawString(Textures.debug, value + "", new Vector2(50, 250), Color.White);
        }
    }
}
