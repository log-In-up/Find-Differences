using Assets.Scripts.UserInterface;

namespace Assets.Scripts.Infrastructure.Services.UserInterface
{
    public interface IGameUI : IService
    {
        void InitializeScreens(ServiceLocator serviceLocator);

        void OpenScreen(ScreenID screenID);

        /// <summary>
        /// Opens the user interface screen - <see cref="screenID"/>.
        /// </summary>
        /// <typeparam name="TPayload">Payload type.</typeparam>
        /// <param name="screenID">ID of the screen to open.</param>
        /// <param name="payload">Payload at open screen.</param>
        //void OpenScreen<TPayload>(ScreenID screenID, TPayload payload) where TPayload : class;
    }
}