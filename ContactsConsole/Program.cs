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
        string json = File.ReadAllText(filePath);

        List<Contact> contacts = JsonSerializer.Deserialize<List<Contact>>(json) ?? new List<Contact>();
        Contact.SetNextId(contacts);

        ContactManager.welcomeMessage(contacts, filePath);

   
        
    }
}