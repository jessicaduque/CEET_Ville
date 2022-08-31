using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InimigoTk : MonoBehaviour
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
    public bool pausa = false;
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
    GameObject heroiDay;
    GameObject heroiJess;
    GameObject heroiPerto;
    float distanciaD;
    float distanciaJ;
    float distanciaMenor;

    // INTELIGÊNCIA
    public int posMax = 14;
    public int posMin = 3;

    // VIDA
    public Text vidaTextoDay;
    public Text vidaTextoJess;

    void Start()
    {
        heroiDay = GameObject.FindGameObjectWithTag("PlayerD");
        heroiJess = GameObject.FindGameObjectWithTag("PlayerJ");
        direcao = "direita";
        mostradorDeImagem = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        MaisProxima();
        if (distanciaMenor < 1.4f && atacar == false)
        {
            if (pausa)
            {
                contPausa++;
                if (contPausa > 340)
                {
                    pausa = false;
                    contPausa = 0;
                }
            }
            else if (pausa == false)
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
            Inteligencia();
        }
        HeroiVivo();
    }

    void MaisProxima()
    {
        distanciaD = Vector3.Distance(transform.position, heroiDay.transform.position);
        distanciaJ = Vector3.Distance(transform.position, heroiJess.transform.position);

        if (distanciaD < distanciaJ)
        {
            distanciaMenor = distanciaD;
            heroiPerto = heroiDay;
        }
        else
        {
            distanciaMenor = distanciaJ;
            heroiPerto = heroiJess;
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

        // ANDAR DIREITA
        else if (direcao == "direita")
        {
            transform.position = new Vector3(transform.position.x + 0.007f, transform.position.y, transform.position.z);
            AnimacaoAndar(andarDireita);
        }
    }
    void Inteligencia()
    {
        if (transform.position.x > posMax)
        {
            direcao = "esquerda";
        }
        if (transform.position.x < posMin)
        {
            direcao = "direita";
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
        if (contPausa < 65)
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
        else if (contPausa == 66)
        {
            pausa = true;
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
            pausa = true;
            contAtaque = 0;
            indiceAtaque = 0;
            AnimacaoAndar(lAnd);
            if (heroiPerto == heroiDay)
            {
                vidaTextoDay.text = (int.Parse(vidaTextoDay.text) - 1).ToString();
            }
            else
            {
                vidaTextoJess.text = (int.Parse(vidaTextoJess.text) - 1).ToString();
            }

        }
    }
    void HeroiVivo()
    {
        if (vidaTextoDay.text == "0" || vidaTextoJess.text == "0")
        {
            if (vidaTextoDay.text == "0")
            {
                heroiJess.GetComponent<Jess>().enabled = false;
                heroiDay.SetActive(false);
            }
            else
            {
                heroiDay.GetComponent<Day>().enabled = false;
                heroiJess.SetActive(false);
            }

            telaDerrota.SetActive(true);


            GameObject[] inimigosVivosSk = GameObject.FindGameObjectsWithTag("InimigoSk");

            foreach (GameObject i in inimigosVivosSk)
            {
                i.GetComponent<InimigoSk>().enabled = false;
            }

            GameObject[] inimigosVivosTk = GameObject.FindGameObjectsWithTag("InimigoTk");

            foreach (GameObject i in inimigosVivosTk)
            {
                i.GetComponent<InimigoTk>().enabled = false;
            }
        }
    }

}
