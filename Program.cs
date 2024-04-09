/*

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

int[] point = new int[10];

int[] point2 = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

int[,] tabel = new int[3, 4];

int[,,] tabel3 = new int[2, 3, 4];

int[,,,] tabel4 = new int[2, 3, 4, 5];

Console.WriteLine(point2[4]);
Console.WriteLine(point[4]);

String[] række = new String[2];

Console.WriteLine(række[1]);

for (int i = 0; i < række.Length; i++)
{
    række[i] = Convert.ToString(i);
}
Console.WriteLine(række);
foreach (string s in række)
{
    Console.WriteLine(s);
}

for (int i = 0; i < tabel.GetLength(0); i++)
    for (int j = 0; j < tabel.GetLength(1); j++)
    {
        tabel[i, j] = i + j;
    }

for (int i = 0; i < tabel.GetLength(0); i++)
{ 
    for (int j = 0; j < tabel.GetLength(1); j++)
    {
        Console.Write(tabel[i,j] + "\t");
    }
    Console.WriteLine();
}

*/

int b = 20 + 2;
int h = 10 + 2;

string[,] plade = new string[h, b];

for (int i = 0; i < h; i++)
{
    for (int j = 0; j < b; j++)
    {
        //plade[i, j] = Convert.ToString(i + j);
        plade[i, j] = "U";
    }
}

// plade[5, 9] = "M";

/*
for (int i = 0; i < h; i++)
{
    for (int j = 0; j < b; j++)
    {
        Console.Write(plade[i, j] + "\t");
    }
    Console.WriteLine();
}
*/


int antalMiner = 10;

//for  (int i = 0; i < antalMiner; i++)
string[,] mineKort = new string[h, b];
int k = 0;
while (k < antalMiner)
{
    Random r = new Random();
    int x = r.Next(1, b - 1);
    int y = r.Next(1, h - 1);

    if (mineKort[y, x] != "M")
    {
        mineKort[y, x] = "M";
        k++;
    }
}


for (int i = 1; i < h - 1; i++)
{
    for (int j = 1; j < b - 1; j++)
    {
        Console.Write(plade[i, j] + " ");
    }
    Console.WriteLine();
}

// minekort
/*
for (int i = 1; i < h - 1; i++)
{
    for (int j = 1; j < b - 1; j++)
    {
        Console.Write(mineKort[i, j] + " ");
    }
    Console.WriteLine();
}
*/

// cursor

// set cursor øverst i venstre hjørne
Console.SetCursorPosition(0, 0);

bool isGameOver = false;
// marker hvor "cursor"
while (!isGameOver)
{
    int left = Console.GetCursorPosition().Left;
    int top = Console.GetCursorPosition().Top;

    // opfange pile taster

    switch (Console.ReadKey().Key)
    {
        case ConsoleKey.LeftArrow:
            if (Console.GetCursorPosition().Left != 0)
            {
                Console.SetCursorPosition(left - 2, top);
            }
            break;
        case ConsoleKey.RightArrow:
            if (Console.GetCursorPosition().Left < (b * 2) - 2)
            {
                Console.SetCursorPosition(left + 2, top);
            }
            break;
        case ConsoleKey.UpArrow:
            if (Console.GetCursorPosition().Top != 0)
            {
                Console.SetCursorPosition(left, top - 1);
            }
            break;
        case ConsoleKey.DownArrow:
            if (Console.GetCursorPosition().Top < h - 1)
            {
                Console.SetCursorPosition(left, top + 1);
            }
            break;
        // TODO ved sæt_flag og ryd_felt skal mellemrum fraregnes inden man indexerer ind på pladen

        // set flag med SHIFT tast
        case ConsoleKey.Enter:
            if (top >= 0 && top <= h - 1 && left >= 0 && left <= (b * 2) - 2)
            {
                if (mineKort[top + 1, (left / 2) + 1] == "M")  // check om der en mine
                {
                    // TODO Havd skal der ske når der ER en mine?
                    Console.SetCursorPosition(left, top);
                    Console.Write("*");
                    Console.SetCursorPosition(left, top);
                    isGameOver = true;
                }
                else
                {
                    Console.SetCursorPosition(left, top);
                    Console.Write(" ");
                    Console.SetCursorPosition(left, top);
                    plade[top + 1,(left / 2) + 1] = " ";
                }
            }
            break;
        case ConsoleKey.Spacebar:
            if (top >= 0 && top <= h - 1 && left >= 0 && left <= (b * 2) - 2)
                if (plade[top +1, (left / 2) + 1] == "U")
                {
                    Console.SetCursorPosition(left , top);
                    Console.Write("F");
                    Console.SetCursorPosition(left , top);
                }
            break;


        // Ryd (clear) felt med CTRL

        default:
            if (top >= 0 && top <= h - 1 && left >= 0 && left <= (b * 2) - 2)
            {
                Console.SetCursorPosition(left, top);
                Console.Write(plade[top + 1, (left / 2) + 1] + " ");
                Console.SetCursorPosition(left, top);
            }
            break;
    }
}

Console.SetCursorPosition(0, h + 2);
Console.WriteLine("Spillet er slut");