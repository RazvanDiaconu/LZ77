using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZ77
{
    class Program
    {
        public static string SB="";
        public static int pozitie=0;

        static void Main(string[] args)
        {
            string deCodat = "ABABABABABAB";
            Console.WriteLine("De codat: " + deCodat);
            List<Token> temp = new List<Token>();
            temp=LZ77(deCodat);
            foreach(Token t in temp)
            {
                Console.WriteLine(t.pozitie + " " + t.lungime +   " " + t.simbol);
            }
            Console.ReadLine();
            
            

        }
       
        
        public static List<Token> LZ77(string LAB)
        {
            List < Token > temp= new List<Token>();
            string buffer = "";
            uint ct = 0;
           
            for (int i = 0; i < LAB.Length; i++)
            {
                    ct++; //tine marimea bufferului 
                    buffer += LAB[i];//incrementeaza bufferul pana cand nu mai gaseste 
                if (CautaInSB(buffer,LAB) == false)
                    { 
                        temp.Add(new Token(pozitie, (uint)ct - 1, LAB[i]));//adauga token
                        buffer = "";//goleste buuferul
                        ct = 0;//resteaza contor
                        pozitie = 0;//resteaza pozitie
                    }
            }
            
            return temp;
        }

        public static bool CautaInSB(string buffer,string lab)
        {
           
            if (SB =="")
            {
                SB += buffer;
                return false;
            }
            for(int i = SB.Length-1; i>=buffer.Length-1; i--)
            {
               
                if (SB.Length == lab.Length-buffer.Length && buffer.Length > 1) 
                {
                    return false;
                }//a ajuns la final si inca nu a returnat false

                string temp = SB.Substring(i+1-buffer.Length, buffer.Length);//combinatii ordonate invers din SB

                if (buffer == temp)//daca gaseste pattern
                {
                    pozitie = SB.Length - 1 - SB.LastIndexOf(buffer);//ia pozitia de inceput a patternului
                    return true;
                 }
            }
            SB += buffer;//daca nu gaseste pattern SB creste;
            return false;
        }
    }
    class Token
    {
        public int pozitie;
        public uint lungime;
        public char simbol;
        public Token(int pozitia,uint lungimea,char simbolul)
        {
            pozitie = pozitia;
            lungime = lungimea;
            simbol = simbolul;
        }
    }
}
