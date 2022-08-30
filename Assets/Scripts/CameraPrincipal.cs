using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPrincipal : MonoBehaviour
{
    public GameObject PontoDay;
    public GameObject PontoJess;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Day = PontoDay.transform.position;
        Vector3 Jess = PontoJess.transform.position;
        float x = Vector3.Distance(Day, Jess) / 2;
        Vector3 P = LerpByDistance(Day, Jess, x);

        transform.position = new Vector3(P.x, P.y, -10);
        GetComponent<Camera>().orthographicSize = x + 2;
    }

    public Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
    {
        Vector3 P = x * Vector3.Normalize(B - A) + A;
        return P;
    }

}
