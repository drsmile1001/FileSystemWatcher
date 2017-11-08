using System;
using System.IO;

namespace FileSystemWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            string path;
            do
            {
                Console.WriteLine("輸入要監視的資料夾:");
                path = Console.ReadLine();
            } while (!Directory.Exists(path));

            using (var watcher = new System.IO.FileSystemWatcher
            {
                Path = path,
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                IncludeSubdirectories = true,
                EnableRaisingEvents = true
            })
            {
                watcher.Changed += OnChanged;
                watcher.Created += OnChanged;
                watcher.Deleted += OnChanged;
                watcher.Renamed += OnRenamed;
                Console.WriteLine("開始監視，按q離開");
                while (Console.Read() != 'q')
                {
                }
            }
            
        }

        private static void OnRenamed(object sender, RenamedEventArgs renamedEventArgs)
        {
            Console.WriteLine($"[Renamed] {renamedEventArgs.OldFullPath} -> {renamedEventArgs.FullPath}");
        }

        private static void OnChanged(object sender, FileSystemEventArgs eventArgs)
        {
            Console.WriteLine($"[{eventArgs.ChangeType}] {eventArgs.FullPath}");
        }
    }
}
