using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaController : MonoBehaviour
{
    public GameObject target;
    public GameObject player;
    public GameObject llave1;
    public GameObject llave2;
    public GameObject llave3;
    public AudioClip[] clips;
    public AudioSource Sonido;
   
    public bool llav1, llav2, llav3;
    public int Cllaves;

    void Start()
    {
        Sonido = GetComponent<AudioSource>();
    }

    void Awake() //desaparecer puntos de Teletransportación
    {
        GetComponent<SpriteRenderer>().enabled = true;
        //GetChild = tranforma al hijo
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false; ;
    }

    void OnTriggerEnter2D(Collider2D col) // cambiar punto de teletransportacion
    {
        if (col.name == "Llave")
        {
            llav1 = true;
            Debug.Log("LLAVES : " +col.name);
        }

        if (col.name == "Llaves2")
        {
            llav2 = true;
            Debug.Log("LLAVES : " + col.name);
        }

        if (col.name == "Llaves3")
        {
            llav3 = true;
            Debug.Log("LLAVES : " + col.name);
        }

        if (llav1 == true && llav2 ==true && llav3 ==true)
        {
            player.transform.position = target.transform.GetChild(0).transform.position;
            Sonido.PlayOneShot(clips[0]);
            Destroy(this.llave1);
            Destroy(this.llave2);
            Destroy(this.llave3);

        }
    }
}
