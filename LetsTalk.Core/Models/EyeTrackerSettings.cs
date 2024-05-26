using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace LetsTalk.Core.Models;

[Serializable]
public sealed class EyeTrackerSettings : INotifyPropertyChanged
{
    private int dwellDuration = 3;
    private int dwellRepeatDuration = 0;
    private int repeatDelayDuration = 0;
    private int cursorRadius = 3;

    [JsonPropertyName("fixation_duration")]
    public int DwellDuration
    {
        get => dwellDuration;
        set
        {
            if (value == dwellDuration) return;
            dwellDuration = value;
            OnPropertyChanged();
        }
    }

    [JsonPropertyName("dwell_repeat_duration")]
    public int DwellRepeatDuration
    {
        get => dwellRepeatDuration;
        set
        {
            if (value == dwellRepeatDuration) return;
            dwellRepeatDuration = value;
            OnPropertyChanged();
        }
    }

    [JsonPropertyName("repeat_delay_duration")]
    public int RepeatDelayDuration
    {
        get => repeatDelayDuration;
        set
        {
            if (value == repeatDelayDuration) return;
            repeatDelayDuration = value;
            OnPropertyChanged();
        }
    }

    [JsonPropertyName("cursor_radius")]
    public int CursorRadius
    {
        get => cursorRadius;
        set
        {
            if (value == cursorRadius) return;
            cursorRadius = value;
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