using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace testWeb.BekendClasslari
{
    public class DosyaAra
    {
        public List<string> arananDosyalarVeKlasorler= new List<string>();
        public void Ara(string path, string aranan)
        {
            arananDosyalarVeKlasorler.Clear();
            AraRecursive(path,aranan);
        }
        private string AraRecursive(string path, string aranan)
        {
            
            string[] Klasorler = { };
            string[] ArananKlasorler = { };
            string[] ArananDosyalar = { };

            try
            {
                Klasorler = Directory.GetDirectories(path);
                ArananKlasorler = Directory.GetDirectories(path, aranan);
                ArananDosyalar = Directory.GetFiles(path, aranan);
            }
            catch { }

            

            foreach (string dosyayolu in ArananDosyalar)
            {
                arananDosyalarVeKlasorler.Add(dosyayolu);
            }

            foreach (string klasorYolu in ArananKlasorler)
            {
                arananDosyalarVeKlasorler.Add(klasorYolu);
            }

            foreach (string klasorYolu in Klasorler)
            {

                AraRecursive(klasorYolu, aranan);
            }
            return null;
        }
        public string[] baslangictaYukle()
        {
            string path = "C:/";
            string[] Klasorler = { };
            try
            {
                Klasorler = Directory.GetDirectories(path);
                
            }
            catch { }

            return Klasorler;
        }
    }
}