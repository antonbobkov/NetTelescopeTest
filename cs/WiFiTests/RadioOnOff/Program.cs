using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioOnOff
{
    class Program
    {
        static void Main(string[] args)
        {
            var access = await WiFiAdapter.RequestAccessAsync();
            if (access == WiFiAccessStatus.Allowed)
            {
                var radios = await Radio.GetRadiosAsync();
                foreach (var radio in radios)
                {
                    if (radio.Kind == RadioKind.WiFi)
                    {
                        await radio.SetStateAsync(RadioState.Off);
                    }
                }
            }
        }
    }
}
