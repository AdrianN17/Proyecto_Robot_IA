using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Decisiones : MonoBehaviour
{
    public Text textCaras;
    public List<Collider> listaObjetivos;
    public NavMeshAgent navMeshAgent;
    public Collider col;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var vec = listaObjetivos[0].bounds.center;

        navMeshAgent.SetDestination(vec);
    }

    public void tomarDecision(string data)
    {
        if(!string.IsNullOrEmpty(data))
        {
            var objData = JsonConvert.DeserializeObject<decisionData>(data);
            textCaras.text = "Caras : " + objData.cantidad_caras;

        }
    }
}
