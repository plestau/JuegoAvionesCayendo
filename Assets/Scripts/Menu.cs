using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void Jugar()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Juego");
    }
}
