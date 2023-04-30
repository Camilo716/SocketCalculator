using System;
using SocketCalculator.Client;

class Program
{
    static void Main(string[] args)
    {
        Client client = new Client("mob.bit4bit.in", 65519);
        client.Connect();

        Console.WriteLine(client.CalcOperation("234234 + 324"));
        Console.WriteLine(client.CalcOperation("342354 - 232"));
    }
}

