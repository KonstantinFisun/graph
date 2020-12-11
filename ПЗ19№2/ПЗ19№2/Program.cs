using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ПЗ19__1
{
	struct Edge
	{
		public int first;
		public int second;

		public void DisplayInfo()
		{
			Console.WriteLine($"First: {first}  Second: {second}");
		}
	}
	class Program
	{
		static bool NextCombobj(int[] soc, int n, int k)
		{

			for (int i = k - 1; i >= 0; --i)//начинаем идти с конца в начало
				if (soc[i] < n - k + i)
				{
					soc[i]++;//берем следующий элемент

					for (int j = i + 1; j < k; j++)//следующий элемент меняем на предыдущий + 1
						soc[j] = soc[j - 1] + 1;

					return true;

				}
			return false;
		}
		static public void Reset(ref int[,] arr, int n, int m) //Обнуление массива
		{
			for (int i = 0; i < n; i++)
				for (int j = 0; j < m; j++)
					arr[i, j] = 0;
		}

		static public void PrintArray(int[,] arr, int n, int m, StreamWriter fs)
		{
			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < m; j++)
					fs.Write(arr[i, j] + " ");

				fs.WriteLine();
			}
			fs.WriteLine("---------------------------------");

		}
		static void Main()
		{
			int p, k, q;

			Console.Write("Введите количество вершин = ");
			p = Convert.ToInt32(Console.ReadLine());

			k = 2;

			int[] soc = new int[k];

			for (int i = 0; i < k; i++)
				soc[i] = i;

			int m = 0;//счетчик ребер

			Edge[] edge = new Edge[50];//Массив ребер

			edge[m].first = soc[0];
			edge[m].second = soc[1];
			m++;
			if (p > k)
			{
				//Построения всех возможных ребер в заданном графе
				while (NextCombobj(soc, p, k))
				{
					edge[m].first = soc[0];
					edge[m].second = soc[1];
					m++;
				}
			}
			else Console.WriteLine("Условие не выполняется");


			Console.WriteLine($"{m} - максимальное количество ребер ");



			for (int i = 0; i < m; i++)
			{
				edge[i].DisplayInfo(); //Вывод всех ребер
			}
			Console.WriteLine($"Строим сочитания из ребер");

			FileInfo output = new FileInfo("out.txt");
			StreamWriter fs = output.CreateText();

			int kol_P_Q = 0;//Счетчик числа p,q графов

			int[,] Matrix = new int[p, p];

			for (int g=0;g<=m;g++)
			{
							
				Reset(ref Matrix, p, p);

				int[] socq = new int[g];

				for (int i = 0; i < g; i++)
				{
					socq[i] = i;
					Console.Write(socq[i] + " ");
					//Заполняем таблицу смежности
					Matrix[edge[socq[i]].first, edge[socq[i]].second] = 1;
					Matrix[edge[socq[i]].second, edge[socq[i]].first] = 1;
				}
				Console.WriteLine();

				kol_P_Q++;
				PrintArray(Matrix, p, p, fs);
				Reset(ref Matrix, p, p);


				if (p == m) Console.WriteLine("1 граф");
				else
				{
					//Строим сочитания из ребер
					while (NextCombobj(socq, m, g))
					{
						for (int i = 0; i < g; i++)
						{
							Console.Write(socq[i] + " ");
							//Заполняем таблицу смежности
							Matrix[edge[socq[i]].first, edge[socq[i]].second] = 1;
							Matrix[edge[socq[i]].second, edge[socq[i]].first] = 1;

						}
						Console.WriteLine();

						PrintArray(Matrix, p, p, fs); //выводим в файл
						Reset(ref Matrix, p, p); //обнуляем
						kol_P_Q++;
					}

				}

			}

			fs.WriteLine("Количество графов - " + kol_P_Q);
			fs.Close();
			Console.WriteLine("Программа завершена");
			Console.ReadKey();
		}
	}
}
