using System;
using System.Collections.Generic;
using System.Text;

namespace Code.Practice.Samples.EnumProgram
{
    //
    // Summary:
    //     Specifies the application elements on which it is valid to apply an attribute.
    [Flags]
    public enum AttributeTargets
    {
        //
        // Summary:
        //     Attribute can be applied to an assembly.
        Assembly = 1,
        //
        // Summary:
        //     Attribute can be applied to a module. Module refers to a portable executable
        //     file (.dll or.exe) and not a Visual Basic standard module.
        Module = 2,
        //
        // Summary:
        //     Attribute can be applied to a class.
        Class = 4,
        //
        // Summary:
        //     Attribute can be applied to a structure; that is, a value type.
        Struct = 8,
        //
        // Summary:
        //     Attribute can be applied to an enumeration.
        Enum = 16,
        //
        // Summary:
        //     Attribute can be applied to a constructor.
        Constructor = 32,
        //
        // Summary:
        //     Attribute can be applied to a method.
        Method = 64,
        //
        // Summary:
        //     Attribute can be applied to a property.
        Property = 128,
        //
        // Summary:
        //     Attribute can be applied to a field.
        Field = 256,
        //
        // Summary:
        //     Attribute can be applied to an event.
        Event = 512,
        //
        // Summary:
        //     Attribute can be applied to an interface.
        Interface = 1024,
        //
        // Summary:
        //     Attribute can be applied to a parameter.
        Parameter = 2048,
        //
        // Summary:
        //     Attribute can be applied to a delegate.
        Delegate = 4096,
        //
        // Summary:
        //     Attribute can be applied to a return value.
        ReturnValue = 8192,
        //
        // Summary:
        //     Attribute can be applied to a generic parameter. Currently, this attribute can
        //     be applied only in C#, Microsoft intermediate language (MSIL), and emitted code.
        GenericParameter = 16384,
        //
        // Summary:
        //     Attribute can be applied to any application element.
        All = 32767
    }
}
