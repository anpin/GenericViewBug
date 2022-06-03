#nullable enable
namespace GenericViewBug;

using System.ComponentModel.DataAnnotations;
using System.Reflection;

public enum eSomething : int
{
    [Display(Name="Something we don't know")]
    Unknown = 0,
    [Display(Name = "Something we might need")]
    FirstCase = 1,
    [Display(Name = "Something we won't accept")]
    OtherCase = 2,
    ThisOneDoesNotHaveAttribute = 3,
}


public class EnumPicker<T> : Picker where T : Enum
{
    public EnumPicker()
    {
        MinimumHeightRequest = 10;
        SelectedIndexChanged += OnSelectedIndexChanged;
        //Fill the Items from the enum
        foreach (T value in Enum.GetValues(typeof(T)))
        {
            Items.Add(GetEnumName(value));
        }
    }

    public new static BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(T),
        typeof(EnumPicker<T>), default(T), propertyChanged: OnSelectedItemChanged,
        defaultBindingMode: BindingMode.TwoWay);

    public new T? SelectedItem
    {
        get => (T)GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    private void OnSelectedIndexChanged(object? sender, EventArgs eventArgs)
    {
        if (SelectedIndex < 0 || SelectedIndex > Items.Count - 1)
        {
            SelectedItem = default(T);
        }
        else
        {
            //try parsing, if item was added using DisplayAttribute Name this will fail
            object? match;
            if (!Enum.TryParse(typeof(T), Items[SelectedIndex], out match))
            {
                //find enum by DisplayAttribute
                match = GetEnumByName(Items[SelectedIndex]);
            }

            SelectedItem = (T)Enum.Parse(typeof(T), ((T)match!).ToString());
        }
    }

    private static void OnSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is EnumPicker<T> picker && newvalue is T castedValue)
        {
            picker.SelectedIndex = picker.Items.IndexOf(GetEnumName(castedValue));
        }
    }

    private static string GetEnumName(T value)
    {
        string result = value.ToString();
        DisplayAttribute? attribute = typeof(T).GetRuntimeField(result)
            ?.GetCustomAttributes<DisplayAttribute>(false).SingleOrDefault();

        if (attribute != null && !string.IsNullOrEmpty(attribute.Name))
            result = attribute.Name;

        return result;
    }

    private T? GetEnumByName(string name) => 
    
        Enum.GetValues(typeof(T)).Cast<T>().FirstOrDefault(x => string.Equals(GetEnumName(x), name));

}
