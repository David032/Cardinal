using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using Runic.UI;

namespace Cardinal.World
{
    public enum Cycle
    {
        Day,
        Night
    }
    public enum Passage
    {
        AM,
        PM
    }

    public class TimeManager : CardinalSingleton<TimeManager>
    {
        int days;
        [Range(0, 10)]
        int minutes;
        [Range(0, 60)]
        int seconds;

        public float timer;

        [Range(1, 10)]
        public int timeRate = 1;

        public Cycle timeCycle = Cycle.Day;
        public Passage timePassage = Passage.AM;

        public UnityEvent DayChange = new();
        public UnityEvent CycleChange = new();

        private void Start()
        {
        }

        void Update()
        {
            Time.timeScale = timeRate;
            timer += Time.deltaTime;

            minutes = Mathf.RoundToInt(timer) / 60;
            seconds = Mathf.RoundToInt(timer) - (minutes * 60);
            if (minutes >= 24)
            {
                days += 1;
                timer -= 600;
                DayChange.Invoke();
            }

            UpdateIndicators();
        }

        void UpdateIndicators()
        {
            if (minutes <= 6)
            {
                timeCycle = Cycle.Night;
                CycleChange.Invoke();
            }
            else if (minutes > 6 && minutes <= 18)
            {
                timeCycle = Cycle.Day;
                CycleChange.Invoke();
            }
            else if (minutes > 18)
            {
                timeCycle = Cycle.Night;
                CycleChange.Invoke();
            }
        }

        public float getRawTime()
        {
            float rawTime = timer + (days * 600);
            return rawTime;
        }

        public int GetMinutes() { return minutes; }
        public int GetSeconds() { return seconds; }
    }
}

