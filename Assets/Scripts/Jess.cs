using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jess : MonoBehaviour
{
    // ANDAR
    public List<Sprite> andarCima;
    public List<Sprite> andarBaixo;
    public List<Sprite> andarDireita;
    public List<Sprite> andarEsquerda;
    public int indiceAndar = 0;
    public int contAndar = 0;
    public string direcao = "baixo";

    // ATACAR
    public bool atacou = false;
    public int indiceAtaque = 0;
    public int contAtaque = 0;
    public List<Sprite> atacarCima;
    public List<Sprite> atacarBaixo;
    public List<Sprite> atacarDireita;
    public List<Sprite> atacarEsquerda;

    // INIMIGOS
    public int indiceInimigos = 0;
    public List<GameObject> inimigos;
    public GameObject telaVitoria;
    public int mortos = 0;
    public int indiceMortos = 0;

    // BOSSES
    public GameObject boss1;
    public GameObject boss2;

    // SPRITES
    public SpriteRenderer mostradorDeImagem;

    // Start is called before the first frame update
    void Start()
    {
        mostradorDeImagem = GetComponent<SpriteRenderer>();
    }

    void Ataque()
    {
        if (atacou)
        {
            if (direcao == "baixo")
            {
                AnimacaoAtaque(atacarBaixo, andarBaixo);
            }
            if (direcao == "cima")
            {
                AnimacaoAtaque(atacarCima, andarCima);
            }
            if (direcao == "direita")
            {
                AnimacaoAtaque(atacarDireita, andarDireita);
            }
            if (direcao == "esquerda")
            {
                AnimacaoAtaque(atacarEsquerda, andarEsquerda);
            }
            CalculoInimigo();
        }
    }

    void CalculoInimigo()
    {
        if (indiceInimigos < inimigos.Count)
        {
            if (inimigos[indiceInimigos] != null)
            {
                Vector3 minhaPos = transform.position;
                Vector3 inimigoPos = inimigos[indiceInimigos].transform.position;
                float distancia = Vector3.Distance(minhaPos, inimigoPos);
                //Debug.Log(distancia);
                if (distancia < 1.8f)
                {
                    Destroy(inimigos[indiceInimigos]);
                }
            }

            indiceInimigos++;
        }
    }

    void AnimacaoAndar(List<Sprite> l)
    {
        int elem = l.Count;
        contAndar++;
        mostradorDeImagem.sprite = l[indiceAndar];
        if (contAndar > 30)
        {
            indiceAndar++;
            contAndar = 0;
            if (indiceAndar > elem - 1)
            {
                indiceAndar = 0;
            }
        }
    }

    void AnimacaoAtaque(List<Sprite> lAtk, List<Sprite> lAnd)
    {
        mostradorDeImagem.sprite = lAtk[indiceAtaque];
        contAtaque++;
        if (contAtaque > 20)
        {
            indiceAtaque++;
            contAtaque = 0;
        }
        if (indiceAtaque >= lAtk.Count)
        {
            atacou = false;
            contAtaque = 0;
            indiceAtaque = 0;
            AnimacaoAndar(lAnd);
        }
    }

    void Movimento()
    {
        // ANDAR ESQUERDA
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(transform.position.x - 0.01f, transform.position.y, transform.position.z);
            direcao = "esquerda";
            AnimacaoAndar(andarEsquerda);
        }

        // ANDAR CIMA
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
            direcao = "cima";
            AnimacaoAndar(andarCima);

        }

        // ANDAR BAIXO
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
            direcao = "baixo";
            AnimacaoAndar(andarBaixo);

        }

        // ANDAR DIREITA
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x + 0.01f, transform.position.y, transform.position.z);
            direcao = "direita";
            AnimacaoAndar(andarDireita);
        }
        else
        {
            contAndar = 0;
        }
    }

    void Mortos()
    {
        if (indiceMortos < inimigos.Count)
        {
            if(inimigos[indiceMortos] == null)
            {
                mortos++;
            }
            if(mortos == inimigos.Count)
            {
                Boss();
            }
            indiceMortos++;
        }
        else
        {
            indiceMortos = 0;
            mortos = 0;
        }
    }

    void Vitoria()
    {
        telaVitoria.SetActive(true);
        mostradorDeImagem.sprite = andarBaixo[0];
        this.GetComponent<Jess>().enabled = false;
    }

    void Boss()
    {
        boss1.SetActive(true);
        boss2.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        if (atacou)
        {
            Ataque();
        }
        else
        {
            Movimento();
            if (Input.GetMouseButtonDown(0))
            {
                atacou = true;
                indiceInimigos = 0;
            }
        }
        Mortos();
    }
}
