using ContactsConsole.Model;

namespace ContactsConsole.View;


public class ContactManager
{
    
    public static void welcomeMessage(List<Contact> contacts, string filePath)
    {
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"What would you like to do?");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($" 1. See all contacts." +
                          $" \n 2. Add new contact. \n 3. Edit contact. \n 4. Delete contact. " +
                          $"\n 5. Exit.");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Please, enter a number between 1 and 5:");
        Console.ResetColor();
        userInput(contacts, filePath);
        Console.Clear();
    }

    public static void userInput(List<Contact> contacts, string filePath)
    {
        var input = Console.ReadLine();
        if (input == "1")
        {
            
            printAllContacts(contacts, filePath);
        }
        else if (input == "2")
        {
            addNewContact(contacts, filePath);
            // printAllContacts(contacts, filePath);
            welcomeMessage(contacts, filePath);
        }
        else if (input == "3")
        {
            // printAllContacts(contacts, filePath);
            editContact(contacts, filePath);
            // Console.WriteLine("Editing is under construction.");
            welcomeMessage(contacts, filePath);
        }
        else if (input == "4")
        {
            // printAllContacts(contacts, filePath);
            deleteContact(contacts, filePath);
            welcomeMessage(contacts, filePath);
            
            
        }
        else if (input == "5")
        {
            Environment.Exit(0);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input. Try again.");
            welcomeMessage(contacts, filePath);
        }
    }

    public static void printAllContacts(List<Contact> contacts, string filePath)
    {
        Console.Clear();
        
        Contact.printTableTop();
        Contact.printContacts(contacts);
        Contact.printTableBottom();
        welcomeMessage(contacts, filePath);
        
    }

    public static void addNewContact(List<Contact> contacts, string filePath)
    {
        Console.Clear();
        Contact.addNewContact(contacts, filePath);
    }

    public static void deleteContact(List<Contact> contacts, string filePath)
    {
        
        Contact.DeleteContact(contacts, filePath);
        
    }
    public static void editContact(List<Contact> contacts, string filePath)
    {
        
        Contact.EditContact(contacts, filePath);
       
    }
}