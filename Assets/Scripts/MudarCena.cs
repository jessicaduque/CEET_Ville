using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MudarCena : MonoBehaviour
{
	public void Iniciar()
	{

		SceneManager.LoadScene("SampleScene");
	}

	public void Sair()
	{
		Application.Quit();
	}
}
