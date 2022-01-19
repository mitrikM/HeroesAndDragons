using Dragon_vs_hero;
using Dragon_vs_hero.Characters;
using Dragon_vs_hero.Equipment;
using Dragon_vs_hero.RandomNumberGenerator;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace Dragon_vs_hero_Mitrik
{
    public class Program
    {
        static void Main(string[] args)
        {
            Sword Gorehowl = new Sword(1, "Gorehowl", 30);
            Sword DoomHammer = new Sword(2, "DoomHammer", 35);
            Shield shield = new Shield(4, "doran's shield", 10);
            Hero hero = new Hero("Gromash", 200, 25, 30, Gorehowl, shield);
            Dragon dragon3 = new Dragon("Onyxia", 300, 60, 10);
            Hero hero1 = new Hero("Thrall", 150, 20, 50, DoomHammer, shield);
            Dragon dragon1 = new Dragon("Deathwing", 180, 40, 40);

            /*
            List<ItemAtributes> Items = new List<ItemAtributes>();
            Items.Add(silverSword);
            Items.Add(silverSword2);
            */

            List<Character> Characters = new List<Character>();
            Characters.Add(dragon3);
            Characters.Add(dragon1);
            Characters.Add(hero1);
            Characters.Add(hero);


            Characters.Sort();
            Console.WriteLine(string.Join(Environment.NewLine, Characters));
            Console.WriteLine(Environment.NewLine + Environment.NewLine);

            double averagePower = Characters.Average(character => character.GetOpScore());
            Console.WriteLine($"Average power of characters is:{averagePower}");

            double MinimumPower = Characters.Min(character => character.GetOpScore());
            Character weakestCharacter = Characters.Find(character => character.GetOpScore() == MinimumPower);

            Console.WriteLine($"Weakest character with score {MinimumPower} is: {weakestCharacter.ToString()}");
            double MaximumPower = Characters.Max(character => character.GetOpScore());

            Character strongestCharacter = Characters.Find(character => character.GetOpScore() == MaximumPower);
            Console.WriteLine($"Strongest character with score {MaximumPower} is: {strongestCharacter.ToString()}");

            List<Character> OverAverage = Characters.FindAll(character => character.GetOpScore() > averagePower);
            Console.WriteLine("Character  with score over average are:");
            Console.WriteLine(string.Join(Environment.NewLine, OverAverage));
            
            Character Lastcharacter = Characters.FindLast(character => character.GetOpScore() < averagePower);
            Console.WriteLine($"last weakest character is:{Lastcharacter.ToString()}");
            
            List<Character> Dragons = Characters.FindAll(character => character.GetType() == typeof(Dragon)); ;
            Console.WriteLine("Dragons are:");
            Console.WriteLine(string.Join(Environment.NewLine, Dragons));
            
            double averageDmg = Characters.Average(character => character.MaxDmg);
            List<Character> DeletedCharacters = Characters.FindAll(character => character.MaxDmg < (averageDmg / 2));
            Console.WriteLine("heroes bellow 1/2 of avg dmg  are:");
            if (DeletedCharacters.Any())
            {
            Console.WriteLine(string.Join(Environment.NewLine, DeletedCharacters));
            }
            else
            {
                Console.WriteLine("there is none heroe bellow 1/2 of  avg dmg");
                Console.WriteLine(); 
            }

            double avgDeff = Characters.Average(character => character.MaxDef);
            List<Character> noobs = Characters.FindAll(character => character.MaxDef < (avgDeff /4));
            if (!noobs.Any())
            {
                Console.WriteLine("every heroe has over 1/4 of avg deff");
            }
            else
            {
                Console.WriteLine("Some heroe has bellow 1/4 of avg deff");
                Console.WriteLine(string.Join(Environment.NewLine, noobs));
            }

            Characters.ForEach(character => character.AttackHappened += AttackInfo);
            Characters.ForEach(character => character.OponentChosen += ChangeOfTarget);


            Console.WriteLine(Environment.NewLine + Environment.NewLine);

            for (int i = 1; IsItPossibleToChooseOpponent(Characters); ++i)
            {

                ConsoleUpgrade.WriteLine($"round number: {i}",ConsoleColor.Blue);
                Console.WriteLine();
                for (int j = 0; j < Characters.Count; ++j)
                {
                    Character Attacker = Characters[j];
                    if (Attacker.IsAlive())
                    {
                        Character oponent = Attacker.ChoiceOfOponents(Characters);
                       
                        if (oponent != null)
                        {
                            oponent.EscapedCooldown(oponent);
                            if (!oponent.EscapeFromFight(oponent))
                            {
                                Attacker.Attack(oponent);
                            }

                        }
                        else
                        {
                            continue;
                        }
                        Console.WriteLine(Environment.NewLine);
                    }
                }
            }
            ConsoleUpgrade.WriteLine("Winners:",ConsoleColor.DarkGreen);
            foreach (var character in Characters)
            {
                if (character.IsAlive())
                {
                    ConsoleUpgrade.WriteLine(character.ToString(),ConsoleColor.Green);
                }
            }
            ConsoleUpgrade.WriteLine("Defeated:",ConsoleColor.DarkRed);
            foreach (var character in Characters)
            {
                if (!character.IsAlive() && character.Escaped != 10)
                {
                    ConsoleUpgrade.WriteLine(character.ToString(),ConsoleColor.Red);
                }
            }
            ConsoleUpgrade.WriteLine("Escaped:",ConsoleColor.DarkYellow);
            foreach (var character in Characters) 
            {
                if (!character.IsAlive() && character.Escaped == 10)
                {
                    ConsoleUpgrade.WriteLine(character.ToString(),ConsoleColor.Yellow);
                }
            }


            /*
                        if (NumberOfAliveHeroes(Characters) > 0)
                        {
                            Console.WriteLine("Heroes have won");
                        }
                        else if(NumberOfAliveDragons(Characters) > 0)
                        {
                            Console.WriteLine("Dragons have won");
                        }
            */
        }
        public static bool IsItPossibleToChooseOpponent(List<Character> characters)
        {
            foreach (Character character in characters)
            {
                if (character.IsAlive() && character.OponentExists(characters) )
                {
                    return true;
                }
            }
            return false;
        }
        /*
        public static int NumberOfAliveDragons(List<Character> characters)
        {
            int NumberOfAliveDragons = 0;
            foreach (Character character in characters)
            {
                if (character is Dragon && character.IsAlive())
                {
                    ++NumberOfAliveDragons;
                }
            }
            return NumberOfAliveDragons;
        }
        public static int NumberOfAliveHeroes(List<Character> characters)
        {
            int NumberOfAliveHeroes = 0;
            foreach (Character character in characters)
            {
                if (character is Hero && character.IsAlive())
                {
                    ++NumberOfAliveHeroes;
                }
            }
            return NumberOfAliveHeroes;
        }
        */
        public static void AttackInfo(Character attacker,Character oponent, int AttackDamage, int Deffense)
        {
            ConsoleUpgrade.WriteLine($"Atack damage of {attacker.Name} is : {AttackDamage}", ConsoleColor.DarkMagenta);
            ConsoleUpgrade.WriteLine($"enemy unit { oponent.Name} has {oponent.Health} health points remaining", ConsoleColor.Yellow);
        }
        public static void ChangeOfTarget(Character attacker,Character oponent)
        {

            if (oponent != null)
            {
                if (attacker.SameTarget != 0 && oponent != null && attacker.SameTarget == oponent.GetHashCode() && oponent.Escaped != 10)
                {
                    Console.WriteLine($"atacker {attacker.Name} is attacking  same target {oponent.Name} as before ");
                }
                else if (oponent.IsAlive() && oponent != null && oponent != null && (attacker.SameTarget == 0 || attacker.SameTarget != oponent.GetHashCode()))
                {

                    Console.WriteLine($"attacker {attacker.Name} is attacking new target {oponent.Name}");


                }

                attacker.SameTarget = oponent.GetHashCode();
            }
        }

    }

}
