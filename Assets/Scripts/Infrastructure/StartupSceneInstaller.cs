using Assets.Scripts.Circles;
using Assets.Scripts.Timing;
using Zenject;

namespace Assets.Scripts.Infrastructure
{
    internal class StartupSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ElementDrawer>().ToSelf().AsSingle();
            Container.Bind<IElementPrecisionProvider>().To<MenuElementPrecisionProvider>().AsSingle();
        }
    }
}