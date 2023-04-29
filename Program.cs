using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

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

class Client
{
    private Socket s;
    private string HOST;
    private int PORT;

    public Client(string host, int port)
    {
        this.s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        this.HOST = host;
        this.PORT = port;
    }

    public string CalcOperation(string operation)
    {
        this.SendData(operation);
        string data = this.ReceiveData();
        return this.ProcessReceivedData(data);
    }

    public void Connect()
    {
        this.s.Connect(this.HOST, this.PORT);
    }

    public void SendData(string operation)
    {
        byte[] msg = Encoding.ASCII.GetBytes(operation + "\n");
        this.s.Send(msg);
    }

    public string ProcessReceivedData(string stringReceived)
    {
        string opResult = stringReceived.Split(':')[1];
        return opResult.Replace("\n", "");
    }

    public string ReceiveData()
    {
        byte[] buffer = new byte[1000000];
        int bytesReceived = this.s.Receive(buffer);
        return Encoding.ASCII.GetString(buffer, 0, bytesReceived);
    }
}

