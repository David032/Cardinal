using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Runic.Managers;

namespace Cardinal.AI.Debug
{
    public class DEBUGTimescale : MonoBehaviour
    {
        public Slider timeSlider;
        public TextMeshProUGUI timeRateText;
        public TimeManager timeController;
        public TextMeshProUGUI rawTimeText;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            timeController.timeRate = Mathf.RoundToInt(timeSlider.value);
            timeRateText.text = timeController.timeRate.ToString();
            rawTimeText.text = timeController.timer.ToString();
        }
    }
}

