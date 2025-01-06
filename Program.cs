using SFML.System;
using SFML.Window;
using SFML.Graphics;

class Program
{
    static void Main()
    {
        // ИНИЦИАЛИЗАЦИЯ ОКНА

        RenderWindow window = new RenderWindow(new VideoMode(800, 600), "SFML 3.0 C# Example");
        View view = window.GetView();

        // ИНИЦИАЛИЗАЦИЯ ОБЪЕКТОВ

       
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

            window.Display();
        }
    }
}
