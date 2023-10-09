﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyagerEngine.Core
{
    internal interface IRenderSystem : ISystem
    {
        void Render(in EntityRegistry registry);
    }
}