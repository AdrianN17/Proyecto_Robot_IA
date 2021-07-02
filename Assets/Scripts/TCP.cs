using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    public IPEndPoint epServer;
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

        /*try
        { 
            byte[] bArray = new byte[client.ReceiveBufferSize];
            int bytesRead = client.GetStream().Read(bArray, 0, client.ReceiveBufferSize);
            Debug.Log("Received : " + Encoding.ASCII.GetString(bArray, 0, bytesRead));
        }
        catch(Exception ex)
        {
            Debug.LogError(ex.Message);
        }*/

        /*if (Input.GetKeyDown(KeyCode.A))
        {
            send("hola");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            send("adios");
        }*/
    }

    public void send(string text)
    {
       
        ASCIIEncoding asen = new ASCIIEncoding();
        byte[] ba = asen.GetBytes(text);
        client.GetStream().Write(ba, 0, ba.Length);
    }

    private void ListenData()
    {
        try
        {
            Byte[] bytes = new Byte[1024];
            while (true)
            {			
                
                int length;
                // Read incomming stream into byte arrary. 					
                while ((length = client.GetStream().Read(bytes, 0, bytes.Length)) != 0)
                {
                    var incommingData = new byte[length];
                    Array.Copy(bytes, 0, incommingData, 0, length);
                    // Convert byte array to string message. 						
                    string serverMessage = Encoding.ASCII.GetString(incommingData);
                    Debug.Log("server message received as: " + serverMessage);
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
