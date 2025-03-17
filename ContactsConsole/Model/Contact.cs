using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Channels;

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
    Console.WriteLine("Enter the ID of the contact you would like to delete:");
    var userInputId = Console.ReadLine();

    if (int.TryParse(userInputId, out int id))
    {
        
        var contactToRemove = contacts.FirstOrDefault(c => c.Id == id);

        if (contactToRemove != null)
        {
            
            Console.WriteLine($"Are you sure you want to delete {contactToRemove.Name} {contactToRemove.Surname}? (y/n):");
            var userAnswer = Console.ReadLine().ToLower(); 

            if (userAnswer == "y")
            {
               
                contacts.Remove(contactToRemove);

                
                string updatedJson = JsonSerializer.Serialize(contacts, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, updatedJson);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Contact deleted successfully.");
               
            }
            else if (userAnswer == "n")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Deletion canceled.");
                
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                
            }
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
    public static void EditContact(List<Contact> contacts, string filePath)
{
    
    Console.WriteLine("Enter the ID of the contact you would like to edit:");
    var userInputId = Console.ReadLine();

    if (int.TryParse(userInputId, out int id))
    {
        
        var contactToEdit = contacts.FirstOrDefault(c => c.Id == id);

        if (contactToEdit != null)
        {
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Current Contact Details:");
            Console.WriteLine($"Name: {contactToEdit.Name}");
            Console.WriteLine($"Surname: {contactToEdit.Surname}");
            Console.WriteLine($"Email: {contactToEdit.Email}");
            Console.WriteLine($"Phone: {contactToEdit.Phone}");
            Console.ResetColor();

          
            Console.WriteLine("Enter new Name (leave blank to keep current):");
            var newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                contactToEdit.Name = newName;
            }

            Console.WriteLine("Enter new Surname (leave blank to keep current):");
            var newSurname = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newSurname))
            {
                contactToEdit.Surname = newSurname;
            }

            Console.WriteLine("Enter new Email (leave blank to keep current):");
            var newEmail = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newEmail))
            {
                contactToEdit.Email = newEmail;
            }

            Console.WriteLine("Enter new Phone (leave blank to keep current):");
            var newPhone = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newPhone))
            {
                contactToEdit.Phone = newPhone;
            }

            
            string updatedJson = JsonSerializer.Serialize(contacts, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, updatedJson);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Contact updated successfully.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Contact not found.");
            Console.ResetColor();
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid ID format.");
        Console.ResetColor();
    }
}

}
