using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlString
{
    class Class1
    {
        private string soldanBoslukSil(string sqlCumlesi, string noktalaIsareti)
        {
            while (sqlCumlesi != sqlCumlesi.Replace(" " + noktalaIsareti, noktalaIsareti))
            {
                sqlCumlesi = sqlCumlesi.Replace(" " + noktalaIsareti, noktalaIsareti);
            }

            return sqlCumlesi;
        }
        private string sagdanBoslukSil(string sqlCumlesi, string noktalaIsareti)
        {
            while (sqlCumlesi != sqlCumlesi.Replace(noktalaIsareti + " ", noktalaIsareti))
            {
                sqlCumlesi = sqlCumlesi.Replace(noktalaIsareti + " ", noktalaIsareti);
            }

            return sqlCumlesi;
        }
        private string bosluklariTekBoslukYap(string sqlCumlesi)
        {
            while (sqlCumlesi != sqlCumlesi.Replace("  ", " "))
            {
                sqlCumlesi = sqlCumlesi.Replace("  ", " ");
            }

            return sqlCumlesi;
        }
        public string sqlCumlesiniDuzenle(string sqlCumlesi)
        {
            sqlCumlesi = sagdanBoslukSil(sqlCumlesi, ",");
            sqlCumlesi = soldanBoslukSil(sqlCumlesi, ",");
            sqlCumlesi = sagdanBoslukSil(sqlCumlesi, "=");
            sqlCumlesi = soldanBoslukSil(sqlCumlesi, "=");
            sqlCumlesi = sagdanBoslukSil(sqlCumlesi, "(");
            sqlCumlesi = soldanBoslukSil(sqlCumlesi, ")");
            sqlCumlesi = bosluklariTekBoslukYap(sqlCumlesi);
            return sqlCumlesi;

        }
    }
}
