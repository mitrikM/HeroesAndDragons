using Dragon_vs_hero.Equipment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_vs_hero.Characters
{
    public class Hero : Character
    {
        public Sword sword;
        public Shield shield;
        public Hero(string Name, int Health, int MaxDmg, int MaxDef, Sword sword=null,Shield shield=null) : base(Name, Health, MaxDmg, MaxDef)
        {
            this.sword = sword;
            this.shield = shield;
        }

        public Hero(string Name, int Health, int MaxDmg, int MaxDef) : base(Name, Health, MaxDmg, MaxDef) 
        { 

        }

        public Hero(int Health, int MaxDmg, int MaxDef) : this(string.Empty, Health, MaxDmg, MaxDef,null,null) 
        { 
        }

        public override void Attack(Character enemy)
        {
            BaseDmg += sword.Attackdmg;
            base.Attack(enemy);
            BaseDmg = 0;
        }

        public override int Deffense()
        {
            BaseDeff = 0;
            BaseDeff += shield.ShieldDeff;
            return base.Deffense();
        }

        protected override bool ControlTheChoiceOfOponent(Character enemy)
        {
            return true;
        }
        public override Character ChoiceOfOponents(List<Character> characters)
        {
            for (int i = 0; i < characters.Count * 2; i++)
            {
                int GeneratedIndex = Generate.Next(0, characters.Count);
                if (this != characters[GeneratedIndex] && characters[GeneratedIndex].IsAlive() && ControlTheChoiceOfOponent(characters[GeneratedIndex]))
                {
                    return characters[GeneratedIndex];
                }
            }

            return base.ChoiceOfOponents(characters);
        }
        public override double GetOpScore()
        {
            return 0.2 * Health + 0.1 * MaxDef + 0.2 * MaxDmg + 0.3 * sword?.Attackdmg ?? 0 + (0.2 * shield?.ShieldDeff) ?? 0;
        }
    }
}


