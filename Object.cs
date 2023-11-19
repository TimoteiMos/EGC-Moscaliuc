using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Drawing;

namespace Moscaliuc_Timotei_3131A_L5
{
    public class Object
    {
        private bool Visibility; // Indicator for object visibility
        private bool GravityAffected; // Indicator for whether the object is affected by gravity
        private Color ObjectColor; // Object's color
        private List<Vector3> CoordinatesList; // List of coordinates for the object
        private Randomizer randomizer; // Random number generator

        private const int GRAVITY_OFFSET = 1; // Gravity offset value

        // Constructor for the 3D object
        public Object(bool gravityStatus, List<Vector3> vertices)
        {
            randomizer = new Randomizer();
            Visibility = true;
            GravityAffected = gravityStatus;
            ObjectColor = randomizer.RandomColor();

            CoordinatesList = new List<Vector3>();

            // Generate object coordinates using offsets and pre-existing coordinates
            int sizeOffset = randomizer.RandomInt(3, 7);
            int heightOffset = randomizer.RandomInt(40, 75);
            int radialOffset = randomizer.RandomInt(-40, 40);
            int radialOffset2 = randomizer.RandomInt(-40, 40);

            for (int i = 0; i < 10; i++)
            {
                CoordinatesList.Add(new Vector3(vertices[i].X * sizeOffset + radialOffset, vertices[i].Y * sizeOffset + heightOffset, vertices[i].Z * sizeOffset + radialOffset2));
            }
        }

        // Method to draw the object in OpenGL
        public void Draw()
        {
            if (Visibility)
            {
                GL.Color3(ObjectColor);
                GL.Begin(PrimitiveType.QuadStrip);

                foreach (Vector3 vertex in CoordinatesList)
                {
                    GL.Vertex3(vertex);
                }
                GL.End();
            }
        }

        // Method to update the object's position, considering gravity
        public void UpdatePosition(bool gravityStatus)
        {
            if (Visibility && gravityStatus && !GroundCollisionDetected())
            {
                for (int i = 0; i < CoordinatesList.Count; i++)
                {
                    CoordinatesList[i] = new Vector3(CoordinatesList[i].X, CoordinatesList[i].Y - GRAVITY_OFFSET, CoordinatesList[i].Z);
                }
            }
        }

        // Method to detect ground collision for the object
        public bool GroundCollisionDetected()
        {
            foreach (Vector3 vertex in CoordinatesList)
            {
                if (vertex.Y <= 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
