using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public GameObject explosion;
    public float umbralBajo = -5f;
    public bool esAvion;

    private void Update()
    {
        if (transform.position.y < umbralBajo)
        {
            GameObject juego = GameObject.Find("GestionJuego");

            if (esAvion)
            {
                juego.GetComponent<Juego>().DecrementarPuntuacion();
            }
            else
            {
                juego.GetComponent<Juego>().IncrementarPuntuacion();
            }

            GameObject instantiateExplosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(instantiateExplosion, 1f);

            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Bala")
        {
            GameObject instantiateExplosion = Instantiate(explosion, collision.transform.position, collision.transform.rotation);
            Destroy(gameObject);
            Destroy(instantiateExplosion, 1f);

            GameObject juego = GameObject.Find("GestionJuego");

            if (esAvion)
            {
                juego.GetComponent<Juego>().IncrementarPuntuacion();
            }
            else
            {
                juego.GetComponent<Juego>().DecrementarPuntuacion();
            }
        }
    }
}