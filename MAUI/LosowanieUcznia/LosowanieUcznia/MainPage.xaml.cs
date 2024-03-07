using System.Collections.ObjectModel;
using LosowanieUcznia.Models;

namespace LosowanieUcznia;

public partial class MainPage : ContentPage
{
    public ObservableCollection<Student> ListaUczniow { get; set; }
    public ListHandler StudentListHandler { get; set; } = new ListHandler();
    public MainPage()
    {
        InitializeComponent();
        ListaUczniow = FileDataHandler.Load();
        BindingContext = this;
    }

    void ListView_Class_Selected(System.Object sender, Microsoft.Maui.Controls.SelectedItemChangedEventArgs e)
    {
        string selectedClass = (string)e.SelectedItem;

        StudentListHandler.changeClass(selectedClass);

        DrawedStudent.Text = "";
    }

    void Draw_Student(System.Object sender, System.EventArgs e)
    {
        DrawedStudent.Text = $"Wylosowany Uczen: {StudentListHandler.RandomStudent()}";
    }

    void Button_Add_User(System.Object sender, System.EventArgs e)
    {
        string FirstName = FirstNameEditor.Text;
        string LastName = LastNameEditor.Text;
        int Number = 0;
        string Class = ClassEditor.Text;

        if (Int32.TryParse(NumberEditor.Text, out int Parsed))
        {
            Number = Parsed;
        }

        if (FirstName == string.Empty)
        {
            errorLabel.Text = "Prosze wpisac Imie";
            return;
        }

        if (LastName == string.Empty)
        {
            errorLabel.Text = "Prosze wpisac Nazwisko";
            return;
        }

        if (Number == 0)
        {
            errorLabel.Text = "Prosze wpisac Numerek";
            return;
        }

        if (Class == string.Empty)
        {
            errorLabel.Text = "Prosze wpisac Klase";
            return;
        }

        FirstNameEditor.Text = "";
        LastNameEditor.Text = "";
        NumberEditor.Text = "";
        ClassEditor.Text = "";

        StudentListHandler.AddStudent(FirstName, LastName, Number, Class);

        StudentListHandler.changeClass(Class);
    }

}