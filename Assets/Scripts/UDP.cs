using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UDP : MonoBehaviour
{
    private UdpClient client;

    private string ipServer = "127.0.0.1";
    private int portServer = 5005;

    private string ipClient = "127.0.0.1";
    private int portClient = 5006;

    public IPEndPoint epServer;

    void Start()
    {
        IPEndPoint epClient = new IPEndPoint(IPAddress.Parse(ipClient), portClient);
        client = new UdpClient(epClient);

        client.Client.Blocking = false;
        client.Client.ReceiveTimeout = 1000;

        epServer = new IPEndPoint(IPAddress.Parse(ipServer), portServer); // endpoint where server is listening
        client.Connect(epServer);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            send("hola");
        }


        try
        {
            var receivedData = client.Receive(ref epServer);


            Debug.Log("receive data from " + epServer.ToString());

        }
        catch (Exception ex)
        {
            Debug.LogError("error : "+ex.Message);
        }
    }

    public void send(string text)
    {
        byte[] send_buffer = Encoding.ASCII.GetBytes(text);

        Debug.LogWarning(send_buffer.Length);

        client.Send(send_buffer, send_buffer.Length);
    }



    private void OnDestroy()
    {
        client.Close();
    }
}
