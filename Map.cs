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

        public void Draw()
        {
            float scaleX = viewSize.X / size.X;
            float scaleY = viewSize.Y / size.Y;

            Program.window.Draw(mapPlane);

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
