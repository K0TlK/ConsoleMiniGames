using System;
using System.Threading;

namespace TicTacToe
{
    class Program
    {
        static void Intro()
        {
            string[] text = new string[]
            {
                "╔═══╗────╔╗─╔╗───────╔╗╔═╗──╔╗─╔╗",
                "║╔═╗║───╔╝╚╦╝╚╗──────║║║╔╝─╔╝╚╗║║",
                "║╚═╝╠═╦═╩╗╔╩╗╔╬╗─╔╗──║╚╝╝╔═╩╗╔╬╣║╔╗",
                "║╔══╣╔╣║═╣║─║║║║─║╠══╣╔╗║║╔╗║║╠╣╚╝╝",
                "║║──║║║║═╣╚╗║╚╣╚═╝╠══╣║║╚╣╚╝║╚╣║╔╗╗",
                "╚╝──╚╝╚══╩═╝╚═╩═╗╔╝──╚╝╚═╩══╩═╩╩╝╚╝",
                "──────────────╔═╝║",
                "──────────────╚══╝"
            };

            int maxlen = 0;
            for (int i=0; i<text.Length; i++) if (text[i].Length > maxlen) maxlen = text[i].Length;
            
            for (int i = 0; i<=maxlen + 1; i++)
            {
                for (int j = 0; j < text.Length; j++)
                {
                    int y = i;
                    if (j % 2 == 1) y = text[j].Length - i;
                    if (y > 0 && y <= text[j].Length)
                    {
                        Console.SetCursorPosition(y, j);
                        Console.Write(text[j][y-1]);
                    }
                }
                Thread.Sleep(30);
            }
            Console.SetCursorPosition(0, text.Length + 1);
            Thread.Sleep(400);
            Console.WriteLine("Помощник в Крестики-Нолики (или просто игра)");
            Console.WriteLine("Билд от 27.07.2020, Версия: 2");
            Console.WriteLine("Управление: стрелки, Enter, Spacebar, Backspace.");
            Console.WriteLine("Press any button to continue.");
            Console.ReadKey();
        }

        static void Printfield(int[,] field)
        {
            for (int i = 0; i< 3; i++)
            {
                for (int j = 0; j< 3; j++)
                {
                    if (field[j, i] == 0) Console.Write("[ - ] ");
                    if (field[j, i] == 1) Console.Write("[ X ] ");
                    if (field[j, i] == 2) Console.Write("[ O ] ");
                }
            Console.WriteLine();
            }
            Console.WriteLine();
        }

        static int EmptyTest(int[,] field)
        {
            int empty = 0;
            for (int i = 0; i < 9; i++) if (field[i % 3, (i - i % 3) / 3] == 0) empty++;
            return empty;
        }

        static int WinTest(int[,] field, int Player)
        {

            //Console.WriteLine("WT use");
            //1 - Победа есть; 0 - Победы нет
            for (int i = 0; i < 3; i++)
            {
                if (field[i, 0] == field[i, 1] && field[i, 1] == field[i, 2] && field[i, 0] == Player) return field[i, 0];
                if (field[0, i] == field[1, i] && field[1, i] == field[2, i] && field[0, i] == Player) return field[0, i];
            }
            if (field[0, 0] == field[1, 1] && field[1, 1] == field[2, 2] && field[0, 0] == Player) return field[0, 0];
            if (field[0, 2] == field[1, 1] && field[1, 1] == field[2, 0] && field[0, 2] == Player) return field[0, 2];
            return 0;
        }

        static int AddandTest(int[,] field, int Stroke, int Player)
        {
            int SwapStroke = 1; if (Stroke == 2) SwapStroke = -1;
            int NotPlayer = 1; if (Player == NotPlayer) NotPlayer = 2;
            int result = 0;

            if (EmptyTest(field) != 0)
            {
                for (int i = 0; i < 9; i++)
                    if (field[i % 3, (i - i % 3) / 3] == 0)
                    {
                        int[,] Copyfield = new int[3, 3];
                        for (int k = 0; k < 9; k++) Copyfield[k % 3, (k - k % 3) / 3] = field[k % 3, (k - k % 3) / 3];
                        Copyfield[i % 3, (i - i % 3) / 3] = Stroke;
                        int test = AddandTest(Copyfield, Stroke + SwapStroke, Player);
                        result += test;
                    }
            }

            if (WinTest(field, NotPlayer) != 0) return (-EmptyTest(field) - 1);
            if (WinTest(field, Player) != 0) return (EmptyTest(field) + 1);
            return result;
        }

        static void PrintInfo(int[,] field, int Stroke)
        {
            Printfield(field);
            int SwapStroke = 1; if (Stroke == 2) SwapStroke = -1;

            if (Stroke == 1) Console.Write("Игрок X, ");
            else Console.Write("Игрок O, ");

            Console.WriteLine("советуем выбрать:");
            if (EmptyTest(field) != 0)
            {
                bool flagWin = true, flagDraw = true;

                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        if (field[j, i] == 0)
                        {
                            int[,] Copyfield = new int[3, 3];
                            for (int k = 0; k < 9; k++) Copyfield[k % 3, (k - k % 3) / 3] = field[k % 3, (k - k % 3) / 3];

                            Copyfield[j, i] = Stroke;
                            if (WinTest(Copyfield, Stroke) != 0)
                            {
                                Console.WriteLine("Позицию ({0};{1}) для победы!!!", i + 1, j + 1);
                                Printfield(Copyfield);
                                flagWin = false;
                            }

                            Copyfield[j, i] = Stroke + SwapStroke;
                            if (WinTest(Copyfield, Stroke + SwapStroke) != 0)
                            {
                                Copyfield[j, i] = Stroke;
                                Console.WriteLine("Позицию ({0};{1}) для защиты.", i + 1, j + 1);
                                Printfield(Copyfield);
                                flagDraw = false;
                            }
                        }
                    }


                if (flagDraw && flagWin)
                {
                    int max = -9999999;
                    int maxi = 0, maxj = 0;
                    
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (field[j, i] == 0)
                            {
                                int[,] Copyfield = new int[3, 3];
                                for (int k = 0; k < 9; k++) Copyfield[k % 3, (k - k % 3) / 3] = field[k % 3, (k - k % 3) / 3];
                                Copyfield[j, i] = Stroke;

                                int SStroke = 1; if (Stroke == SStroke) SStroke = 2;
                                int test = AddandTest(Copyfield, SStroke, Stroke);

                                if (test > max)
                                {
                                    max = test;
                                    maxi = i;
                                    maxj = j;
                                }
                            }
                        }
                    }

                    {
                        Console.WriteLine("Позицию ({0};{1}).", maxj + 1, maxi + 1);
                        int[,] Copyfield = new int[3, 3];
                        for (int k = 0; k < 9; k++) Copyfield[k % 3, (k - k % 3) / 3] = field[k % 3, (k - k % 3) / 3];
                        Copyfield[maxj, maxi] = Stroke;
                        Printfield(Copyfield);
                    }

                }
            }

            if (Stroke == 1) Stroke = 2;
            else Stroke = 1;

        }
        
        static void PrintforArrow(int[,] field, int x, int y, int oldx, int oldy)
        {
            Console.SetCursorPosition(oldy * 6, oldx);
            if (field[oldy, oldx] == 0) Console.Write("[ - ]");
            if (field[oldy, oldx] == 1) Console.Write("[ X ]");
            if (field[oldy, oldx] == 2) Console.Write("[ O ]");

            Console.SetCursorPosition(y * 6, x);
            if (field[y, x] == 0) Console.Write("[>-<]");
            if (field[y, x] == 1) Console.Write("[>X<]");
            if (field[y, x] == 2) Console.Write("[>O<]");
            Console.SetCursorPosition(y * 6 + 2, x);
        }

        static void Controller(ref int x, ref int y, ref int Player, ref int[,] field, ref int info)
        {
            info = 0;
            string key = Convert.ToString(Console.ReadKey().Key);
            if (key.Length > 4)
            {
                if (key.Substring(key.Length - 5, 5) == "Arrow")
                {
                    if (key == "UpArrow")
                    {
                        if (x > 0)
                        {
                            PrintforArrow(field, x - 1, y, x, y);
                            x--;
                        }
                        info = 1;
                    }
                    if (key == "DownArrow")
                    {
                        if (x < 2)
                        {
                            PrintforArrow(field, x + 1, y, x, y);
                            x++;
                        }
                        info = 1;
                    }
                    if (key == "LeftArrow")
                    {
                        if (y > 0)
                        {
                            PrintforArrow(field, x, y - 1, x, y);
                            y--;
                        }
                        info = 1;
                    }
                    if (key == "RightArrow")
                    {
                        if (y < 2)
                        {
                            PrintforArrow(field, x, y+1, x, y);
                            y++;
                        }
                        info = 1;
                    }
                }
            }

            if (key == "Enter")
            {
                if (field[y, x] == 0)
                {
                    field[y, x] = Player;
                    if (Player == 1) Player = 2;
                    else Player = 1;
                    Console.Clear();
                    info = 3;
                }
                else
                {
                    info = 0;
                }
            }
            if (key == "Spacebar")
            {
                if (Player == 2) Player = 1;
                else Player = 2;
                Console.Clear();
                info = 4;
            }
            if (key == "Backspace")
            {
                Console.Clear();
                info = 5;
            }
        }

        static void Main(string[] args)
        {
            Intro();
            // 0 - Ничего 1 - Крестик 2 - Нолик
            int Stroke = 1, x = 0, y = 0, info = 0, helper = 1;
            int[,] field = new int[3, 3];

            
            bool reset = true;
            for ( ; ; )
            {
                if (reset)
                {
                    Console.Clear();
                    x = 0; y = 0;
                    for (int i = 0; i < 9; i++) field[i % 3, (i - i % 3) / 3] = 0;
                    if (helper == 1) PrintInfo(field, Stroke);
                    else Printfield(field);
                    Console.SetCursorPosition(1, 0);
                    Console.WriteLine(">-<");
                    Console.SetCursorPosition(2, 0);
                    reset = false;
                }

                Controller(ref x, ref y, ref Stroke, ref field, ref info);
                if (info == 5) if (helper == 1) helper = 0; else helper = 1;


                if (info > 2)
                {
                    Console.Clear();
                    if (helper == 1) PrintInfo(field, Stroke);
                    else Printfield(field);
                    PrintforArrow(field, x, y, x, y);
                }
                else PrintforArrow(field, x, y, x, y);

                if (WinTest(field, 1) != 0)
                {
                    Console.Clear();
                    Console.WriteLine("Победа за игроком X!");
                    Console.WriteLine("Press any button to continue.");
                    Printfield(field);
                    reset = true;
                    Console.ReadKey();
                }
                if (WinTest(field, 2) != 0)
                {
                    Console.Clear();
                    Console.WriteLine("Победа за игроком O!");
                    Console.WriteLine("Press any button to continue.");
                    reset = true;
                    Console.ReadKey();
                }
                if (EmptyTest(field) == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Ходов больше нет...");
                    Console.WriteLine("Press any button to continue.");
                    reset = true;
                    Console.ReadKey();
                }
            }
            
        }
    }
}
