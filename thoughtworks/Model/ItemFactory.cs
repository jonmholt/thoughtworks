using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;

namespace thoughtworks
{
    public class ItemFactory
    {
        private static List<string> books = new List<string> {"book"};//(ConfigurationManager.AppSettings["books"].Split(','));
        private static List<string> foods = new List<string> { "chocolate"};
        private static List<string> medical = new List<string> { "pill" };

        static ItemFactory() {
            string config = ConfigurationManager.AppSettings["books"];
            if (!String.IsNullOrEmpty(config))
            {
                books = new List<string>(config.Split(','));
            }
            config = ConfigurationManager.AppSettings["foods"];
            if (!String.IsNullOrEmpty(config))
            {
                foods = new List<string>(config.Split(','));
            }
            config = ConfigurationManager.AppSettings["medical"];
            if (!String.IsNullOrEmpty(config))
            {
                medical = new List<string>(config.Split(','));
            }
        }

        public static List<IItem> Parse(Stream stream)
        {            
            List<IItem> list = new List<IItem>();
            using (StreamReader sr = new StreamReader(stream))
            {
                string line = "";
                while (!sr.EndOfStream)
                {
                    try
                    {
                        //for each line, create an Item
                        line = sr.ReadLine();
                        //get the number in case
                        int space=line.IndexOf(" ");
                        int count = int.Parse(line.Substring(0, space));
                        if (count > 500)
                            throw new ArgumentException(String.Format("{0} seems a little excessive, shouldn't you be shopping in the bulk store?", count));
                        //get the item.  the item and price are dilineated by "at"
                        string what = line.Substring(space+1,line.IndexOf(" at ")-space).TrimEnd();
                        //check if its imported
                        bool imported = what.Contains("imported");
                        if (imported)
                        {
                            //move 'imported' to the beginning of the string
                            what = "imported " + what.Replace("imported ", "");
                        }
                        //and now the price
                        double price = double.Parse(line.Substring(line.IndexOf(" at ")+4));

                        //Now, just in case someone gets sneaky and put a 2 in front
                        for (int i = count - 1; i >= 0; i--)
                        {
                            //Create the object
                            IItem item = null;                            
                            if (books.Any(s=>what.Contains(s)))
                            {
                                item = new Book();
                            }
                            else if (foods.Any(s => what.Contains(s)))
                            {
                                item = new Food();
                            }
                            else if (medical.Any(s => what.Contains(s)))
                            {
                                item = new Medical();
                            }
                            else
                            {
                                //Its just something else.
                                item = new Item();
                            }
                            item.isImported = imported;
                            item.Price = price;
                            item.Description = what;

                            //Add it to the list
                            list.Add(item);
                        }
                    }
                    catch (Exception e)
                    {
                        //hmmm, that line doesn't seem to work, now what
                        throw new ArgumentException("I can't seem to read \"" + line+"\":\n"+e.Message);
                    }
                }
            }

            return list;
        }
    }
}
