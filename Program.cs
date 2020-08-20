using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int n = 6;
                string[,] ars = new string[,]{
                 { "83" ,"255","-99","711","w"  ,"199"}
                ,{ "670","Q"  ,"134","210","164","178"}
                ,{ "135","123","330","241","177","213"}
                ,{ "q"  ,"169","143","154","194","126"}
                ,{ "956","459","444","122","555","453"}
                ,{ "333","677","888","832","245","228"}};

                getResult(ars, n);
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("Error! Установите числа как '405' а не как '4f5' , а символы как 'a' а не как 'aa' ");
            }
            Console.ReadLine();
        }

        static void getResult(string[,] mo, int n)
        {
            //Показываем изначальную матрицу с символами
            Console.WriteLine();
            Console.WriteLine("Изначальная матрица:");
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(mo[i, j]);
                    Console.Write("\t");
                }
                Console.WriteLine();
            }

            //(Часть 1.)Переводим все в матрицу цифр(символы в цыфры) 
            int[,] intMatryx = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    //если длина больше 1 значит это число (или кто-то плохо пишет цыфры по типу "9q3 что произведет к ошибке в Main которую поймает catch блок")
                    if (mo[i, j].Length > 1)
                        intMatryx[i, j] = Convert.ToInt32(mo[i, j]);
                    //Иначе если днила 1 то это символ,символы в байты а 1-9 это тоже символы юникода соответствующие своему значению
                    else if (mo[i, j].Length == 1)
                        intMatryx[i, j] = (int)Convert.ToChar(mo[i, j]);
                    //Если строка пустая - заменим на 0
                    else
                        intMatryx[i, j] = 0;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Матрица с кодом ASCII вместо символов:");
            Console.WriteLine();
            //Выводим на екран матрицу с цифрами где заменили символы на цифры
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(intMatryx[i, j]);
                    Console.Write("\t");
                }
                Console.WriteLine();
            }

            //(Часть 2.) Самый главный алгоритм который будет менять местами диагональ с минимальным элементом  
            int ki = 0, kj = 0;
            int min = 0;
            //Переменная посмотреть берём ли мы первый перебираемый элемент,если да то считаем что он минимальный а дальше ищем если есть что-то минимальнее
            bool firstStep = true;
            //Переменные для перестановки местами 
            string r1, r2;
            int temp;

            for (int k = 0; k < n; k++)
            {

                firstStep = true;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        //Если на диагонали (i==j) и диагональный элемент находиться на месте,где мы уже установили минимальное значение(i<k) где i индекс по горизонтали а k это шаг то пропускаем элемент и переходим дальше
                        //Это значит что минимальный элемент уже установлен на свое место,и мы ищем следующий минимальный элемент в матрице избега ситуации когда берется один и тот же элемент
                        if (i == j && i < k)
                            continue;
                        //Если нашли элемент поменьше текущего или это первый просмотр элемента то запоминаем его как "минимальный" вместе с индексами
                        else if (min >= intMatryx[i, j] || firstStep)
                        {
                            //Ну и конечно говорим что первый элемент взяли как минимальный чтобы не брать все подряд а сравнивать
                            firstStep = false;
                            min = intMatryx[i, j];
                            ki = i;
                            kj = j;
                        }
                    }
                }
                //Меняем местами элементы исходной таблицы (с символами)
                r1 = mo[k, k];
                r2 = mo[ki, kj];

                mo[k, k] = r2;
                mo[ki, kj] = r1;
                //Меняем местами элементы талицы чисел 
                temp = intMatryx[k, k];
                intMatryx[k, k] = min;
                intMatryx[ki, kj] = temp;
            }

            Console.WriteLine();
            Console.WriteLine("Поменяли местами:");
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(mo[i, j]);
                    Console.Write("\t");
                }
                Console.WriteLine();
            }
        }
    }
}
