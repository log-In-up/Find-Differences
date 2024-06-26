using Assets.Scripts.Infrastructure.Services;
using UnityEngine;

namespace Assets.Scripts.UserInterface.DialogueScreens
{
    [DisallowMultipleComponent]
    public abstract class DialogueWindow : MonoBehaviour
    {
        private bool _isOpened = false;
        public abstract DialogWindowID ID { get; }

        public bool IsOpen => _isOpened;

        public virtual void Activate()
        {
            _isOpened = true;
            gameObject.SetActive(_isOpened);
        }

        public virtual void Deactivate()
        {
            _isOpened = false;
            gameObject.SetActive(_isOpened);
        }

        public virtual void Setup(ServiceLocator serviceLocator)
        {
        }
    }
}