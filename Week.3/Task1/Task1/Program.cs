using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Task1
{

    enum FSIMode
    {
        DirectoryInfo = 1,
        FileInfo = 2
    }
    class Program
    {
        static bool pathExists(string path, FSIMode mode)
        {
            if ((mode == FSIMode.DirectoryInfo && new DirectoryInfo(path).Exists) || (mode == FSIMode.FileInfo && new FileInfo(path).Exists))
                return true;
            else
                return false;
        }
        class Layer
        {
            public FileSystemInfo[] Content { get; set; }
            public int SelectedIndex { get; set; }
            public int FileIndex { get; set; }
            public void Draw()
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
                for (int i = 0; i < Content.Length; ++i)
                {
                    Console.BackgroundColor = (i == SelectedIndex) ? ConsoleColor.Blue : ConsoleColor.Black;
                    if (i >= FileIndex) Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine((i + 1) + ". " + Content[i].Name);
                }
            }
        }
        static FileSystemInfo[] Combine(DirectoryInfo[] di, FileInfo[] fi)
        {
            FileSystemInfo[] fsi = new FileSystemInfo[di.Length + fi.Length];
            for (int i = 0; i < di.Length; ++i)
                fsi[i] = di[i];
            for (int i = 0; i < fi.Length; ++i)
                fsi[i + di.Length] = fi[i];
            return fsi;
        }
        static void Main(string[] args)
        {
            DirectoryInfo startPath = new DirectoryInfo(@"C:\Users\Олжас\Downloads\Left4_Dead2\Backup");
            if (!startPath.Exists)
            {
                Console.WriteLine("Directory not exist");
                return;
            }
            Layer startLayer = new Layer
            {
                Content = Combine(startPath.GetDirectories(), startPath.GetFiles()),
                SelectedIndex = 0,
                FileIndex = startPath.GetDirectories().Length
            };
            Stack<Layer> history = new Stack<Layer>();
            history.Push(startLayer);
            bool esc = false;
            FSIMode LayerMode = FSIMode.DirectoryInfo;
            FSIMode SelectedMode = FSIMode.DirectoryInfo;
            while (!esc)
            {
                Layer l = history.Peek();
                if (LayerMode == FSIMode.DirectoryInfo)
                {
                    if (l.Content.Length > 0)
                        SelectedMode = (l.Content[l.SelectedIndex].GetType() == typeof(DirectoryInfo)) ? FSIMode.DirectoryInfo : FSIMode.FileInfo;
                    l.Draw();
                }
                ConsoleKeyInfo consolekeyInfo = Console.ReadKey();
                switch (consolekeyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (l.SelectedIndex > 0)
                        {
                            l.SelectedIndex--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (l.SelectedIndex < l.Content.Length - 1)
                        {
                            l.SelectedIndex++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (l.Content.Length == 0)
                            break;
                        if (LayerMode == FSIMode.FileInfo)
                            break;
                        if (SelectedMode == FSIMode.DirectoryInfo)
                        {
                            DirectoryInfo dir = l.Content[l.SelectedIndex] as DirectoryInfo;
                            history.Push(new Layer
                            {
                                Content = Combine(dir.GetDirectories(), dir.GetFiles()),
                                SelectedIndex = 0,
                                FileIndex = dir.GetDirectories().Length
                            });
                        }
                        else
                        {
                            LayerMode = FSIMode.FileInfo;
                            using (FileStream fs = new FileStream(l.Content[l.SelectedIndex].FullName, FileMode.Open, FileAccess.Read))
                            {
                                using (StreamReader sr = new StreamReader(fs))
                                {
                                    Console.BackgroundColor = ConsoleColor.White;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.Clear();
                                    Console.WriteLine(sr.ReadToEnd());
                                }
                            }
                        }
                        break;
                    case ConsoleKey.Escape:
                        esc = true;
                        break;
                    case ConsoleKey.Backspace:
                        if (LayerMode == FSIMode.DirectoryInfo)
                        {
                            if (history.Count() > 1)
                                history.Pop();
                        }
                        else
                            LayerMode = FSIMode.DirectoryInfo;
                        break;
                    case ConsoleKey.Delete:
                        if (LayerMode == FSIMode.FileInfo || l.Content.Length == 0)
                            break;
                        if (SelectedMode == FSIMode.FileInfo)
                            (l.Content[l.SelectedIndex] as FileInfo).Delete();
                        else
                            (l.Content[l.SelectedIndex] as DirectoryInfo).Delete(true);
                        history.Pop();
                        if (history.Count == 0)
                        {
                            startPath = new DirectoryInfo(@"C:\Users\Олжас\Downloads\Left4_Dead2\Backup");
                            history.Push(new Layer
                            {
                                Content = Combine(startPath.GetDirectories(), startPath.GetFiles()),
                                SelectedIndex = Math.Min(l.SelectedIndex, l.Content.Length - 2),
                                FileIndex = startPath.GetDirectories().Length
                            });
                        }
                        else
                        {
                            DirectoryInfo dir = new DirectoryInfo(history.Peek().Content[history.Peek().SelectedIndex].FullName);
                            history.Push(new Layer
                            {
                                Content = Combine(dir.GetDirectories(), dir.GetFiles()),
                                SelectedIndex = Math.Min(l.SelectedIndex, l.Content.Length - 2),
                                FileIndex = dir.GetDirectories().Length
                            });
                        }
                        break;
                    case ConsoleKey.F:
                        if (LayerMode == FSIMode.FileInfo || l.Content.Length == 0)
                            break;
                        string fullname = l.Content[l.SelectedIndex].FullName;
                        string name = l.Content[l.SelectedIndex].Name;
                        string path = fullname.Remove(fullname.Length - name.Length);
                        Console.WriteLine("Please enter the new name, to rename {0}:", name);
                        string newname = Console.ReadLine();
                        while (newname.Length == 0 || pathExists(path + newname, SelectedMode))
                        {
                            Console.WriteLine("This directory was created, Enter the new one");
                            newname = Console.ReadLine();
                        }
                        if (SelectedMode == FSIMode.DirectoryInfo)
                            new DirectoryInfo(fullname).MoveTo(path + newname);
                        else
                            new FileInfo(fullname).MoveTo(path + newname);
                        DirectoryInfo di = new DirectoryInfo(path + newname);
                        l.Content[l.SelectedIndex] = di as FileSystemInfo;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
