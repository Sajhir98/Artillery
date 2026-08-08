using Unity.VisualScripting;
using UnityEngine;

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


    private void Start()
    {
        PuntaCanon = transform.Find("Cañon/PuntaCanon").gameObject;
        SonidoDisparo = GameObject.Find("SonidoDisparo");
        SourceDisparo = SonidoDisparo.GetOrAddComponent<AudioSource>();
    }

    
    void Update()
    {
        rotacion += Input.GetAxis("Horizontal") * AdministradorJuego.SingletonAdministradorJuego.VelocidadRotacion;
        if (rotacion <= 90 && rotacion >= 0)
        {
            transform.eulerAngles = new Vector3(rotacion, 90, 0.0f);
        }

        if (rotacion > 90) rotacion = 90;
        if (rotacion < 0) rotacion = 0;

        if (Input.GetKeyDown(KeyCode.Space) && AdministradorJuego.SingletonAdministradorJuego.DisparosPorJuego > 0 && !Bloqueado)
        {
            AdministradorJuego.SingletonAdministradorJuego.DisparosPorJuego--;

            GameObject temp = Instantiate(BalaPrefab, PuntaCanon.transform.position, transform.rotation);
            Rigidbody tempRB = temp.GetComponent<Rigidbody>();
            SeguirCamara.objetivo = temp;

            Vector3 direccionDisparo = transform.rotation.eulerAngles;
            direccionDisparo.y = 90 - direccionDisparo.x;
            Vector3 direcciónparticulas = new Vector3(-90 + direccionDisparo.x, 90, 0);
            GameObject Particulas = Instantiate
                (ParticulasDisparo, PuntaCanon.transform.position, Quaternion.Euler(direcciónparticulas),transform);
            tempRB.linearVelocity = direccionDisparo.normalized * AdministradorJuego.SingletonAdministradorJuego.VelocidadBala;
            //SourceDisparo.PlayOneShot(clipDisparo);
            SourceDisparo.Play();
            Debug.Log($"Disparos restantes: {AdministradorJuego.SingletonAdministradorJuego.DisparosPorJuego}");
            Bloqueado = true;
        }
    }
}
