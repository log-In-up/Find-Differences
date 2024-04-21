using UnityEngine;

namespace Assets.Scripts.Logic.VFX
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ParticleSystem))]
    public class FindingEffect : MonoBehaviour
    {
        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        private void Start()
        {
            Destroy(gameObject, _particleSystem.main.duration);
        }
    }
}