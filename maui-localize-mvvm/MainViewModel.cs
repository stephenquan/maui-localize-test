﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Globalization;

namespace maui_localize_mvvm;

public partial class MainViewModel : ObservableObject
{
    public string TITLE_MAIN
        => Resources.Strings.AppStrings.TITLE_MAIN;
    public string LBL_HELLO
        => Resources.Strings.AppStrings.LBL_HELLO;
    public string LBL_WELCOME
        => Resources.Strings.AppStrings.LBL_WELCOME;

    public FlowDirection FlowDirection
        => Culture.TextInfo.IsRightToLeft
        ? FlowDirection.RightToLeft
        : FlowDirection.LeftToRight;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ClickText))]
    private int _counter = 0;
    public string ClickText => Counter switch
    {
        0 => Resources.Strings.AppStrings.STR_CLICK_ME,
        1 => Resources.Strings.AppStrings.STR_CLICKED_1_TIME,
        _ => string.Format(Resources.Strings.AppStrings.STR_CLICKED_N_TIMES, Counter)
    };

    [RelayCommand]
    private void ClickMe()
        => Counter++;

    public CultureInfo Culture
    {
        get => CultureInfo.CurrentUICulture;
        set
        {
            if (value.Name == CultureInfo.CurrentUICulture.Name) return;
            CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = value;
            CultureChanged?.Invoke(null, EventArgs.Empty);
            OnPropertyChanged(nameof(Culture));
            OnPropertyChanged(nameof(TITLE_MAIN));
            OnPropertyChanged(nameof(LBL_HELLO));
            OnPropertyChanged(nameof(LBL_WELCOME));
            OnPropertyChanged(nameof(FlowDirection));
            OnPropertyChanged(nameof(ClickText));
        }
    }

    [ObservableProperty]
    private ObservableCollection<LanguageInfo> _languages = new ObservableCollection<LanguageInfo>()
    {
        new LanguageInfo("en-US"),
        new LanguageInfo("fr-FR"),
        new LanguageInfo("de-DE"),
        new LanguageInfo("zh-CN"),
        new LanguageInfo("ar-SA")
    };

    [RelayCommand]
    private void ChangeLanguage(CultureInfo language)
        => Culture = language;

    public static EventHandler CultureChanged;
}
