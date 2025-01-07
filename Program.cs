using SFML.System;
using SFML.Window;
using SFML.Graphics;
using System;
using System.Collections.Generic;

namespace Pseudo3D
{
    class Program
    {
        // ИНИЦИАЛИЗАЦИЯ ОКНА
        public static RenderWindow window = new RenderWindow(new VideoMode(1280, 720), "3D game!");
        private static View view = window.GetView();

        public static Map map = new Map(new Vector2f(1000, 1000), new Vector2f(200, 200));

        public static string mapS;

        static void Main()
        {
            // ИНИЦИАЛИЗАЦИЯ ОБЪЕКТОВ
            mapS =
                "##########\n" +
                "#........#\n" +
                "#..####..#\n" +
                "#........#\n" +
                "#........#\n" +
                "#........#\n" +
                "#........#\n" +
                "#........#\n" +
                "#........#\n" +
                "##########";

            InitializeMap();

            // ОБРАБОТКА СОБЫТИЙ
            window.Closed += (sender, e) =>
            {
                window.Close();
                Console.WriteLine("Close");
            };

            window.Resized += (sender, e) =>
            {
                view.Reset(new FloatRect(0, 0, e.Width, e.Height));
            };

            // ЛОГИКА ОКНА
            while (window.IsOpen)
            {
                window.DispatchEvents();

                if (window.HasFocus())
                {

                }

                window.SetView(view);
                window.Clear();

                map.Draw();

                window.Display();
            }
        }

        // Метод для инициализации карты на основе строки
        static void InitializeMap()
        {
            string[] rows = mapS.Split('\n');
            for (int y = 0; y < rows.Length; y++)
            {
                for (int x = 0; x < rows[y].Length; x++)
                {
                    if (rows[y][x] == '#')
                    {
                        // Создаем блок размером 100x100
                        map.NewObject(new FloatRect(x * 100, y * 100, 100, 100));
                    }
                }
            }
        }
    }
}
