using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoveDuplicatas
{
    internal class Program
    {
        static string dir = "";
        static List<string> files = new List<string>();
        static int contagemDuplicatas = 0;

        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Digite o diretório onde deve ser feita a leitura dos arquivos:");
                dir = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(dir)); 
            
            try
            {
                AnaliseDeDiretorios();
                Console.WriteLine("Começando análise dos arquivos");
                files = Directory.GetFiles(dir).ToList();
                foreach (var file in files.ToList()){

                    var fileSemExtensao = Path.GetFileNameWithoutExtension(file);
                    var duplicatas = files.Where(x => x.Contains(fileSemExtensao + " - Copia") || x.Contains(fileSemExtensao + " (")).Select(x => x).ToList();
                    if (duplicatas.Count > 0)
                        MoveDuplicatas(duplicatas);
                }
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("Análise finalizada! {0} Duplicatas encontradas!", contagemDuplicatas);
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("Aperte qualquer tecla para finalizar!");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


            


        public static void AnaliseDeDiretorios()
        {
            try
            {
                if (!Directory.Exists(dir + @"\duplicatas"))
                    Directory.CreateDirectory(dir + @"\duplicatas");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void MoveDuplicatas(List<string> duplicatas)
        {
            try
            {
                Console.WriteLine("Duplicata encontrada! {0} cópias encontradas", duplicatas.Count);
                duplicatas.ForEach((duplicata) =>
                {
                    contagemDuplicatas++;
                    File.Move(duplicata, dir + @"\duplicatas\" + Path.GetFileName(duplicata));
                    files.Remove(duplicata);
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
