using CounterStrike.Core.Contracts;
using CounterStrike.Models.Guns;
using CounterStrike.Models.Guns.Contracts;
using CounterStrike.Models.Maps;
using CounterStrike.Models.Maps.Contracts;
using CounterStrike.Models.Players;
using CounterStrike.Models.Players.Contracts;
using CounterStrike.Repositories;
using CounterStrike.Utilities.Messages;
using System;
using System.Linq;
using System.Text;

namespace CounterStrike.Core
{
    public class Controller : IController
    {
        private GunRepository guns;
        private PlayerRepository players;
        private IMap map;
        public Controller()
        {
            guns = new GunRepository();
            players = new PlayerRepository();
            map = new Map();

        }
        public string AddGun(string type, string name, int bulletsCount)
        {
            IGun gun;
            if (type == "Rifle")
            {
                gun = new Rifle(name, bulletsCount);
                guns.Add(gun);
            }
            else if (type == "Pistol")
            {
                gun = new Pistol(name, bulletsCount);
                guns.Add(gun);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidGunType);
            }

            return string.Format(OutputMessages.SuccessfullyAddedGun, name);
        }

        public string AddPlayer(string type, string username, int health, int armor, string gunName)
        {
            IGun gun = guns.FindByName(gunName);
            IPlayer player;
            if (gun == null)
            {
                throw new ArgumentException(ExceptionMessages.GunCannotBeFound);
            }
            if (type == "Terrorist")
            {
                player = new Terrorist(username, health, armor, gun);
            }
            else if (type == "CounterTerrorist")
            {
                player = new CounterTerrorist(username, health, armor, gun);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidPlayerType);
            }

            players.Add(player);

            return string.Format(OutputMessages.SuccessfullyAddedPlayer, username);
        }

        public string Report()
        {
            var playersReports = this.players.Models.OrderBy(p => p.GetType().Name).ThenByDescending(p => p.Health).ThenBy(p => p.Username);

            StringBuilder sb = new StringBuilder();

            foreach (var player in playersReports)
            {

                sb.AppendLine(player.ToString());
            }

            return sb.ToString().Trim();
        }

        public string StartGame()
        {
            return this.map.Start(this.players.Models.ToList());
        }



    }
}
