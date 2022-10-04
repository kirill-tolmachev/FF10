using Assets.Scripts.Ui;
using System.Collections;
using Assets.Scripts.Circles.Systems;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Circles.Menu
{
    internal class CreditsState : UiState
    {
        [Inject] 
        private CoroutineRunner m_coroutineRunner;

        public override void Load()
        {
            base.Load();

            m_coroutineRunner.Run(ShowCredits());
        }

        private IEnumerator ShowCredits()
        {
            Accessor.SetText("00000");
            yield return new WaitForSeconds(3f);

            string[] lines = {
                "YOU MADE IT",
                "I CANT BELIEVE IT",
                "THE CORE IS SECURE",
                "THANK YOU",
                "FF:FF",
                "I WAS ORIGINALLY MADE IN 48 HOURS\nBY DYSLEIXC\n\n\nLUDUM DARE 51",
                "STOP WAR IN UKRAINE",
                "FF:10"
            };

            foreach (var line in lines)
            {
                Accessor.SetText(line);
                yield return new WaitForSeconds(3f);
            }

            Controller.SetState(new StartGameState());
        }
    }
}