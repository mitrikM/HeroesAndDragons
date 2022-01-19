using System;
using System.Collections.Generic;
using System.Text;
using Dragon_vs_hero.Characters;

namespace Dragon_vs_hero.Equipment
{
    public class ItemAtributes
    {

        public int Weight { get; set; }
        public string Name { get; set; }
        public Hero hero;
        public ItemAtributes(int weight, string name)
        {
            this.Weight = weight;
            Name = name;
        }
    }
}
