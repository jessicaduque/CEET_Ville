using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoSk : MonoBehaviour
{
    // ANDAR
    public List<Sprite> andarCima;
    public List<Sprite> andarBaixo;
    public List<Sprite> andarDireita;
    public List<Sprite> andarEsquerda;
    public int indiceAndar = 0;
    public int contAndar = 0;

    // ATAQUE
    public int contAtaque = 0;
    public bool atacar = false;
    public int indiceAtaque = 0;
    public List<Sprite> atacarCima;
    public List<Sprite> atacarBaixo;
    public List<Sprite> atacarDireita;
    public List<Sprite> atacarEsquerda;
    public int contPausa = 0;
    public GameObject telaDerrota;

    // SPRITE
    public SpriteRenderer mostradorDeImagem;
    public string direcao;

    // PERSONAGEM
    GameObject heroi;
    public int vida = 3;

    // INTELIGÊNCIA
    public int contDirecao = 0;
    

    void Start()
    {

        heroi = GameObject.FindGameObjectWithTag("Player");
        direcao = "direita";
        mostradorDeImagem = GetComponent<SpriteRenderer>();
        
    }

    private void Update()
    {
        float distancia = Vector3.Distance(transform.position, heroi.transform.position);
        if (distancia < 5 && atacar == false)
        {
            Perseguir();
            if(distancia < 2)
            {
                atacar = true;
            }
        }
        else if (atacar)
        {
            Atacar();
        }
        else
        {
            Movimento();
            InteligenciaRandomizado();
        }
        Vivo();
    }
    float SubPositivo(float n1, float n2)
    {
        if(n1 - n2 >= 0)
        {
            return n1 - n2;
        }
        else
        {
            return n2 - n1;
        }
    }
    void Perseguir()
    {
        float xH = heroi.transform.position.x;
        float yH = heroi.transform.position.y;
        float xI = transform.position.x;
        float yI = transform.position.y;

        if (xH > xI && (xH - xI) >= SubPositivo(yH, yI))
        {
            transform.position = new Vector3(transform.position.x + 0.007f, transform.position.y, transform.position.z);
            direcao = "direita";
            AnimacaoAndar(andarDireita);
        }
        else if (yH > yI && (yH - yI) > SubPositivo(xH, xI))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.007f, transform.position.z);
            direcao = "cima";
            AnimacaoAndar(andarCima);
        }
        else if (xH < xI && (xI - xH) >= SubPositivo(yH, yI))
        {
            transform.position = new Vector3(transform.position.x - 0.007f, transform.position.y, transform.position.z);
            direcao = "esquerda";
            AnimacaoAndar(andarEsquerda);
        }
        else if (yH < yI && (yI - yH) > SubPositivo(xH, xI))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.007f, transform.position.z);
            direcao = "baixo";
            AnimacaoAndar(andarBaixo);
        }
    }
    void Movimento()
    {
        // ANDAR ESQUERDA
        if (direcao == "esquerda")
        {
            transform.position = new Vector3(transform.position.x - 0.007f, transform.position.y, transform.position.z);
            AnimacaoAndar(andarEsquerda);
        }

        // ANDAR CIMA
        if (direcao == "cima")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.007f, transform.position.z);
            AnimacaoAndar(andarCima);
        }

        // ANDAR BAIXO
        if (direcao == "baixo")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.007f, transform.position.z);
            AnimacaoAndar(andarBaixo);
        }

        // ANDAR DIREITA
        if (direcao == "direita")
        {
            transform.position = new Vector3(transform.position.x + 0.007f, transform.position.y, transform.position.z);
            AnimacaoAndar(andarDireita);
        }
    }
    void InteligenciaRandomizado()
    {
        contDirecao++;
        if(contDirecao > 100)
        {
            contDirecao = 0;
            int numeroDestino = Random.Range(0, 4);
            if(numeroDestino == 0)
            {
                direcao = "esquerda";
            }
            if (numeroDestino == 1)
            {
                direcao = "direita";
            }
            if (numeroDestino == 2)
            {
                direcao = "cima";
            }
            if (numeroDestino == 3)
            {
                direcao = "baixo";
            }
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
    void Atacar()
    {
        contPausa++;
        if(contPausa < 60)
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
        }
        else if(contPausa > 120)
        {
            contPausa = 0;
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
            atacar = false;
            contAtaque = 0;
            indiceAtaque = 0;
            AnimacaoAndar(lAnd);
            vida -= 1;
        }
    }
    void Vivo()
    {
        if(vida == 0)
        {
            heroi.GetComponent<Day>().enabled = false;
            telaDerrota.SetActive(true);
        }
    }

}
