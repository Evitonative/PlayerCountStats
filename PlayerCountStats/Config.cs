using System.ComponentModel;
using Exiled.API.Interfaces;

namespace PlayerCountStats
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = false;
        [Description("Path: WIN: C:\\Users\\USER\\AppData\\Roaming\\EXILED\\playerCount.csv | Linux: /home/USER/EXILED/playerCount.csv")]
        public string fileName { get; set; } = "";
    }
}
