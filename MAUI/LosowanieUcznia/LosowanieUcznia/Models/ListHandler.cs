using System;
using System.Collections.ObjectModel;

namespace LosowanieUcznia.Models
{
    public class ListHandler
    {
        public ObservableCollection<Student> Students { get; set; } = FileDataHandler.Load();

        public ObservableCollection<string> ClassList { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<Student> SelectedClassStudents { get; set; }

        public ListHandler()
        {
            foreach (var item in Students.OrderBy(el => el.Class))
            {
                if (!ClassList.Contains(item.Class))
                {
                    ClassList.Add(item.Class);
                }
            }

            SelectedClassStudents = new ObservableCollection<Student>(Students.Where(el => ClassList[0] == el.Class).ToList());
        }

        public void changeClass(string Class)
        {
            SelectedClassStudents.Clear();

            foreach (var item in Students.Where(el => el.Class == Class))
            {
                SelectedClassStudents.Add(item);
            }

        }
        public string RandomStudent()
        {
            Random rnd = new Random();
            int studentIndex = rnd.Next(0, SelectedClassStudents.Count());

            Student student = SelectedClassStudents[studentIndex];
            return student.FirstName;
        }

        public void AddStudent(string firstName, string lastName, int number, string className)
        {
            Student student = new Student { FirstName = firstName, LastName = lastName, Class = className, Number = number.ToString() };

            Students.Add(student);

            if (!ClassList.Contains(className))
            {
                ClassList.Add(className);
            }

            if (SelectedClassStudents[0].Class == className)
            {
                SelectedClassStudents.Add(student);
            }

            FileDataHandler.Save(Students);

        }

        public void AddClass(string className)
        {

        }
    }
}
