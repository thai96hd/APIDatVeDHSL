using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace APIDatVe.Helper
{
    public class DataHelper
    {
        public static string RandomString(int numberChar = 10)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < numberChar; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(36);
                if (temp != -1 && temp == t)
                {
                    return RandomString(numberChar);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }
        public static string HeadSpecialString(int _level = 0)
        {
            if (_level > 0)
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < _level; i++)
                    stringBuilder.Append("- ");
                return "|" + stringBuilder.ToString();
            }
            else
                return "";
        }
        public static string ConvertToUnSign(string _text)
        {
            for (int i = 33; i < 48; i++)
            {
                _text = _text.Replace(((char)i).ToString(), "");
            }
            for (int i = 58; i < 65; i++)
            {
                _text = _text.Replace(((char)i).ToString(), "");
            }
            for (int i = 91; i < 97; i++)
            {
                _text = _text.Replace(((char)i).ToString(), "");
            }
            for (int i = 123; i < 127; i++)
            {
                _text = _text.Replace(((char)i).ToString(), "");
            }
            _text = _text.Replace("  ", " ");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string strFormD = _text.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').ToLower();
        }
        public static string Currency(double _price)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            return _price.ToString("#,###", cul.NumberFormat);
        }
    }
}