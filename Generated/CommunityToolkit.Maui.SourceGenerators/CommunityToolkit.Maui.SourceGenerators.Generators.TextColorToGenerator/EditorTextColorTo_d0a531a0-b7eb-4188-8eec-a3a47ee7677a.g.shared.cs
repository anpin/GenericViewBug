﻿// AutoGenerated Code
// See: CommunityToolkit.Maui.SourceGenerators.TextColorToGenerator
#nullable enable
using System;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Core.Extensions;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace Microsoft.Maui.Controls;
public static partial class ColorAnimationExtensions_Editor
{
    /// <summary>
    /// Animates the TextColor of an <see cref="Microsoft.Maui.ITextStyle"/> to the given color
    /// </summary>
    /// <param name="element"></param>
    /// <param name="color">The target color to animate the <see cref="Microsoft.Maui.ITextStyle.TextColor"/> to</param>
    /// <param name="rate">The time, in milliseconds, between the frames of the animation</param>
    /// <param name="length">The duration, in milliseconds, of the animation</param>
    /// <param name="easing">The easing function to be used in the animation</param>
    /// <returns>Value indicating if the animation completed successfully or not</returns>
    public static Task<bool> TextColorTo(this Microsoft.Maui.Controls.Editor element, Color color, uint rate = 16u, uint length = 250u, Easing? easing = null)
    {
        ArgumentNullException.ThrowIfNull(element);
        ArgumentNullException.ThrowIfNull(color);
        if (element is not Microsoft.Maui.ITextStyle)
            throw new ArgumentException($"Element must implement {nameof(Microsoft.Maui.ITextStyle)}", nameof(element));
        //Although TextColor is defined as not-nullable, it CAN be null
        //If null => set it to Transparent as Animation will crash on null BackgroundColor
        element.TextColor ??= Colors.Transparent;
        var animationCompletionSource = new TaskCompletionSource<bool>();
        try
        {
            new Animation{{0, 1, GetRedTransformAnimation(element, color.Red)}, {0, 1, GetGreenTransformAnimation(element, color.Green)}, {0, 1, GetBlueTransformAnimation(element, color.Blue)}, {0, 1, GetAlphaTransformAnimation(element, color.Alpha)}, }.Commit(element, nameof(TextColorTo), rate, length, easing, (d, b) => animationCompletionSource.SetResult(true));
        }
        catch (ArgumentException aex)
        {
            //When creating an Animation too early in the lifecycle of the Page, i.e. in the OnAppearing method,
            //the Page might not have an 'IAnimationManager' yet, resulting in an ArgumentException.
            System.Diagnostics.Debug.WriteLine($"{aex.GetType().Name} thrown in {typeof(ColorAnimationExtensions_Editor).FullName}: {aex.Message}");
            animationCompletionSource.SetResult(false);
        }

        return animationCompletionSource.Task;
        static Animation GetRedTransformAnimation(Microsoft.Maui.Controls.Editor element, float targetRed) => new(v => element.TextColor = element.TextColor.WithRed(v), element.TextColor.Red, targetRed);
        static Animation GetGreenTransformAnimation(Microsoft.Maui.Controls.Editor element, float targetGreen) => new(v => element.TextColor = element.TextColor.WithGreen(v), element.TextColor.Green, targetGreen);
        static Animation GetBlueTransformAnimation(Microsoft.Maui.Controls.Editor element, float targetBlue) => new(v => element.TextColor = element.TextColor.WithBlue(v), element.TextColor.Blue, targetBlue);
        static Animation GetAlphaTransformAnimation(Microsoft.Maui.Controls.Editor element, float targetAlpha) => new(v => element.TextColor = element.TextColor.WithAlpha((float)v), element.TextColor.Alpha, targetAlpha);
    }
}