using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFinNivel : MonoBehaviour
{
    public void SiguienteNivel()
    {
        var siguienteNivel = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings > siguienteNivel)
        {
            AdministradorJuego.SingletonAdministradorJuego.ReiniciarDisparos();
            SceneManager.LoadScene(siguienteNivel);
        }
        else
        {
            CargarMenuPrincipal();
        }
    }

    public void CargarMenuPrincipal()
    {
        SceneManager.LoadScene(0);
    }


    public void ReintentarNivel()
    {
        AdministradorJuego.SingletonAdministradorJuego.ReiniciarDisparos();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
