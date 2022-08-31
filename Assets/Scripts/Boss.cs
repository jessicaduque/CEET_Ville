using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
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
    public int contDirecao = 0;

    // VIDA
    public Text vidaTextoDay;
    public Text vidaTextoJess;

    // Start is called before the first frame update
    void Start()
    {
        heroiDay = GameObject.FindGameObjectWithTag("PlayerD");
        heroiJess = GameObject.FindGameObjectWithTag("PlayerJ");
        direcao = "direita";
        mostradorDeImagem = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MaisProxima();
        if (distanciaMenor < 8 && atacar == false)
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
            else if (distanciaMenor < 2 && pausa == false)
            {
                atacar = true;
            }
            else
            {
                Perseguir();
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
    float SubPositivo(float n1, float n2)
    {
        if (n1 - n2 >= 0)
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
        float xH = heroiPerto.transform.position.x;
        float yH = heroiPerto.transform.position.y;
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
        else if (direcao == "cima")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.007f, transform.position.z);
            AnimacaoAndar(andarCima);
        }

        // ANDAR BAIXO
        else if (direcao == "baixo")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.007f, transform.position.z);
            AnimacaoAndar(andarBaixo);
        }

        // ANDAR DIREITA
        else if (direcao == "direita")
        {
            transform.position = new Vector3(transform.position.x + 0.007f, transform.position.y, transform.position.z);
            AnimacaoAndar(andarDireita);
        }
    }
    void InteligenciaRandomizado()
    {
        contDirecao++;
        if (contDirecao > 100)
        {
            contDirecao = 0;
            int numeroDestino = Random.Range(0, 4);
            if (numeroDestino == 0)
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

            GameObject[] bossesVivos = GameObject.FindGameObjectsWithTag("Boss");

            foreach (GameObject i in bossesVivos)
            {
                i.GetComponent<Boss>().enabled = false;
            }
        }
    }
}
