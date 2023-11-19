using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Moscaliuc_Timotei_3131A_L5
{
    class Grid
    {
        private readonly Color Color; // Grid color
        private bool Visibility; // Variable for grid visibility

        private readonly Color DefaultColor = Color.White; // Default grid color
        private const int CellSize = 10; // Size of a grid cell
        private const int NumOfUnits = 100; // Number of cells in each direction
        private const int PointOffset = CellSize * NumOfUnits; // Offset for drawing the grid
        private const int MicroOffset = 1; // Offset to prevent overlap of axes on the grid

        // Constructor for the Grid class
        public Grid()
        {
            Color = DefaultColor; // Set the default color
            Visibility = true; // The grid is initially visible
        }

        // Method to toggle grid visibility
        public void ToggleVisibility()
        {
            Visibility = !Visibility;
        }

        // Method to show the grid
        public void Show()
        {
            Visibility = true;
        }

        // Method to hide the grid
        public void Hide()
        {
            Visibility = false;
        }

        // Method to draw the grid
        public void Draw()
        {
            if (Visibility) // Check if the grid is visible
            {
                GL.Begin(PrimitiveType.Lines);
                GL.Color3(Color);

                // Draw lines in the XZ plane, parallel to Oz and Ox axes
                for (int i = -1 * CellSize * NumOfUnits; i <= CellSize * NumOfUnits; i += CellSize)
                {
                    // Lines parallel to Oz
                    GL.Vertex3(i + MicroOffset, 0, PointOffset);
                    GL.Vertex3(i + MicroOffset, 0, -1 * PointOffset);

                    // Lines parallel to Ox
                    GL.Vertex3(PointOffset, 0, i + MicroOffset);
                    GL.Vertex3(-1 * PointOffset, 0, i + MicroOffset);
                }
                GL.End();
            }
        }
    }
}
