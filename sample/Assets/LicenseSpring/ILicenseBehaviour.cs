﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseSpring.Unity
{
    public interface ILicenseBehaviour
    {
        bool AllowableFeature { get; }
    }
}
