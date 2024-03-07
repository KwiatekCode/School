using System;
using System.Collections.ObjectModel;
using System.IO;

namespace LosowanieUcznia.Models
{
    public class FileDataHandler
    {
        static public string FilePath { get; set; } = Path.Combine(FileSystem.Current.AppDataDirectory, "ListaUczniow.txt");
        static public ObservableCollection<Student> Load()
        {
            Console.WriteLine(FilePath);
            ObservableCollection<Student> listaUczniow = new ObservableCollection<Student>();
            StreamReader streamReader = null;

            try
            {
                if (!File.Exists(FilePath))
                {
                    File.Create(FilePath).Close();
                }
                else
                {
                    streamReader = new StreamReader(FilePath);
                    string line;

                    while ((line = streamReader.ReadLine()) != null)
                    {
                        string[] studentData = line.Split(',');
                        Student student = new Student
                        {
                            FirstName = studentData[0],
                            LastName = studentData[1],
                            Class = studentData[2],
                            Number = studentData[3]
                        };
                        listaUczniow.Add(student);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception($"error: {e}");
            }
            finally
            {
                streamReader?.Close();
            }

            return listaUczniow;
        }

        static public void Save(ObservableCollection<Student> listaUczniow)
        {
            try
            {
                List<string> lista = new List<string>();
                foreach (var student in listaUczniow)
                {
                    lista.Add($"{student.FirstName},{student.LastName},{student.Class},{student.Number}");
                }
                File.WriteAllLines(FilePath, lista);
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e}");
            }
        }
    }
}