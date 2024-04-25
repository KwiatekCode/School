using System;
using System.Collections.ObjectModel;
using CollectionManagementSystem.Services;
using CollectionManagementSystem.Views;
using System.Diagnostics;

namespace CollectionManagementSystem;

public partial class MainPage : ContentPage
{
	ObservableCollection<string> CollectionNames;
    string collectionNamePicked;

	void Refresh()
	{
        CollectionNames = FileDataHandler.ReadCollection();
        CollectionView.ItemsSource = CollectionNames;
    }

	public MainPage()
	{
		InitializeComponent();
		CollectionNames = new ObservableCollection<string>();
		Refresh();
        Debug.WriteLine(FileDataHandler.directoryPath);
    }

	

    void AddNewCollection(System.Object sender, System.EventArgs e)
    {
		if(CollectionName.Text != String.Empty)
		{
            FileDataHandler.CreateFile(CollectionName.Text);
			Refresh();
			CollectionName.Text = String.Empty;
        }
    }

    void OpenCollection(System.Object sender, System.EventArgs e)
    {
    if (!string.IsNullOrEmpty(collectionNamePicked))
        {
            Navigation.PushAsync(new CollectionDetailsPage(collectionNamePicked));
        }
    }

    void DeleteCollection(System.Object sender, System.EventArgs e)
    {
        if (!string.IsNullOrEmpty(collectionNamePicked))
        {
            FileDataHandler.DeleteCollection(collectionNamePicked);
            Refresh();
            collectionNamePicked = String.Empty;
        }
    }

    void CollectionView_SelectionChanged(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
    {
        if(e.CurrentSelection != null && e.CurrentSelection.Any())
        {
            collectionNamePicked = e.CurrentSelection.FirstOrDefault().ToString();

        }
    }

    
}


