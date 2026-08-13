using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{

    public GameObject MenuOpciones;
    public GameObject MenuInicial;


    public void IniciarJuego()
    {
        Debug.Log("BOTON DE INICIAR DETECTADO");
        SceneManager.LoadScene(1);
    }

    public void FinalizarJuego()
    {
        Application.Quit();
    }

  
    public void MostrarMenuInicial()
    {
        MenuOpciones.SetActive(false);
        MenuInicial.SetActive(true);
    }

}
