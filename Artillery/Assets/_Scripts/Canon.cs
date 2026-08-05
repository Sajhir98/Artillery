using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private GameObject BalaPrefab;
    private GameObject PuntaCanon;
    private float rotacion;


    private void Start()
    {
        PuntaCanon = transform.Find("Cañon/PuntaCanon").gameObject;
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

        if (Input.GetKeyDown(KeyCode.Space) && AdministradorJuego.SingletonAdministradorJuego.DisparosPorJuego > 0)
        {
            AdministradorJuego.SingletonAdministradorJuego.DisparosPorJuego--;

            GameObject temp = Instantiate(BalaPrefab, PuntaCanon.transform.position, transform.rotation);
            Rigidbody tempRB = temp.GetComponent<Rigidbody>();

            Vector3 direccionDisparo = transform.rotation.eulerAngles;
            direccionDisparo.y = 90 - direccionDisparo.x;

            tempRB.linearVelocity = direccionDisparo.normalized * AdministradorJuego.SingletonAdministradorJuego.VelocidadBala;

            Debug.Log($"Disparos restantes: {AdministradorJuego.SingletonAdministradorJuego.DisparosPorJuego}");
        }
    }
}
