using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace ProductCatalogue.Core;

class ObservableObject : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnProperyChanged([CallerMemberName] string name = null!)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
