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
    public Collider abastecedor;
    public robotData rdata;
    public string estado;

    // Start is called before the first frame update
    void Start()
    {
        rdata = new robotData();
        rdata.tipo = "robotData";
        rdata.accion = "";
    }

    // Update is called once per frame
    void Update()
    {
        estado = rdata.accion;

        switch(rdata.accion)
        {
            case "no_abastecido":
                {
                    irA(abastecedor);
                    break;
                }
            case "irA":
                {
                    irA(listaObjetivos[0]);
                    break;
                }
            case "irB":
                {
                    irA(listaObjetivos[1]);
                    break;
                }
            case "irC":
                {
                    irA(listaObjetivos[2]);
                    break;
                }

        }
    }

    private void irA(Collider coll)
    {
        var vec = coll.bounds.center;

        //Debug.Log(Vector3.Distance(col.bounds.center, vec));

        navMeshAgent.SetDestination(vec);
    }

    public void tomarDecision(string data)
    {
        if(!string.IsNullOrEmpty(data))
        {
            var objData = JsonConvert.DeserializeObject<decisionData>(data);
            textCaras.text = "Caras : " + objData.cantidad_caras;

            rdata.accion = objData.accion;

        }
    }


}
