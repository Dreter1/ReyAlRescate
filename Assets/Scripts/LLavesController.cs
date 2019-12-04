using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LLavesController : MonoBehaviour
{
    private SpringJoint2D spring;
    public AudioClip[] clips;
    public AudioSource Sonido;

    void Start()
    {
        spring = GetComponent<SpringJoint2D>();
        spring.enabled = false;
        GameObject mochila = GameObject.FindWithTag("MochilaLlaves");
        Sonido = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            spring.enabled = true;
            Sonido.PlayOneShot(clips[0]);
        }
    }
}
