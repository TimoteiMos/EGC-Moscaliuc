using System;
using System.Drawing;

namespace Moscaliuc_Timotei_3131A_L5
{
    // Class for generating random colors
    internal class Random_Color_Generator
    {
        private Random randomizer; // Random number generator

        public Random_Color_Generator()
        {
            randomizer = new Random(); // Initializing the randomizer
        }

        // Method that generates and returns a Color object with random RGB values
        public Color Generate()
        {
            int red = randomizer.Next(0, 255); // Generating random value for red channel
            int green = randomizer.Next(0, 255); // Generating random value for green channel
            int blue = randomizer.Next(0, 255); // Generating random value for blue channel

            // Creating a Color object using the random RGB values
            Color randomColor = Color.FromArgb(red, green, blue);

            return randomColor; // Returning the generated color
        }
    }
}
