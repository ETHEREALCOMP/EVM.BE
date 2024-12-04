namespace EVM.Data.Enums;

[Flags]
public enum UserRole
{
    None = 0,

    Read = 1 << 1,

    Write = 1 << 2,

    Admin = Read | Write,
}
