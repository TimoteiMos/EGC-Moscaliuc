using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;

namespace Moscaliuc_Timotei_3131A_L5
{
    // Class for managing a cube in 3D space
    class Cube
    {
        // Vector of Vector3 where the cube coordinates will be read from the file
        private List<Vector3> Vertices;

        // Variables for color channels
        private double r = 1;
        private double g = 1;
        private double b = 1;
        private double a = 1;

        // Colors for triangle coordinates
        private Color vertexColor1;
        private Color vertexColor2;
        private Color vertexColor3;

        // Declaration of the color handler variable for modifying triangle colors
        private Color_Handler colorHandler;

        private Random_Color_Generator colorGenerator;

        // Constructor for the Cube class
        public Cube(string filePath)
        {
            Vertices = new List<Vector3>();

            // Reading cube coordinates from the provided file
            string text = System.IO.File.ReadAllText(@filePath);
            string[] lines = text.Split('\n');

            for (int i = 0; i < 36; i++)
            {
                string[] cb = lines[i].Split(' ');
                Vertices.Add(new Vector3(int.Parse(cb[0]), int.Parse(cb[1]), int.Parse(cb[2])));
            }

            // Instantiating objects for the color controller and random color generator
            colorHandler = new Color_Handler();
            colorGenerator = new Random_Color_Generator();
        }

        // Method for setting the color of the cube and a triangle within it
        public void SetCubeColor()
        {
            // Define objects for the keyboard and mouse state
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            // Set cube color based on the pressed keys
            colorHandler.SetColor(keyboard, ref r, ref g, ref b, ref a);

            // Set color of a triangle within the cube based on the pressed keys
            colorHandler.SetVertexColors(keyboard, ref vertexColor1, ref vertexColor2, ref vertexColor3);
        }

        // Method for the actual drawing of the cube on the screen
        public void Draw()
        {
            GL.Begin(PrimitiveType.Triangles);

            // Loop through each face of the cube and draw triangles
            for (int i = 0; i < 36; i = i + 6) // Incrementing by 6 to consider two triangles forming a face of the cube
            {
                // Setting color for the face
                if (i > 28)
                {
                    GL.Color4(r, g, b, a); // Apply set color
                }
                else
                {
                    GL.Color3(Color.White); // Use default color
                }

                // Setting color for individual vertices of a triangle within the cube
                if (i == 18)
                {
                    GL.Color3(vertexColor1);
                }
                GL.Vertex3(Vertices[i]);
                if (i == 18)
                {
                    GL.Color3(vertexColor2);
                }
                GL.Vertex3(Vertices[i + 1]);
                if (i == 18)
                {
                    GL.Color3(vertexColor3);
                }
                GL.Vertex3(Vertices[i + 2]);

                if (i == 18)
                {
                    GL.Color3(vertexColor1);
                }
                GL.Vertex3(Vertices[i + 3]);
                if (i == 18)
                {
                    GL.Color3(vertexColor2);
                }
                GL.Vertex3(Vertices[i + 4]);
                if (i == 18)
                {
                    GL.Color3(vertexColor3);
                }
                GL.Vertex3(Vertices[i + 5]);
            }

            GL.End();
        }
    }
}
