using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    public float avgFrameRate;
    public Text texto;

    public void Update()
    {
        avgFrameRate = Time.frameCount / Time.time;
        texto.text = "FPS : " + avgFrameRate;
    }
}
