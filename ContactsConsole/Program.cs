using System.Text.Json;
using ContactsConsole.Model;

namespace ContactsConsole;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Current Working Directory: " + Directory.GetCurrentDirectory());
        string filePath = @"..\..\..\data\contacts.json";
        try
        {
            string jsonResponse = await File.ReadAllTextAsync(filePath);
            List<Contact> contacts = JsonSerializer.Deserialize<List<Contact>>(jsonResponse);
            Console.WriteLine("name: |  surname: |  email: |  phone: |  address: |");
            foreach (var contact in contacts)
            {
                
                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine($"{contact.Name} | {contact.Surname} | {contact.Email} | {contact.Phone}");
                
            }

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            throw;
        }
        

        
        
    }
}