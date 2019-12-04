using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public GameObject personaje;
    Transform tranform;

    void Start()
    {
        tranform = GetComponent<Transform>();
    }

    void Update()
    {
        var t = personaje.GetComponent<Transform>();
        var y = t.position.y;
        var x = t.position.x;
        personaje.GetComponent<Transform>();
        tranform.position = new Vector3(x, y, tranform.position.z);
    }
}
