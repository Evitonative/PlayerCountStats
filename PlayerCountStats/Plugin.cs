using System;
using System.IO;
using System.Linq;
using Exiled.API.Features;
using Exiled.Events.EventArgs;

namespace PlayerCountStats
{
    public class Plugin : Plugin<Config>
    {
        private static Config config = new Config();
        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.Joined += Join;
            Exiled.Events.Handlers.Player.Destroying += Destroy;
            Exiled.Events.Handlers.Server.WaitingForPlayers += Waiting;
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Joined -= Join;
            Exiled.Events.Handlers.Player.Destroying -= Destroy;
            Exiled.Events.Handlers.Server.WaitingForPlayers -= Waiting;
        }

        private void Waiting()
        {
            TryCreateFile();
        }

        private void Destroy(DestroyingEventArgs ev)
        {
            AppendedCountLine(Exiled.API.Features.Player.List.Count() - 1);
        }

        private void Join(JoinedEventArgs ev)
        {
            AppendedCountLine(Exiled.API.Features.Player.List.Count() + 1);
        }

        private static void AppendedCountLine(int count)
        {
            string line =
                $"\"{DateTime.Now.ToString(System.Globalization.CultureInfo.CurrentCulture)}\";\"{count}\"";
            File.AppendAllLines(config.fileName, new []{ line });
        }

        private static void TryCreateFile()
        {
            if (!File.Exists(config.fileName))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(config.fileName) ?? string.Empty);
                File.AppendAllLines(config.fileName, new[] { "Time;Count" });
                Log.Info("File was missing. Creating: " + config.fileName);
            }
        }
    }
}
