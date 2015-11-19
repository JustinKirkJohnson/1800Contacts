using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Justin._1800Contacts.Logic.Class;
using Justin._1800Contacts.Logic.Interface;

namespace Justin._1800Contacts.App
{
	class Program
	{
		static void Main(string[] args)
		{

			if (args.Count() != 1)
			{
				Console.WriteLine("Usage: Justin.1800Contacts.App.Exe [source]");
				Console.WriteLine();
				Console.WriteLine("\tsource\tSpecifies the location of the source file.");
				Console.WriteLine();
				Console.WriteLine("Press any key to continue...");
				Console.ReadKey();
			}

			string sourceFilePath = args[0];
			if (!File.Exists(sourceFilePath))
			{
				Console.WriteLine("'{0}' could not be found.", sourceFilePath);
				Console.WriteLine();
				Console.WriteLine("Press any key to continue...");
				Console.ReadKey();
			}

			try
			{
				IList<string> fileInputList = new List<string>();
				using (StreamReader file = new StreamReader(sourceFilePath))
				{
					string line;
					while((line = file.ReadLine()) != null)
					{
						fileInputList.Add(line);
					}
				}

				IClassLogic classLogic = new ClassLogic();
				Console.WriteLine(string.Join(", ", classLogic.SortByPrerequisite(fileInputList)));
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: {0}", e.Message);
			}

			Console.WriteLine();
			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();

		}
	}
}
