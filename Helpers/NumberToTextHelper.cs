using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CodeTest.Helpers
{
    public class NumberToTextHelper
    {

        public string ConvertNumberToTextString(string inputNumber)
        {
            //if the number is zero then skip all the work and just return zero
            if ("0" == inputNumber) return "zero";

            //define some constants to help with conversion
            string[] singles = { "one", "two", "three", "four", "five",
                           "six", "seven", "eight", "nine" };

            string[] doubles = { "eleven", "twelve", "thirteen", "fourteen", "fifteen",
                           "sixteen", "seventeen", "eighteen", "nineteen" };

            string[] tens = { "ten", "twenty", "thirty", "forty", "fifty",
                          "sixty", "seventy", "eighty", "ninety" };

            string[] thousandsPlus = { "thousand", "million", "billion", "trillion", "quadrillion", "quintillion"};

            //support for negative values
            string sign = String.Empty;
            if ("-" == inputNumber.Substring(0, 1))
            {
                sign = "minus ";
                inputNumber = inputNumber.Substring(1);
            }

            //check that our value is within the supported range, current max = quintillions
            int maxLen = thousandsPlus.Length * 3;
            int inputLen = inputNumber.Length;
            if (inputLen > maxLen)
                throw new InvalidCastException(String.Format("{0} is beyond the support length of {1} digits. To convert to text, you must add a new entry to the thousandsPlus array.", inputLen, maxLen));

            //since the code accepts the input value as a string, need to check if it is indeed a proper numeric value
            int n; //setting up return value for tryparse

            if (!inputNumber.All(c => int.TryParse(c.ToString(), out n)))
                throw new InvalidCastException();

            string fraction = String.Empty;
            if (inputNumber.Contains("."))
            {
                string[] split = inputNumber.Split('.');
                inputNumber = split[0];
                fraction = split[1];
            }

            StringBuilder word = new StringBuilder();
            ulong i = 0;

            while (0 < inputNumber.Length)
            {
                int startPos = Math.Max(0, inputNumber.Length - 3);
                string crntBlock = inputNumber.Substring(startPos);
                if (0 < crntBlock.Length)
                {
                    //define hundreds tens & singles for the current block
                    int h = crntBlock.Length > 2 ? int.Parse(crntBlock[crntBlock.Length - 3].ToString()) : 0;
                    int t = crntBlock.Length > 1 ? int.Parse(crntBlock[crntBlock.Length - 2].ToString()) : 0;
                    int s = crntBlock.Length > 0 ? int.Parse(crntBlock[crntBlock.Length - 1].ToString()) : 0;

                    StringBuilder output = new StringBuilder();

                    if (0 < s)
                        output.Append(1 == t ? doubles[s - 1] : singles[s - 1]);

                    if (1 != t)
                    {
                        if (1 < t && 0 < s) output.Insert(0, "-");
                        if (0 < t) output.Insert(0, tens[t - 1]);
                    }

                    if (0 < h)
                    {
                        if (t > 0 | s > 0) output.Insert(0, " and ");
                        output.Insert(0, String.Format("{0} hundred", singles[h - 1]));
                    }

                    //if we have values remaining keep going
                    bool remaining = 3 < inputNumber.Length;
                    if (remaining && (0 == h) && (0 == i))
                        output.Insert(0, " and ");
                    else if (remaining)
                        output.Insert(0, String.Format(" {0}, ", thousandsPlus[i]));

                    word.Insert(0, output);
                }

                inputNumber = inputNumber.Substring(0, startPos);

                i++;
            }
            return word.Insert(0, sign).ToString();
        }

    }
}