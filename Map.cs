using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace Pseudo3D
{
    class Map
    {
        public List<RectangleShape> boxes;
        private Vector2f size;
        private Vector2f viewSize;
        private RectangleShape mapPlane;

        private CircleShape player = new CircleShape();
        public VertexArray viewCamera = new VertexArray(PrimitiveType.TriangleFan);

        public Map(Vector2f size, Vector2f viewSize)
        {
            this.size = size;
            this.viewSize = viewSize;

            this.mapPlane = new RectangleShape
            {
                Size = viewSize,
                FillColor = Color.White
            };

            this.boxes = new List<RectangleShape>();

            player.Position = new Vector2f(-50, -50);
            player.Radius = 50;
            player.FillColor = Color.Red;
        }

        public void NewObject(FloatRect floatRect)
        {
            RectangleShape shape = new RectangleShape
            {
                Position = new Vector2f(floatRect.Left, floatRect.Top),
                Size = new Vector2f(floatRect.Width, floatRect.Height),
                FillColor = Color.Green
            };

            boxes.Add(shape);
        }

        public void setPlayerPosition(Vector2f newPos)
        {
            player.Position = newPos;
        }

        public void Draw()
        {
            float scaleX = viewSize.X / size.X;
            float scaleY = viewSize.Y / size.Y;

            // Рисуем плоскость карты
            Program.window.Draw(mapPlane);

            // Масштабируем и рисуем ViewCamera
            if (viewCamera.VertexCount >= 2)
            {
                VertexArray scaledViewCamera = new VertexArray(PrimitiveType.TriangleFan);

                for (int i = 0; i < viewCamera.VertexCount; ++i)
                {
                    Vertex vertex = viewCamera[(uint)i];
                    // Масштабируем каждую вершину
                    Vector2f scaledPosition = new Vector2f(vertex.Position.X * scaleX, vertex.Position.Y * scaleY);
                    scaledViewCamera.Append(new Vertex(scaledPosition, vertex.Color));
                }

                Program.window.Draw(scaledViewCamera);
            }

            // Масштабируем и рисуем Player
            CircleShape scaledPlayer = new CircleShape
            {
                Position = new Vector2f(player.Position.X * scaleX, player.Position.Y * scaleY),
                Radius = player.Radius * Math.Min(scaleX, scaleY), // Масштабируем радиус пропорционально
                FillColor = player.FillColor
            };

            Program.window.Draw(scaledPlayer);

            // Масштабируем и рисуем все объекты в списке boxes
            foreach (RectangleShape box in boxes)
            {
                RectangleShape scaledBox = new RectangleShape
                {
                    Position = new Vector2f(box.Position.X * scaleX, box.Position.Y * scaleY),
                    Size = new Vector2f(box.Size.X * scaleX, box.Size.Y * scaleY),
                    FillColor = box.FillColor
                };

                Program.window.Draw(scaledBox);
            }
        }

    }
}
