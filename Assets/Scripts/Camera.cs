using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Camera : MonoBehaviour
{

    IEnumerator RecordFrame()
    {
        yield return new WaitForEndOfFrame();
        var texture = ScreenCapture.CaptureScreenshotAsTexture();
        // do something with texture



        string b64Data = System.Convert.ToBase64String(texture.EncodeToPNG()).ToString();

        string path = "Assets/text.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(b64Data);
        writer.Close();



        // cleanup
        Object.Destroy(texture);
    }

    public void LateUpdate()
    {   
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(RecordFrame());
        }
        
    }
}
