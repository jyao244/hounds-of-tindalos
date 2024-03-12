using System.Net;
using System.Net.Sockets;
using System.Text;
using H002.SDK;

// use class Socket and TCP

namespace H002;

internal class Program
{
    // byte array buffer
    private static readonly byte[] dataBuffer = new byte[1024];

    public static void Main(string[] args)
    {
        StartServerAsync();
        Console.ReadKey();
    }

    // async function and must be static 
    private static void StartServerAsync()
    {
        // address type;socket type:Dgram(UDP) TCP Stream; protocol type: TCP
        var serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        var ipAddress = IPAddress.Parse("127.0.0.1"); // 127.0.0.1 local machine

        // IpAddress xx.xx.xx.xx IpEedPoint IP address + port number
        var ipEndPoint = new IPEndPoint(ipAddress, 8080);

        // bind the endpoint
        serverSocket.Bind(ipEndPoint);

        // start listening and 0 means no limitation of the connection
        serverSocket.Listen(0);

        // accept the client connection and invoke the callback function
        serverSocket.BeginAccept(AcceptCallBack, serverSocket);
    }

    // callback function, when accept a connection from client side, start to listen it
    private static void AcceptCallBack(IAsyncResult ar)
    {
        var serverSocket = ar.AsyncState as Socket;
        var clientSocket = serverSocket.EndAccept(ar);
        // continuous receive data from the client side
        clientSocket.BeginReceive(dataBuffer, 0, 1024, SocketFlags.None, ReceiveCallBack, clientSocket);

        // when we finish one client, start the next one
        serverSocket.BeginAccept(AcceptCallBack, serverSocket);
    }

    // the service receive a msg, it will invoke this callback
    private static void ReceiveCallBack(IAsyncResult ar)
    {
        // using try/catch to close connection when error happened
        Socket clientSocket = null;
        try
        {
            clientSocket = ar.AsyncState as Socket;
            // count the size of data
            var count = clientSocket.EndReceive(ar);
            // if no receive any data, the connection will close
            if (count == 0)
            {
                clientSocket.Close();
                return;
            }

            var msg = Encoding.UTF8.GetString(dataBuffer, 0, count);
            Console.WriteLine("The data from the client side: " + msg);
            var watch = new Watch(msg);
            // send a confirm msg to the client
            var CmdCodeArray = watch.getConfirmMessages();
            if (CmdCodeArray != null && CmdCodeArray.Count > 0)
            {
                Console.WriteLine("Start to send the confirm messages");
                for (var i = 0; i < CmdCodeArray.Count; i++) clientSocket.Send(CmdCodeArray[i]);
            }

            // continuous receive data
            clientSocket.BeginReceive(dataBuffer, 0, 1024, SocketFlags.None, ReceiveCallBack, clientSocket);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            // close connection
            if (clientSocket != null) clientSocket.Close();
        }
    }
}