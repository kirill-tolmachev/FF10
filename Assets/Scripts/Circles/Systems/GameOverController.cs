using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles.Messages;
using UniMediator;
using UnityEngine;

namespace Assets.Scripts.Circles.Systems
{
    internal class GameOverController : GameSystem, IMulticastMessageHandler<GameOver>
    {
        public void Handle(GameOver message) {
            Mediator.Publish(new GamePaused());
            StartCoroutine(ReturnToStartScreen());
        }

        private IEnumerator ReturnToStartScreen()
        {
            yield return new WaitForSeconds(2f);
            Mediator.Publish(new ReturnToStartScreen());
        }
    }
}
