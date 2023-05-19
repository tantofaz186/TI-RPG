using UnityEngine;

namespace Skills
{
    public abstract class Skill : MonoBehaviour
    {
        public string nomeSkill;
        [TextArea(1, 3)]
        public string skillDescricao;
        public Skill[] skillAnterior;
        public abstract void Update();
        public abstract void OnEnable();
        public abstract void OnDisable();
    }
}