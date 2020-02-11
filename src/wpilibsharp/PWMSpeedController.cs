﻿using System;
using System.Collections.Generic;
using System.Text;
using WPILib.SmartDashboard;

namespace WPILib
{
    public abstract class PWMSpeedController : PWM, ISpeedController, ISendable
    {
        protected PWMSpeedController(int channel) : base(channel)
        {

        }

        public override string Description => $"PWM {Channel.ToString()}";

        public bool Inverted { get; set; }

        public void Set(double speed)
        {
            Speed = Inverted ? -speed : speed;
            Feed();
        }

        public double Get()
        {
            return Speed;
        }

        public void Disable()
        {
            SetDisabled();
        }

        void ISendable.InitSendable(ISendableBuilder builder)
        {
            builder.SmartDashboardType = "Speed Controller";
            builder.IsActuator = true;
            builder.SafeState = SetDisabled;
            builder.AddDoubleProperty("Value", Get, Set);
        }
    }
}
