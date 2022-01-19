using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_vs_hero.Equipment
{
    public class Sword : ItemAtributes
    {
        public int Attackdmg { get; set; }

        public Sword(int Weight, string Name, int attackdmg) : base(Weight, Name)
        {
            this.Attackdmg = attackdmg;
        }
       
    }
}
