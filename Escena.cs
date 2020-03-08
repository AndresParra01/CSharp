using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Escena : MonoBehaviour
{
    public GameObject Jugador;
    public Camera camaraJuego;
    public GameObject[] bloquesPrefab;
    public Text textoJuego;
    public Rigidbody rB;
    public float punteroJuego;
    public float lugarGeneracion;
    public bool perder;
    public static int puntos = 0;
    public int MaximoDePuntos;
    // Start is called before the first frame update
    public Escena()
    {
        punteroJuego = -7;
        lugarGeneracion = 12;
        perder = false;
        puntos = 0;
        //textoJuego = gameObject.AddComponent<Text>();
        if(textoJuego != null)
        textoJuego.text = "Puntaje: 0" + puntos;
    }

    public void Awake()
    {
        rB = GetComponent<Rigidbody>();
        //puntos = 0;
        textoJuego.text = "Puntaje: 0" + puntos;
    }

    // Update is called once per frame
    void Update()
    {
        if (puntos >= MaximoDePuntos)
        {
            GameObject.Destroy(Jugador.gameObject);
            textoJuego.text = "Felicitaciones Hasunt¡ Has logrado salvar a kank de esos malhechores, lo hiciste :)";
        }
        else if (Jugador != null && puntos < 100)
        {
            camaraJuego.transform.position = new Vector3(Jugador.transform.position.x,
                                                     camaraJuego.transform.position.y,
                                                     camaraJuego.transform.position.z);

            
            textoJuego.text = "Puntaje " + puntos;

        }
        else
        {


            if (perder == false) 
            { 
                perder = true;
                textoJuego.text = "Se termino el juego! \nPresione R para reintentarlo";
            }
            if (perder)
            {
                if (Input.GetKeyDown("r"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }

        while(Jugador != null && punteroJuego < Jugador.transform.position.x + lugarGeneracion)
        {
            int indiceBloque = Random.Range(0, bloquesPrefab.Length - 1);
            if(punteroJuego < 0)
            {
                indiceBloque = 6;
            }

            GameObject objetoBloque = Instantiate(bloquesPrefab[indiceBloque]);
            objetoBloque.transform.SetParent(this.transform);
            Bloque miBloque = objetoBloque.GetComponent<Bloque>();
            objetoBloque.transform.position = new Vector2(punteroJuego + miBloque.tamaño / 2, 0);
            punteroJuego += miBloque.tamaño;
        }
    }

    public float PunteroJuego
    {
        get { return punteroJuego; }
    }

    public float LugarGeneracion
    {
        get { return lugarGeneracion; }
    }

    public bool Perder
    {
        get { return perder; }
    }

    public static void IncrementarPuntos()
    {

          puntos++;
    }
}
