using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketCalculator.Client;

public class Client
{
    private Socket _socket;
    private string HOST;
    private int PORT;

    public Client(string host, int port)
    {
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        HOST = host;
        PORT = port;
    }

    public string CalcOperation(string operation)
    {
        SendData(operation);
        string data = ReceiveData();
        return ProcessReceivedData(data);
    }

    public void Connect()
    {
        _socket.Connect(HOST, PORT);
    }

    public void SendData(string operation)
    {
        byte[] msg = Encoding.ASCII.GetBytes(operation + "\n");
        _socket.Send(msg);
    }

    public string ProcessReceivedData(string stringReceived)
    {
        string opResult = stringReceived.Split(':')[1];
        return opResult.Replace("\n", "");
    }

    public string ReceiveData()
    {
        byte[] buffer = new byte[1000000];
        int bytesReceived = _socket.Receive(buffer);
        return Encoding.ASCII.GetString(buffer, 0, bytesReceived);
    }
}