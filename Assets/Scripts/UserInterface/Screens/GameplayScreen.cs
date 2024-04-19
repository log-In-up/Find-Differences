using Assets.Scripts.Infrastructure.Services;

namespace Assets.Scripts.UserInterface.Screens
{
    public class GameplayScreen : Screen
    {
        public override ScreenID ID => ScreenID.Gameplay;

        public override void Activate()
        {
            base.Activate();
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        public override void Setup(ServiceLocator serviceLocator)
        {
            base.Setup(serviceLocator);
        }
    }
}