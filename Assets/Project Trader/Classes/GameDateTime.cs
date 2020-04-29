﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Timeline;

namespace ProjectTrader
{
    [Serializable]
    public struct GameDateTime
    {
        private const int TicksPerSecond = 1;
        private const int TicksPerMinute = TicksPerSecond * 60;
        private const int TicksPerHour = TicksPerMinute * 60;
        private const int TicksPerDay = TicksPerHour * 24;
        private const int TicksPerMonth = TicksPerDay * 30;
        private const int TicksPerYear = TicksPerDay * 12;

        /// <summary>
        /// 2,147,483,647초
        /// </summary>
        public const int MaxSeconds = int.MaxValue / TicksPerSecond;
        /// <summary>
        /// 35,791,394분
        /// </summary>
        public const int MaxMinutes = MaxSeconds / 60;
        /// <summary>
        /// 596,523시간
        /// </summary>
        public const int MaxHours = MaxMinutes / 60;
        /// <summary>
        /// 24,855일
        /// </summary>
        public const int MaxDays = MaxHours / 24;
        /// <summary>
        /// 828달
        /// </summary>
        public const int MaxMonth = MaxDays / 30;
        /// <summary>
        /// 69년
        /// </summary>
        public const int MaxYear = MaxMonth / 30;

        public int Ticks { get; set; }

        public int TotalSeconds
        {
            get => Ticks / TicksPerSecond;
            set => Ticks = value * TicksPerSecond;
        }

        public int TotalMinutes
        {
            get => Ticks / TicksPerMinute;
            set => Ticks = value * TicksPerMinute;
        }

        public int TotalHours
        {
            get => Ticks / TicksPerHour;
            set => Ticks = value * TicksPerHour;
        }

        public int TotalDays
        {
            get => Ticks / TicksPerDay;
            set => Ticks = value * TicksPerDay;
        }

        public int TotalMonths
        {
            get => Ticks / TicksPerMonth;
            set => Ticks = value * TicksPerMonth;
        }

        public int TotalYears
        {
            get => Ticks / TicksPerYear;
            set => Ticks = value * TicksPerYear;
        }

        public int Second
        {
            get => TotalSeconds % 60;
            set => AddSecond(value - Second);
        }

        public int Minute
        {
            get => TotalMinutes % 60;
            set => AddMinute(value - Minute);
        }

        public int Hour
        {
            get => TotalHours % 24;
            set => AddHour(value - Hour);
        }

        public int Day
        {
            get => TotalDays % 30;
            set => AddDay(value - Day);
        }

        public int Month
        {
            get => TotalMonths % 12;
            set => AddMonth(value - Month);
        }

        public int Year
        {
            get => TotalYears;
            set => TotalYears = value;
        }

        public void AddSecond(int value) => TotalSeconds += value;
        public void AddMinute(int value) => TotalMinutes += value;
        public void AddHour(int value) => TotalHours += value;
        public void AddDay(int value) => TotalDays += value;
        public void AddMonth(int value) => TotalMonths += value;
        public void AddYear(int value) => TotalYears += value;
    }
}
