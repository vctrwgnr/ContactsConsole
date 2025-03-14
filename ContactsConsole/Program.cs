using System.Text.Json;
using ContactsConsole.Model;

namespace ContactsConsole;

class Program
{
    static void Main(string[] args)
    {
        // string filePath = Path.Combine(Directory.GetCurrentDirectory(), "data", "contacts.json");
        string filePath = @"..\..\..\data\contacts.json";
        string json = File.ReadAllText(filePath);

        List<Contact> contacts = JsonSerializer.Deserialize<List<Contact>>(json);
        Console.WriteLine("┌──────────────┬──────────────┬───────────────────────┬───────────────┐");
        Console.WriteLine("│ Name         │ Surname      │ Email                 │ Phone         │");
        Console.WriteLine("├──────────────┼──────────────┼───────────────────────┼───────────────┤");

        foreach (var contact in contacts)
        {
            Console.WriteLine($"│ {contact.Name,-12} │ {contact.Surname,-12} │ {contact.Email,-21} │ {contact.Phone,-13} │");
        }

        Console.WriteLine("└──────────────┴──────────────┴───────────────────────┴───────────────┘");

        
        contacts.Add(new Contact("Anna", "Smith", "anna@example.com", "555123456"));

        string updatedJson = JsonSerializer.Serialize(contacts, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, updatedJson);




        
        
    }
}