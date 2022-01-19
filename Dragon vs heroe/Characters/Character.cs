using Dragon_vs_hero.RandomNumberGenerator;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Dragon_vs_hero.Characters
{
    public abstract class Character : Object,IComparable<Character>
    {

        public int Health { get; set; }
        public string Name { get; set; }
        public int MaxDmg { get; set; }
        public int MaxDef { get; set; }
        public int BaseDmg { get; set; }
        public int BaseDeff { get; set; }
        public int MaxHealth { get; set; }
        public int Escaped { get; set; }
        public int SameTarget { get; set; }


        protected Generator Generate = Generator.Instance;
        public event Action<Character, Character, int, int> AttackHappened;
        public event Action<Character, Character> OponentChosen;

        public Character(string Name, int Health, int MaxDmg, int MaxDef, int BaseDmg = 0, int BaseDeff = 0, int MaxHealth = 0, int Escaped = -2,int SameTarget=0)
        {
            this.Name = Name;
            this.Health = Health;
            this.MaxDmg = MaxDmg;
            this.MaxDef = MaxDef;
            this.BaseDmg = BaseDmg;
            this.BaseDeff = BaseDeff;
            this.MaxHealth = Health;
            this.Escaped = Escaped;
            this.SameTarget = SameTarget;
        }


        public virtual void Attack(Character enemy)
        {

            int crit = Crit();
            int AttackDamage = Convert.ToInt32(Generate.NextDouble() * MaxDmg) * crit + BaseDmg;
            int deffense = enemy.Deffense();
            if (AttackDamage - deffense < 0)
            {
                AttackDamage = 0;
            }
            else
            {
                AttackDamage -= deffense;
            }

            if (enemy.Health - AttackDamage < 0)
            {

                enemy.Health = 0;
            }
            else
            {
                enemy.Health -= AttackDamage;
            }

            AttackHappened?.Invoke(this, enemy, AttackDamage, deffense);

        }
        public virtual int Deffense()
        {
            int deffense = 0;
            if (Generate.NextDouble() <= 0.5)
            {
                deffense = Generate.Next(0, MaxDef) + BaseDeff;
                ConsoleUpgrade.WriteLine($"Deffense of  {Name} is:{deffense}",ConsoleColor.Cyan);
            }
            return deffense;
        }

        public bool IsAlive()
        {
            if (Health > 0 && Escaped != 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int Crit()
        {
            int crit;
            if (Generate.NextDouble() > 0.90)
            {
                crit = 2;
                ConsoleUpgrade.WriteLine($"{Name} used Crittical strike",ConsoleColor.DarkRed);
            }
            else
            {
                crit = 1;
            }
            return crit;
        }
        public virtual Character ChoiceOfOponents(List<Character> characters)
        {

            return ChoiceOfOponentsElementary(characters);
        }
        private Character ChoiceOfOponentsElementary(List<Character> characters)
        {
            Character oponent = null;

            foreach (var character in characters)
            {
                if (this != character && character.IsAlive() && ControlTheChoiceOfOponent(character))
                {
                    oponent = character;
                    break;
                }
            }
            OponentChosen?.Invoke(this, oponent);
            return oponent;

        }
        protected abstract bool ControlTheChoiceOfOponent(Character enemy);

        public bool OponentExists(List<Character> characters)
        {

            return ChoiceOfOponentsElementary(characters) != null ? true : false;
        }

        public bool EscapeFromFight(Character character)
        {
            double rng = Generate.NextDouble();
            bool escape = false;

            if (character.IsAlive() && character.Health < character.MaxHealth * 0.2 && character.Escaped == 0)
            {
                ConsoleUpgrade.WriteLine($"{Name} is trying to escape",ConsoleColor.Yellow);
                if (rng > 0.5)
                {
                    ConsoleUpgrade.WriteLine($"{Name}'s Escape was succesfull",ConsoleColor.Green);
                    character.Escaped = 10;
                    escape = true;
                    return escape;
                }
                else
                {
                    ConsoleUpgrade.WriteLine($"{Name}'s Escape attempt failed",ConsoleColor.Red);
                    character.Escaped = -2;
                    escape = false;
                    return escape;
                }
            }
            return escape; 
        }
        public int EscapedCooldown(Character character)
        {
            if (character.Escaped<0)
            {
                character.Escaped++;
            }
            return character.Escaped;
        }
        public override string ToString()
        {
            return $"Name: {Name}, OP Score:{GetOpScore()} Health: {Health}, Max.Dmg:{MaxDmg} , Max.Deff: {MaxDef}";
        }

        public int CompareTo([AllowNull] Character other)
        {
            if (other == null)
            {
                return 1;
            }

            return this.GetOpScore().CompareTo(other.GetOpScore());
        }
        public virtual double GetOpScore()
        {
            return 0.2 * Health + 0.3 * MaxDef + 0.5 * MaxDmg; 
        }
    }
}
