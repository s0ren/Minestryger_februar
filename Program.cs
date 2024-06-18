
int b = 22 + 2;
int h = 10 + 2;

string[,] plade = new string[h, b];

for (int i = 1; i < h - 1; i++)
{
    for (int j = 1; j < b - 1; j++)
    {
        plade[i, j] = "U";
    }
}

int antalMiner = 20;

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

Console.BackgroundColor = ConsoleColor.DarkGray;
ConsoleColor oldBackColor = Console.BackgroundColor;

for (int i = 1; i < h - 1; i++)
{
    for (int j = 1; j < b - 1; j++)
    {
        
        Console.Write(plade[i, j]);
        if(j < b - 2)
        {
            Console.Write(' ');
        }
    }
    Console.WriteLine();
}

int left = 0;
int top = 0;
bool isGameOver = false;
bool vinner = false;
bool taber = false;
double Procentryddet = 0;
int antalFlag = 0;
int antalUkendte = 0;
int antalFelter = 0;

void update_status()
{
    //throw new NotImplementedException();
    antalFlag = 0;
    antalUkendte = 0;
    antalFelter = (b - 2) * (h - 2);

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

    Console.BackgroundColor = ConsoleColor.Black;
    Console.SetCursorPosition(0, h);
    Console.WriteLine($"Flag: {antalFlag:D2} Miner: {antalMiner:D2}");
    Console.WriteLine($"antalFelter: {antalFelter}, antalUkendte: {antalUkendte:D3}, antalRydede: {antalFelter - antalUkendte:D3}");
    Console.WriteLine($"Procent ryddet:{Procentryddet:F2}%");
    Console.BackgroundColor = oldBackColor; 
    Console.SetCursorPosition(left, top);

    if (!taber && Procentryddet == 100 && antalFlag == antalMiner)
    {
        isGameOver = true;
        vinner = true;
    }
    else if (taber)
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

update_status();

Console.SetCursorPosition(0, 0);

// marker hvor "cursor"
while (!isGameOver)
{
    left = Console.GetCursorPosition().Left;
    top = Console.GetCursorPosition().Top;

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
                    taber = true;
                    plade[top + 1, (left / 2) + 1] = "*";
                    update_status();
                }
                else
                {
                    int x = left2x(left);
                    int y = top2y(top);
                    if (visAntalNaboMiner(x, y) == 0)
                    {
                        rydNaboFelter(x, y);
                    }
                    update_status();
                }
            }
            break;
        case ConsoleKey.Spacebar:
            Console.SetCursorPosition(left, top);
            if (top >= 0 && top <= h - 3 && left >= 0 && left <= (b * 2) - 6)
                if (plade[top +1, (left / 2) + 1] != "F")
                {
                    plade[top + 1, (left / 2) + 1] = "F";

                    Console.Write("F");

                    update_status();
                }
                else if (plade[top + 1, (left / 2) + 1] == "F")
                {
                    plade[top + 1, (left / 2) + 1] = "U";
                    
                    Console.Write("U");

                    update_status();
                }
            break;


        // Ryd (clear) felt med CTRL

        default:
            if (top >= 0 && top <= h - 3 && left >= 0 && left <= (b * 2) - 6)
            {
                Console.SetCursorPosition(left, top);
                Console.Write(plade[top + 1, (left / 2) + 1]);
                Console.SetCursorPosition(left, top);
            }
            break;
    }
}
if (isGameOver)
{
    Console.BackgroundColor = ConsoleColor.Black;

    if (vinner)
    {
        Console.SetCursorPosition(0, h + 4);
        Console.WriteLine("VINNER!!!");
    }
    else if (taber)
    {
        Console.SetCursorPosition(0, h + 4);
        Console.WriteLine("BOOM!!!");
    }

    for (int i = 1; i < h - 1; i++)
    {
        for (int j = 1; j < b - 1; j++)
        {
            left = x2left(j);
            top = y2top(i);
            if (mineKort[i, j] == "M" && plade[i, j] == "F")
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(left, top);
                Console.Write("*");
            }
            else if (taber && plade[i, j] == "F" && mineKort[i, j] != "M")
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(left, top);
                Console.Write("F");
            }
            else if (taber && mineKort[i, j] == "M" && plade[i, j] != "F")
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(left, top);
                Console.Write("*");

            }
        }
    }
}
Console.ReadKey();