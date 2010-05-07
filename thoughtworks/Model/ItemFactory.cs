using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace thoughtworks
{
    public class ItemFactory
    {
        private static List<string> books = new List<string> { "book" };
        //I guess you could do a "contains chocolate" but that just seems to convenient
        private static List<string> foods = new List<string> { "chocolates", "box of chocolates", "chocolate bar" };
        private static List<string> medical = new List<string> { "headache pills" };

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
                        int count = int.Parse(line.Substring(0, 1));
                        //get the item.  the item and price are dilineated by "at"
                        string what = line.Substring(2,line.IndexOf("at")-2).TrimEnd();
                        //check if its imported
                        bool imported = what.StartsWith("imported");
                        if (imported)
                        {
                            //remove 'imported' from the type
                            what = what.Substring(what.IndexOf(" "));
                        }
                        //and now the price
                        double price = double.Parse(line.Substring(line.IndexOf("at")+2));

                        //Now, just in case someone gets sneaky and put a 2 in front
                        for (int i = count - 1; i >= 0; i--)
                        {
                            //Create the object
                            IItem item = null;
                            if (books.Contains(what))
                            {
                                item = new Book();
                            }
                            else if (foods.Contains(what))
                            {
                                item = new Food();
                            }
                            else if (medical.Contains(what))
                            {
                                item = new Medical();
                            }
                            else
                            {
                                //Its just something else.
                                item = new Item();
                            }
                            item.Price = price;
                            item.isImported = imported;
                            item.Description = what;

                            //Add it to the list
                            list.Add(item);
                        }
                    }
                    catch
                    {
                        //hmmm, that line doesn't seem to work, now what
                        throw new ArgumentException("I can't seem to read:" + line);
                    }
                }
            }

            return list;
        }
    }
}
