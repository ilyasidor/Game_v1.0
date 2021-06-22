using System;
using System.Threading;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Weapon knife = new Weapon("Ножик", 10);
            Weapon arm = new Weapon("смертоносная лапка!", 5);

            Person MAIN_HERO = new Person("Ilya",100,100,knife);
            Person enemy_fox = new Person("Fox", 40, 10, arm);

            MAIN_HERO.GetMoneyHero();

            while (true)
            {
                Random random = new Random();
                int choice = random.Next(0, 2);
                if (choice == 0)
                {
                    Person.Attack(MAIN_HERO, enemy_fox);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(enemy_fox.name+" нанес удар");
                    Console.WriteLine(MAIN_HERO.HP + " осталось у "+ MAIN_HERO.name);
                    Console.ResetColor(); 
                    Thread.Sleep(100);
                }
                else
                {
                    Person.Attack(enemy_fox,MAIN_HERO);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(MAIN_HERO.name + " нанес удар");
                    Console.WriteLine(enemy_fox.HP + " осталось у " + enemy_fox.name);
                    Console.ResetColor(); 
                    Thread.Sleep(100);


                }
                if ((MAIN_HERO.HP <= 0)|| (enemy_fox.HP <= 0)){
                    if (MAIN_HERO.HP <= 0)
                    {
                        enemy_fox.money += MAIN_HERO.money;
                        MAIN_HERO.money = 0;
                    }
                    else
                    {
                        MAIN_HERO.money += enemy_fox.money;
                        enemy_fox.money = 0;
                    }
                    break;
                }
            }
            if (MAIN_HERO.HP >= 0)
            {
                MAIN_HERO.GetMoneyHero();
                Console.WriteLine($"Вы можете забрать оружие {enemy_fox.name} это {enemy_fox.weapon.name}" +
                    $" она имеет {enemy_fox.weapon.dmg} урона\n" +
                    $"Забрать - 1\n" +
                    $"Не брать - 2");
                int choice = 0;
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Вы некоректно ввели значение!!!");
                }
                if (choice == 1)
                {
                    MAIN_HERO.weapon = enemy_fox.weapon;
                    Console.WriteLine($"Теперь ваше оружие {MAIN_HERO.weapon.name}") ;
                }
                else if (choice == 2)
                {
                    Console.WriteLine("Вы прошли мимо, ваше оружие при вас");
                }
                Console.WriteLine("Вы видите перед собой пещеру, пойдемте внутрь!");
                Thread.Sleep(2400);
                Console.WriteLine("Вот мы и внутри! О ужас спереди нас злой медведь! В атаку!\n" +
                    "Что бы уклонится от удара медведя вовремя нажмите на W\n" +
                    "Медведь сильнее и страшнее вас, осторожнее!!!");
                Weapon paw = new Weapon("ЛАПА МЕДВЕДЯ", 24);
                Person bear = new Person("Медведь",80,100,paw);
                
            }
        }

    }
     class Person
    {
        public string name;
        private int hp;

        private static int counter;
        public int HP
        {

            set
            {
                hp = value;
                if (value <= 0)
                {
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine(name+" умер и потерял "+money+" золота!!!");
                    counter--;
                }
            }
            get
            {
                return hp;
            }
            
        }
        public decimal money;
       
        
        public Weapon weapon = new Weapon();
        public int dmg;
        public Person(string Name,int HP,int Money,Weapon Weapon)
        {
            this.name = Name;
            this.hp = HP;
            this.money = Money;
            this.weapon = Weapon;
            this.dmg = weapon.dmg;
            counter++;
        }

        public Person(string Name, int Money, Weapon Weapon)
        {
            this.name = Name;
            this.money = Money;
            this.hp = 100;
            this.weapon = Weapon;
            this.dmg = weapon.dmg;
            counter++;
        }
        public Person(string Name, Weapon Weapon)
        {
            this.name = Name;
            this.money = 100;
            this.hp = 100;
            this.weapon = Weapon;
            this.dmg = weapon.dmg;
            counter++;
        }

        public static int operator -(Person person1, Person person2)
        {
            return person1.hp - person2.dmg;
        }
        public static void GetCountPersons()
        {
            Console.WriteLine($"В игре на данный момент {counter}");
        }

        public void GetMoneyHero()
        {
            Console.WriteLine($"{name} имеет {money} золота!");
        }
        public static void Attack(Person attacker, Person defender)
        {

            attacker.HP -= defender.dmg;
        }
        

    }
    class Weapon
    {
        public string name;
        public int dmg;

        public Weapon(string Name,int DMG)
        {
            this.name = Name;
            this.dmg = DMG;
        }
        public Weapon()
        {
            this.name = "";
            this.dmg = 0;
        }
    }
}
