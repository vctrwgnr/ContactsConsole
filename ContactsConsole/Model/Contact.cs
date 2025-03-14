using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContactsConsole.Model;



public class Contact
{
    private static int nextId = 1;

    public int Id { get; private set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public Contact(string name, string surname, string email, string phone)
    {
        Id = nextId++;
        Name = name;
        Surname = surname;
        Email = email;
        Phone = phone;
    }
    public Contact() { }

    [JsonConstructor]
    public Contact(int id, string name, string surname, string email, string phone)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Email = email;
        Phone = phone;
    }

    public static void SetNextId(List<Contact> contacts)
    {
        nextId = contacts.Count > 0 ? contacts.Max(c => c.Id) + 1 : 1;
    }

    public static void printTableTop()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("┌────┬──────────────┬──────────────┬───────────────────────┬───────────────┐");
        Console.WriteLine("│ ID │ Name         │ Surname      │ Email                 │ Phone         │");
        Console.WriteLine("├────┼──────────────┼──────────────┼───────────────────────┼───────────────┤");
    }

    public static void printTableBottom()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("└────┴──────────────┴──────────────┴───────────────────────┴───────────────┘");
    }

    public static void printContacts(List<Contact> contacts)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        foreach (var contact in contacts)
        {
            Console.WriteLine($"│ {contact.Id,-2} │ {contact.Name,-12} │ {contact.Surname,-12} │ {contact.Email,-21} │ {contact.Phone,-13} │");
        }
    }

    public static void addNewContact(List<Contact> contacts, string filePath)
    {
        contacts.Add(printInputQuestions());
        string updatedJson = JsonSerializer.Serialize(contacts, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, updatedJson);
        
    }

    private static Contact printInputQuestions()
    {
        Console.WriteLine("Name:");
        var name = Console.ReadLine();
        Console.WriteLine("Surname:");
        var surname = Console.ReadLine();
        Console.WriteLine("Email:");
        var email = Console.ReadLine();
        Console.WriteLine("Phone:");
        var phone = Console.ReadLine();
        return new Contact(name, surname, email, phone);
    }

    public static void DeleteContact(List<Contact> contacts, string filePath)
    {
        Console.WriteLine("Enter the ID of the contact you would like to delete: ");
        var userInputId = Console.ReadLine();

        
        if (int.TryParse(userInputId, out int id))
        {
            
            var contactToRemove = contacts.FirstOrDefault(c => c.Id == id);

            if (contactToRemove != null)
            {
                
                contacts.Remove(contactToRemove);

                
                string updatedJson = JsonSerializer.Serialize(contacts, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, updatedJson);
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Contact deleted successfully.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Contact not found.");
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid ID format.");
        }
    }

}
