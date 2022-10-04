using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinemachine;
using Zenject;

namespace Assets.Scripts.Circles
{
    internal class CameraManager
    {
        public CinemachineVirtualCamera MenuCamera { get; }
        public CinemachineVirtualCamera GameCamera { get; }

        public CameraManager(
            [Inject(Id = "Menu")] CinemachineVirtualCamera menuCamera,
            [Inject(Id = "Game")] CinemachineVirtualCamera gameCamera)
        {
            MenuCamera = menuCamera;
            GameCamera = gameCamera;
        }

        public void SwitchToMenuCamera() {
            MenuCamera.Priority = 50;
            GameCamera.Priority = 0;
        }

        public void SwitchToGameCamera() {
            GameCamera.Priority = 50;
            MenuCamera.Priority = 0;
        }
    }
}
