using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoTk : MonoBehaviour
{

    public List<Sprite> andarCima;
    public List<Sprite> andarBaixo;
    public List<Sprite> andarDireita;
    public List<Sprite> andarEsquerda;
    public SpriteRenderer mostradorDeImagem;
    public int indice = 0;
    public int cont = 0;
    public int posMax = 5;
    public int posMin = -5;
    public string destino;

    // Start is called before the first frame update
    void Start()
    {
        destino = "Direita";
        mostradorDeImagem = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimento();
        Inteligencia();
    }

    void Inteligencia()
    {
        if (transform.position.x > posMax)
        {
            destino = "Esquerda";
        }
        if (transform.position.x < posMin)
        {
            destino = "Direita";
        }
    }

    void Movimento()
    {
        // ANDAR ESQUERDA
        if (destino == "Esquerda")
        {
            transform.position = new Vector3(transform.position.x - 0.01f, transform.position.y, transform.position.z);
            Animacao(andarEsquerda);
        }

        // ANDAR CIMA
        if (destino == "Cima")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
            Animacao(andarCima);
        }

        // ANDAR BAIXO
        if (destino == "Baixo")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
            Animacao(andarBaixo);
        }

        // ANDAR DIREITA
        if (destino == "Direita")
        {
            transform.position = new Vector3(transform.position.x + 0.01f, transform.position.y, transform.position.z);
            Animacao(andarDireita);
        }
    }

    void Animacao(List<Sprite> l)
    {
        int elem = l.Count;
        cont++;
        mostradorDeImagem.sprite = l[indice];
        if (cont > 30)
        {
            indice++;
            cont = 0;
            if (indice > elem - 1)
            {
                indice = 0;
            }
        }
    }
}
