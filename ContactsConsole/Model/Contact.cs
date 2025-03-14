namespace ContactsConsole.Model;

public class Contact
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    
    public Contact(string name, string surname, string email, string phone)
    {
        Name = name;
        Surname = surname;
        Email = email;
        Phone = phone;
        
    }
}