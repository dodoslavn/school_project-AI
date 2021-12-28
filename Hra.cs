using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
    {
	struct Suradnice
		{ 
		public int stlp, riad;
		public int hodnota; 
		public Suradnice(int stlp, int riad) { this.stlp = stlp; this.riad = riad; this.hodnota = 0;}
		};

    class Hra
        {
        private int velkost, vyherne;
        private char[,] pole;
        private char hrac;
        private char ai;
		private char prazdne = '~';
		private byte hlbka = 3;

        public Hra(int velkost, int vyherne, bool znak)
			{
            if (znak)
				{
                this.hrac = 'O';
                this.ai = 'X';
				}
            else
				{
                this.hrac = 'X';
                this.ai = 'O';
				}
			Console.WriteLine("Hrac ma: "+hrac.ToString());
			Console.WriteLine("PC ma: "+ai.ToString());

            this.velkost = velkost;
			this.vyherne = vyherne;
            pole = new char[velkost, velkost];
			for (int i=0; i<velkost;i++)
				{ for (int j=0; j<velkost;j++) pole[i,j] = prazdne; }

			//pole[0,0] = hrac;
			//pole[0,2] = hrac;
			//pole[1,2] = hrac;
			//pole[1,0] = ai;
			//pole[0,1] = ai;
			//pole[1,1] = ai;
			//pole[2,2] = ai;
			}

		public bool ideHrac(int x, int y)
			{
			if (p(x,y) != hrac | p(x,y) != ai) 
				{
				pole[x,y] = hrac;
				return true;
				}
			return false;
			}

		public char p(int x, int y)
			{
			if (x < velkost & y < velkost & x >= 0 & y >= 0 ) return pole[x,y];
			else 
				{
				Console.WriteLine("mimo mapu: "+x.ToString()+" "+y.ToString());
				return prazdne;
				}
			}

		public void vykresli()
			{
			Console.WriteLine();
			for (int i=0; i<velkost;i++)
				{
				for (int j=0; j<velkost;j++)  Console.Write(pole[i,j].ToString()+" ");
				Console.WriteLine();
				}
			Console.WriteLine();
			}



		public bool volneKroky()
			{
			for (int i=0; i<velkost;i++)
				{ for (int j=0; j<velkost;j++) if (pole[i,j] == prazdne) return true; }
			return false;
			}



		public char vyhodnot()
			{
			//vodorovne I = riadok, J = stlpec
			for (int i = 0; i < velkost; i++)
				{
				for(int j = 0; j <= velkost - velkost; j++ )
					{
					char posledny = prazdne;
					int k;
					for(k = 0; k < vyherne; k++)
						{
						if (p(i,j+k) == prazdne) break;
						else if ( k == 0) posledny = p(i,j+k);
						else if ( posledny != p(i,j+k) ) break;
						}
					if (k >= vyherne) return posledny;
					}
				}

			//zvysle I = riadok, J = stlpec
			for (int i = 0; i <= velkost - vyherne; i++)
				{
				for(int j = 0; j < velkost; j++ )
					{
					char posledny = prazdne;
					int k;
					for(k = 0; k < vyherne; k++)
						{
						if (p(i+k,j) == prazdne) break;
						else if ( k == 0) posledny = p(i+k,j);
						else if ( posledny != p(i+k,j) ) break;
						}
					if (k >= vyherne) return posledny;
					}
				}


			//diagonalne zlava do prava
			for (int i = 0; i <= velkost - vyherne; i++)
				{
				for(int j = 0; j <= velkost - vyherne; j++ )
					{
					char posledny = prazdne;
					int k;
					for(k = 0; k < vyherne; k++)
						{
						if (p(i+k,j+k) == prazdne) break;
						else if ( k == 0) posledny = pole[i+k,j+k];
						else if ( posledny != pole[i+k,j+k] ) break;
						}
					if (k >= vyherne) return posledny;
					}
				}



			//diagonalne zprava do lava
			char [,] pole1 = new char[velkost,velkost];
			for (int i = 0; i < velkost; i++)
				{ for(int j = 0; j < velkost; j++ ) pole1[velkost-i-1,j] = pole[i,j]; }

			for (int i = 0; i <= velkost - vyherne; i++)
				{
				for(int j = 0; j <= velkost - vyherne; j++ )
					{
					char posledny = prazdne;
					int k;
					for(k = 0; k < vyherne; k++)
						{
						if (pole1[i+k,j+k] == prazdne) break;
						else if ( k == 0) posledny = pole1[i+k,j+k];
						else if ( posledny != pole1[i+k,j+k] ) break;
						}
					if (k >= vyherne) return posledny;
					}
				}

			return prazdne;
			}

		private int ohodnot(char[,] p_pole, Suradnice co, int ako)
			{
			int navrat = 0;
			char koho = p_pole[co.riad, co.stlp];
            char protihrac;
            if (koho == ai) protihrac = hrac;
            else protihrac = ai;
			int prazdne = 0, nase = 0;
			if (ako == 1) //riadok
				{
				for (int i = 0; i<vyherne; i++)
					{
					if ( co.riad + i >= velkost ) break;
					if (p_pole[co.riad+i,co.stlp] == protihrac) break;
					if (p_pole[co.riad+i,co.stlp] == this.prazdne ) prazdne++;
					if (p_pole[co.riad+i,co.stlp] == koho ) nase++;
                    //Console.WriteLine("nase "+nase.ToString());
					}
				if (nase + prazdne == vyherne) //tu by to slo
					{ navrat = nase * 20 + prazdne; }
				}
			else if (ako == 2) //stlpec
				{
				for (int i = 0; i<vyherne; i++)
					{
					if ( co.stlp + i >= velkost ) break;
					if (p_pole[co.riad,co.stlp+i] == protihrac) break;
					if (p_pole[co.riad,co.stlp+i] == this.prazdne ) prazdne++;
					if (p_pole[co.riad,co.stlp+i] == koho ) nase++;
					}
				if (nase + prazdne == vyherne) //tu by to slo
					{ navrat = nase * 20 + prazdne; }
				}

			if (koho == hrac) navrat++;
			//if (navrat > 1) Console.WriteLine(koho.ToString()+" "+co.riad.ToString()+":"+co.stlp.ToString()+" "+navrat);
			return navrat;
			}

		private int potencial(char[,] p_pole, Suradnice co)
			{
			int naj = 0, pom = 0;

			pom = ohodnot(p_pole,co,1);
            if (pom > naj)
                {
                naj = pom;
                //Console.WriteLine(naj.ToString());
                }

			pom = ohodnot(p_pole,co,2);
            if (pom > naj)
                {
                naj = pom;
                //Console.WriteLine(naj.ToString());
                }
			return naj;
			}

		private int hodnot(char[,] p_pole, int p_hlbka)
			{
			int naj = 0;
			for(int i = 0; i < velkost; i++)
				{
				for(int j = 0; j < velkost; j++)
					{
					if (p_pole[i,j] == ai)
						{
						int pom = potencial(p_pole, new Suradnice(j,i));
                        pom = pom * (vyherne - p_hlbka);
                        if (pom > naj)
                            {
                            naj = pom;
                            //Console.WriteLine(naj.ToString());
                            }
						}
					}
				}
			return naj;
			}

		private int minimax(char[,] p_pole, char p_strana, int p_hlbka)
			{
			int hlbka = p_hlbka;
			hlbka++;
			int navrat = 0;
			
			char skuska = vyhodnot();
			if ( skuska != prazdne)
				{
				if (skuska == ai )  { return 500; }
				else { return 500; }
				}

			if (!volneKroky()) return hodnot(p_pole,hlbka);
			if (hlbka >= this.hlbka) return hodnot(p_pole,hlbka);
			

			if (p_strana == hrac)
				{
				for(int ii = 0; ii < velkost; ii++)
					{
					
					for(int jj = 0; jj < velkost; jj++)
						{
						if ( p_pole[ii,jj] == prazdne) 
							{
							p_pole[ii,jj] = hrac;
							int vysledok = minimax(p_pole, ai, hlbka);
							if ( vysledok >= navrat ) { navrat = vysledok; }
							p_pole[ii,jj] = prazdne;
							}
						}
					}
				}
			else
				{
				for(int ii = 0; ii < velkost; ii++)
					{
					for(int jj = 0; jj < velkost; jj++)
						{
						if ( p_pole[ii,jj] == prazdne) 
							{
							p_pole[ii,jj] = ai;
							int vysledok = minimax(p_pole, hrac, hlbka);
							if ( vysledok >= navrat ) { navrat = vysledok; }
							p_pole[ii,jj] = prazdne;
							}
						}
					}
				}
			return navrat;
			}

		public Suradnice hladaj()
			{
			Suradnice sur = new Suradnice(-1,-1);

            int pocet = 0;
            for (int i = 0; i < velkost; i++)
                { for (int j = 0; j < velkost; j++) { if (pole[i, j] == ai) pocet++; } }
            if (pocet == 0)
                {
                sur.riad = velkost / 2;
                sur.stlp = velkost / 2;
                pole[sur.riad, sur.stlp] = ai;
                return sur;
                }
			for (int i = 0; i<velkost; i++)
				{
				for (int j = 0; j<velkost; j++)
					{
					if ( pole[i,j] == prazdne) 
						{
						pole[i,j] = ai;
                        char pom = vyhodnot();
                        if (pom == ai)
                            {
                            //Console.WriteLine("chaaa");
                            sur.riad = i;
                            sur.stlp = j;
                            sur.hodnota = 5000;
                            break;
                            }
                        else
                            {
						    int vysledok = minimax(pole, hrac, 0);
                            //Console.WriteLine(vysledok.ToString()+" "+i.ToString()+":"+j.ToString());
						    if (vysledok >= sur.hodnota ) 
							    { 
							    sur.riad = i; 
							    sur.stlp = j; 
							    sur.hodnota = vysledok;
							    }
                            pole[i, j] = prazdne;
                            }
						}
                    if (sur.hodnota == 5000) break;
					}
				}
			if (sur.riad != -1)  { pole[sur.riad, sur.stlp] = ai; }
			return sur;
			}
        }
    }
