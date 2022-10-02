using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles.Messages;
using Assets.Scripts.Ui;
using UniMediator;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

namespace Assets.Scripts.Circles.Systems
{
    internal class GameWonController : GameSystem, IMulticastMessageHandler<GameWon>
    {
        [Inject] 
        private StartButtonController m_startButtonController;

        [Inject] private RestartCounter m_restartCounter;

        public void Handle(GameWon message) {
            StartCoroutine(OnGameWon());
        }

        private IEnumerator OnGameWon() {
            Mediator.Publish(new GamePaused());
            Mediator.Publish(new CameraReset());

            Mediator.Publish(new ReturnToStartScreen(true));

            m_startButtonController.SetText("00000");
            yield return new WaitForSeconds(3f);

            string[] lines = {
                "YOU MADE IT",
                "I CANT BELIEVE IT",
                "THE CORE IS SECURE",
                "THANK YOU",
                "FF:FF",
                "I WAS MADE IN 48 HOURS FOR LUDUM DARE 51\nBY DYSLEIXC",
                "STOP WAR IN UKRAINE",
                "FF:10"
            };

            foreach (var line in lines) {
                m_startButtonController.SetText(line);
                yield return new WaitForSeconds(3f);
            }

            m_startButtonController.Unlock();
            m_restartCounter.Count = 2;
        }
    }
}
