using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ReplaceSource
{
    class Program
    {
        public static StreamWriter w = new StreamWriter("ReplaceSource.log");
        static string[] LoadList(string filename)
        {
            using(StreamReader r = new StreamReader(filename))
            {
                return r.ReadToEnd().Split(';');
            }
        }
        static void Write(string text)
        {
            w.Write(text);
            Console.Write(text);
        }

        static void WriteLine(string text)
        {
            w.WriteLine(text);
            Console.WriteLine(text);
        }
        static void Main(string[] args)
        {
            w.AutoFlush = true;

            if(args.Length <= 2)
            {
                WriteLine("USE: ReplaceSource [folder1] [folder2] [fileList.txt (splt by ;)]");
                WriteLine("");
                WriteLine("Example:");
                WriteLine("ReplaceSource C:\\project2 D:\\project2 fileList.txt");
                Environment.Exit(500);
            }
            DirectoryInfo f1 = new DirectoryInfo(args[0]);
            DirectoryInfo f2 = new DirectoryInfo(args[1]);
            string[] files = LoadList(args[2]);
            WriteLine("Folder 1: " + f1);
            WriteLine("Folder 2: " + f2);
            //WriteLine();
            //List<FileInfo> files1 = new List<FileInfo>();
            //List<FileInfo> files2 = new List<FileInfo>();
            //foreach (var f in Directory.EnumerateFiles(f1, pattern))
            //{
            //    FileInfo fI = new FileInfo(f);
            //    files1.Add(fI);
            //    WriteLine(String.Format("[1:{0}] +{1}", files1.Count, fI.Name));
            //}
            //foreach (var f in Directory.EnumerateFiles(f2, pattern))
            //{
            //    FileInfo fI = new FileInfo(f);
            //    files2.Add(fI);
            //    WriteLine(String.Format("[2:{0}] +{1}", files1.Count, fI.Name));
            //}

            foreach(var file in files)
            {
                FileInfo OnePath = new FileInfo(f1.FullName + "\\" + file);
                FileInfo TwoPath = new FileInfo(f2.FullName + "\\" + file);
                DateTime in1 = OnePath.LastWriteTime;
                DateTime in2 = TwoPath.LastWriteTime;

                Console.ForegroundColor = ConsoleColor.Gray;
                Write(OnePath.Name + " ["+ f1.Name +"] " + in1.ToString());
                if (in1 == in2)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Write(" = ");
                }
                else if (in1 > in2)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Write(" > ");
                    File.Copy(OnePath.FullName, TwoPath.FullName, true);
                }
                else if (in1 < in2)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Write(" < ");
                    File.Copy(TwoPath.FullName, OnePath.FullName, true);
                }

                Console.ForegroundColor = ConsoleColor.Gray;
                WriteLine("["+ f2.Name +"] " + in2.ToString());
            }
            WriteLine("Done");
        }
    }
}
