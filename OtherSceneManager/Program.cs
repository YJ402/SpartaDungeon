namespace ItemPlayer
{
    /// <summary>
    /// 아이템
    /// </summary>
    public interface IItem
    {
        public void Buyable()
        {
            //구매 가능
        }

        public void Sellable()
        {
            //판매 가능
        }
    }

    public interface IPotion : IItem
    {
        public void Drinkable()
        {
            //마시기 가능
        }
    }

    public class HealingPotion : IPotion
    {
        float healAmount = 20;
        int Price = 100;

        public void Buyable()
        {
            //구매 가능
        }

        public void Sellable()
        {
            //판매 가능
        }

        public void Drinkable()
        {
            //마시기 가능
        }
    }

    public interface IEquipment : IItem
    {
        public void Equippable()
        {
            //장착 가능
        }
    }

    public class Weapon : IEquipment
    {
        int Attack;
        int Price;
        string Description;

        public void Attackable()
        {
            //공격 가능
        }
    }

    public class Spartan_Spear : Weapon
    {
        string Description = "스파르타의 전사들이 사용했다는 전설의 창입니다.";
        public void Buyable()
        {
            //구매 가능
        }

        public void Sellable()
        {
            //판매 가능
        }

        public void Equippable()
        {
            //장착 가능
        }

        public Spartan_Spear(int attack, int price)
        {
            int Attack = attack;
            int Price = price;
        }
    }

    public class Armor : IEquipment
    {
        int Defense;
        int Price;
        string Description;

        public void Attackable()
        {
            //공격 가능
        }
    }


    public class Novice_Armor : Armor
    {
        string Description = "수련에 도움을 주는 갑옷입니다.";

        public void Buyable()
        {
            //구매 가능
        }

        public void Sellable()
        {
            //판매 가능
        }

        public void Equippable()
        {
            //장착 가능
        }

        public Novice_Armor(int defense, int price)
        {
            int Defense = defense;
            int Price = price;
        }
    }


    /// <summary>
    /// 생물(플레이어)
    /// </summary>


    public abstract class Creature
    {
        public string Name { get; set; }

        public float Health { get; set; }

        public int Attack { get; set; }

        public int Defense { get; set; }

    }

    public class Player : Creature
    {

        public Player(string name, float health, int attack, int defense)
        {
            Name = name;
            Health = health;
            Attack = attack;
            Defense = defense;
        }
    }


}