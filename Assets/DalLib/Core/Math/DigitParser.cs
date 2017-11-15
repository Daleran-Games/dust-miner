using System.Collections.Generic;

namespace DaleranGames
{
    public class DigitParser
    {

        Dictionary<int, int[]> lookupTable;

        public DigitParser()
        {
            lookupTable = new Dictionary<int, int[]>();
            BuildLookupTable();
        }

        public int[] GetDigitArray(int number)
        {
            int[] output = { 0 };

            if (lookupTable.TryGetValue(number, out output))
                return output;
            else
                return GetIntArray(number);
        }

        int[] GetIntArray(int num)
        {
            List<int> listOfInts = new List<int>();
            while (num > 0)
            {
                listOfInts.Add(num % 10);
                num = num / 10;
            }
            listOfInts.Reverse();
            return listOfInts.ToArray();
        }

        private void BuildLookupTable()
        {
            for (int i = -99; i < -9; i++)
            {
                int[] digits = new int[2];
                digits[0] = (i / 10) % 10 * -1;
                digits[1] = i % 10 * -1;
                lookupTable.Add(i, digits);
            }
            for (int i = -9; i < 0; i++)
            {
                int[] digits = new int[1];
                digits[0] = i % 10 * -1;
                lookupTable.Add(i, digits);
            }
            for (int i = 0; i < 10; i++)
            {
                int[] digits = new int[1];
                digits[0] = i % 10;
                lookupTable.Add(i, digits);
            }
            for (int i = 10; i < 100; i++)
            {
                int[] digits = new int[2];
                digits[0] = (i / 10) % 10;
                digits[1] = i % 10;
                lookupTable.Add(i, digits);
            }

            for (int i = 100; i < 999; i++)
            {
                int[] digits = new int[3];
                digits[0] = (i / 100) % 10;
                digits[1] = (i / 10) % 10;
                digits[2] = i % 10;
                lookupTable.Add(i, digits);
            }
        }
    }
}

