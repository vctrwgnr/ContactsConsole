using System.Text.Json;
using ContactsConsole.Model;
using ContactsConsole.View;

namespace ContactsConsole;

class Program
{
    static void Main(string[] args)
    {
        // string filePath = Path.Combine(Directory.GetCurrentDirectory(), "data", "contacts.json");
        const string filePath = @"..\..\..\data\contacts.json";
        try
        {
            string json = File.ReadAllText(filePath);

            List<Contact> contacts = JsonSerializer.Deserialize<List<Contact>>(json) ?? new List<Contact>();
            Contact.SetNextId(contacts);

            ContactManager.welcomeMessage(contacts, filePath);

        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Error: the file was not found.");
            throw;
        }

        catch (Exception)
        {
            Console.WriteLine($"Error.");
        }
        
    }
}