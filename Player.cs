using SFML.System;
using SFML.Window;
using SFML.Graphics;
using System;

namespace Pseudo3D
{
    class Player
    {
        public Vector2f position = new Vector2f(500, 500);
        public float angle = 0; // поворот игрока
        public float FOV = (float)(Math.PI / 3); // угол обзора (60 градусов)

        public float speed = 5;

        Vector2i lastMousePos = Mouse.GetPosition();

        public void Move()
        {
            Console.WriteLine($"Angle: {angle}");
            angle += (Mouse.GetPosition().X - lastMousePos.X) * 0.001f;
            Mouse.SetPosition(new Vector2i((int)VideoMode.DesktopMode.Width / 2, 0));
            lastMousePos = Mouse.GetPosition();

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                position.X += (float)Math.Cos(angle) * speed;
                position.Y += (float)Math.Sin(angle) * speed;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                position.X -= (float)Math.Cos(angle) * speed;
                position.Y -= (float)Math.Sin(angle) * speed;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                position.X += (float)Math.Cos(angle + Math.PI / 2) * speed;
                position.Y += (float)Math.Sin(angle + Math.PI / 2) * speed;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                position.X -= (float)Math.Cos(angle + Math.PI / 2) * speed;
                position.Y -= (float)Math.Sin(angle + Math.PI / 2) * speed;
            }

            Program.map.setPlayerPosition(position);
        }

        public void RayCast()
        {

        }
    }
}
