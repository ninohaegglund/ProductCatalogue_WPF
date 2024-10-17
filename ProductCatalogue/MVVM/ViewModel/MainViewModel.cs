using ProductCatalogue.Core;
using ProductCatalogue.MVVM.View;
using System.ComponentModel.DataAnnotations;
using System.Windows;


namespace ProductCatalogue.MVVM.ViewModel;

class MainViewModel : ObservableObject
{
    public RelayCommand HomeViewCommand { get; set; }
    public RelayCommand DetailViewCommand { get; set; }

    public HomeViewModel HomeVM { get; set; }
    public DetailViewModel DetailVM { get; set; }

    private object _currentView;
	public object CurrentView
	{
		get { return _currentView; }
		set 
		{ 
			_currentView = value;
			OnPropertyChanged();
		}
	}


	public MainViewModel()
    {
        HomeVM = new HomeViewModel();
        DetailVM = new DetailViewModel();
        CurrentView = HomeVM;

        HomeViewCommand = new RelayCommand(o => 
        { 
            CurrentView = HomeVM;
        });

        DetailViewCommand = new RelayCommand(o =>
        {
            CurrentView = DetailVM;
        });

    }
}
