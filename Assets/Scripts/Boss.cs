using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public List<GameObject> inimigosRandom;
    public List<GameObject> inimigosRonda;
    public int indiceMRandom = 0;
    public int mortosRandom = 0;
    public bool randomMortos = false;
    public int indiceMRonda = 0;
    public int mortosRonda = 0;
    public bool rondaMortos = false;

    public SpriteRenderer mostradorDeImagem;

    // Start is called before the first frame update
    void Start()
    {
        mostradorDeImagem = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
    }
}
