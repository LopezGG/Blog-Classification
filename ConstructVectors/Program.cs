using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructVectors
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
                throw new Exception("Incorrect number of arguments");
            String outputVectorFile = args[0];
            if (File.Exists(outputVectorFile))
                File.Delete(outputVectorFile);
            string DirList = args[1];
            string Line;
            string label, fileName;


            using (StreamReader Sd = new StreamReader(DirList))
            {
                while ((Line = Sd.ReadLine()) != null)
                {
                    if (String.IsNullOrWhiteSpace(Line))
                        continue;
                    string[] fileEntries = Directory.GetFiles(Line);
                    label = Path.GetFileName(Line);
                    foreach (var filePath in fileEntries)
                    {
                        fileName = Path.GetFileName(filePath);
                        ClassificationMaxEnt.ProcessInputFile.ProcessInput(outputVectorFile, filePath, label);
                    }
                }
            }
        }
    }
}
