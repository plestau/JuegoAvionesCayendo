using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Juego : MonoBehaviour
{
    public GameObject[] enemigos;
    public float generarEnemigo = 100f;
    private float elapsedTime = 0f;
    public GameObject bala;
    public GameObject arCamera;
    public TextMeshProUGUI puntuacionText;
    private int puntuacion = 0;

    void Start()
    {
        puntuacion = 0;
        puntuacionText.text = "Puntuacion: " + puntuacion;
        Physics.gravity = new Vector3(0, -0.2F, 0);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= generarEnemigo)
        {
            elapsedTime = 0f;
            GenerarEnemigoAleatorio();
        }
    }

    public void GenerarEnemigoAleatorio()
    {
        Vector3 posicionEnemigo = new Vector3(Random.Range(-2, 1), Random.Range(3, 4), Random.Range(1, 4));
        GameObject enemigoAGenerar;

        if (Random.value < 0.8f)
        {
            enemigoAGenerar = enemigos[0];
        }
        else
        {
            enemigoAGenerar = enemigos[1];
        }

        Instantiate(enemigoAGenerar, posicionEnemigo, Quaternion.identity);
    }

    public void DispararBala()
    {
        GameObject nuevaBala = Instantiate(bala, arCamera.transform.position, arCamera.transform.rotation) as GameObject;
        nuevaBala.GetComponent<Rigidbody>().AddForce(arCamera.transform.forward * 2000);
    }

    public void SalirJuego()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
    
    public void IncrementarPuntuacion()
    {
        puntuacion++;
        puntuacionText.text = "Puntuacion: " + puntuacion;
    }
    
    public void DecrementarPuntuacion()
    {
        puntuacion--;
        puntuacionText.text = "Puntuacion: " + puntuacion;
    }
}