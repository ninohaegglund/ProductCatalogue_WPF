using ProductCatalogue.Core;


namespace ProductCatalogue.MVVM.ViewModel;

class MainViewModel : ObservableObject
{

    public StartViewModel StartVM { get; set; }


    private object _currentView;

	public object CurrentView
	{
		get { return _currentView; }
		set 
		{ 
			_currentView = value;
			OnProperyChanged();
		}
	}


	public MainViewModel()
    {
        
    }
}
