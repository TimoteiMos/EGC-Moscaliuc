using OpenTK;
using System;
using System.Drawing;

namespace Moscaliuc_Timotei_3131A_L5
{
    // Randomizer class for generating random values
    class Randomizer
    {
        private Random randomizer; // Random object for generating random numbers

        // Constants defining the limits for random values
        private const int MinInteger = -30;
        private const int MaxInteger = 30;
        private const int MinCoordinate = -60;
        private const int MaxCoordinate = 60;

        // Constructor initializing the Random object
        public Randomizer()
        {
            randomizer = new Random();
        }

        // Method to generate a random color
        public Color RandomColor()
        {
            int red = randomizer.Next(0, 255);
            int green = randomizer.Next(0, 255);
            int blue = randomizer.Next(0, 255);

            Color color = Color.FromArgb(red, green, blue);

            return color;
        }

        // Method to generate a random 3D point using OpenTK's Vector3
        public Vector3 Random3DPoint()
        {
            int coordA = randomizer.Next(MinCoordinate, MaxCoordinate);
            int coordB = randomizer.Next(MinCoordinate, MaxCoordinate);
            int coordC = randomizer.Next(MinCoordinate, MaxCoordinate);

            Vector3 generatedVector = new Vector3(coordA, coordB, coordC);

            return generatedVector;
        }

        // Method to generate a random integer between MinInteger and MaxInteger
        public int RandomInt()
        {
            int randomNumber = randomizer.Next(MinInteger, MaxInteger);
            return randomNumber;
        }

        // Method to generate a random integer between specified min and max values
        public int RandomInt(int min, int max)
        {
            int randomNumber = randomizer.Next(min, max);
            return randomNumber;
        }

        // Method to generate a random integer between 0 and max
        public int RandomInt(int max)
        {
            int randomNumber = randomizer.Next(max);
            return randomNumber;
        }
    }
}
