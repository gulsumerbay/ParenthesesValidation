using System;

namespace ParenthesesValidation
{
    class Program
    {
        public static void Main(String[] args)
        {
            string expr = "[[{(({[{{{}}}]}))}((((]]"; //geçersiz
            //string expr = "[[{(({[{{{}}}]}))}]]"; //geçerli

            Console.WriteLine("Algoritma1");
            if (IsValid(expr.ToCharArray()))
                Console.WriteLine("Süper");
            else
                Console.WriteLine("Bir karışıklık var gibi:/");




            Console.WriteLine("Algoritma2");
            if (IsValid2(expr))
            {
                Console.WriteLine("Süper");
            }
            else
            {
                Console.WriteLine("Bir karışıklık var gibi:/");
            }
        }
        // Parantezlerin Kurallı olup olmadığını kontrol eder
        static bool IsValid(char[] arr)
        {
            string charsStr = new string(arr);
            Console.WriteLine(charsStr);
            int length = arr.Length;
            if (length == 0)
                return true;
            if (length == 1)
                return false;
            if (arr[0] == ')' || arr[0] == '}' || arr[0] == ']')
                return false;

            // açma parantezi türüne göre kapama parantezini bul
            char closing = FindClosingParentheses(arr[0]);

            int i;

            int count = 0; //aynı parantezden iç içe kaç tane olduğunu tutuyor
            for (i = 1; i < length; i++)
            {
                if (arr[i] == arr[0])
                    count++;
                if (arr[i] == closing)
                {
                    if (count == 0)
                        break;
                    count--;
                }
            }

            // kapama parantezi dizide yoksa
            if (i == length)
                return false;

            // Kapama parantezi açma parantezinin yanındaysa,yani ikinci elemansa
            if (i == 1)
                return IsValid(GetRangeChars(arr, i + 1, length));
            // Kapama parantezi ortadaysa 
            return
                IsValid(GetRangeChars(arr, 1, length - 1))//açma ve kapama parantezi arasındakileri kontrol et
                &&
                    IsValid(GetRangeChars(arr, (i + 1), length));//kapama parantezinden sonrakileri kontrol et
        }
        // Parantezlerin Kurallı olup olmadığını kontrol eder
        private static bool IsValid2(string expr)
        {
            if (string.Empty.Equals(GetLeastArray(expr)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static char[] GetRangeChars(char[] arr, int first, int last)
        {
            int length = last - first;
            char[] arr2 = new char[length];
            Array.Copy(arr, first, arr2, 0, length);
            return arr2;
        }
        static char FindClosingParentheses(char x)
        {
            switch (x)
            {
                case '(': return ')';
                case '[': return ']';
                case '{': return '}';
                default: break;
            }
            return char.MinValue;
        }
        private static string GetLeastArray(string arr)
        {
            var temp = arr;
            Simplify(ref arr);
            Console.WriteLine(arr);
            if (arr.Length < 2 || temp.Equals(arr))
            {
                return arr;
            }
            return GetLeastArray(arr);
        }
        public static void Simplify(ref string val)
        {
            val = val.Replace("()", "").Replace("[]", "").Replace("{}", "");
        }
    }

}
