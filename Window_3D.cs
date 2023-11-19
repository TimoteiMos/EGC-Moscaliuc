using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Moscaliuc_Timotei_3131A_L5
{
    class Window_3D : GameWindow
    {
        // Previous keyboard and mouse state
        private KeyboardState previousKeyboard;
        private MouseState previousMouse;

        private readonly Randomizer r; // Random number generator
        private readonly Axis xyz; // Object for coordinate axes
        private readonly Grid grid; // Object for the grid
        private readonly Camera_3D cam; // Object for the camera
        private bool displayMarker; // Indicator for displaying markers
        private ulong updatesCounter; // Counter for updates
        private ulong framesCounter; // Counter for frames
        private BigObject obj; // Main 3D object

        private List<Object> ObjList; // List of objects
        private List<Vector3> Ver; // List of coordinate vectors
        private List<BigObject> bigObjList; // List of BigObject objects

        private readonly Color Background_Color = Color.FromArgb(28, 136, 204); // Default background color

        // Constructor for the 3D window
        public Window_3D() : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On; // Enable VSync

            r = new Randomizer();
            xyz = new Axis();
            grid = new Grid();
            cam = new Camera_3D();
            obj = new BigObject(Color.Yellow);

            Ver = readVerticesFromFile(@"./../../coordonate.txt"); // Read coordinates from file

            ObjList = new List<Object>(); // Initialize list of objects

            bigObjList = new List<BigObject>(); // Initialize list of BigObject objects

            displayHelp();

            displayMarker = false; // Initialize marker indicator
            updatesCounter = 0; // Initialize update counter
            framesCounter = 0; // Initialize frame counter
        }

        // Method for loading the window
        protected override void OnLoad(EventArgs e)
        {
            // Configure OpenGL on window load
            base.OnLoad(e);
            GL.ClearColor(Color.GreenYellow);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }

        // Method for resizing the window
        protected override void OnResize(EventArgs e)
        {
            // Resize and set perspective
            base.OnResize(e);
            GL.Viewport(0, 0, this.Width, this.Height);
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)Width / (float)Height, 1, 256);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
            cam.SetCamera();
        }

        // Method for updating the window
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            // Update state
            base.OnUpdateFrame(e);
            updatesCounter++; // Increment update counter

            // Display marker if active
            if (displayMarker)
            {
                TimeStampIt("Update", updatesCounter.ToString());
            }

            KeyboardState thisKeyboard = Keyboard.GetState();
            MouseState thisMouse = Mouse.GetState();

            // Check pressed keys for different actions
            if (thisKeyboard[Key.Escape])
            {
                Exit();
                return;
            }
            if (thisKeyboard[Key.R] && !previousKeyboard[Key.R])
            {
                GL.ClearColor(Background_Color);
                xyz.Show();
                grid.Show();
                ObjList.Clear();
            }
            if (thisKeyboard[Key.T] && !previousKeyboard[Key.T])
            {
                xyz.ToggleVisibility();
            }
            if (thisKeyboard[Key.F] && !previousKeyboard[Key.F])
            {
                GL.ClearColor(r.RandomColor());
            }
            if (thisKeyboard[Key.G] && !previousKeyboard[Key.G])
            {
                grid.ToggleVisibility();
            }
            if (thisKeyboard[Key.Y] && !previousKeyboard[Key.Y])
            {
                obj.ToggleVisibility();
            }

            // Move the camera
            if (thisKeyboard[Key.W])
            {
                cam.MoveForward();
            }
            if (thisKeyboard[Key.S])
            {
                cam.MoveBackward();
            }
            if (thisKeyboard[Key.A])
            {
                cam.MoveLeft();
            }
            if (thisKeyboard[Key.D])
            {
                cam.MoveRight();
            }

            // Zoom
            if (thisKeyboard[Key.E])
            {
                cam.ZoomOut();
            }
            if (thisKeyboard[Key.Q])
            {
                cam.ZoomIn();
            }

            // Set camera to predefined positions
            if (thisKeyboard[Key.Number1])
            {
                cam.Far();
            }
            if (thisKeyboard[Key.Number2])
            {
                cam.Near();
            }

            // Add objects
            if (thisMouse[MouseButton.Left] && !previousMouse[MouseButton.Left])
            {
                ObjList.Add(new Object(true, Ver));
            }

            foreach (Object obj in ObjList)
            {
                obj.UpdatePosition(true);
            }
            foreach (BigObject obj in bigObjList)
            {
                obj.UpdatePosition(true);
            }

            previousKeyboard = thisKeyboard;
            previousMouse = thisMouse;
        }

        // Method for rendering the window
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            // Render elements
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            grid.Draw();
            xyz.Draw();
            obj.Draw();

            foreach (Object obj in ObjList)
            {
                obj.Draw();
            }

            foreach (BigObject obj in bigObjList)
            {
                obj.Draw();
            }

            SwapBuffers();
        }

        // Function to read vertices from file
        public List<Vector3> readVerticesFromFile(string fileName)
        {
            // Read and parse coordinates from file
            List<Vector3> file = new List<Vector3>();

            using (StreamReader sr = new StreamReader(fileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var numbers = line.Split(',');
                    int i = 0;
                    float[] coordinates = new float[3];
                    foreach (var num in numbers)
                    {
                        coordinates[i++] = float.Parse(num);

                        if (coordinates[i - 1] < 0 || coordinates[i - 1] > 250)
                        {
                            throw new ArithmeticException("Invalid vertex!");
                        }
                    }
                    file.Add(new Vector3(coordinates[0], coordinates[1], coordinates[2]));
                }
            }
            return file;
        }

        // Function to timestamp in the code
        private void TimeStampIt(String source, String counter)
        {
            // Display a timestamp
            String dt = DateTime.Now.ToString("hh:mm:ss.ffff");
            Console.WriteLine("\tTSTAMP from <" + source + "> on iteration <" + counter + ">: " + dt);
        }

        // Function to display help information
        private void displayHelp()
        {
            // Display usage instructions
            Console.WriteLine("To exit the application, press: ESC");
            Console.WriteLine("To toggle grid visibility, press: G");
            Console.WriteLine("To reset the scene to initial values, press: R");
            Console.WriteLine("To change the background color, press: F");
            Console.WriteLine("To toggle axis visibility, press: T");
            Console.WriteLine("To move the camera, press: W, A, S, D");
            Console.WriteLine("To set the camera to predefined positions, press: 1, 2");
            Console.WriteLine("For zooming, press: Q to zoom in and E to zoom out");
            Console.WriteLine("To generate a random object, press: Right Click");
        }
    }
}