using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReseteoController : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource Sonido;

    void Start()
    {
        Sonido = GetComponent<AudioSource>();  
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("RPG");
        Sonido.PlayOneShot(clips[0]);
    }
}
