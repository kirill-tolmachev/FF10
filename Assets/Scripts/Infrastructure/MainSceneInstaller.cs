using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles;
using Assets.Scripts.Circles.Menu;
using Assets.Scripts.Circles.Systems;
using Assets.Scripts.Timing;
using Assets.Scripts.Ui;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure
{
    internal class MainSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ElementDrawer>().ToSelf().AsSingle();
            Container.Bind<IElementPrecisionProvider>().FromMethod(x => x.Container.Resolve<Config>());
            Container.Bind<CameraManager>().ToSelf().AsSingle();
            Container.Bind<MessageRedirector>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CoroutineRunner>().FromComponentInHierarchy().AsSingle();

            InstallGameSystem<Timer>();
            InstallGameSystem<Config>();
            InstallGameSystem<Scaler>();
            InstallGameSystem<VerticalController>();
            InstallGameSystem<LandedElementsController>();
            InstallGameSystem<StartButtonController>();
            InstallGameSystem<GameUiController>();
            InstallGameSystem<RestartCounter>();
            InstallGameSystem<UiController>();

            Container.Bind<UiController.UiAccessor>()
                .FromMethod(x => x.Container.Resolve<UiController>().Override())
                .WhenInjectedInto<UiState>();

            InstallUiState<StartGameState>();
            InstallUiState<InGameState>();
            InstallUiState<CreditsState>();
            InstallUiState<RestartState>();

            InstallVirtualCamera("Menu");
            InstallVirtualCamera("Game");
        }

        private void InstallGameSystem<TSystem>() => Container.Bind<TSystem>().FromComponentInHierarchy().AsSingle();

        private void InstallUiState<TState>() where TState : UiState => Container.Bind<TState>().ToSelf().AsTransient();

        private void InstallVirtualCamera(string resolveTag) =>
            Container.Bind<CinemachineVirtualCamera>()
                .WithId(resolveTag)
                .ToSelf()
                .FromComponentsInHierarchy(c => c.CompareTag(resolveTag))
                .AsSingle();
    }
    
}
