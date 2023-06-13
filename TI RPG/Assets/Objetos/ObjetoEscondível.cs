using System.Collections;
using Player;
using UnityEngine;

namespace Objetos
{
    public class ObjetoEscondível : Interagível
    {
        private bool estaEscondido = false;
        protected override void Interagir() => Esconder();

        private void Esconder()
        {
            player.gameObject.SetActive(estaEscondido);
            estaEscondido = !estaEscondido;
        }


    }
}