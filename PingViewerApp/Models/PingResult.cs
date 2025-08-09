using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PingViewerApp;

public class PingResult : INotifyPropertyChanged
{
    private string _name;
    private string _host;
    private string _status;
    private int? _timeMs;

    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public string Host
    {
        get => _host;
        set => SetField(ref _host, value);
    }

    public string Status
    {
        get => _status;
        set => SetField(ref _status, value);
    }

    public int? TimeMs
    {
        get => _timeMs;
        set => SetField(ref _timeMs, value);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;
        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        return true;
    }
}
