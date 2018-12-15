using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class HumanNumberFind
    {


        public List<int> num = new List<int>();
        public List<int> pos = new List<int>();


        public void findNumberPositions(List<string> l)
        {
            string tempnum = string.Empty;
            int tmpindx = 0;
            
            string[] Numwords = { "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه", 
                                  "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" ,
                                  "ده", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" , 
                                    "بیلیون" , "میلیون" ,"هزار" , "صد" , "یکهزار",
                                    "یکصد","دویست","سیصد","چهارصد","پانصد","ششصد","هفتصد","هشتصد","نهصد"
                                };

            for (int i = 0; i < l.Count; i++)
            {

                if (Array.Exists(Numwords, element => element == l[i]))
                {   //something 
                    tempnum += " " + l[i];
                    num.Add(i);
                    pos.Add(tmpindx);
                }
                else if (l[i] == "و")
                {
                    if ((i + 1 <= l.Count) && (Array.Exists(Numwords, element => element == l[i + 1])))
                    {
                        tempnum += " " + l[i];
                        num.Add(i);
                        pos.Add(tmpindx);
                    }
                }
                else
                {
                    if (!tempnum.EndsWith("-"))
                        tempnum += "-";
                    num.Add(0);
                    pos.Add(-1);
                }
                tmpindx += l[i].Length;
            }
        }
    }
}
