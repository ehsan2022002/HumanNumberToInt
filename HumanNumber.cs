using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    static class HumanNumber
    {

    public static int ParseFarsi(string number)
        {


            if (number.StartsWith("صد") || number.StartsWith("هزار"))
                number = "یک " + number;

            number = number.Replace("دویست", "دو صد");
            number = number.Replace("سیصد", "سه صد");
            number = number.Replace("چهارصد", "چهار صد");
            number = number.Replace("پانصد", "پنج صد");
            number = number.Replace("ششصد", "شش صد");
            number = number.Replace("هفتصد", "هفت صد");
            number = number.Replace("هشتصد", "هشت صد");
            number = number.Replace("هشتصد", "نه صد");
            
            string[] words = number.ToLower().Split(new char[] { ' ', '-', ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] ones = { "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه" };
            string[] teens = { "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
            string[] tens = { "ده", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
            Dictionary<string, int> modifiers = new Dictionary<string, int>() {
                {"بیلیون", 1000000000},
                {"میلیون", 1000000},
                {"هزار", 1000},
                {"صد", 100}
            };

            if (number == "یازده بیلیون")
                return int.MaxValue; // 110,000,000,000 is out of range for an int!

            int result = 0;
            int currentResult = 0;
            int lastModifier = 1;

                        
            foreach (string word in words)
            {
                if (modifiers.ContainsKey(word))
                {
                    lastModifier *= modifiers[word];
                }
                else
                {
                    int n;

                    if (lastModifier > 1)
                    {
                        result += currentResult * lastModifier;
                        lastModifier = 1;
                        currentResult = 0;
                    }

                    if ((n = Array.IndexOf(ones, word) + 1) > 0)
                    {
                        currentResult += n;
                    }
                    else if ((n = Array.IndexOf(teens, word) + 1) > 0)
                    {
                        currentResult += n + 10;
                    }
                    else if ((n = Array.IndexOf(tens, word) + 1) > 0)
                    {
                        currentResult += n * 10;
                    }
                    else if (word != "و")
                    {
                        throw new ApplicationException("Unrecognized word: " + word);
                    }
                }
            }

            return result + currentResult * lastModifier;
        }


    public static int ParseEnglish(string number)
    {
        string[] words = number.ToLower().Split(new char[] { ' ', '-', ',' }, StringSplitOptions.RemoveEmptyEntries);
        string[] ones = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        string[] teens = { "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        string[] tens = { "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
                Dictionary<string, int> modifiers = new Dictionary<string, int>() {
                {"billion", 1000000000},
                {"million", 1000000},
                {"thousand", 1000},
                {"hundred", 100}
            };

        if (number == "eleventy billion")
            return int.MaxValue; // 110,000,000,000 is out of range for an int!

        int result = 0;
        int currentResult = 0;
        int lastModifier = 1;

        foreach (string word in words)
        {
            if (modifiers.ContainsKey(word))
            {
                lastModifier *= modifiers[word];
            }
            else
            {
                int n;

                if (lastModifier > 1)
                {
                    result += currentResult * lastModifier;
                    lastModifier = 1;
                    currentResult = 0;
                }

                if ((n = Array.IndexOf(ones, word) + 1) > 0)
                {
                    currentResult += n;
                }
                else if ((n = Array.IndexOf(teens, word) + 1) > 0)
                {
                    currentResult += n + 10;
                }
                else if ((n = Array.IndexOf(tens, word) + 1) > 0)
                {
                    currentResult += n * 10;
                }
                else if (word != "and")
                {
                    throw new ApplicationException("Unrecognized word: " + word);
                }
            }
        }

        return result + currentResult * lastModifier;
    }

        
    private static string FriendlyInteger(int n, string leftDigits, int thousands)
    {
        string[] ones = new string[] { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
        string[] teens = new string[] { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        string[] tens = new string[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
        string[] thousandsGroups = { "", " Thousand", " Million", " Billion" };


        if (n == 0)
        {
            return leftDigits;
        }

        string friendlyInt = leftDigits;

        if (friendlyInt.Length > 0)
        {
            friendlyInt += " ";
        }

        if (n < 10)
        {
            friendlyInt += ones[n];
        }
        else if (n < 20)
        {
            friendlyInt += teens[n - 10];
        }
        else if (n < 100)
        {
            friendlyInt += FriendlyInteger(n % 10, tens[n / 10 - 2], 0);
        }
        else if (n < 1000)
        {
            friendlyInt += FriendlyInteger(n % 100, (ones[n / 100] + " Hundred"), 0);
        }
        else
        {
            friendlyInt += FriendlyInteger(n % 1000, FriendlyInteger(n / 1000, "", thousands + 1), 0);
            if (n % 1000 == 0)
            {
                return friendlyInt;
            }
        }

        return friendlyInt + thousandsGroups[thousands];
    }
    public static string IntegerToWritten(int n)
    {
        if (n == 0)
        {
            return "Zero";
        }
        else if (n < 0)
        {
            return "Negative " + IntegerToWritten(-n);
        }

        return FriendlyInteger(n, "", 0);
    }





    public static string IntegerToWrittenFA(int n)
    {
        if (n == 0)
        {
            return "صفر";
        }
        else if (n < 0)
        {
            return "منفی " + IntegerToWrittenFA(-n);
        }

        return FriendlyIntegerFA(n, "", 0);
    }
        
    private static string FriendlyIntegerFA(int n, string leftDigits, int thousands)
    {
        string[] ones = new string[] { "", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه" };
        string[] teens = new string[] { "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
        string[] tens = new string[] { "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
        string[] thousandsGroups = { "", " هزار", " میلیون", " بیلیون" };


        if (n == 0)
        {
            return leftDigits;
        }

        string friendlyInt = leftDigits;

        if (friendlyInt.Length > 0)
        {
            friendlyInt += " ";
        }

        if (n < 10)
        {
            friendlyInt += ones[n];
        }
        else if (n < 20)
        {
            friendlyInt += teens[n - 10];
        }
        else if (n < 100)
        {
            friendlyInt += FriendlyIntegerFA(n % 10, tens[n / 10 - 2], 0);
        }
        else if (n < 1000)
        {
            friendlyInt += FriendlyIntegerFA(n % 100, (ones[n / 100] + " هزار"), 0);
        }
        else
        {
            friendlyInt += FriendlyIntegerFA(n % 1000, FriendlyIntegerFA(n / 1000, "", thousands + 1), 0);
            if (n % 1000 == 0)
            {
                return friendlyInt;
            }
        }

        return friendlyInt + thousandsGroups[thousands];
    }
            

    } //class
}
