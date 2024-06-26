using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.UserInterface;
using Assets.Scripts.UserInterface.DialogueScreens;
using System;
using System.Collections.Generic;
using UnityEngine;
using Screen = Assets.Scripts.UserInterface.Screens.Screen;

namespace Assets.Scripts.UserInterface
{
    [DisallowMultipleComponent]
    public class GameUI : MonoBehaviour, IGameUI, IGameDialogUI
    {
        [SerializeField]
        private RectTransform _dialogBackground;

        [SerializeField]
        private List<DialogueWindow> _dialogueWindows;

        [SerializeField]
        private List<Screen> _screens;

        public void CloseDialogWindows()
        {
            foreach (DialogueWindow dialogueWindow in _dialogueWindows)
            {
                if (dialogueWindow.IsOpen)
                {
                    dialogueWindow.Deactivate();
                }
            }
            _dialogBackground.gameObject.SetActive(false);
        }

        public void InitializeScreens(ServiceLocator serviceLocator)
        {
            foreach (Screen screen in _screens)
            {
                screen.SetScreenData(this);

                screen.Setup(serviceLocator);
            }
        }

        public void InitializeWindows(ServiceLocator serviceLocator)
        {
            _dialogBackground.gameObject.SetActive(false);

            foreach (DialogueWindow dialogueWindow in _dialogueWindows)
            {
                dialogueWindow.Setup(serviceLocator);
            }
        }

        public void OpenDialogWindow(DialogWindowID dialogWindowID)
        {
            int index = _dialogueWindows.FindIndex(window => window.ID == dialogWindowID);

            _dialogBackground.gameObject.SetActive(true);
            _dialogueWindows[index].Activate();
        }

        public void OpenScreen(ScreenID screenID)
        {
            foreach (Screen screen in _screens)
            {
                if (screen.ID.Equals(screenID))
                {
                    screen.Activate();
                }
                else if (screen.IsOpen)
                {
                    screen.Deactivate();
                }
            }
        }
    }
}