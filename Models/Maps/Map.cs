using CounterStrike.Models.Maps.Contracts;
using CounterStrike.Models.Players;
using CounterStrike.Models.Players.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CounterStrike.Models.Maps
{
    public class Map : IMap
    {
        public string Start(ICollection<IPlayer> players)
        {
            var terrorists = players.Where(p => p.GetType().Name == nameof(Terrorist));
            var counterTerrorists = players.Where(p => p.GetType().Name == nameof(CounterTerrorist));

            while (terrorists.Any(t => t.IsAlive) && counterTerrorists.Any(c => c.IsAlive))
            {
                foreach (var terorist in terrorists)
                {
                    foreach (var contraterrorist in counterTerrorists)
                    {
                        contraterrorist.TakeDamage(terorist.Gun.Fire());
                    }
                }

                foreach (var contraterrorist in counterTerrorists)
                {
                    foreach (var terorist in terrorists)
                    {
                        terorist.TakeDamage(contraterrorist.Gun.Fire());
                    }
                }
            }
            return counterTerrorists.Any(c => c.IsAlive) ? "Counter Terrorist wins!" : "Terrorist wins!";
        }
    }
}
