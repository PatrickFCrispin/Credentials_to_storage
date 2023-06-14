using System;

namespace Common.IoC
{
    public class InstanceNotResolvedException : Exception
    {
        public InstanceNotResolvedException(Type classType)
            : base($"The type '{classType.FullName}' does not have a valid factory.")
        {
        }

        public InstanceNotResolvedException(Type classType, Type arg1Type)
            : base(string.Format(
                "The type '{0}' (arguments '{1}') does not have a valid factory.",
                classType.FullName,
                string.Join(";", new[] { arg1Type.FullName })))
        {
        }

        public InstanceNotResolvedException(Type classType, Type arg1Type, Type arg2Type)
            : base(string.Format(
                "The type '{0}' (arguments '{1}') does not have a valid factory.",
                classType.FullName,
                string.Join(";", new[] { arg1Type.FullName, arg2Type.FullName })))
        {
        }
    }
}