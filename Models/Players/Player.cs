using CounterStrike.Models.Guns.Contracts;
using CounterStrike.Models.Players.Contracts;
using CounterStrike.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace CounterStrike.Models.Players
{
    public abstract class Player : IPlayer
    {
        string username;
        int health;
        int armor;
        IGun gun;

        public Player(string username, int health, int armor, IGun gun)
        {
            this.Username = username;
            this.Health = health;
            this.Armor = armor;
            this.Gun = gun;
        }
        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlayerName);
                }
                this.username = value;
            }
        }
        public int Health
        {
            get
            {
                return this.health;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlayerHealth);
                }
                this.health = value;
            }
        }
        public int Armor
        {
            get
            {
                return this.armor;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlayerArmor);
                }
                this.armor = value;
            }
        }


        public IGun Gun
        {
            get => this.gun;

            private set
            {
                if (value == null)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidGun);
                }
                this.gun = value;
            }

        }

        public bool IsAlive => this.Health > 0;
	
        public void TakeDamage(int points)
        {
            if (points<this.Armor)
            {
                this.Armor -= points;
            }
            else
            {
                points -= this.Armor;
                this.Armor = 0;
                if (points<this.Health)
                {
                    this.Health -= points;
                }
                else
                {
                    this.Health = 0;
                }
            }
            
           
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.GetType().Name}: {username}");
            sb.AppendLine($"--Health: {health}");
            sb.AppendLine($"--Armor: {armor}");
            sb.AppendLine($"--Gun: {gun.Name}");
            
            return sb.ToString().Trim(); 
        }
    }
}
