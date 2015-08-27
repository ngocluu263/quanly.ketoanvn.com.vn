using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vpro.functions;

namespace ThanhLapDN.Data
{
    public class Function
    {
        public string Getprice(object Price)
        {
            decimal _dPrice = Utils.CDecDef(Price);
            return _dPrice != 0 ? String.Format("{0:0,0}", _dPrice).Replace(",", ".") : "0";
        }
        public string fomartPrice(object Price)
        {
            decimal _dPrice = Utils.CDecDef(Price);
            return _dPrice != 0 ? String.Format("{0:###,###}", _dPrice) : "";
        }
        public string getDate(object News_PublishDate)
        {
            return string.Format("{0:dd/MM/yyyy}", News_PublishDate);
        }
        public double Round(double value, int digits)
        {
            if (digits >= 0) return System.Math.Round(value, digits);

            double n = System.Math.Pow(10, -digits);
            return System.Math.Round(value / n, 0, MidpointRounding.AwayFromZero) * n;
        }
    }
}