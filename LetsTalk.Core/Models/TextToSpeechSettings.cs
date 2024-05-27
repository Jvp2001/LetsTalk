using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace LetsTalk.Core.Models;



/// <summary>
/// This class is used to store the settings for the text to speech system.
/// </summary>
/// <remarks>
/// <see cref="INotifyPropertyChanged"/> is used to notify the UI when a property has changed.
/// I bound to the PropertyChangedEvent to allow the settings to be saved when the UI changes the settings.
/// This stops me from having multiple callbacks for each property in a view model which save the settings after each value is changed.
/// </remarks>
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
            if (Math.Abs(value - rate) < float.Epsilon) return;
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