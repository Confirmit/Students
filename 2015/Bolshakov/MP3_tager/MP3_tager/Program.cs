﻿using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using RetagerLib;

namespace MP3_tager
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 1)
                {
                    if (args[0] == "help")
                        Console.WriteLine(Messeges.LongHelp);
                    else
                        Console.WriteLine(Messeges.ShortHelp);
                }
                else if (args.Length == 2)
                {
                    var tager = new Retager();
                    tager.TagFile(args[0], args[1]);
                }
                else
                    Console.WriteLine(Messeges.ShortHelp);
            }
            catch(FileNotFoundException excep)
            {
                Console.WriteLine(Messeges.FileNotExist);
            }
            catch(NotValidPatternException excep)
            {
                Console.WriteLine(Messeges.NotValidPatter);
            }
            catch(KeyNotFoundException excep)
            {
                Console.WriteLine(Messeges.KeyNotFound);
            }
            
        }
    }
}
