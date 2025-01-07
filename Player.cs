using SFML.System;
using SFML.Window;
using SFML.Graphics;
using System;

namespace Pseudo3D
{
    class Player
    {
        public Vector2f position = new Vector2f(450, 500);
        public float angle = -(float)(Math.PI / 2); // поворот игрока
        public float FOV = (float)(Math.PI / 3); // угол обзора (60 градусов)

        public float speed = 5 * 30;

        Vector2i lastMousePos = Mouse.GetPosition();
        public VertexArray walls = new VertexArray(PrimitiveType.Quads);

        public void Move()
        {
            Console.WriteLine($"Angle: {angle}");
            angle += (Mouse.GetPosition().X - lastMousePos.X) * 0.02f * Program.deltaTime;
            angle = (angle + (float)Math.PI * 2) % ((float)Math.PI * 2);
            Mouse.SetPosition(new Vector2i((int)VideoMode.DesktopMode.Width / 2, 0));
            lastMousePos = Mouse.GetPosition();

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                position.X += (float)Math.Cos(angle) * speed * Program.deltaTime;
                position.Y += (float)Math.Sin(angle) * speed * Program.deltaTime;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                position.X -= (float)Math.Cos(angle) * speed * Program.deltaTime;
                position.Y -= (float)Math.Sin(angle) * speed * Program.deltaTime;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                position.X += (float)Math.Cos(angle + Math.PI / 2) * speed * Program.deltaTime;
                position.Y += (float)Math.Sin(angle + Math.PI / 2) * speed * Program.deltaTime;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                position.X -= (float)Math.Cos(angle + Math.PI / 2) * speed * Program.deltaTime;
                position.Y -= (float)Math.Sin(angle + Math.PI / 2) * speed * Program.deltaTime;
            }

            Program.map.setPlayerPosition(position);
        }

        public void RayCast()
        {
            Program.map.viewCamera.Clear();
            walls.Clear();

            int numRays = (int)(FOV / Math.PI * 180); // Количество лучей в поле зрения

            for (int i = 0; i < numRays; ++i)
            {
                bool isColliding = false;
                Vector2f rayStart = position + new Vector2f(50, 50);
                Vector2f rayEnd = position + new Vector2f(50, 50);

                float rayDir = angle - (FOV / 2) + (i * (FOV / numRays)); // Направление луча
                float rayLength = 0;
                const float maxRayLength = 1500f;

                while (!isColliding)
                {
                    // Вычисление позиции конечной точки луча
                    rayEnd.X = rayStart.X + (float)Math.Cos(rayDir) * rayLength;
                    rayEnd.Y = rayStart.Y + (float)Math.Sin(rayDir) * rayLength;

                    foreach (RectangleShape rect in Program.map.boxes)
                    {
                        // Проверяем, столкнулся ли луч с объектом
                        if (rect.GetGlobalBounds().Contains(rayEnd))
                        {
                            isColliding = true;

                            // Расстояние до стены
                            float distanceToWall = rayLength;

                            // Проекция стены на экран
                            float wallHeight = 500 / distanceToWall; // Чем дальше, тем ниже стена на экране

                            // Центр стенки на экране
                            float wallCenterX = (i * Program.window.Size.X) / numRays;

                            // Вычисление верхней и нижней точки стены на экране
                            float wallTopY = Program.window.Size.Y / 2 - wallHeight / 2 * 200;
                            float wallBottomY = Program.window.Size.Y / 2 + wallHeight / 2 * 200;

                            // Убираем пробелы между стенами, для этого уменьшаем ширину каждого прямоугольника
                            float wallWidth = 15; // Ширина стены (можно настроить)

                            float stpGray = 255 - Math.Max(0, Math.Min(255, (int)(255 * rayLength / maxRayLength)));

                            Color wallColor = new Color((byte)stpGray, (byte)stpGray, (byte)stpGray);

                            // Создаем прямоугольник для стены с учетом перспективы
                            walls.Append(new Vertex(new Vector2f(wallCenterX - wallWidth, wallTopY), wallColor));
                            walls.Append(new Vertex(new Vector2f(wallCenterX + wallWidth, wallTopY), wallColor));
                            walls.Append(new Vertex(new Vector2f(wallCenterX + wallWidth, wallBottomY), wallColor));
                            walls.Append(new Vertex(new Vector2f(wallCenterX - wallWidth, wallBottomY), wallColor));


                            Program.map.viewCamera.Append(new Vertex(rayStart, Color.Yellow));
                            Program.map.viewCamera.Append(new Vertex(rayEnd, Color.Red));

                            break;
                        }
                    }

                    rayLength += 1; // Шаг перемещения луча
                    if (rayLength > maxRayLength)
                    {
                        isColliding = true;

                        // Если луч не сталкивается, рисуем его
                        Program.map.viewCamera.Append(new Vertex(rayStart, Color.Yellow));
                        Program.map.viewCamera.Append(new Vertex(rayEnd, Color.Red));
                    }
                }
            }
        }

    }
}
