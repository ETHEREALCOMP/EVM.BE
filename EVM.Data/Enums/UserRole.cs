namespace EVM.Data.Enums;

[Flags]
public enum UserRole
{
    None = 0,

    Gues = 1,

    Organizer = 2,

    Admin = Gues | Organizer,
}
