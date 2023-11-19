using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace Moscaliuc_Timotei_3131A_L5
{
    /// <summary>
    /// Represents a 3D coordinate system with visible axes.
    /// </summary>
    internal class Axis
    {
        /// <summary>
        /// Size of the axis lines.
        /// </summary>
        public const int AxisSize = 75;

        /// <summary>
        /// Indicator for axis visibility.
        /// </summary>
        private bool isVisible;

        /// <summary>
        /// Creates a new instance of the Axis class.
        /// </summary>
        public Axis()
        {
            isVisible = true; // Axes are initially visible
        }

        /// <summary>
        /// Draws the axes on the screen.
        /// </summary>
        public void Draw()
        {
            GL.Begin(PrimitiveType.Lines);

            // Positive X-axis
            DrawAxisLine(Color.Black, 0, 0, 0, AxisSize, 0, 0);
            // Negative X-axis
            DrawAxisLine(Color.Black, 0, 0, 0, -AxisSize, 0, 0);
            // Positive Y-axis
            DrawAxisLine(Color.Black, 0, 0, 0, 0, AxisSize, 0);
            // Negative Y-axis
            DrawAxisLine(Color.Black, 0, 0, 0, 0, -AxisSize, 0);
            // Positive Z-axis
            DrawAxisLine(Color.Black, 0, 0, 0, 0, 0, AxisSize);
            // Negative Z-axis
            DrawAxisLine(Color.Black, 0, 0, 0, 0, 0, -AxisSize);

            GL.End();
        }

        /// <summary>
        /// Toggles the visibility of the axes.
        /// </summary>
        public void ToggleVisibility()
        {
            isVisible = !isVisible;
        }

        /// <summary>
        /// Shows the axes.
        /// </summary>
        public void Show()
        {
            isVisible = true;
        }

        /// <summary>
        /// Hides the axes.
        /// </summary>
        public void Hide()
        {
            isVisible = false;
        }

        /// <summary>
        /// Helper method to draw an axis line.
        /// </summary>
        private static void DrawAxisLine(Color color, int x1, int y1, int z1, int x2, int y2, int z2)
        {
            GL.Color3(color);
            GL.Vertex3(x1, y1, z1);
            GL.Vertex3(x2, y2, z2);
        }
    }
}
