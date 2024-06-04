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

Console.Clear();

int b = 22 + 2;
int h = 10 + 2;

string[,] plade = new string[h, b];

for (int i = 1; i < h-1; i++)
{
    for (int j = 1; j < b-1; j++)
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


int antalMiner = 20;

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

bool isGameOver = false;
double Procentryddet = 0;
void update_status()
{
    //throw new NotImplementedException();
    int antalFlag = 0;
    int antalUkendte = 0;
    int antalFelter = (b - 2) * (h - 2);
    foreach (string felt in plade)
    {
        if (felt == "F")
        {
            antalFlag += 1;
        }
        //if (felt == "U" || felt == "F") 
        if (felt == "U")
        {
            antalUkendte += 1;
        }
    }
    Procentryddet = (antalFelter - antalUkendte) / Convert.ToDouble(antalFelter) * 100.0;
    Console.SetCursorPosition(0, h);
    Console.WriteLine($"Flag: {antalFlag:D2} Miner: {antalMiner:D2}");
    Console.WriteLine($"antalFelter: {antalFelter}, antalUkendte: {antalUkendte:D3}, antalRydede: {antalFelter - antalUkendte:D3}");
    Console.WriteLine($"Procent ryddet:{Procentryddet:F2}%");
    if(Procentryddet == 100)
    {
        isGameOver = true;
    }
}
int x2left(int x)
{
    return (x - 1) * 2;
}

int left2x(int left) 
{ 
    return left / 2 + 1;
}

int y2top(int y)
{
    return y - 1;
}

int top2y(int top)
{
    return top + 1;
}

int findAntalNaboMiner(int x, int y)
{
    int miner = 0;

    //           y,   x
    if (mineKort[y - 1, x - 1] == "M")
    {
        miner += 1;
    }

    //           y,   x
    if (mineKort[y - 1, x ] == "M")
    {
        miner += 1;
    }

    //           y,   x
    if (mineKort[y - 1, x + 1] == "M")
    {
        miner += 1;
    }

    //           y,   x
    if (mineKort[y , x - 1] == "M")
    {
        miner += 1;
    }

    //           y,   x
    if (mineKort[y, x + 1] == "M")
    {
        miner += 1;
    }
    
    //           y,   x
    if (mineKort[y + 1, x - 1] == "M")
    {
        miner += 1;
    } 
    
    //           y,   x
    if (mineKort[y + 1, x ] == "M")
    {
        miner += 1;
    }
    
    //           y,   x
    if (mineKort[y + 1, x + 1] == "M")
    {
        miner += 1;
    }
    return miner;
}

int visAntalNaboMiner(int x, int y)
{
    int left = x2left(x);
    int top = y2top(y);
    int antalNaboMiner = findAntalNaboMiner(x, y);
    Console.SetCursorPosition(left, top);
    if (antalNaboMiner == 0)
    {
        Console.Write(" ");
    }
    else
    {
        Console.Write(antalNaboMiner);
    }
    Console.SetCursorPosition(left, top);
    plade[y, x] = " ";
    return antalNaboMiner;
}

void rydNaboFelter(int x, int y)
{

    //           y,   x
    if (plade[y - 1, x - 1] == "U" && mineKort[y - 1, x - 1] != "M")
    {
        if (visAntalNaboMiner(x - 1, y - 1) == 0)
        {
            rydNaboFelter(x - 1, y - 1);
        }
    }

    //           y,   x
    if (plade[y - 1, x] == "U" && mineKort[y - 1, x] != "M")
    {
        if (visAntalNaboMiner(x, y - 1) == 0)
        {
            rydNaboFelter(x, y - 1);
        }
    }

    //           y,   x
    if (plade[y - 1, x + 1] == "U" && mineKort[y - 1, x + 1] != "M")
    {
        if (visAntalNaboMiner(x + 1, y - 1) == 0)
        {
            rydNaboFelter(x + 1, y - 1);
        }
    }

    //           y,   x
    if (plade[y, x - 1] == "U" && mineKort[y, x - 1] != "M")
    {
        if (visAntalNaboMiner(x - 1, y) == 0)
        {
            rydNaboFelter(x - 1, y);
        }
    }

    //           y,   x
    if (plade[y, x + 1] == "U" && mineKort[y, x + 1] != "M")
    {
        if (visAntalNaboMiner(x + 1, y) == 0)
        {
            rydNaboFelter(x + 1, y);
        }
    }

    //           y,   x
    if (plade[y + 1, x - 1] == "U" && mineKort[y + 1, x - 1] != "M")
    {
        if (visAntalNaboMiner(x - 1, y + 1) == 0)
        {
            rydNaboFelter(x - 1, y + 1);
        }
    }

    //           y,   x
    if (plade[y + 1, x] == "U" && mineKort[y + 1, x] != "M")
    {
        if (visAntalNaboMiner(x, y + 1) == 0)
        {
            rydNaboFelter(x, y + 1);
        }
    }

    //           y,   x
    if (plade[y + 1, x + 1] == "U" && mineKort[y + 1, x + 1] != "M")
    {
        if (visAntalNaboMiner(x + 1, y + 1) == 0)
        {
            rydNaboFelter(x + 1, y + 1);
        }
    }
}

// cursor

// set cursor øverst i venstre hjørne
Console.SetCursorPosition(0, 0);


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
            if (Console.GetCursorPosition().Left < (b * 2) - 6)
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
            if (Console.GetCursorPosition().Top < h - 3)
            {
                Console.SetCursorPosition(left, top + 1);
            }
            break;
        // TODO ved sæt_flag og ryd_felt skal mellemrum fraregnes inden man indexerer ind på pladen

        // set flag med SHIFT tast
        case ConsoleKey.Enter:
            if (top >= 0 && top <= h - 3 && left >= 0 && left <= (b * 2) - 6)
            {
                if (mineKort[top + 1, (left / 2) + 1] == "M")  // check om der en mine
                {
                    // TODO Havd skal der ske når der ER en mine?
                    Console.SetCursorPosition(left, top);
                    Console.Write("*");
                    Console.SetCursorPosition(left-1, top);
                    isGameOver = true;
                }
                else
                {
                    //int antalNaboMiner = findAntalNaboMiner((left / 2) + 1, top + 1);
                    //Console.SetCursorPosition(left, top);
                    //Console.Write(antalNaboMiner);
                    //Console.SetCursorPosition(left, top);
                    //plade[top + 1,(left / 2) + 1] = " ";

                    int x = left2x(left);
                    int y = top2y(top);
                    if (visAntalNaboMiner(x, y) == 0)
                    {
                        rydNaboFelter(x, y);
                    }
                    update_status();
                    Console.SetCursorPosition(left, top);
                }
            }
            break;
        case ConsoleKey.Spacebar:
            if (top >= 0 && top <= h - 3 && left >= 0 && left <= (b * 2) - 6)
                if (plade[top +1, (left / 2) + 1] == "U")
                {
                    plade[top + 1, (left / 2) + 1] = "F";
                    update_status();

                    Console.SetCursorPosition(left , top);
                    Console.Write("F");
                    Console.SetCursorPosition(left , top);
                }
                else if (plade[top + 1, (left / 2) + 1] == "F")
                {
                    plade[top + 1, (left / 2) + 1] = "U";
                    update_status();

                    Console.SetCursorPosition(left, top);
                    Console.Write("U");
                    Console.SetCursorPosition(left, top);
                }
            break;


        // Ryd (clear) felt med CTRL

        default:
            if (top >= 0 && top <= h - 3 && left >= 0 && left <= (b * 2) - 6)
            {
                Console.SetCursorPosition(left, top);
                Console.Write(plade[top + 1, (left / 2) + 1] + " ");
                Console.SetCursorPosition(left, top);
            }
            break;
    }
}

ConsoleColor oldBackColor = Console.BackgroundColor;
if (Procentryddet == 100)
{
    Console.SetCursorPosition(0, h + 4);
    Console.WriteLine("VINNER!!!"); 
    for (int i = 1; i < h - 1; i++)
    {
        for (int j = 1; j < b - 1; j++)
        {
            if()
            Console.BackgroundColor = ConsoleColor.Green;

            Console.BackgroundColor = oldBackColor;
        }
    }
    
}