using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Juego : MonoBehaviour
{
    public GameObject[] enemigos;
    public float generarEnemigo = 100f;
    private float elapsedTime = 0f;
    public GameObject bala;
    public GameObject arCamera;
    public TextMeshProUGUI puntuacionText;
    public TextMeshProUGUI maxPuntuacionText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI recordText;
    public Button salirButton;
    public Button dispararButton;
    public GameObject corazonPrefab;
    public Transform corazonesContainer;
    public GameObject mira;
    private int puntuacion = 0;
    private int vidas = 3;
    private int maxVidas = 5;
    private int maxPuntuacion = 0;
    private bool nuevoRecord = false;
    private float baseGravedad = -0.2f;
    private float incrementoGravedad = -0.01f;
    private float tiempoMinimoGeneracion;

    void Start()
    {
        puntuacion = 0;
        maxPuntuacion = PlayerPrefs.GetInt("MaxPuntuacion", 0);
        puntuacionText.text = "Puntuacion: " + puntuacion;
        maxPuntuacionText.text = "Max Puntuacion: " + maxPuntuacion;
        gameOverText.gameObject.SetActive(false);
        recordText.gameObject.SetActive(false);
        Physics.gravity = new Vector3(0, baseGravedad, 0);
        ActualizarCorazones();
        tiempoMinimoGeneracion = generarEnemigo; // Almacena el valor inicial
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
        if (puntuacion > maxPuntuacion)
        {
            maxPuntuacion = puntuacion;
            maxPuntuacionText.text = "Max Puntuacion: " + maxPuntuacion;
            PlayerPrefs.SetInt("MaxPuntuacion", maxPuntuacion);
            nuevoRecord = true;
        }
        Physics.gravity = new Vector3(0, baseGravedad + (puntuacion * incrementoGravedad), 0);
        generarEnemigo = Mathf.Max(tiempoMinimoGeneracion, tiempoMinimoGeneracion / (1 + puntuacion * 0.1f));
    }

    internal void PerderVida()
    {
        if (vidas > 0)
        {
            vidas--;
            ActualizarCorazones();
        }

        if (vidas <= 0)
        {
            GameOver();
        }
    }

    public void CurarVida()
    {
        if (vidas < maxVidas)
        {
            vidas++;
            ActualizarCorazones();
        }
    }

    private void ActualizarCorazones()
    {
        foreach (Transform child in corazonesContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < vidas; i++)
        {
            GameObject corazon = Instantiate(corazonPrefab, corazonesContainer);
            RectTransform rectTransform = corazon.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(i * 50, 0);
            rectTransform.localScale = new Vector3(2, 2, 2);
        }
    }

    private void GameOver()
    {
        puntuacionText.gameObject.SetActive(false);
        maxPuntuacionText.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        mira.SetActive(false);
        if (nuevoRecord)
        {
            recordText.gameObject.SetActive(true);
        }
        salirButton.gameObject.SetActive(false);
        dispararButton.gameObject.SetActive(false);
        StartCoroutine(EsperarYMostrarMenu());
    }

    private IEnumerator EsperarYMostrarMenu()
    {
        yield return new WaitForSeconds(5);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}