using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colision : MonoBehaviour
{
    public TCP tcp;
    public string objetivo_nombre;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ia"))
        {
            destinoData d = new destinoData();

            d.tipo = "objetivo";
            d.objetivo = objetivo_nombre;
            tcp.send(JsonConvert.SerializeObject(d));
        }
    }

 
}
