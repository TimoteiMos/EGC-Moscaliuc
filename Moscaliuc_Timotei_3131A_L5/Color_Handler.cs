using OpenTK.Input;
using System;
using System.Drawing;

namespace Moscaliuc_Timotei_3131A_L5
{
    internal class Color_Handler
    {
        private const double COLOR_ADJUSTMENT_STEP = 0.05;

        /// <summary>
        /// Adjusts the color channels based on keyboard input.
        /// </summary>
        public void SetColor(KeyboardState keyboard, ref double r, ref double g, ref double b, ref double a)
        {
            // Red
            if (keyboard[Key.Up] && keyboard[Key.R] && r < 1)
            {
                r += COLOR_ADJUSTMENT_STEP;
            }
            else if (keyboard[Key.Down] && keyboard[Key.R] && r > 0)
            {
                r -= COLOR_ADJUSTMENT_STEP;
            }

            // Blue
            if (keyboard[Key.Up] && keyboard[Key.A] && b < 1)
            {
                b += COLOR_ADJUSTMENT_STEP;
            }
            else if (keyboard[Key.Down] && keyboard[Key.A] && b > 0)
            {
                b -= COLOR_ADJUSTMENT_STEP;
            }

            // Green
            if (keyboard[Key.Up] && keyboard[Key.V] && g < 1)
            {
                g += COLOR_ADJUSTMENT_STEP;
            }
            else if (keyboard[Key.Down] && keyboard[Key.V] && g > 0)
            {
                g -= COLOR_ADJUSTMENT_STEP;
            }

            // Transparency
            if (keyboard[Key.Up] && keyboard[Key.T] && a < 1)
            {
                a += COLOR_ADJUSTMENT_STEP;
            }
            else if (keyboard[Key.Down] && keyboard[Key.T] && a > 0)
            {
                a -= COLOR_ADJUSTMENT_STEP;
                if (a < COLOR_ADJUSTMENT_STEP)
                {
                    a = 0;
                }
            }
        }

        /// <summary>
        /// Sets colors for three vertices based on keyboard input.
        /// </summary>
        public void SetVertexColors(KeyboardState keyboard, ref Color clr1, ref Color clr2, ref Color clr3)
        {
            Color tempColor1 = clr1; // Vertex 1
            Color tempColor2 = clr2; // Vertex 2
            Color tempColor3 = clr3; // Vertex 3

            // Vertex 1
            if (keyboard[Key.Number1])
                clr1 = Color.FromArgb(255, 255, 0, 0); // Red
            if (keyboard[Key.Number2])
                clr1 = Color.FromArgb(255, 0, 255, 0); // Green
            if (keyboard[Key.Number3])
                clr1 = Color.FromArgb(255, 0, 0, 255); // Blue

            // Vertex 2
            if (keyboard[Key.Number4])
                clr2 = Color.FromArgb(255, 255, 0, 0); // Red
            if (keyboard[Key.Number5])
                clr2 = Color.FromArgb(255, 0, 255, 0); // Green
            if (keyboard[Key.Number6])
                clr2 = Color.FromArgb(255, 0, 0, 255); // Blue

            // Vertex 3
            if (keyboard[Key.Number7])
                clr3 = Color.FromArgb(255, 255, 0, 0); // Red
            if (keyboard[Key.Number8])
                clr3 = Color.FromArgb(255, 0, 255, 0); // Green
            if (keyboard[Key.Number9])
                clr3 = Color.FromArgb(255, 0, 0, 255); // Blue

            // Display in console
            if (tempColor1 != clr1)
                Console.WriteLine("Vertex 1: " + clr1);
            if (tempColor2 != clr2)
                Console.WriteLine("Vertex 2: " + clr2);
            if (tempColor3 != clr3)
                Console.WriteLine("Vertex 3: " + clr3);
        }
    }
}
