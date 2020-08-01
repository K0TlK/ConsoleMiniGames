using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Saper_0._0._0._0._0._0._1
{
    class Program
    {
        static void Main(string[] args)
        {  /*Console.WriteLine(" ■ 2 _ _ _ ▬ ■>2<■ ■");
            for (int i=0;i<3;i++)
            {
                Console.Beep();
                Thread.Sleep(90);
            }
            Console.WriteLine(@"Опа, дорова
А это мой сапёр кстати");
            Thread.Sleep(3000); Console.Beep();
            Console.Clear();*/

            Random rand = new Random();
            int x=3,y=3,sx,sy;
            bool boo = false, PO = false ;
            Console.WriteLine("Какое поле делаем?");
            
            Console.Write("x=");
            y = Convert.ToInt32(Console.ReadLine());

            Console.Write("y=");
            x = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("А бомб?");
            sx = Convert.ToInt32(Console.ReadLine());
            if (x >= 100) x = 99;
            if (x <= 1) x = 2;
            if (y >= 100) y = 99;
            if (y <= 0) y = 1;
            bool[,,] Game = new bool[4, x+1, y+1];

            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                {
                    Game[2, i, j] = false;
                    Game[3, i, j] = false;
                }
                    

            if (sx >= (x * y)) sx = x * y - 1;
            if (sx <= 0) sx = 1;
            int flag = sx, bomb = sx;

            do
            {
                boo = true;
                do
                {
                    int i = rand.Next(1, x+1)-1;
                    int j = rand.Next(1, y+1)-1;
                    if (Game[1, i, j] == false)
                    {
                        Game[1, i, j] = true;
                        boo = false;
                    }
                } while (boo);
                sx--;
            } while (sx != 0);

            sx = 0;
            sy = 0;
            Console.Clear();

            do
            {
                for (int i = 0; i < x; i++)
                {
                    Console.Write(" ");
                    for (int j = 0; j < y; j++)
                    {
                        if (Game[2, i, j] == false)
                        {
                            if (Game[3, i, j] == false) Console.Write("■ "); else Console.Write("▬ ");
                        }
                        else
                        {
                            int boom = 0;

                            for (int i2 = -1; i2 < 2; i2++)
                                if (((i + i2) >= 0) & ((i + i2) <= x))
                                    for (int j2 = -1; j2 < 2; j2++)
                                        if (((j + j2) >= 0) & ((j + j2) <= y))
                                            if (Game[1, i + i2, j + j2] == true) boom++;

                            if (Game[1, i, j] == false)
                            {
                                if (boom > 0) Console.Write("{0} ", boom);
                                else Console.Write("_ ");
                            }
                            else Console.Write("O ");
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine("Кол-во ▬: {0}; Кол-во бомб: {1}", flag, bomb);

                Console.SetCursorPosition((sy + 1) * 2 - 2, sx); Console.Write(">");
                Console.SetCursorPosition((sy + 1) * 2, sx); Console.Write("<");
                Console.SetCursorPosition((sy + 1) * 2 - 1, sx);

                string key = Convert.ToString(Console.ReadKey(true).Key);
                if (key == "Enter")
                {
                    if ((Game[3, sx, sy] == false) & (flag > 0))
                    {
                        flag--;
                        Game[3, sx, sy] = true;
                        PO = true;
                    }
                    else if (Game[3, sx, sy] == true)
                    {
                        flag++;
                        Game[3, sx, sy] = false;
                    }
                }
                if (key == "Spacebar")
                {
                    if (Game[1, sx, sy] == false)
                    {
                        PO = true;
                        boo = false;

                        for (int i = -1; i < 2; i++) if (((i + i) >= 0) & ((i + i) <= x))
                                for (int j = -1; j < 2; j++) if (((j + j) >= 0) & ((j + j) <= y))
                                        if (Game[1, sx + i, sy + j] == true) boo = true;

                        if (boo == true) Game[2, sx, sy] = true;
                        else
                        {
                            for (int i = -1; i < 2; i++) if (((i + i) >= 0) & ((i + i) <= x))
                                    for (int j = -1; j < 2; j++) if (((j + j) >= 0) & ((j + j) <= y))
                                            Game[2, sx + i, sy + j] = true;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        for (int j = 0; j < 9; j++)
                        {
                            Console.WriteLine("Блин, ты лох");
                            Thread.Sleep(500);
                        }
                        Console.WriteLine("Блин, ты лох :з");
                        break;
                    }
                }

                if (key == "RightArrow")
                {
                    if (sy + 1 < y) sy++;
                }
                if (key == "DownArrow")
                {
                    if (sx + 1 < x) sx++;
                }
                if (key == "LeftArrow")
                {
                    if (sy - 1 >= 0) sy--;
                }
                if (key == "UpArrow")
                {
                    if (sx - 1 >= 0) sx--;
                }

                {
                    int final = x * y;
                    do
                    {
                        PO = false;
                        for (int i0 = 0; i0 < x; i0++)
                            for (int j0 = 0; j0 < y; j0++)
                                if (Game[2, i0, j0] == true)
                                {
                                    int boom = 0;

                                    for (int i2 = -1; i2 < 2; i2++)
                                        if (((i0 + i2) >= 0) & ((i0 + i2) <= x))
                                            for (int j2 = -1; j2 < 2; j2++)
                                                if (((j0 + j2) >= 0) & ((j0 + j2) <= y))
                                                    if ((Game[1, i0 + i2, j0 + j2] == true) || ((Game[3, i0 + i2, j0 + j2] == true))) boom++;

                                    if (boom == 0)
                                    {
                                        for (int i = -1; i < 2; i++) if (((i0 + i) >= 0) & ((i0 + i) <= x))
                                                for (int j = -1; j < 2; j++) if (((j0 + j) >= 0) & ((j0 + j) <= y))
                                                        if (Game[2, i0 + i, j0 + j] == false)
                                                        {
                                                            Game[2, i0 + i, j0 + j] = true;
                                                            PO = true;
                                                        }
                                    }
                                    
                                    final--;
                                }

                    } while (PO);
                    if (final == bomb)
                    {
                        Console.Clear();
                        do
                        {
                            Console.Beep();
                            Console.WriteLine("ПОБЕДА! ТЫ ЖИВ!");
                            Thread.Sleep(2000);
                        } while (true);
                    }
                }
                

                Console.SetCursorPosition(0, 0);
            } while (true);
        }
    }
}
