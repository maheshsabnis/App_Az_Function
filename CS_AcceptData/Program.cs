// See https://aka.ms/new-console-template for more information
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using CS_Models;
using System.Text.Json;

Console.WriteLine("Enter Order Data Here");

try
{

    Order order = AcceptOrderData();
    AddOrderToQueue(order);
}
catch (Exception ex)
{
    Console.WriteLine($"Error Occurred : {ex.Message}");
}


Console.ReadLine();

static Order AcceptOrderData()
{
    Order order = new Order();
    Console.WriteLine("Enter Order Id");
    order.OrderId = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Enter Order Name");
    order.OrderName = Console.ReadLine();
    Console.WriteLine("Enter Quantity");
    order.Quantity = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Enter Advance");
    order.Advance = Convert.ToInt32(Console.ReadLine());

    return order;
}


static void AddOrderToQueue(Order order)
{
    try
    {
        // Replace with your connection string
        string connectionString = "";
        string queueName = "orders";

        // Define an access policy
        QueueSignedIdentifier accessPolicy = new QueueSignedIdentifier
        {
            Id = "mypolicy",
            AccessPolicy = new QueueAccessPolicy
            {
                // Set the expiry time to 5 Minutes from now
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(5),
                // Set the start time to now
                StartsOn = DateTimeOffset.UtcNow,
                // Set the permissions to allow adding messages
                Permissions = "rau"
            }
        };

        // Create a QueueClient
        QueueClient queueClient = new QueueClient(connectionString, queueName);

        // Create the queue if it doesn't already exist
        queueClient.CreateIfNotExists();

        // Set the access policy on the queue
        queueClient.SetAccessPolicy(new QueueSignedIdentifier[] { accessPolicy });



        if (queueClient.Exists())
        {
            // Serialize the order object to JSON
            string orderJson = JsonSerializer.Serialize(order);

            // Add the message to the queue
            queueClient.SendMessage(orderJson);

            Console.WriteLine("Order added to the queue successfully.");
        }
        else
        {
            Console.WriteLine("Failed to create or connect to the queue.");
        }
    }
    catch (Exception ex)
    {
        throw ex;
    }
}
