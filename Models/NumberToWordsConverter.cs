namespace NumberToWordsApp.Models
{
    public static class NumberToWordsConverter
    {
        public static string Convert(decimal amount)
        {
            var isNegative = amount < 0;
            var absAmount = Math.Abs(amount);
            var dollars = (int)absAmount;
            var cents = (int)((absAmount - dollars) * 100);

            var dollarWords = $"{NumberToWords(dollars).ToUpper()} DOLLAR" + (dollars != 1 ? "S" : "");

            if (cents == 0)
            {
                return isNegative ? "MINUS " + dollarWords : dollarWords;
            }
            else
            {
                var centWords = $"{NumberToWords(cents).ToUpper()} CENT" + (cents != 1 ? "S" : "");
                var result = $"{dollarWords} AND {centWords}";
                return isNegative ? "MINUS " + result : result;
            }
        }


        private static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string[] unitsMap = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", 
                                  "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", 
                                  "eighteen", "nineteen" };
            string[] tensMap = { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words.Trim();
        }
    }
}
