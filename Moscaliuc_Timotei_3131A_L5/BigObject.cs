using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Moscaliuc_Timotei_3131A_L5
{
    public class BigObject
    {
        private const String FILENAME = "assets/slime.obj"; // Name of the .obj file for the 3D object

        private const int FSI = 100; // Scaling factor for coordinates read from the .obj file

        private const int GRAVITY_OFFSET = 2; // Gravity offset applied in position update

        private List<Vector3> cList; // List of vectors for object vertex coordinates

        private bool Visibility; // Indicator for object visibility

        private Color Color; // Object color

        private bool Error; // Indicator for error in object loading

        // Constructor for the BigObject class
        public BigObject(Color clr)
        {
            try
            {
                cList = LoadFromObjFile(FILENAME); // Attempt to load the vertex coordinates from the .obj file

                // Check if valid coordinates exist in the object list
                if (cList.Count == 0)
                {
                    Console.WriteLine("Object creation failed: object not found/missing coordinates!");
                    return;
                }

                // Initial state of the object
                Visibility = false;
                Color = clr;
                Error = false;

                Console.WriteLine("\nThe 3D object has been loaded! It has " + cList.Count.ToString() + " available vertices!\n"); // Display the number of successfully loaded vertices
            }
            // If an error occurs, display the corresponding message and set Error to true
            catch (Exception)
            {
                Console.WriteLine("\nERROR!!!!\nFile <" + FILENAME + "> does not exist!!!\n");
                Error = true;
            }
        }

        // Method to toggle object visibility
        public void ToggleVisibility()
        {
            // Check if there was an error during object loading
            if (Error == false)
            {
                Visibility = !Visibility;
            }
        }

        // Method for drawing the object
        public void Draw()
        {
            // Check if there was an error and if the object is visible
            if (Error == false && Visibility == true)
            {
                GL.Color3(Color);
                GL.Begin(PrimitiveType.Triangles);

                // Iterate through each vertex and add it to the drawing
                foreach (var vert in cList)
                {
                    GL.Vertex3(vert);
                }

                GL.End();
            }
        }

        // Method to load coordinates from the .obj file
        private List<Vector3> LoadFromObjFile(string fname)
        {
            List<Vector3> vector = new List<Vector3>(); // Initialize a list for vertex coordinates

            var lines = File.ReadLines(fname); // Read lines from the file

            // Iterate through each line
            foreach (var line in lines)
            {
                // Check if the line has sufficient length
                if (line.Trim().Length > 2)
                {
                    // Extract the first two characters from the line
                    string ch1 = line.Trim().Substring(0, 1);
                    string ch2 = line.Trim().Substring(1, 1);

                    // Check if the line represents a vertex
                    if (ch1 == "v" && ch2 == " ")
                    {
                        string[] block = line.Trim().Split(' '); // Split the line into separate blocks

                        // Check if the block has valid coordinates (x, y, z)
                        if (block.Length == 4)
                        {
                            // Extract the coordinates and add them to the list, scaling them
                            float x = float.Parse(block[1].Trim()) * FSI;
                            float y = float.Parse(block[2].Trim()) * FSI;
                            float z = float.Parse(block[3].Trim()) * FSI;

                            vector.Add(new Vector3((int)x, (int)y, (int)z));
                        }
                    }
                }
            }
            return vector;
        }

        // Method to detect ground collision
        public bool GroundCollisionDetected()
        {
            // Check each vertex for collision with the ground (Y <= 0)
            foreach (Vector3 v in cList)
            {
                if (v.Y <= 0)
                {
                    return true;
                }
            }
            return false;
        }

        // Method to update the object's position
        public void UpdatePosition(bool gravityStatus)
        {
            // Check if the object is visible, gravity is enabled, and there are no ground collisions
            if (Visibility && gravityStatus && !GroundCollisionDetected())
            {
                // Update the position of each vertex for gravity simulation
                for (int i = 0; i < cList.Count; i++)
                {
                    cList[i] = new Vector3(cList[i].X, cList[i].Y - GRAVITY_OFFSET, cList[i].Z);
                }
            }
        }
    }
}
