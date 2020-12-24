using System;
using System.Collections.Generic;
using System.IO;
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
            DirectoryInfo info =new DirectoryInfo(@"C:\Users\x\Desktop\didem");

            long sda = 0;
            
            foreach(FileInfo x in info.GetFiles("*", SearchOption.AllDirectories))
            {
                sda += x.Length;
            }
            Console.WriteLine(sda);
            //while (true)
            //{
            //    string sqlCumlesi = Console.ReadLine();
            //    Console.WriteLine("");
            //    sqlSyntaxCheck(sqlCumlesi);
            //    Console.WriteLine("---\n");
            //}

            bool xa = true;
            //try
            //{
            //    string[] x = Directory.GetDirectories("C:");
            //}
            //catch
            //{
            //    xa = false;

            //}

            //if (xa)
            //{
            //    Console.WriteLine("Path is correct");
            //}
            //else
            //{
            //    Console.WriteLine("Invalid path");
            //}

            
            Console.ReadKey();
        }
        static string soldanBoslukSil(string sqlCumlesi,string noktalaIsareti)
        {
            while (sqlCumlesi != sqlCumlesi.Replace(" "+noktalaIsareti, noktalaIsareti))
            {
                sqlCumlesi = sqlCumlesi.Replace(" " + noktalaIsareti, noktalaIsareti);
            }

            return sqlCumlesi;
        }
        static string sagdanBoslukSil(string sqlCumlesi, string noktalaIsareti)
        {
            while (sqlCumlesi != sqlCumlesi.Replace( noktalaIsareti+ " ", noktalaIsareti))
            {
                sqlCumlesi = sqlCumlesi.Replace( noktalaIsareti + " ", noktalaIsareti);
            }

            return sqlCumlesi;
        }
        static string bosluklariTekBoslukYap(string sqlCumlesi)
        {
            while (sqlCumlesi != sqlCumlesi.Replace("  ", " "))
            {
                sqlCumlesi = sqlCumlesi.Replace("  ", " ");
            }

            return sqlCumlesi;
        }
        static string[] sqlCumlesiniDuzenle(string sqlCumlesi)
        {
            sqlCumlesi = sagdanBoslukSil(sqlCumlesi, ",");
            sqlCumlesi = soldanBoslukSil(sqlCumlesi, ",");
            sqlCumlesi = sagdanBoslukSil(sqlCumlesi, "=");
            sqlCumlesi = soldanBoslukSil(sqlCumlesi, "=");
            sqlCumlesi = sagdanBoslukSil(sqlCumlesi, "(");
            sqlCumlesi = soldanBoslukSil(sqlCumlesi, ")");
            sqlCumlesi = sagdanBoslukSil(sqlCumlesi, "*");
            sqlCumlesi = soldanBoslukSil(sqlCumlesi, "*");
            sqlCumlesi = sqlCumlesi.Replace("=", " = ");
            sqlCumlesi = sqlCumlesi.Replace("*", " * ");
            sqlCumlesi = bosluklariTekBoslukYap(sqlCumlesi);
            sqlCumlesi = sqlCumlesi.Trim();
            Console.WriteLine(sqlCumlesi);
            string[] sqlYapisi = sqlCumlesi.Split(' ');
            if (sqlYapisi[2]!="from")
            {
                sqlCumlesi=sqlCumlesi.Replace("* ","*");
            }
            Console.WriteLine(sqlCumlesi);
            sqlYapisi = sqlCumlesi.Split(' ');
            foreach(string c in sqlYapisi)
            {
                Console.WriteLine(c);
            }
            return sqlYapisi;

        }
        static void syntaxHatali()
        {
            Console.WriteLine("Syntax hatali");
        }
        static void sqlSyntaxCheck(string sqlCumlesi)
        {
            string[] sqlYapisi = sqlCumlesiniDuzenle(sqlCumlesi);
            if (sqlYapisi[0].ToLower() =="select")
            {
                Console.WriteLine(selectSyntaxCheck(sqlYapisi));
            }
            else
            {
                Console.WriteLine("SQL Syntax Error: Ilk kelime select,insert veya delete olmali");
            }                
        }
        static bool tabloStunElemanKontrol(string cumle)
        {
            
            string[] parcalanmisCumle = cumle.Split(',');
            string[] tabloStunlariDizi = { "*","name", "size", "permission", "hardlink", "user", "group", "modified date", "file type" };
            List<string> tabloStunlari = new List<string>(tabloStunlariDizi);
            foreach(string deger in parcalanmisCumle)
            {
                if (tabloStunlari.Contains(deger))
                {
                    tabloStunlari.Remove(deger);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        static string selectSyntaxCheck(string[] sqlYapisi)
        {
            if (sqlYapisi.Length < 4)
            {
                return "SQL Syntax Error : Tam bir sql cumlesi girilmedi";
            }
            for(int i=1; i<sqlYapisi.Length; i++)
            {
                if (i == 1)
                {
                    if (!tabloStunElemanKontrol(sqlYapisi[i]))
                    {
                        return "SQL Syntax Error : Ikinci kelimede stun adi bulunamadi";
                    }
                    else
                    {
                        // Asil kodlar buraya
                    }
                }else if (i == 2)
                {
                    if (sqlYapisi[i] != "from")
                    {
                        return "SQL Syntax Error : Ucuncu kelime from olmali";
                    }
                }else if (i == 3)
                {
                    bool sart = true;
                    // Path var mi kontrol ediliyor
                    try
                    {
                        string[] x = Directory.GetDirectories(sqlYapisi[i]);
                    }
                    catch
                    {
                        sart = false;

                    }

                    if (sart)
                    {
                        
                    }
                    else
                    {
                        return "SQL Syntax Error : Path bulunamadi.";
                    }
                }else if (i == 4)
                {
                    if (sqlYapisi[i].ToLower() != "where")
                    {
                        return "SQL Syntax Error : Sart kelimesi hatali";
                    }
                    else
                    {
                        //Sart oldugu programa soylenecek
                    }
                }else if (i==5)
                {
                    
                    if (!tabloStunElemanKontrol(sqlYapisi[i]))
                    {
                        return "SQL Syntax Error : Stun adi hatali";
                    }
                    else
                    {
                        //stun adi dogru. atama yapilacak 
                    }
                    
                    // hic bir return e girilmediyse gerekli sartlar programa bildirilecek
                }else if (i == 6)
                {
                    if (sqlYapisi[i] != "=")
                    {
                        return "SQL Syntax Error : '=' operatoru yazilmamis veya yanlis operator kullanilmis";
                    }
                }
               
            }
            return "Syntax Dogru";
        }
    }   
}
    


