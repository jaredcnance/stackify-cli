using System;
using System.Threading;

namespace StackifyCli.Terminal
{
    public static class ConsoleSpiner
    {
        static int counter = 0;
        public static void Turn()
        {
            counter++;
            switch (counter % 16)
            {
                case 0: Console.Write("(|--------)"); break;
                case 1: Console.Write("(-|-------)"); break;
                case 2: Console.Write("(--|------)"); break;
                case 3: Console.Write("(---|-----)"); break;
                case 4: Console.Write("(----|----)"); break;
                case 5: Console.Write("(-----|---)"); break;
                case 6: Console.Write("(------|--)"); break;
                case 7: Console.Write("(-------|-)"); break;
                case 8: Console.Write("(--------|)"); break;
                case 9: Console.Write("(-------|-)"); break;
                case 10: Console.Write("(------|--)"); break;
                case 11: Console.Write("(-----|---)"); break;
                case 12: Console.Write("(----|----)"); break;
                case 13: Console.Write("(---|-----)"); break;
                case 14: Console.Write("(--|------)"); break;
                case 15: Console.Write("(-|-------)"); break;
            }
            Thread.Sleep(50);
            Console.SetCursorPosition(0, Console.CursorTop);
        }

        private static bool _isLoading;
        private static Thread _thread = new Thread(new ThreadStart(() =>
        {
            while (_isLoading)
                Turn();

            ClearBuffer();
        }));

        public static void StartLoading()
        {
            _isLoading = true;
            _thread.Start();
        }

        public static void StopLoading()
        {
            _isLoading = false;
            ClearBuffer();
        }

        private static void ClearBuffer()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            var buffer = new string(' ', Console.BufferWidth);
            Console.WriteLine(buffer);
        }
    }
}