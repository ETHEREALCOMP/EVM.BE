namespace EVM.Data.Enums;

[Flags]
public enum UserRole
{
    None = 0,

    Organizer = 1 << 1,

    Gues = 1 << 2,

    Admin = Gues | Organizer,
}
