using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles.Systems;
using Assets.Scripts.Ui;
using UniMediator;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Circles
{
    internal class Tutorial : GameSystem
    {
        [Inject]
        private GameUiController m_uiController;

        [Inject] 
        private RestartCounter m_restartCounter;

        public override void Handle(GameStarted message) {
            base.Handle(message);

            if (m_restartCounter.Count < 3)
                StartCoroutine(ShowTutorial());
            else {
                StartCoroutine(ShowRandomText());
            }
        }

        private IEnumerator ShowTutorial() {
            string[] lines = {
                "USE\nA/D",
                "USE\nSHIFT"
            };

            m_uiController.Lock();
            m_uiController.SetText("SAVE ME");
            yield return new WaitForSeconds(2f);

            foreach (var line in lines) {
                m_uiController.SetText(line);
                yield return new WaitForSeconds(8f / lines.Length);
            }

            m_uiController.Unlock();
        }

        private IEnumerator ShowRandomText() {
            var variants = new[] {
                RandomOneLiner(), 
                TryHarder(), 
                JustDoIt(), 
                Hexagon(),
                Highscore()
            };
            return variants[Random.Range(0, variants.Length)];
        }

        private IEnumerator RandomOneLiner() {
            string[] lines = {
                "PRACTICE\nMAKES\nPERFECT", 
                "GOOD\nLUCK",
                "SYSTEM33",
                "DEFEND",
                "RRRRR",
                "HALP",
                "HAHA\nHAHA",
                "AGAIN",
                "SUPER\nFF10",
                "IS IT\nBEATABLE",
                "WHY",
                "DEAD\nBEEF",
                "WHY\nHEX",
                "AA:AA",
                "A=10",
                "10:10"
            };

            m_uiController.Lock();
            var line = lines[Random.Range(0, lines.Length)];
            m_uiController.SetText(line);
            yield return new WaitForSeconds(4);
            m_uiController.Unlock();
        }

        private IEnumerator TryHarder() {
            m_uiController.Lock();
            m_uiController.SetText("HARD MODE: ON");
            yield return new WaitForSeconds(4);
            m_uiController.SetText("JUST KIDDING");
            yield return new WaitForSeconds(2);
            m_uiController.Unlock();
        }

        private IEnumerator JustDoIt() {
            m_uiController.Lock();
            m_uiController.SetText("JUST");
            yield return new WaitForSeconds(1);
            m_uiController.SetText("DO");
            yield return new WaitForSeconds(1);
            m_uiController.SetText("IT");
            yield return new WaitForSeconds(1);
            m_uiController.Unlock();
        }

        private IEnumerator Hexagon() {
            m_uiController.Lock();
            m_uiController.SetText("YES");
            yield return new WaitForSeconds(1);
            m_uiController.SetText("ITS LIKE");
            yield return new WaitForSeconds(1);
            m_uiController.SetText("SUPERR");
            yield return new WaitForSeconds(1);
            m_uiController.SetText("HEXXX...");
            yield return new WaitForSeconds(1);
            m_uiController.SetText("NEVERMIND");
            yield return new WaitForSeconds(1);
            m_uiController.Unlock();
        }

        private IEnumerator Highscore()
        {
            m_uiController.Lock();
            m_uiController.SetText("WORLD\nRECORD");
            yield return new WaitForSeconds(1);
            m_uiController.SetText("");
            yield return new WaitForSeconds(1);
            m_uiController.SetText("WORLD\nRECORD");
            yield return new WaitForSeconds(1);
            m_uiController.SetText("");
            yield return new WaitForSeconds(1);
            m_uiController.SetText("WORLD\nRECORD");
            yield return new WaitForSeconds(1);
            m_uiController.SetText("JUST\nKIDDING");
            yield return new WaitForSeconds(2);
            m_uiController.Unlock();
        }
    }
}
