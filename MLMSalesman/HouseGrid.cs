using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLMSalesman
{
    public class HouseGrid
    {
        public HouseGrid(int row, int column)
        {
            GridOfHouses = new House[row, column];
            Unemployed = row * column;
            InitArrayOfHouses(row);
            SetStartPosition(row - 1);
        }

        public House[,] GridOfHouses { get; set; }
        public int Unemployed { get; set; }
        public List<Salesman> Salesmen { get; set; } = new List<Salesman>();

        //Setting start position of first salesman
        //Handle the first house conversion
        private void SetStartPosition(int max)
        {
            Random r = new Random();
            int[] startPositions = new int[] { 0, max };
            int randomX = startPositions[r.Next(2)];
            int randomY = startPositions[r.Next(2)];
            GridOfHouses[randomX, randomY].Converted = 1;
            Salesman newSalesman = new Salesman(new Tuple<int, int>(randomX, randomY));
            Salesmen.Add(newSalesman);
            Salesmen.Add(newSalesman);
            Unemployed--;
        }

        private void InitArrayOfHouses(int max)
        {
            for (int i = 0; i < max; i++)
            {
                for (int j = 0; j < max; j++)
                {
                    GridOfHouses[i, j] = new House();
                }
            }
        }
    }
}
