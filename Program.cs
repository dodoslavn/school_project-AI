using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
    {
    class Program
        {
        static void Main(string[] args)
            {
            Hra nova = new Hra(10, 5, true);
			bool run = true;
			while(run)
				{
				Console.WriteLine("----------------");
				nova.vykresli();
				Console.Write("Riadok (0-"+(10-1).ToString()+"): "); int x = Convert.ToInt32(Console.ReadLine());
				Console.Write("Stlpec (0-"+(10-1).ToString()+"): "); int y = Convert.ToInt32(Console.ReadLine());
				if (!nova.ideHrac(x,y) ) Console.WriteLine("nevlozilo sa!!!!");
				Suradnice tah = nova.hladaj();
				Console.WriteLine("\r\nPocitac oznacil: "+tah.riad+" "+tah.stlp);
				char vyhral = nova.vyhodnot();
				if (!nova.volneKroky()) 
					{
					nova.vykresli();
					Console.WriteLine("\r\nRemiza!");
					run = false;
					}
				if (vyhral == 'O') 
					{
					nova.vykresli();
					Console.WriteLine("\r\nVyhral hrac!");
					run = false;
					}
				else if (vyhral == 'X') 
					{
					nova.vykresli();
					Console.WriteLine("\r\nVyhral PC!");
					run = false;
					}
				}
			Console.Read();
            }
        }
    }
