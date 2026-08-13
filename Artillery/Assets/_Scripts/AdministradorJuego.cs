using UnityEngine;

public class AdministradorJuego : MonoBehaviour
{
    public static AdministradorJuego SingletonAdministradorJuego;

    [SerializeField] private int velocidadBala = 30;
    [SerializeField] private int disparosPorJuego = 10;
    [SerializeField] private float velocidadRotacion = 1;
    [SerializeField] private int disparosIniciales = 10;
    [SerializeField] private int disparosPorJuegos = 10;

    public GameObject CanvasGanar;
    public GameObject CanvasPerder;

    public int VelocidadBala
    {
        get { return velocidadBala; }
        set { velocidadBala = value; }

    }

    public int DisparosPorJuego
    {
        get { return disparosPorJuego; }
        set { disparosPorJuego = value; }
    }

    public float VelocidadRotacion
    {
        get { return velocidadRotacion; }
        set { velocidadRotacion = value; }
    }
    private void Awake()
    {
        if (SingletonAdministradorJuego == null)
        {
            SingletonAdministradorJuego = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }


    private void Update()
    {
        if (DisparosPorJuego <= 0)
        {
            PerderJuego();
        }
    }

    public void GanarJuego()
    {
        CanvasGanar.SetActive(true);
    }

    public void PerderJuego()
    {
        CanvasPerder.SetActive(true);
    }

    public void ReiniciarDisparos()
    {
        disparosPorJuego = disparosIniciales;
    }

}
