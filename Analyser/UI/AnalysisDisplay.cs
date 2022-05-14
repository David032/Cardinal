using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Analyser.UI
{
    public class AnalysisDisplay : MonoBehaviour
    {
        public GameObject DisplayWindow;

        [Header("Display Elements")]
        public HexadTypeDisplay Philanthropist;
        public HexadTypeDisplay Socialiser;
        public HexadTypeDisplay FreeSpirit;
        public HexadTypeDisplay Achiever;
        public HexadTypeDisplay Disruptor;
        public HexadTypeDisplay Player;

        Analyser Analyser;

        private void Start()
        {
            Analyser = Analyser.Instance;
        }

        private void Update()
        {
            //Why unity no support dicts in editor :(
            Philanthropist.TypeValue.text = Analyser.PhilanthropistValue.ToString();
            Philanthropist.TypeSlider.value = Analyser.PhilanthropistValue;
            Socialiser.TypeValue.text = Analyser.SocialiserValue.ToString();
            Socialiser.TypeSlider.value = Analyser.SocialiserValue;
            FreeSpirit.TypeValue.text = Analyser.FreeSpiritValue.ToString();
            FreeSpirit.TypeSlider.value = Analyser.FreeSpiritValue;
            Achiever.TypeValue.text = Analyser.AchieverValue.ToString();
            Achiever.TypeSlider.value = Analyser.AchieverValue;
            Disruptor.TypeValue.text = Analyser.DisruptorValue.ToString();
            Disruptor.TypeSlider.value = Analyser.DisruptorValue;
            Player.TypeValue.text = Analyser.PlayerValue.ToString();
            Player.TypeSlider.value = Analyser.PlayerValue;
        }

        public void ToggleDisplayWindow() 
        {
            DisplayWindow.SetActive(!DisplayWindow.activeSelf);
        }
    }
}

