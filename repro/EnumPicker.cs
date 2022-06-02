#nullable enable
namespace GenericViewBug;

public class EnumPicker<T> : BasePicker where T : Enum
{

}
public class StructPicker<T> : BasePicker where T : struct
{

}

public class ClassPicker<T> : BasePicker where T : class 
{

}

public class InterfacePicker<T> : BasePicker where T : IMyInterface
{

}

public class MultipleArgsPicker<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK> : BasePicker 
    where TA : notnull, IMyInterface
    where TB : class
    where TC : struct
    where TD : class, IMyInterface,   new()
    //TE has no constraints 
    where TF : notnull
    where TG : unmanaged
    where TH : IMyInterface?
    where TI : class?
    where TJ : IMyInterface
    where TK : new()

    
{

}

public class BasePicker : Picker
{

}

public interface IMyInterface
{
    
}

