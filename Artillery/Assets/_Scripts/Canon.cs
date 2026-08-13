using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Canon : MonoBehaviour
{
    public static bool Bloqueado;

    public AudioClip clipDisparo;
    private GameObject SonidoDisparo;
    private AudioSource SourceDisparo; 

    [SerializeField] private GameObject BalaPrefab;
    public GameObject ParticulasDisparo;
    private GameObject PuntaCanon;
    private float rotacion;

    public CanonControls canonControls;
    private InputAction apuntar;
    private InputAction modificarfuerza;
    private InputAction disparar;

    [SerializeField] private Slider sliderFuerza;
    [SerializeField] private float velocidadCambioFuerza = 25f;


    private void Awake()
    {
        canonControls = new CanonControls();
    }

    private void OnEnable()
    {
        apuntar = canonControls.Canon.Apuntar;
        modificarfuerza = canonControls.Canon.ModificarFuerza;
        disparar = canonControls.Canon.Disparar;
        apuntar.Enable();
        modificarfuerza.Enable();
        disparar.Enable();
        disparar.performed += Disparar;
    }

    private void OnDisable()
    {
        disparar.performed -= Disparar;

        apuntar.Disable();
        modificarfuerza.Disable();
        disparar.Disable();
    }
    private void Start()
    {
        PuntaCanon = transform.Find("Cañon/PuntaCanon").gameObject;
        SonidoDisparo = GameObject.Find("SonidoDisparo");
        SourceDisparo = SonidoDisparo.GetOrAddComponent<AudioSource>();
    }

    
    void Update()
    {
        rotacion += apuntar.ReadValue<float>() * AdministradorJuego.SingletonAdministradorJuego.VelocidadRotacion;
        if (rotacion <= 90 && rotacion >= 0)
        {
            transform.eulerAngles = new Vector3(rotacion, 90, 0.0f);
        }

        if (rotacion > 90) rotacion = 90;
        if (rotacion < 0) rotacion = 0;

        float cambioFuerza = modificarfuerza.ReadValue<float>();
        sliderFuerza.value += cambioFuerza * velocidadCambioFuerza * Time.deltaTime;



        //Disparar();

    }

    private void Disparar(InputAction.CallbackContext context)
    {
        if (AdministradorJuego.SingletonAdministradorJuego.DisparosPorJuego <= 0)
        {
            return;
        }
        if (Bloqueado)
        {
            return;
        }
        
        AdministradorJuego.SingletonAdministradorJuego.DisparosPorJuego--;

        GameObject temp = Instantiate(BalaPrefab, PuntaCanon.transform.position, transform.rotation);
        Rigidbody tempRB = temp.GetComponent<Rigidbody>();
        SeguirCamara.objetivo = temp;

        Vector3 direccionDisparo = transform.rotation.eulerAngles;
        direccionDisparo.y = 90 - direccionDisparo.x;
        Vector3 direcciónparticulas = new Vector3(-90 + direccionDisparo.x, 90, 0);
        GameObject Particulas = Instantiate
            (ParticulasDisparo, PuntaCanon.transform.position, Quaternion.Euler(direcciónparticulas), transform);
        //tempRB.linearVelocity = direccionDisparo.normalized * AdministradorJuego.SingletonAdministradorJuego.VelocidadBala;
        float fuerzaDisparo = AdministradorJuego.SingletonAdministradorJuego.VelocidadBala * (sliderFuerza.value / 100f);
        tempRB.linearVelocity = direccionDisparo.normalized * fuerzaDisparo;
        //SourceDisparo.PlayOneShot(clipDisparo);
        SourceDisparo.Play();
        Debug.Log($"Disparos restantes: {AdministradorJuego.SingletonAdministradorJuego.DisparosPorJuego}");
        Bloqueado = true;
    }
}
