using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausa;
    


    public void MostrarMenuPausa()
    {
        menuPausa.SetActive(true);
    }

    public void ocultarMenuPausa()
    {
        menuPausa.SetActive(false);
    }

    public void RegresarAPantallaPrincipal()
    {
        SceneManager.LoadScene(0);
    }

    
}
