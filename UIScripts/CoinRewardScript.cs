using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI_scripts
{
    public class CoinRewardScript : MonoBehaviour
    {
        public int minValue;
        public int maxValue;
        public Text Text;

        public void Increase()
        {
            int value = Int32.Parse(Text.text);
            value = Math.Min(value + 1, maxValue);
            Text.text = value.ToString();
        }

        public void Decrease()
        {
            int value = Int32.Parse(Text.text);
            value = Math.Max(value - 1, minValue);
            Text.text = value.ToString();
        }
    }
}