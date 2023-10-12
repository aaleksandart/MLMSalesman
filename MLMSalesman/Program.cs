using MLMSalesman;

bool correctInput = false;
int houseRowsAndColumns = 0;
int repetions = 0;

//Validating input
while(!correctInput)
{
    try
    {
        //User decides how many rows/columns with houses and the amount of times to repeat
        Console.WriteLine("How many rows/columns of houses do you want?");
        houseRowsAndColumns = int.Parse(Console.ReadLine());
        Console.WriteLine("How many times do you want to repeat the process? To get an more accurate answer. ");
        repetions = int.Parse(Console.ReadLine());
        correctInput = true;
    }
    catch { correctInput = false; }
}

double hours = 0;
int repetionCount = 0;

//Creating the grid of houses
HouseGrid grid = new HouseGrid(houseRowsAndColumns, houseRowsAndColumns);

while (repetionCount < repetions)
{
    //Setting a new grid for every loop
    if (repetionCount != 0)
        grid = new HouseGrid(houseRowsAndColumns, houseRowsAndColumns);

    repetionCount++;
    while (grid.Unemployed > 0)
    {
        //Moving all the salesmen and displaying the updated grid
        grid.Salesmen = MoveAllSalesMen(grid.Salesmen);
        Console.Clear();
        for (int i = 0; i < houseRowsAndColumns; i++)
        {
            for (int j = 0; j < houseRowsAndColumns; j++)
            {
                Console.Write(grid.GridOfHouses[i, j].Converted);
            }
            Console.WriteLine();
        }
        hours++;
        //Every hour is represented as 1.2 seconds
        Thread.Sleep(1200);
    }
}

Console.WriteLine($"\nAvarage hours: {hours / repetions}");
Console.WriteLine($"Total hours for all repetions: {hours}");


#region Movement methods

List<Salesman> MoveAllSalesMen(List<Salesman> salesmen)
{
    //Putting all salesmen with new positions in a new list and returning that list
    List<Salesman> salesmen2 = new List<Salesman>();
    for (int i = salesmen.Count - 1; i >= 0; i--)
    {
        Salesman s = new Salesman(MoveSalesMan(salesmen[i].Position));
        //If the new position of a salesman is not visited, convert the house and add new salesman
        if (grid.GridOfHouses[s.Position.Item1, s.Position.Item2].Converted == 0)
        {
            grid.GridOfHouses[s.Position.Item1, s.Position.Item2].Converted = 1;
            salesmen2.Add(s);
            salesmen2.Add(s);
            grid.Unemployed--;
        }
        else
            salesmen2.Add(s);
    }
    return salesmen2;
}
Tuple<int, int> MoveSalesMan(Tuple<int, int> salesmanPosition)
{
    //Validating possible movements according to current position
    //Returning a random move of all possible movements
    List<Tuple<int, int>> validMovements = new List<Tuple<int, int>>();
    Random r = new Random();

    if (salesmanPosition.Item1 != 0)
        validMovements.Add(new Tuple<int, int>(salesmanPosition.Item1 - 1, salesmanPosition.Item2));
    if (salesmanPosition.Item1 != (houseRowsAndColumns - 1))
        validMovements.Add(new Tuple<int, int>(salesmanPosition.Item1 + 1, salesmanPosition.Item2));
    if (salesmanPosition.Item2 != 0)
        validMovements.Add(new Tuple<int, int>(salesmanPosition.Item1, salesmanPosition.Item2 - 1));
    if (salesmanPosition.Item2 != (houseRowsAndColumns - 1))
        validMovements.Add(new Tuple<int, int>(salesmanPosition.Item1, salesmanPosition.Item2 + 1));

    return validMovements[r.Next(validMovements.Count)];
}
#endregion