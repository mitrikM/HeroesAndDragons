using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_vs_hero.Equipment
{
    public class Shield:ItemAtributes
    {
        public int ShieldDeff { get; set; }

        public Shield(int weight,string name,int shieldDeff) :base(weight,name)
        {
            ShieldDeff = shieldDeff;

        }


    }
}
