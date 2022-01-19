using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_vs_hero.Characters
{
    public class Dragon : Character
    {


        public Dragon(string Name, int Health, int MaxDmg, int MaxDef) : base(Name, Health, MaxDmg, MaxDef)
        {

        }

        protected override bool ControlTheChoiceOfOponent(Character enemy)
        {
            return true;
            // return enemy.GetType() != this.GetType();
        }
       
    }
}
