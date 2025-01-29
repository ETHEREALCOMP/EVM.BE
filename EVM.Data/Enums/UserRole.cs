namespace EVM.Data.Enums;

[Flags]
public enum UserRole
{
    None = 0,

    Guest = 1,

    Organizer = 2,

    Admin = Guest | Organizer,
}
