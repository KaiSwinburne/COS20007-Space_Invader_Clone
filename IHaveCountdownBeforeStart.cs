﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom_project
{
    public interface IHaveCountdownBeforeStart
    {
        public SplashKitSDK.Timer CountdownTimer
        {
            get;
        }
    }
}