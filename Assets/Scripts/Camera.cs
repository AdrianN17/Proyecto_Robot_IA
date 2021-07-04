using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Camera : MonoBehaviour
{
    public Canvas canvas;
    private float timer;
    public float maxTimer;
    public bool val;
    public TCP tcp;
    public Decisiones decision;


    void Start()
    {
        timer = 0;
    }

    IEnumerator RecordFrame()
    {
        canvas.enabled = false;
        yield return new WaitForEndOfFrame();
        var texture = ScreenCapture.CaptureScreenshotAsTexture();
        // do something with texture
        canvas.enabled = true;


        string b64Data = System.Convert.ToBase64String(texture.EncodeToPNG()).ToString();

        //string path = "Assets/text.txt";

        //Write some text to the test.txt file
        /*StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(b64Data);
        writer.Close();*/


        decision.rdata.data = b64Data;

        var json = JsonConvert.SerializeObject(decision.rdata);
        tcp.send(json);
        decision.rdata.data = "";

        Object.Destroy(texture);
        val = true;

        Debug.Log("prueba");
    }

    public void LateUpdate()
    {
        if(val)
            timer= timer + Time.deltaTime;

        if(timer> maxTimer && val)
        {
            timer = 0;
            val = false;
            StartCoroutine(RecordFrame());
        }
        
    }

}
