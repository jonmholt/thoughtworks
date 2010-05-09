using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace thoughtworks
{
    class ProblemTwo
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                string message = 
                    "ProblemTwo is Jon Holt's coding test application that, oddly enough, responds to Problem Two of \n"+
                    "the assignment statement.  You're missing something, so check out the instructions below.\n"+
                    "\nusage: ProblemTwo <file> [<results_file>]\n"+
                    "\n"+
                    "The file format that this program accepts is really simple.  It is a basket with one shopping item per line.  The line is formatted as follows:\n"+
                    "\t{num of items} {description of item} at {price of item}\n"+
                    "Each of these items comes with its own quirks:\n"+
                    "\tnum of items:\t\tthis will accept numbers greater than one, but if so entered, will print multiple lines to the receipt\n"+
                    "\tprice of item:\t\tthis is expected to the be price, without any taxes applied\n" +
                    "\tdescription of item:\tthis describes the item.  If it contains:\n" +
                    "\t\timported:\timport duty will be assessed\n"+
                    "\t\tchocolate:\tit will be treated as food and is tax exempt\n"+
                    "\t\tpills:\t\tit will be treated as a medical purchase and will be tax exempt\n"+
                    "Otherwise, the item will be taxed with the standard sales tax.\n\n"+
                    "Please enjoy responsibly.\n";

                if (!System.IO.File.Exists(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().GetName().CodeBase).Replace("file:\\", ""), "Input1.txt")))
                {
                    try
                    {
                        //Write out the files
                        Assembly _assembly = Assembly.GetExecutingAssembly();
                       
                        Stream _fileStream = _assembly.GetManifestResourceStream("thoughtworks.Inputs.Input1.txt");
                        StreamReader r = new StreamReader(_fileStream);
                        
                        StreamWriter w = new StreamWriter("Input1.txt");
                        w.Write(r.ReadToEnd());
                        w.Close();
                        _fileStream.Close();
                        r.Close();

                        _fileStream = _assembly.GetManifestResourceStream("thoughtworks.Inputs.Input2.txt");
                        r = new StreamReader(_fileStream);
                        w = new StreamWriter("Input2.txt");
                        w.Write(r.ReadToEnd());
                        w.Close();
                        _fileStream.Close();
                        r.Close();

                        _fileStream = _assembly.GetManifestResourceStream("thoughtworks.Inputs.Input3.txt");
                        r = new StreamReader(_fileStream);
                        w = new StreamWriter("Input3.txt");
                        w.Write(r.ReadToEnd());
                        w.Close();
                        _fileStream.Close();
                        r.Close();

                        message +=
                            "\n\nOh! And just cause I'm a nice guy, I figured since you missed the file, I'd give you some to use.\n" +
                            "Try using the files Input1.txt, Input2.txt or Input3.txt as your first parameter";
                    }
                    catch (Exception e)
                    {
                        System.Console.Error.WriteLine(e.Message);
                    }
                }
                System.Console.WriteLine(message);
                Environment.Exit(1);
            }

            //Args are there, now lets see
            try
            {
                string inFile = args[0];
                List<IItem> list = ItemFactory.Parse(new FileStream(inFile,FileMode.Open,FileAccess.Read));

                string receipt = ReceiptFormatter.getReceipt(list);
                if (args.Length > 1)
                {
                    //write it out to file
                    StreamWriter w = new StreamWriter(args[1]);
                    w.Write(receipt);
                    w.Close();
                }
                else
                {
                    System.Console.WriteLine(receipt);
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Hmmmm.  That didn't work:");
                System.Console.WriteLine(e.Message);
                Environment.Exit(1);
            }

        }

    }
}
