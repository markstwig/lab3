string selection = "";
do
{
    Console.WriteLine("╔═══════════════╗");
    Console.WriteLine("║ Вариант 17    ║");
    Console.WriteLine("╠═══════════════╣");
    Console.WriteLine("║ 1. Задание 1  ║");
    Console.WriteLine("║ 2. Задание 2  ║");
    Console.WriteLine("║ 3. Задание 3  ║");
    Console.WriteLine("║ 0. Выход      ║");
    Console.WriteLine("╚═══════════════╝");
    Console.Write("Выберите задание: ");
    selection = Console.ReadLine();
    switch (selection)
    {
        case "1":
            Console.Write("Введите количество элементов массива: ");
            int n = int.Parse(Console.ReadLine());
            double[] array = GenerateArray(n);
            int pos = FindPos(array);
            int lastZero = FindLastZero(array);
            
            Console.WriteLine($"- Количество положительных элементов массива: {pos}");
            
            if (lastZero != -1) {
                double sumAfterLastZero = FindSumAfterElement(array, lastZero);
                Console.WriteLine($"- Сумма элементов после последнего нуля: {sumAfterLastZero}");
            } else
            {
                Console.WriteLine("- В массиве нет элементов, равных нулю");
            }
            TransformArray(array);
            Console.Write("Массив после преобразования: ");
            PrintArray(array);
            break;
        case "2":
            Console.Write("Введите количество строк: ");
            int s = int.Parse(Console.ReadLine());
            Console.Write("Введите количество столбцов: ");
            int m = int.Parse(Console.ReadLine());
            double[,] matrix = GenerateMatrix(s,m);
            int nonpos = FindFirstNonPositiveRow(matrix);
            if (nonpos == -1)
            {
                Console.WriteLine("Все строки матрицы имеют хотя бы один положительный элемент");
            } else
            {
                Console.WriteLine($"Номер первой из строк, не содержащих ни одного положительного элемента: {nonpos+1}");
            }
            TransformMatrix(matrix);
            break;
        case "3":
            // Строка содержит произвольный текст. Проверить совпа-дают ли в ней самые
            // короткие слова (короткое слово – это слово с наименьшим количеством символов).
            Console.Write("Введите строку: ");
            string str = Console.ReadLine();
            if (ShortestWordsAreEqual(str))
            {
                Console.WriteLine("Самые короткие слова совпадают");
            } else
            {
                Console.WriteLine("Самые короткие слова не совпадают");
            }
            break;
        case "0":
            Console.WriteLine("Выхожу из программы");
            break;
        default:
            Console.WriteLine("Введён неправильный номер");
            break;
    }
} while (selection != "0");
 
double[] GenerateArray(int n)
{
    double[] array = new double[n];
    Console.WriteLine("Выберите, над каким массивом работать: ");
    Console.WriteLine("1. Случайно сгенерированный");
    Console.WriteLine("2. Введённый пользователем");
    Console.WriteLine("Ваш выбор: ");
    string selection = Console.ReadLine();
    switch (selection)
    {
        case "1":
            Random rd = new Random();
            for (var i = 0; i<n; i++)
            {
                array[i] = rd.Next(-50,50) / 10.0;
            }
            break;
        case "2":
            for (var i = 0; i < n; i++)
            {
                Console.Write($"Элемент {i+1}: ");
                array[i] = double.Parse(Console.ReadLine());
            }
            break;
        default:
            Console.WriteLine("Введён неправильный номер");
            break;
    }
    Console.Write("Полученный массив: ");
    PrintArray(array);
    return array;
}

double[,] GenerateMatrix(int n, int m)
{
    double[,] array = new double[n,m];
    Console.WriteLine("Выберите, над какой матрицей работать: ");
    Console.WriteLine("1. Случайно сгенерированная");
    Console.WriteLine("2. Введённая пользователем");
    Console.WriteLine("Ваш выбор: ");
    string selection = Console.ReadLine();
    switch (selection)
    {
        case "1":
            Random rd = new Random();
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < m; j++)
                    array[i,j] = rd.Next(-50, 50) / 10.0;
            }
            break;
        case "2":
            Console.WriteLine("Введите элементы каждой строки через пробел: ");
            for (var i = 0; i < n; i++)
            {
                Console.Write($"{i+1} строка: ");
                string[] row = Console.ReadLine().Split(" ");
                for (var j = 0; j < m; j++)
                {
                    array[i,j] = double.Parse(row[j]);
                }
            }
            break;
        default:
            Console.WriteLine("Введён неправильный номер");
            break;
    }
    Console.WriteLine("Полученная матрица: ");
    PrintMatrix(array);
    return array;
}

int FindPos(double[] array)
{
    int pos = 0;
    foreach (double i in array)
    {
        if (i > 0) { pos++; }
    }
    return pos;
}

int FindLastZero(double[] array)
{

    int n = array.Length;
    int lastZero = -1;
    for (var i = 0; i < n; i++)
    {
        if (array[i] == 0) { lastZero = i; }
    }
    return lastZero;
}

double FindSumAfterElement(double[] array, int element)
{
    int n = array.Length;
    double sum = 0;
    for (var i = element + 1; i < n; i++)
    {
        sum += array[i];
    }
    return sum;
}

void TransformArray(double[] array)
{
    double[] result = new double[array.Length];
    int ind = 0;
    for (var i = 0; i < array.Length; i++)
    {
        if (Math.Truncate(array[i]) <= 1)
        {
            result[ind] = array[i];
            ind++;
        }
    }
    for (var i = 0; i < array.Length; i++)
    {
        if (Math.Truncate(array[i]) > 1)
        {
            result[ind] = array[i];
            ind++;
        }
    }
    for (var i = 0; i < array.Length; i++)
    {
        array[i] = result[i];
    }
}

void TransformMatrix(double[,] matrix)
{
    double[] arr = MatrixToArray(matrix);
    BubbleSort(arr);
    for (int k = 0; k < Math.Min(matrix.GetLength(0), matrix.GetLength(1)); k++)
    {
        double cur = arr[^(k+1)];
        for (int i =0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++) {
                if (matrix[i,j] == cur)
                {
                    matrix[i, j] = matrix[k, k];
                    matrix[k, k] = cur;
                } 
            }
        }
    }
    Console.WriteLine("Матрица после перестановки элементов:");
    PrintMatrix(matrix);
}

int FindFirstNonPositiveRow(double[,] matrix)
{
    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        bool matches = true;
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            if (matrix[i,j] > 0)
            {
                matches = false;
            }
        }
        if (matches)
        {
            return i;
        }
    }
    return -1;
}

bool ShortestWordsAreEqual(string s)
{
    string[] arr = s.Split(" ");
    int min = int.MaxValue;
    string shortest = "";
    bool res = true;
    foreach (string c in arr)
    {
        if (c.Length < min)
        {
            min = c.Length;
            shortest = c;
            res = true;
        }
        if (c.Length == min && res)
        {
            res = c == shortest;
        }
    }
    return res;
}

void PrintArray(double[] array)
{
    foreach (double c in array)
    {
        Console.Write(c + " ");
    }
    Console.WriteLine();
}

void PrintMatrix(double[,] array)
{
    for (var i = 0; i <  array.GetLength(0); i++)
    {
        for (var j = 0; j < array.GetLength(1); j++)
        {
            Console.Write(array[i, j] + " ");
        }
        Console.WriteLine();
    }
}

double[] MatrixToArray(double[,] matrix)
{
    double[] result = new double[matrix.Length];
    int i = 0;
    foreach (var c in matrix)
    {
        result[i] = c;
        i++;
    }
    return result;
}
void BubbleSort(double[] arr)
{
    int n = arr.Length;
    for (int i = 0; i < n - 1; i++)
    {
        for (int j = 0; j < n - i - 1; j++)
        {
            if (arr[j] > arr[j + 1])
            {
                double temp = arr[j];
                arr[j] = arr[j + 1];
                arr[j + 1] = temp;
            }
        }
    }
}
