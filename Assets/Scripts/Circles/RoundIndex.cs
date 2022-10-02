using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Circles.Messages;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Circles
{
    internal class RoundIndex : MonoBehaviour, IRemovable
    {
        [SerializeField] 
        private TMP_Text m_indexText;

        public void SetRound(int round) => m_indexText.text = round.ToString(CultureInfo.InvariantCulture);
    }
}
