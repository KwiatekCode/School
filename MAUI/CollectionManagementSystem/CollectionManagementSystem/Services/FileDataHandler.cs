using System;
using System.IO;
using System.Collections.ObjectModel;
using System.Collections;

namespace CollectionManagementSystem.Services
{
    public class FileDataHandler
    {

        static public string directoryPath = Path.Combine(FileSystem.Current.AppDataDirectory, "Collections");

        static public void CreateFile(string collectionName)
        {
            string collectionPath = Path.Combine(directoryPath, $"{collectionName}.txt");
            if (!File.Exists(collectionPath))
            {
                File.Create(collectionPath).Close();
            }
        }
        static public ObservableCollection<string> ReadCollection()
        {
            ObservableCollection<string> CollectionNames = new ObservableCollection<string>();

            foreach (var file in Directory.GetFiles(directoryPath).ToList())
            {
                CollectionNames.Add(Path.GetFileNameWithoutExtension(file));
            }

            return CollectionNames;
        }

        static public void DeleteCollection(string collectionName)
        {
            string collectionPath = Path.Combine(directoryPath, $"{collectionName}.txt");
            if (File.Exists(collectionPath))
            {
                File.Delete(collectionPath);
            }
        }

        static public ObservableCollection<string> ReadItems(string collectionName)
        {
            string collectionPath = Path.Combine(directoryPath, $"{collectionName}.txt");

            ObservableCollection<string> ItemNames = new ObservableCollection<string>();

            using (StreamReader reader = new StreamReader(collectionPath))
            {
                string singleLine;

                while ((singleLine = reader.ReadLine()) != null)
                {
                    ItemNames.Add(singleLine);
                }
            }

            return ItemNames;
        }

        static public void AppendItem(string collectionName, string itemName)
        {
            string collectionPath = Path.Combine(directoryPath, $"{collectionName}.txt");

            if (new FileInfo(collectionPath).Length == 0)
            {
                File.AppendAllText(collectionPath, $"{itemName}");
            }
            else
            {
                File.AppendAllText(collectionPath, $"\n{itemName}");
            }

        }

        static public void DeleteItem(string collectionName, string ItemName)
        {
            string collectionPath = Path.Combine(directoryPath, $"{collectionName}.txt");

            var list = ReadItems(collectionName);

            string name;

            name = list.FirstOrDefault(e => e == ItemName);
            
                list.Remove(name);
                File.WriteAllText(collectionPath, String.Empty);
                foreach (var item in list)
                {
                    AppendItem(collectionName, item);
                }
            
        }
    } 
}

