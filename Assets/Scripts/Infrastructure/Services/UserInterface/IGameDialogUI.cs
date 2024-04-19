using Assets.Scripts.UserInterface;
using System;

namespace Assets.Scripts.Infrastructure.Services.UserInterface
{
    public interface IGameDialogUI : IService
    {
        void CloseDialogWindows();

        void InitializeWindows(ServiceLocator serviceLocator);

        void OpenDialogWindow(DialogWindowID dialogWindowID);

        /// <summary>
        /// Opens dialog window - <see cref="dialogWindowID"/>.
        /// </summary>
        /// <typeparam name="TPayload">Payload type.</typeparam>
        /// <param name="dialogWindowID">The ID of the dialog box to open.</param>
        /// <param name="payload">Payload at open window.</param>
        //void OpenDialogWindow<TPayload>(DialogWindowID dialogWindowID, TPayload payload) where TPayload : class;

        //void AddWindowActions(DialogWindowID dialogWindowID, Action onCancel, Action onApply);
    }
}