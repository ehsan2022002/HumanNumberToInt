using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleApplication1.Tokenizer;

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
                    else
                    {
                        num.Add(0);
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

        public string ReplaceHumanNum(string input_s)
        {
            WordTokenizer wt = new WordTokenizer();
            var l = wt.Tokenize(input_s);

            HumanNumberFind hmf = new HumanNumberFind();
            hmf.findNumberPositions(l);

            string santance_buffer = string.Empty;
            string number_buffer = string.Empty;

            int j = 0;
            //hmf.num.Add(0); // to finish & flush loop 
            
            foreach (int i in hmf.num)
            {
                if (i == 0 && hmf.pos[j] == 0)
                {
                    number_buffer += " " + l.ToArray()[i];
                    j = i + 1;
                }                
                else if (i == 0) //not number
                {

                    if (number_buffer.Trim().Length > 0)
                    { //convert number and flush                         
                        santance_buffer += " " + HumanNumber.ParseFarsi(number_buffer.Trim());
                        number_buffer = string.Empty;
                    }

                    santance_buffer += " " + l.ToArray()[j];
                    j = j + 1;
                }
                else
                {
                    number_buffer += " " + l.ToArray()[i];
                    j = i + 1;
                }
            }

            //test for buffer
            if (number_buffer.Trim().Length > 0)
            { //convert number and flush                         
                santance_buffer += " " + HumanNumber.ParseFarsi(number_buffer.Trim());
                number_buffer = string.Empty;
            }


            return santance_buffer;
        }

    }
}
