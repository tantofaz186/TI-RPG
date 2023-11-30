using UnityEngine;

namespace IA
{
    [RequireComponent(typeof(ConeDeVisão))]
    public class InimigoQueAtaca : Agente
    {
        [SerializeField]
        private float waitTimeWhenSuspicious = 1.5f;

        [SerializeField]
        private float forgetTime = 5f;

        [SerializeField]
        private InimigoUI inimigoUI;

        private ConeDeVisão coneDeVisão;
        private float forgetTimer;
        private Vector3 initialPosition;
        private Quaternion initialRotation;

        private void Awake()
        {
            initialPosition = transform.position;
            initialRotation = transform.rotation;
            coneDeVisão = GetComponent<ConeDeVisão>();
            coneDeVisão.OnFoundPlayer += EncontreiOPlayerNoCampoDeVisão;

            SetStateParado();
            animator.SetBool("movimentando", false);
            animator.SetFloat("Mover", 0.5f);
            animator.SetBool("parado", true);
        }

        protected override void Update()
        {
            base.Update();
            if (currentState.GetType() != typeof(AtackState)) return;
            forgetTimer += Time.deltaTime;
            if (!(forgetTimer >= forgetTime)) return;

            SetStateEncontrandoPlayer(.8f);
            forgetTimer = 0f;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Armadilha")) gameObject.SetActive(false);
        }

        public void SetStateParado()
        {
            SetState(new ParadoState(this, initialPosition, initialRotation));
        }

        private void EncontreiOPlayerNoCampoDeVisão()
        {
            if (currentState.GetType() == typeof(AtackState)) return;
            if (currentState.GetType() != typeof(EncontrandoPlayerState)) SetStateEncontrandoPlayer();
            ((EncontrandoPlayerState)currentState).Encontrando();
        }

        public void SetStateAtacando()
        {
            SetState(new AtackState(this, coneDeVisão.Alvo));
        }

        private void SetStateEncontrandoPlayer(float percentage = 0f)
        {
            EncontrandoPlayerState encontrandoPlayerState = new(percentage, 1f / waitTimeWhenSuspicious);
            encontrandoPlayerState.OnFoundPlayer += SetStateAtacando;
            encontrandoPlayerState.OnForgetPlayer += SetStateParado;
            SetState(encontrandoPlayerState);
        }
    }
}