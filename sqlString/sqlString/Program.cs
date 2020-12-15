using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace sqlString
{
    class Program
    {
        static void Main(string[] args)
        {
            string sqlCumlesi = Console.ReadLine();

            if (kelimeKontrol(sqlCumlesi) == "NP")
            {
                Console.WriteLine("Problem yok.");
            }
            else if(kelimeKontrol(sqlCumlesi) == "ilk kelime hatali")
            {
                Console.WriteLine("Ilk kelime select,insert veya delete olmali");
            }
            else
            {
                Console.WriteLine("ingilizce soz dizimi kullaniniz");
            }
           
            Console.ReadKey();
        }
        static string kelimeKontrol(string cumle)
        {
            string[] kelimeler = cumle.Split(' ');

            if (Regex.IsMatch(kelimeler[0], "^[a-zA-Z0-9]*$"))
            {
                if (kelimeler[0].ToLower()=="select"|| kelimeler[0].ToLower() == "insert"|| kelimeler[0].ToLower() == "delete")
                {
                    return "NP";
                }
                else
                {
                    return "ilk kelime hatali";
                }
            }
            else {
                return "ingilizce soz dizimi kullaniniz";
            }
            

        }
       
        
    }
    

}
