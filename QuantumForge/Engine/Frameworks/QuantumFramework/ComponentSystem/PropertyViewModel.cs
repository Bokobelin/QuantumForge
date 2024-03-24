using System.ComponentModel;

public class PropertyViewModel : INotifyPropertyChanged
{
    private string _key;
    private object _value;

    public string Key
    {
        get { return _key; }
        set
        {
            if (_key != value)
            {
                _key = value;
                OnPropertyChanged(nameof(Key));
            }
        }
    }

    public object Value
    {
        get { return _value; }
        set
        {
            if (_value != value)
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
