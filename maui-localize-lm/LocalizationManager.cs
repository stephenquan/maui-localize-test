﻿using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace maui_localize_lm;

public static class LocalizationManager
{
    public static Type DefaultResourceType;
    public static EventHandler CultureChanged;
    public static EventHandler FlowDirectionChanged;

    public static IStringLocalizer GetLocalizer<TResource>()
        => ServiceHelper.GetService<IStringLocalizer<TResource>>();

    public static IStringLocalizer GetLocalizer(Type ResourceType)
        => (IStringLocalizer)ServiceHelper.GetService(typeof(IStringLocalizer<>).MakeGenericType(new Type[] { ResourceType }));

    public static CultureInfo Culture
    {
        get => CultureInfo.CurrentUICulture;
        set
        {
            if (CultureInfo.CurrentCulture.Name == value.Name) return;
            CultureInfo.CurrentUICulture = value;
            CultureInfo.CurrentCulture = value;
            CultureChanged?.Invoke(null, EventArgs.Empty);
            FlowDirectionChanged?.Invoke(null, EventArgs.Empty);
        }
    }

    public static FlowDirection FlowDirection
        => CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft
        ? FlowDirection.RightToLeft
        : FlowDirection.LeftToRight;
}
