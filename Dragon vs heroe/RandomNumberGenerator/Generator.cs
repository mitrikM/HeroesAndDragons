using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_vs_hero.RandomNumberGenerator
{
    public class Generator : Random
    {
        private static Generator instance;
        public static Generator Instance
        {
            get
            {
                if (instance==null)
                {
                    instance = new Generator();
                }
                return instance;
            }
        }
        private Generator() 
        { 

        }
    }
}
