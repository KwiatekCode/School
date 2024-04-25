using System.Collections;
using System.Collections.ObjectModel;
using CollectionManagementSystem.Services;
namespace CollectionManagementSystem.Views;

public partial class CollectionDetailsPage : ContentPage
{
    private string collectionName;
    string ItemNamePicked;
    ObservableCollection<string> ItemNames;

    void Refresh()
    {
        ItemNames = FileDataHandler.ReadItems(collectionName);
        ItemView.ItemsSource = ItemNames;
    }

    public CollectionDetailsPage(string collectionName)
    {
        InitializeComponent();
        this.collectionName = collectionName;
        Refresh();
        ContentPageName.Title = collectionName;
    }

    void CollectionView_SelectionChanged(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection != null && e.CurrentSelection.Any())
        {
            ItemNamePicked = e.CurrentSelection.FirstOrDefault().ToString();

        }
    }

    void AddNewItem(System.Object sender, System.EventArgs e)
    {
        if (ItemName.Text != String.Empty)
        {
            FileDataHandler.AppendItem(collectionName, ItemName.Text);
            Refresh();
            ItemName.Text = String.Empty;
        }
    }

    void EditItem(System.Object sender, System.EventArgs e)
    {
        ItemName.Text = ItemNamePicked;
        FileDataHandler.DeleteItem(collectionName, ItemNamePicked);
        Refresh();
    }

    void DeleteItem(System.Object sender, System.EventArgs e)
    {
        FileDataHandler.DeleteItem(collectionName, ItemNamePicked);
        Refresh();
        ItemNamePicked = String.Empty;
    }
}
