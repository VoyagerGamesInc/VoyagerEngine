using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyagerEngine.Data
{
    public class AudioSourceData : IAudioData
    {
        uint IAudioData.Source { get; set; }
        uint IAudioData.Buffer { get; set; }
    }
}
