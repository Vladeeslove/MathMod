using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathModeling
{
    #region Dijkstra
    class Dijkstra
    {
        int[,,] matrix = new int[10, 10, 5];
        int hod = 1;
        int take = 0;
        public Dijkstra(int[,,] _matrix)
        {
            matrix = _matrix;
        }
        public void Switch()
        {
        metka:
            Console.WriteLine("1 for out matrix\n" +
                "5 forexit\n" +
                "2 for StartDijkstra");
            try
            {
                int sw = Convert.ToInt32(Console.ReadLine());

                switch (sw)
                {
                    case 1:
                        {
                            MatrixTop();
                            goto metka;
                        };
                    case 666:
                        {
                            Console.WriteLine("Исходная вершина");
                            take = Convert.ToInt32(Console.ReadLine());
                            AdjustMatrixWithDekstr(take - 1);
                            goto metka;
                        };
                    case 5:
                        return;
                    default:
                        goto metka;
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);goto metka;
            }
        }

        private void MatrixTop()//матрица смежности
        {
            int Top = 0;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("   1 2 3 4 5 6 7 8 9 10\n");
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < 10; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                if (i + 1 < 10)
                    Console.Write(Convert.ToString(i + 1) + "  ");
                else
                    Console.Write(Convert.ToString(i + 1) + " ");
                Console.ForegroundColor = ConsoleColor.White;
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(Convert.ToString(matrix[i, j, Top]) + " ");
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private void AdjustMatrixWithDekstr(int sw)
        {
            matrix[sw, sw, 4] = 1;
            matrix[sw, sw, 2] = 0;
            int temp;

            for (int Top = sw, i = 0; i < 10; i++)
            {
                /* if (Top <10) Top = 9 - i;
                   Console.WriteLine("Top " + Top);*/
                temp = FindFriendTop(Top);
                if (passiveTop(Top) == true)
                {
                    matrix[Top, Top, 4] = 1;
                    //  Console.WriteLine(Top + " Вершина постоянная");
                }
                Top = temp;

                if (Top + 1 < 1 || Top == 999)
                {
                    //  Console.WriteLine("все пост" +Top);
                    break;
                }


            }
            Console.WriteLine("\nПодводим результат:");
            OutInfo(sw);
            Console.ReadKey(true);
        }
        private int FindFriendTop(int CurrentTop)
        {
            bool chek = false;

            Console.WriteLine("\nШаг№" + hod + "|Расмотрим вершину " + (CurrentTop + 1));
            int ind = 999;
            int sum = matrix[CurrentTop, CurrentTop, 2];//
            ColorBlue("Вершина  Метка Статус");//


            chek = false;
            for (int i = 0; i < 10; i++)
            {
                if (matrix[CurrentTop, i, 1] + matrix[CurrentTop, CurrentTop, 2] < matrix[i, i, 2] && matrix[CurrentTop, i, 0] == 1 && matrix[i, i, 4] != 1 && CurrentTop != i)
                {
                    matrix[i, i, 2] = matrix[CurrentTop, i, 1] + matrix[CurrentTop, CurrentTop, 2];
                    matrix[i, i, 3] = CurrentTop + 1;
                    matrix[CurrentTop, i, 3] = 100;
                    if (ind == 999)
                        ind = i;
                    chek = true;
                    Console.Write("{0,4}{1,7},{2,1}", i + 1, matrix[i, i, 2], matrix[i, i, 3]);
                    Console.WriteLine("   Врем.");
                }
                else if (i != CurrentTop)
                    matrix[CurrentTop, i, 3] = 100;
                if (matrix[i, i, 4] == 1)
                {
                    Console.Write("{0,4}{1,7},{2,1}", i + 1, matrix[i, i, 2], matrix[i, i, 3]);
                    Console.WriteLine("   Пост.");
                }
                else if (passiveTop(i) == true)
                {
                    Console.Write("{0,4}{1,7},{2,1}", i + 1, matrix[i, i, 2], matrix[i, i, 3]);
                    Console.WriteLine("   Пост.");
                    matrix[i, i, 4] = 1;
                }
            }

            matrix[CurrentTop, CurrentTop, 4] = 1;
            hod++;
            int min = 10000;
            //  if(passiveTop(i))
            for (int i = 0; i < 10; i++)
            {
                //ipassiveTop(i)
                if (min > matrix[i, i, 2] && matrix[i, i, 4] != 1 && matrix[i, i, 2] != 0)
                {
                    min = matrix[i, i, 2];
                    ind = i;
                    //  Console.WriteLine("minimal" + ind);
                }
            }
            CurrentTop = ind;
            if (chek == false)
            {
                if (CheckIt() == 999)
                    return 999;
                else
                    CurrentTop = CheckIt();
            }
            return CurrentTop;
        }
        public int CheckIt()
        {
            int min = 10000;
            int ind = 999;
            for (int i = 0; i < 10; i++)
            {
                //ipassiveTop(i)
                if (min > matrix[i, i, 2] && matrix[i, i, 4] != 1 && matrix[i, i, 2] != 0)
                {
                    min = matrix[i, i, 2];
                    ind = i;
                    //  Console.WriteLine("Вот  оно" + ind);
                }
            }
            return ind;
        }
        public bool passiveTop(int index)
        {
            for (int i = 0; i < 10; i++)
            {
                if (matrix[i, index, 3] != 100 && matrix[i, index, 0] == 1)
                {
                    return false;
                }
            }
            return true;
        }
        public void OutInfo(int currentTop)
        {

            int temp = 1;
            ColorCreen();
            int sw = 0;// Convert.ToInt32(Console.ReadLine());
            string[] str = new string[10];
            for (int k = 0; k < 10; k++)
            {
                // sw=k;
                sw = k;
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("\nD{0}-{1}\n", currentTop + 1, (sw + 1));
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
                for (int i = 0, j = 0; ; j++)
                {

                    Console.Write((sw + 1) + "(");
                    Console.Write(matrix[sw, sw, 2] + ")");
                    if (sw == 0)
                        break;
                    else
                    {
                        Console.Write("<---");
                    }
                    if (matrix[sw, sw, 2] == 0)
                        break;
                    if (matrix[sw, sw, 3] == 0)
                        sw = matrix[sw, sw, 3];
                    sw = matrix[sw, sw, 3] - 1;
                }

            }

        }
        public void ColorCreen()
        {
            ColorBlue("Вершина  Метка Статус");
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 0; i < 10; i++)
            {
                Console.Write("{0,4}{1,7},{2,1}", i + 1, matrix[i, i, 2], matrix[i, i, 3]);
                Console.WriteLine("   Пост.");
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public void ColorBlue(string text)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine(text);
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
    #endregion Dijkstra

    #region Floid
    class Floid
    {
        int[,] D = new int[10, 10];
        int[,] S = new int[10, 10];
        public Floid()
        {
            //outMatrixs();
        }
        public Floid(int[,] _D, int[,] _S)
        {
            D = _D;
            S = _S;
        }
        public void FloidSwitch()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Шаг №" + (i + 1));
                outMatrixs(i);
                InsiderWithStep(i);
                outMatrixs(i);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("_______________________________________________________________________");
                Console.ForegroundColor = ConsoleColor.Gray;

            }
            OutResult();
        }
        protected void InsiderWithStep(int k)
        {
            for (int i = 0; i < 10; i++)
            {
                if (i == k)
                    continue;
                for (int j = 0; j < 10; j++)
                {
                    if (i == j || j == k)
                        continue;
                    if (D[i, j] > D[i, k] + D[k, j])
                    {
                        D[i, j] = D[i, k] + D[k, j];
                        S[i, j] = k + 1;
                    }
                }
            }
        }
        public void outMatrixs(int currentStep)
        {

            ColorGreen(Convert.ToString("D  1  2  3  4  5  6  7  8  9  10"));
            Console.Write("      ");
            ColorColorBlue(Convert.ToString("S   1  2  3  4  5  6  7  8  9  10\n"));//семь пробелов от 1- до S
            for (int i = 0; i < 10; i++)
            {
                if (i < 9)
                    ColorGreen(Convert.ToString((i + 1) + " "));
                else
                    ColorGreen(Convert.ToString((i + 1)));
                for (int j = 0; j < 10; j++)
                {
                    if (i == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    if (i == currentStep || j == currentStep)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (D[i, j] < 10)
                        {
                            if (j != 0 && D[i, j - 1] < 10)
                                Console.Write("  " + D[i, j]);
                            else
                                Console.Write(" " + D[i, j]);
                        }
                        else if (j != 0 && D[i, j - 1] < 10)
                            Console.Write("  " + D[i, j]);
                        else
                            Console.Write(" " + D[i, j]);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        continue;
                    }
                    if (j != 0 && D[i, j - 1] < 10)
                        Console.Write("  " + D[i, j]);
                    else
                        Console.Write(" " + D[i, j]);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                if (i <= 9)
                {
                    if (D[i, 9] < 10)
                        Console.Write("       ");
                    else
                        Console.Write("      ");
                    ColorColorBlue(Convert.ToString((i + 1) + " "));
                }
                else
                {
                    Console.Write("       ");
                    ColorColorBlue(Convert.ToString((i + 1)));
                }
                for (int j = 0; j < 10; j++)
                {
                    if (i == j)
                        Console.ForegroundColor = ConsoleColor.Yellow;

                    if (i == currentStep || j == currentStep)
                    {

                        Console.ForegroundColor = ConsoleColor.Red;
                        if (S[i, j] < 10 && S[i, j] < 10)
                            Console.Write("  " + S[i, j]);
                        else
                            Console.Write("  " + S[i, j]);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        continue;
                    }
                    Console.Write("  " + S[i, j]);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        protected void OutResult()
        {
            Console.WriteLine("Введите начальную вершину ");
            int first = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите конечную вершину ");
            int last = Convert.ToInt32(Console.ReadLine());
            Console.Write("{0}--->", first);
            int temp = 0;
            while (first != last)
            {
                if (first != last)
                    Console.Write("{0}({1})---->", (S[first - 1, last - 1]), D[first - 1, S[first - 1, last - 1] - 1]);
                else
                    Console.Write("{0}({1})\n", (S[first - 1, last - 1]), D[first - 1, S[first - 1, last - 1] - 1]);

                temp += D[first - 1, S[first - 1, last - 1] - 1];
                first = S[first - 1, last - 1];
            }
            Console.WriteLine("\nОбщий вес: " + temp);
        }
        public void ColorGreen(string text)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(text);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public void ColorColorBlue(string text)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }

    }
    #endregion Floid

    #region Ford
    class Ford
    {
        int min = 0;
        int maxFlow = 0;
        int[] way = new int[10];
        bool AccessTop = true;
        int[,,] Matrix = new int[10, 10, 2];
        int amountTemp = 0;
        int[,] MatrixSm =
          {
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
            };
        int[,] E =
         {
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
            };
        public Ford()
        {
            for (int i = 0; i < E.GetLongLength(1); i++)
            {
                for (int j = 0; j < E.GetLongLength(1); j++)
                {
                    E[i, j] = 0;
                }
            }
            E[0, 1] = 110;
            E[0, 2] = 100;
            E[0, 3] = 150;
            E[1, 3] = 50;
            E[1, 5] = 50;
            E[1, 7] = 30;
            E[2, 3] = 60;
            E[2, 4] = 20;
            E[3, 4] = 30;
            E[3, 6] = 60;
            E[3, 8] = 50;
            E[3, 5] = 25;
            E[4, 9] = 60;
            E[5, 6] = 20;
            E[5, 7] = 40;
            E[6, 4] = 30;
            E[6, 8] = 45;
            E[7, 9] = 100;
            E[8, 7] = 15;
            E[8, 9] = 80;
            Console.WriteLine("Матрица смежности");

            for (int i = 0; i < E.GetLongLength(1); i++)
            {
                for (int j = 0; j < E.GetLongLength(1); j++)
                {
                    if (0 != E[i, j])
                    {
                        MatrixSm[i, j] = 1;
                    }
                    else
                        MatrixSm[i, j] = 0;
                    Console.Write(MatrixSm[i, j] + " ");
                }
                Console.WriteLine();
            }
            startFord();
        }
        private void startFord()
        {
        metka1:
            int indexPrev;
            int min = 10000;

            int indexCurrent = 0;
        metka:
            bool check = false;
            int max = 0;
            int indexTemp = indexCurrent;

            Console.WriteLine("_____________________________________________________________________________________________________\nВыбирается вершина с пропускной способностью && с которой смежна текущая && не была посещена\nВершины которые удовлетворяют условию: ");
            for (int i = 0; i < 10; i++)
            {
                if (max < E[indexCurrent, i] && MatrixSm[indexCurrent, i] == 1 && Matrix[indexCurrent, i, 0] == 0)
                {

                    Console.WriteLine("({0}) емкость={1}", i, E[indexCurrent, i]);
                    if (i == 0)
                        min = E[indexCurrent, i];
                    max = E[indexCurrent, i];
                    //+
                    Console.WriteLine("Top {0} to {1}\nLong {2}", indexCurrent, i, E[indexCurrent, i]);
                    //indexPrev = indexCurrent;
                    indexTemp = i;
                    //finded = true;
                    check = true;//ddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd
                }

            }
            Matrix[indexCurrent, indexTemp, 0] = 1;
            try
            {

                way[amountTemp] = indexCurrent;
            }
            catch (Exception e)
            {
                Console.WriteLine(amountTemp);
                return;
            }
            amountTemp++;
            if (GoBack(indexTemp) == true && indexTemp != 9 || check == false)
            {
                Console.WriteLine("Проверка показала что из вершины {0} нет не посещённых вершин! ", indexTemp);
                min = 10000;

                amountTemp -= 2;
                if (indexTemp == 0)
                {
                    Console.WriteLine(maxFlow);
                    return;
                }
                indexCurrent = way[amountTemp];
                goto metka;
            }
            if (min > max && max != 0)
            {
                Console.WriteLine("min({0})={1}", min, max);
                min = max;
            }
            indexCurrent = indexTemp;
            Console.WriteLine("Переходим к вершине {0} с максимальной емкостью {1}", indexCurrent, max);
            if (indexCurrent == 9)
            {
                amountTemp--;
                for (int i = 9; i != 0;)
                {
                    Console.WriteLine("Рассматриваем обратны путь, путь от {0} к {1}", way[amountTemp], i);
                    E[way[amountTemp], i] -= min;
                    i = way[amountTemp];
                    amountTemp--;
                }
                indexCurrent = 0;
                maxFlow += min;
                Console.WriteLine("Текущее значени maxFlow=" + maxFlow);
                Array.Clear(Matrix, 0, Matrix.Length);
                amountTemp = 0;
                goto metka1;
            }
            else
            {

                Console.WriteLine("Возобновление стандартное");
                goto metka;
            }
        }

        private bool GoBack(int currentTop)
        {
            for (int j = 0; j < 10; j++)
                if (Matrix[currentTop, j, 0] == 0 && MatrixSm[currentTop, j] == 1)
                    return false;
            return true;
        }

    }
    #endregion Ford
    //Ford badly decoratedб sry(
    class MathModeling
    {
        static void Main(string[] args)
        {
            #region MatrixForDijkstra
            //where 
            //      the first element is adjacency elements (not diagonal)
            //      the second element is value of arcs (not diagonal)
            //      the third element is weight value by vertex

            //      in processing of the Matrix cells(for vertex):
            //          the 4 element is mark of visit

            //is uncomment for test work algh.Dijkstras
            int[,,] TheMatrix =
          {
                  { { 0,0,10000,0,0 }, { 1,6,0,0,0 }, { 1,16,0,0,0}, { 1,1/*17*/,0,0,0 }, { 0,0,0,0,0 }, { 0,0,0,0,0 }, { 0,0,0,0,0 }, { 0,0,0,0,0 } , { 0,0,0,0,0 } , { 0,0,0,0,0 }  },
                  { { 1,6,0,0,0 }, { 0,0,10000,0,0 }, { 1,8,0,0,0 }, { 0,0,0,0,0 }, { 1,15,0,0,0 }, { 1,8,0,0,0 }, { 0,0,0,0,0 } , { 0,0,0,0,0 } , { 0,0,0,0,0 } , { 0,0,0,0,0 } },
                  { { 0,0,0,0,0 }, { 1,8,0,0,0 }, { 0,0,10000,0,0 }, { 1,5,0,0,0 }, { 1,7,0,0,0 }, { 0,0,0,0,0 }, { 0,0,0,0,0 }, { 1,8,0,0,0 }, { 0,0,0,0,0 } , { 0,0,0,0,0 }  },
                  { { 1,17,0,0,0 }, { 1,4,0,0,0 }, { 0,0,0,0,0 }, { 0,0,10000,0,0 }, { 0,0,0,0,0 }, { 1,9,0,0,0 }, { 1,7,0,0,0 }, { 0,0,0,0,0 } , { 0,0,0,0,0 } , { 0,0,0,0,0 }  },
                  { { 1,14,0,0,0 }, { 1,15,0,0,0 }, { 1,7,0,0,0 }, { 1,20,0,0,0 }, { 0,0,10000,0,0 }, { 1,10,0,0,0 }, { 0,0,0,0,0 }, { 0,0,0,0,0 } , { 0,0,0,0,0 } , { 0,0,0,0,0 }  },
                  { { 0,0,0,0,0 }, { 0,0,0,0,0 }, { 0,0,0,0,0 }, { 0,0,0,0,0 }, { 1,10,0,0,0 }, { 0,0,10000,0,0 }, { 1,15,0,0,0 }, { 0,0,0,0,0 } , { 1,8,0,0,0 } , { 0,0,0,0,0 }  },
                  { { 0,0,0,0,0 }, { 0,0,0,0,0 }, { 0,0,0,0,0 }, { 1,7,0,0,0 }, { 0,0,0,0,0 }, { 1,15,0,0,0 }, { 0,0,10000,0,0 }, { 0,0,0,0,0 } , { 0,0,0,0,0 } , { 1,16,0,0,0 }  },
                  { { 0,0,0,0,0 }, { 0,0,0,0,0 }, { 1,8,0,0,0 }, { 0,0,0,0,0 }, { 1,10,0,0,0 }, { 0,0,0,0,0 }, { 0,0,0,0,0 }, { 0,0,10000,0,0 } , { 1,9,0,0,0 } , { 0,0,0,0,0 }  },
                  { { 0,0,0,0,0 }, { 0,0,0,0,0 }, { 0,0,0,0,0 }, { 0,0,0,0,0 }, { 0,0,0,0,0 }, { 1,8,0,0,0 }, { 0,0,0,0,0 }, { 1,9,0,0,0 } , { 0,0,10000,0,0 } , { 1,10,0,0,0 }  },
                  { { 0,0,0,0,0 }, { 0,0,0,0,0 }, { 0,0,0,0,0 }, { 0,0,0,0,0 }, { 1,14,0,0,0 }, { 0,0,0,0,0 }, { 0,0,0,0,0 }, { 0,0,0,0,0 } , { 1,10,0,0,0 } , { 0,0,10000,0,0 }  },
              };
            //  Dijkstra dijkstra = new Dijkstra(TheMatrix);
            //  dijkstra.Switch();
            #endregion MatrixforDijkstra

            #region Floid
            int[,] D =
            {
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
            };
            int[,] S =
             {
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
                { 1,2,3,4,5,6,7,8,9,10 },
            };
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == j)
                    {
                        D[i, j] = 0;
                        S[i, j] = 0;
                        //continue;
                    }
                    if (TheMatrix[i, j, 1] != 0)
                        D[i, j] = TheMatrix[i, j, 1];
                    else
                        D[i, j] = 99;
                    S[i, j] = j + 1;
                }
            }
        #endregion Floid

        #region Ford

        #endregion Ford
        metka:
            Console.WriteLine("(99 for exit)\n1 for Dijkstra\n2 for Floid\n3 for Ford");
            try
            {

                int sw = Convert.ToInt32(Console.ReadLine());

                switch (sw)
                {
                    case 1:
                        {
                            Dijkstra Dekstr = new Dijkstra(TheMatrix);
                            Dekstr.Switch();
                            goto metka;
                        }
                    case 2:
                        {
                            Floid ob = new Floid(D, S);
                            ob.FloidSwitch();
                            goto metka;
                        }
                    case 3:
                        {
                            Ford ob = new Ford();
                            goto metka;
                        }
                    case 99:
                        break;
                    default:throw new Exception("Такого пока что нет:(");
                        
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                goto metka;
            }

        }
    }
}
