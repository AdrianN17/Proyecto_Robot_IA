using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class TCP : MonoBehaviour
{
    private TcpClient client;

    private string ipServer = "127.0.0.1";
    private int portServer = 5005;

    private string ipClient = "127.0.0.1";
    private int portClient = 5006;

    private Thread clientReceiveThread;

    private IPEndPoint epServer;

    public Decisiones decision;
    void Start()
    {
        IPEndPoint epClient = new IPEndPoint(IPAddress.Parse(ipClient), portClient);
        client = new TcpClient(epClient);


        epServer = new IPEndPoint(IPAddress.Parse(ipServer), portServer);
        client.Connect(epServer);

        clientReceiveThread = new Thread(new ThreadStart(ListenData));
        clientReceiveThread.IsBackground = true;
        clientReceiveThread.Start();


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void send(string text)
    {
       
        ASCIIEncoding asen = new ASCIIEncoding();
        byte[] ba = asen.GetBytes(text);
        client.GetStream().Write(ba, 0, ba.Length);
        client.GetStream().Flush();
    }

    private void ListenData()
    {
        try
        {

            while (true)
            {
                var bytes = new byte[1024];

                int length;
                // Read incomming stream into byte arrary. 					
                while ((length = client.GetStream().Read(bytes, 0, bytes.Length)) != 0)
                {
                    var incommingData = new byte[length];
                    Array.Copy(bytes, 0, incommingData, 0, length);
                    // Convert byte array to string message. 						
                    string serverMessage = Encoding.ASCII.GetString(incommingData);
                    Debug.Log("server message received as: " + serverMessage);
                    UnityMainThreadDispatcher.Instance().Enqueue(() => decision.tomarDecision(serverMessage));
                    
                }
                
            }
        }
        catch(Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    private void OnDestroy()
    {
        clientReceiveThread.Abort();
        client.Close();
    }
}
