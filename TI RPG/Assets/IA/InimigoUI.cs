using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InimigoUI : MonoBehaviour
{
	[SerializeField] private Image exclamação;
	
	public void MostrarImagem(bool mostrar){
		exclamação.gameObject.SetActive(mostrar);
		}
}
