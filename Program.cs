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

        public static Player player = new Player();
        public static Map map = new Map(new Vector2f(1000, 1000), new Vector2f(200, 200));

        public static string mapS = "";

        public static Clock clock = new Clock();
        public static float deltaTime = 0;
        public static int frameCount = 0; // счётчик кадров
        public static float fps = 0; // FPS

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
                Console.Clear();
                Console.WriteLine(" Close ");
            };

            window.Resized += (sender, e) =>
            {
                view.Reset(new FloatRect(0, 0, e.Width, e.Height));
            };


            // ЛОГИКА ОКНА
            while (window.IsOpen)
            {
                UpdateFPS();

                deltaTime = clock.Restart().AsSeconds();
                window.DispatchEvents();
                Console.SetCursorPosition(0, 0);

                if (window.HasFocus())
                {
                    player.Move();
                    player.RayCast();
                }

                window.SetView(view);
                window.Clear();

                window.Draw(player.walls);
                map.Draw();

                window.Display();
            }
        }

        static void InitializeMap()
        {
            string[] rows = mapS.Split('\n');
            for (int y = 0; y < rows.Length; y++)
            {
                for (int x = 0; x < rows[y].Length; x++)
                {
                    if (rows[y][x] == '#')
                    {
                        map.NewObject(new FloatRect(x * 100, y * 100, 100, 100));
                    }
                }
            }
        }

        public static void UpdateFPS()
        {
            frameCount++;
            if (frameCount >= 60)
            {
                fps = frameCount / deltaTime;
                Console.WriteLine($"FPS: {fps}");
                frameCount = 0;
            }
        }
    }
}
