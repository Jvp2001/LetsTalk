using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace LetsTalk.Core.Models;

[Serializable]
public sealed class TextToSpeechSettings : INotifyPropertyChanged
{
    private int volume = 50;
    private double rate = 1;

    [JsonPropertyName("volume")]
    public int Volume
    {
        get => volume;
        set
        {
            if (value == volume) return;
            volume = value;
            OnPropertyChanged();
        }
    }

    [JsonPropertyName("rate")]
    public double Rate
    {
        get => rate;
        set
        {
            if (value == rate) return;
            rate = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}