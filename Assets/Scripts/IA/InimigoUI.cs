using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IA
{
	public class InimigoUI : MonoBehaviour
	{
		[SerializeField] private Image exclamação;

		private void Awake()
		{
			GetComponent<Canvas>().worldCamera = Camera.main;
		}

		public void MostrarImagem(bool mostrar)
		{
			exclamação.gameObject.SetActive(mostrar);
		}
	}
}