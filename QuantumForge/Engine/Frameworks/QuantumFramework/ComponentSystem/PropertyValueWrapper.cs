using System.ComponentModel;

public class PropertyValueWrapper : INotifyPropertyChanged
{
    private object _value;
    public object Value
    {
        get { return _value; }
        set
        {
            _value = value;
            OnPropertyChanged(nameof(Value));
        }
    }

    public PropertyValueWrapper(object value)
    {
        _value = value;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
