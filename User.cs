using Spectre.Console;

namespace bookstore_system;

public abstract class User
{
    private string _username;
    private string _password;
    private int _permission;
}