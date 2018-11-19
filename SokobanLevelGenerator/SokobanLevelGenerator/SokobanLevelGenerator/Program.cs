using System;

namespace SokobanLevelGenerator
{
	class MainClass
	{
		int height;
		int width;
		//Ebben fogom tárolni az adatokat első nekifutásra
		//az utolsó 4 számjegy a falakat fogja kezelni
		//1 fal Fent
		//2 fal jobbra
		//4 fal lent
		//8 fal bal
		//Ezt követő két számjegy a dolgozók és dobozok jelenlétét jelölik
		// 00 üres
		// 01 dolgozó
		// 10 doboz
		// 11 civil
		//Ezt követő három számjegy a fejlesztések jelenlétét jelölik
		//000 
		//001 szemétTároló
		//010 lyuk
		//011 gomb
		//100 futószalag
		//101 csadaajtó
		//110 portál
		//111 daráló
		//Ezt követő két számjegy a dolgozók és dobozok jelenlétét jelölik
		// dolgozó
		//
		//Az ezt következők a feature-öket jelölik
		int[][] map;

		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
		}

		public void GenerateCodes() { 
		
		}


	}


}
