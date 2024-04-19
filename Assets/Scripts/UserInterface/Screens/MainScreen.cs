using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UserInterface.Screens
{
    public class MainScreen : Screen
    {
        [SerializeField]
        private Button _quit;

        [SerializeField]
        private Button _start;

        private IPersistentProgressService _persistentProgressService;
        private IGameStateMachine _stateMachine;

        public override ScreenID ID => ScreenID.Main;

        public override void Activate()
        {
            _start.onClick.AddListener(OnClickStart);
            _quit.onClick.AddListener(OnClickQuit);

            base.Activate();
        }

        public override void Deactivate()
        {
            _quit.onClick.RemoveListener(OnClickQuit);
            _start.onClick.RemoveListener(OnClickStart);

            base.Deactivate();
        }

        public override void Setup(ServiceLocator serviceLocator)
        {
            base.Setup(serviceLocator);

            _persistentProgressService = serviceLocator.GetService<IPersistentProgressService>();
            _stateMachine = serviceLocator.GetService<IGameStateMachine>();
        }

        private void OnClickQuit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void OnClickStart()
        {
            uint level = _persistentProgressService.CurrentGameData.CurrentLevel;

            _stateMachine.Enter<LoadLevelState, uint>(level);
        }
    }
}