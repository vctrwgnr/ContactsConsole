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
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Contact successfully added!");
        
    }

    private static Contact printInputQuestions()
    {
        
        var name = UserInputValidator.SimpleStringValidation(
            "Please enter your name (it cannot be empty):",
            "Error: Name cannot be empty. Try again.");
       
        var surname = UserInputValidator.SimpleStringValidation(
            "Please enter your surname (it cannot be empty):",
            "Error: Surname cannot be empty. Try again.");
        
        var email = UserInputValidator.EmailValidation(
            "Please enter your email (it cannot be empty):",
            "Error: It is not a valid email. Try again.");
        
        var phone = UserInputValidator.PhoneValidation(
            "Please enter your phone number (it cannot be empty):",
            "Invalid input! Please enter a valid phone number containing only digits (no letters or symbols).");
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
