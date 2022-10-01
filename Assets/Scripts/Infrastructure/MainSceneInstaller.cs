﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles;
using Assets.Scripts.Timing;
using Zenject;

namespace Assets.Scripts.Infrastructure
{
    internal class MainSceneInstaller : MonoInstaller
    {
        public override void InstallBindings() {
            Container.Bind<Timer>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ElementDrawer>().ToSelf().AsSingle();
            Container.Bind<IElementPrecisionProvider>().FromMethod(x => x.Container.Resolve<Config>());
            Container.Bind<Config>().FromComponentInHierarchy().AsSingle();
            Container.Bind<Scaler>().FromComponentInHierarchy().AsSingle();
            Container.Bind<VerticalController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<LandedElementsController>().FromComponentInHierarchy().AsSingle();
        }
    }
}
