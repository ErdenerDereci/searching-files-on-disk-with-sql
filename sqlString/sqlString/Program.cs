﻿using System;
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
        static Dictionary<string, string> stunlarVeDegerler = new Dictionary<string, string>();
        static void Main(string[] args)
        {

            while (true)
            {
                string sqlCumlesi = Console.ReadLine();
                if (sqlCumlesi == "1")
                {
                    break;
                }
                Console.WriteLine("");
                sqlSyntaxCheck(sqlCumlesi);




                Console.WriteLine("---\n");
            }
            sqlAramasiKonsolaBastir();
            //Console.WriteLine(sartCheck("permission=\"R\"&&sie=100k&&name=\"2\"size=asd"));

            //string[] asd = { "&&" };
            //List<string> sartListesi = new List<string>();
            //sartListesi = "2&&".Split(asd, System.StringSplitOptions.None).ToList<string>();

            //    Console.WriteLine(sartListesi.Count);

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

            Console.WriteLine("bitti...");
            Console.ReadKey();
        }
        static string soldanBoslukSil(string sqlCumlesi, string noktalaIsareti)
        {
            while (sqlCumlesi != sqlCumlesi.Replace(" " + noktalaIsareti, noktalaIsareti))
            {
                sqlCumlesi = sqlCumlesi.Replace(" " + noktalaIsareti, noktalaIsareti);
            }

            return sqlCumlesi;
        }
        static string sagdanBoslukSil(string sqlCumlesi, string noktalaIsareti)
        {
            while (sqlCumlesi != sqlCumlesi.Replace(noktalaIsareti + " ", noktalaIsareti))
            {
                sqlCumlesi = sqlCumlesi.Replace(noktalaIsareti + " ", noktalaIsareti);
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
            sqlCumlesi = sagdanBoslukSil(sqlCumlesi, "&");
            sqlCumlesi = soldanBoslukSil(sqlCumlesi, "&");
            sqlCumlesi = sqlCumlesi.Replace("*", " * ");
            sqlCumlesi = bosluklariTekBoslukYap(sqlCumlesi);
            sqlCumlesi = sqlCumlesi.Trim();
            Console.WriteLine(sqlCumlesi);
            string[] sqlYapisi = sqlCumlesi.Split(' ');
            if (sqlYapisi.Length > 3 && sqlYapisi[2] != "from")
            {
                sqlCumlesi = sqlCumlesi.Replace("* ", "*");
            }
            Console.WriteLine(sqlCumlesi);
            sqlYapisi = sqlCumlesi.Split(' ');
            foreach (string c in sqlYapisi)
            {
                Console.WriteLine(c);
            }
            return sqlYapisi;

        }
        public static int areCornerQuote(string s)
        {
            int n = s.Length;
            if (n < 2)
                return -1;
            if (s[0] == s[n - 1] && s[0] == '"')
                return 1;
            else
                return 0;
        }
        static void sqlSyntaxCheck(string sqlCumlesi)
        {
            string[] sqlYapisi = sqlCumlesiniDuzenle(sqlCumlesi);
            if (sqlYapisi[0].ToLower() == "select")
            {
                Console.WriteLine(selectSyntaxCheck(sqlYapisi));
            }
            else
            {
                Console.WriteLine("SQL Syntax Error: Ilk kelime select,insert veya delete olmali");
            }
        }
        static string sartCheck(string sart, string ikinciKelime)
        {


            List<string> ikinciKelimeSartiKontrolListesi = ikinciKelime.Split(',').ToList<string>();
            string[] asd = { "&&" };
            List<string> sartListesi = new List<string>();
            sartListesi = sart.Split(asd, System.StringSplitOptions.None).ToList<string>();

            List<string> temp = new List<string>();

            for (int i = 0; i < sartListesi.Count; i++)
            {
                temp.Clear();

                temp = sartListesi[i].Split('=').ToList<string>();
                if (temp.Count < 2)
                {
                    return "SQL Syntax Error : \"=\" operatorunu kullaniniz.";
                }
                if (temp[0] == "")
                {
                    return "SQL Syntax Error : \"&&\" operatorunden sonra sart girilmedi";
                }
                if (temp[1] == "")
                {
                    return "SQL Syntax Error : Istenilen stuna yapilacak atamanin degeri girilmedi";
                }

                if (temp.Count > 2)
                {
                    return "SQL Syntax Error : Sartlar && operatoru ile ayrilmali";
                }
                if (!tabloStunElemanKontrol(temp[0]))
                {
                    return "SQL Syntax Error : Stun adi dogru degil";
                }
                else
                {
                    try
                    {
                        ikinciKelimeSartiKontrolListesi.Remove(temp[0]);
                    }
                    catch
                    {
                        return "SQL Syntax Error : " + temp[0] + " stunu belirtilen stunlar arasinda yok. Atama yapilamaz. ";
                    }
                    if (temp[0] == "size")
                    {

                        if (temp[1].Last<char>() == 'm' || temp[1].Last<char>() == 'k' || temp[1].Last<char>() == 'g')
                        {
                            string size = temp[1].Substring(0, temp[1].Length - 1);
                            if (!size.All(char.IsDigit))
                            {
                                return "SQL Syntax Error : Size degeri numerik olmali";
                            }
                            else
                            {
                                if (temp[1].Last<char>() == 'm')
                                {
                                    try
                                    {
                                        stunlarVeDegerler.Add(temp[0], (Convert.ToInt32(size) * 1024 * 1024).ToString());
                                    }
                                    catch
                                    {
                                        return "SQL Syntax Error : " + temp[0] + " stunu icin zaten atama yapilmis.";
                                    }

                                }
                                else if (temp[1].Last<char>() == 'k')
                                {
                                    try
                                    {
                                        stunlarVeDegerler.Add(temp[0], (Convert.ToInt32(size) * 1024).ToString());
                                    }
                                    catch
                                    {
                                        return "SQL Syntax Error : " + temp[0] + " stunu icin zaten atama yapilmis.";
                                    }

                                }
                                else
                                {

                                    try
                                    {
                                        stunlarVeDegerler.Add(temp[0], (Convert.ToInt32(size) * 1024 * 1024 * 1024).ToString());
                                    }
                                    catch
                                    {
                                        return "SQL Syntax Error : " + temp[0] + " stunu icin zaten atama yapilmis.";
                                    }

                                }
                            }
                        }
                        else
                        {
                            return "SQL Syntax Error : Size degeri belirtilmedi.(k,g,m)";
                        }
                    }
                    else if (temp[0] == "permission")
                    {
                        if (areCornerQuote(temp[1]) != 1)
                        {
                            return "SQL Syntax Error : \" isareti kullanilmadi.";
                        }
                        else if (temp[1] == "\"R\"" || temp[1] == "\"W\"" || temp[1] == "\"X\"")
                        {
                            try
                            {
                                stunlarVeDegerler.Add(temp[0], stringdenCikar(temp[1]));
                            }
                            catch
                            {
                                return "SQL Syntax Error : " + temp[0] + " stunu icin zaten atama yapilmis.";
                            }
                        }
                        else
                        {
                            return "SQL Syntax Error : Yanlis permisson degeri girildi.(R,W,X)";
                        }
                    }
                    else if (temp[0] == "name")
                    {
                        if (areCornerQuote(temp[1]) != 1)
                        {
                            return "SQL Syntax Error : \" isareti kullanilmadi. String deger giriniz.";
                        }
                        else
                        {
                            try
                            {
                                stunlarVeDegerler.Add(temp[0], stringdenCikar(temp[1]));
                            }
                            catch
                            {
                                return "SQL Syntax Error : " + temp[0] + " stunu icin zaten atama yapilmis.";
                            }

                        }
                    }
                }
            }
            if (ikinciKelimeSartiKontrolListesi.Count != 0)
            {
                string tempString = "";
                foreach (string a in ikinciKelimeSartiKontrolListesi)
                {
                    tempString += a + " ";
                }
                return "SQL Syntax Error : " + tempString + "stunlarina atama yapilmamis.";
            }
            //foreach (string x in sartListesi)
            //{
            //    Console.WriteLine(x);
            //}
            //for(int i=0; i<stunlarVeDegerler.Count; i++)
            //{
            //    Console.WriteLine(stunlarVeDegerler["size"]);
            //}
            foreach (KeyValuePair<string, string> x in stunlarVeDegerler)
            {
                Console.WriteLine(x.Key + " " + x.Value);
            }
            return "pass";
        }
        static string stringdenCikar(string cumle)
        {
            while(cumle!=cumle.Replace("\"", ""))
            {
                cumle = cumle.Replace("\"", "");
            }
            return cumle;
        }
        static bool tabloStunElemanKontrol(string cumle)
        {
            string[] tabloStunlariDizi = { "*", "name", "size", "permission", "hardlink", "user", "group", "modified date", "file type" };
            List<string> tabloStunlari = new List<string>(tabloStunlariDizi);

            if (!tabloStunlari.Contains(cumle))
            {
                return false;
            }
            return true;
        }
        static bool tabloStunElemanKontrol(string cumle, char split)
        {

            string[] parcalanmisCumle = cumle.Split(split);
            string[] tabloStunlariDizi = { "*", "name", "size", "permission", "hardlink", "user", "group", "modified date", "file type" };
            List<string> tabloStunlari = new List<string>(tabloStunlariDizi);
            foreach (string deger in parcalanmisCumle)
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
            stunlarVeDegerler.Clear();
            stunlarVeDegerler.Add("islem", "select");
            if (sqlYapisi.Length < 4)
            {
                return "SQL Syntax Error : Tam bir sql cumlesi girilmedi";
            }
            for (int i = 1; i < sqlYapisi.Length; i++)
            {
                if (i == 1)
                {
                    if (!tabloStunElemanKontrol(sqlYapisi[i], ','))
                    {
                        return "SQL Syntax Error : Ikinci kelimede stun adi bulunamadi";
                    }
                    else
                    {
                        try
                        {
                            string x = sqlYapisi[5];
                        }
                        catch
                        {
                            return "SQL Syntax Error : Belirtilen sartlarin atamasi yapilmali";
                        }
                        // Asil kodlar buraya
                    }
                } else if (i == 2)
                {
                    if (sqlYapisi[i] != "from")
                    {
                        return "SQL Syntax Error : Ucuncu kelime from olmali";
                    }
                } else if (i == 3)
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
                        stunlarVeDegerler.Add("path", sqlYapisi[i]);
                    }
                    else
                    {
                        return "SQL Syntax Error : Path bulunamadi.";
                    }
                } else if (i == 4)
                {
                    if (sqlYapisi[i].ToLower() != "where")
                    {
                        return "SQL Syntax Error : Sart kelimesi hatali";
                    }
                    else
                    {
                        if (sqlYapisi.Length < 6)
                        {
                            return "SQL Syntax Error : Sart girilmedi";
                        }
                        else
                        {
                            // sart atamasi yapilacak
                        }

                    }
                } else if (sqlYapisi.Length > 6)
                {
                    return "SQL Syntax Error : Sartlar bosluk ile baglanmis. && operatoru kullaniniz.";
                }
                else if (i == 5)
                {

                    if (sartCheck(sqlYapisi[i], sqlYapisi[1]) == "pass")
                    {

                    }
                    else
                    {
                        return sartCheck(sqlYapisi[i], sqlYapisi[1]);
                    }
                }
            }
            return "Syntax Dogru";
        }

        static void sqlAramasiKonsolaBastir()
        {
            
            if (stunlarVeDegerler["islem"] == "select")
            {
                AraRecursive(stunlarVeDegerler["path"]);
            }
            

        }


        public static string AraRecursive(string path)
        {

            string[] Klasorler = { };
            string[] ArananDosyalar = { };
            

            try
            {
                Klasorler = Directory.GetDirectories(path);
                ArananDosyalar = Directory.GetFiles(path);
            }
            catch { }



            foreach (string dosyayolu in ArananDosyalar)
            {
                try
                {
                    FileInfo info = new FileInfo(dosyayolu);
                    if (info.Name.ToString().ToUpper().Contains(stunlarVeDegerler["name"].ToUpper()) && 
                        info.Length>=Convert.ToInt32(stunlarVeDegerler["size"]))
                    {
                        Console.WriteLine(info.Name.ToString());
                    }
                }
                catch
                {

                }
                
            }



            foreach (string klasorYolu in Klasorler)
            {

                AraRecursive(klasorYolu);
            }
            return null;
        }
    } 
}
    


