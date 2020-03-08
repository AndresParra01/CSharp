using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public int fuerzaSalto, velocidadMovimiento;
    private bool tocandoPiso, colided;

    // Start is called before the first frame update
    public Jugador()
    {
        fuerzaSalto = 0;
        velocidadMovimiento = 0;
        tocandoPiso = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)  && tocandoPiso)
        {
            tocandoPiso = false;
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, fuerzaSalto));
        }

        this.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadMovimiento,
                                                                this.GetComponent<Rigidbody2D>().velocity.y);
        colided = false;
    }

    public int FuerzaSalto
    {
        get { return fuerzaSalto; }
    }


    public int VelocidadMovimiento
    {
        get { return velocidadMovimiento; }
    }

    public bool TocandoPiso
    {
        get { return tocandoPiso; }
    }

    private void OnTriggerEnter2D(Collider2D c1)
    {
        tocandoPiso = true;
        if(c1.gameObject.tag.Equals("Obstaculo"))
        {
            GameObject.Destroy(this.gameObject);
        }
        if (c1.gameObject.tag.Equals("Objetivo"))
        {
            GameObject.Destroy(c1.gameObject);
            if (!colided)
            {
                Escena.IncrementarPuntos();
                colided = true;

            }


        }
        if (c1.gameObject.tag.Equals("Trampolin"))
        {
            tocandoPiso = false;
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, fuerzaSalto*2));
        }
    }

}
