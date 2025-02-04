using ItemPlayer;
using GameLoop;
using System.ComponentModel;
using System.Collections.Generic;
using System.Numerics;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace ItemPlayer
{
    /// <summary>
    /// 아이템
    /// </summary>
    /// 
    public enum EquipmentSlot
    {
        body = 1,
        weapon,
    }

    public interface IItem
    {
        float CoreValue { get; }
        string CoreValueName { get; }
        int Price { get; }
        string Name { get; }
        string Description { get; }
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
        public void UseItem(Player player, IPotion item, Inventory inventory);
    }

    public class HealingPotion : IPotion
    {
        public float CoreValue => healAmount;
        public string CoreValueName { get { return "치유량"; } }
        float healAmount = 20;
        public int Price { get { return 100; } }
        public string Name { get { return "치유물약"; } }
        public string Description { get { return "마시면 체력 20이 회복된다."; } }


        public void UseItem(Player player, IPotion item, Inventory inventory)
        {
            player.Health += 20;
            inventory.InventoryItem.Remove(item);   
        }
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
        public void EquipOrUnEquip(Player player, IEquipment item, Inventory inventory);
    }

    public abstract class Weapon : IEquipment
    {
        public float CoreValue => WeaponAttack;
        public string CoreValueName { get { return "공격력"; } }
        protected abstract int WeaponAttack { get; }
        public abstract int Price { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }

        public EquipmentSlot Slot = EquipmentSlot.weapon;

        public void EquipOrUnEquip(Player player, IEquipment item, Inventory inventory)
        {
            //착용하고 있는 아이템인지 검사
            if (!inventory.EquippedItem.Contains(item))
            {
                //착용 부위인지 검사
                if (!player.equippingSlot.ContainsValue(Slot))
                {
                    player.Attack += WeaponAttack;
                    player.equippingSlot.Add(item.Name, Slot);
                    inventory.EquippedItem.Add(item);
                }
                else
                {
                    Console.WriteLine("이미 착용한 부위입니다");
                    Thread.Sleep(1000);
                }
            }
            else
            {
                //착용 해제하기
                player.Attack -= WeaponAttack;
                player.equippingSlot.Remove(item.Name);
                inventory.EquippedItem.Remove(item);
            }
        }
        //public void UnEquip(Player player)
        //{
        //    if (!player.equippingSlot.ContainsValue(Slot))
        //    {
        //        player.Attack += WeaponAttack;
        //        player.equippingSlot.Add(item.Name, Slot);
        //    }
        //    else
        //    {
        //        Console.WriteLine("이미 착용한 부위입니다");
        //    }
        //    player.Attack -= WeaponAttack;
        //}

        public void Attackable()
        {
            //공격 가능
        }
    }
    public class Old_Sword : Weapon
    {
        public override string Name { get { return "낡은 검"; } }
        protected override int WeaponAttack { get { return 2; } }
        public override int Price { get { return 100; } }
        public override string Description { get { return "찔리면 파상풍에 걸릴 거 같은 검입니다."; } }

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
    }

    public class Spartan_Spear : Weapon
    {
        public override string Name { get { return "스파르타의 창"; } }
        protected override int WeaponAttack { get { return 7; } }
        public override int Price { get { return 2000; } }
        public override string Description { get { return "스파르타의 전사들이 사용했다는 전설의 창입니다."; } }

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
    }

    public abstract class Armor : IEquipment
    {
        public float CoreValue => ArmorDefense;
        public string CoreValueName { get { return "방어력"; } }
        protected abstract int ArmorDefense { get; }
        public abstract int Price { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }

        public EquipmentSlot Slot = EquipmentSlot.body;

        public void EquipOrUnEquip(Player player, IEquipment item, Inventory inventory)
        {
            //착용하고 있는 아이템인지 검사
            if (!inventory.EquippedItem.Contains(item))
            {
                //착용 부위인지 검사
                if (!player.equippingSlot.ContainsValue(Slot))
                {
                    player.Defense += ArmorDefense;
                    player.equippingSlot.Add(item.Name, Slot);
                    inventory.EquippedItem.Add(item);
                }
                else
                {
                    Console.WriteLine("이미 착용한 부위입니다");
                    Thread.Sleep(1000);
                }
            }
            else
            {
                //착용 해제하기
                player.Defense -= ArmorDefense;
                player.equippingSlot.Remove(item.Name);
                inventory.EquippedItem.Remove(item);
            }
        }
    }


    public class Novice_Armor : Armor
    {
        public override string Name { get { return "수련자의 갑옷"; } }
        protected override int ArmorDefense { get { return 5; } }
        public override int Price { get { return 1000; } }
        public override string Description { get { return "수련에 도움을 주는 갑옷입니다."; } }

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
        // public static Player player; // 왜 선언한 거지?

        public ClassGroup playerClass;

        public float Gold { get; set; }

        public int Lv { get; set; }

        public Dictionary<string, EquipmentSlot> equippingSlot = new Dictionary<string, EquipmentSlot>(); // 플레이어가 착용 중임을 의미하는 equippingSlot 딕셔너리 


        public Player(int level, string name, ClassGroup classGroup)
        {

            playerClass = classGroup;
            Attack = playerClass.ClassAttack;
            Defense = playerClass.ClassDefense;
            Health = playerClass.ClassHealth;
            Gold = playerClass.ClassGold;
            Lv = level;
            Name = name;
        }
    }

    public class Inventory
    {
        public List<IItem> InventoryItem = new List<IItem> { };
        public List<IEquipment> EquippedItem = new List<IEquipment> { };

        Shop shop = new Shop();

        //// 아이템관리 //
        //public void EquipItem(List<IItem> InventoryItem, List<IEquipment> EquippedItem, IEquipment item)
        //{
        //    AddToEquippedItem(EquippedItem, item);
        //}

        //public void UnEquipItemp(List<IItem> InventoryItem, List<IEquipment> EquippedItem, IEquipment item)
        //{
        //    RemoveFromEquippedItem(EquippedItem, item);
        //}

        //public void UseItem(List<IItem> InventoryItem, IItem item)
        //{
        //    RemoveFromInventoryItem(InventoryItem, item);
        //}

        //// 상점 //
        //public void BuyFromShop(Player player, List<IItem> InventoryItem, IItem item)
        //{
        //    AddToInventoryItem(InventoryItem, item);
        //    player.Gold -= item.Price;
        //}

        //public void SellToShop(Player player, List<IItem> InventoryItem, IItem item)
        //{
        //    RemoveFromInventoryItem(InventoryItem, item);
        //    player.Gold += item.Price * (float)0.5;
        //}

        //// 조합 메서드 //

        //public void RemoveFromInventoryItem(List<IItem> InventoryItem, IItem item)
        //{
        //    InventoryItem.Remove(item);
        //}

        //public void RemoveFromEquippedItem(List<IEquipment> EquippedItem, IEquipment item)
        //{
        //    EquippedItem.Remove(item);
        //}

        //public void AddToInventoryItem(List<IItem> InventoryItem, IItem item)
        //{
        //    InventoryItem.Add(item);
        //}

        //public void AddToEquippedItem(List<IEquipment> EquippedItem, IEquipment item)
        //{
        //    EquippedItem.Add(item);
        //}


    }

    public class Shop
    {
        public List<IItem> SellingItem = new List<IItem> { };
        public Action<Player, List<IItem>, IItem> Buying;
        public Action<Player, List<IItem>, IItem> Selling;

        public void TriggerBuying(Player player, List<IItem> InventoryItem, IItem item)
        {
            Buying?.Invoke(player, InventoryItem, item);
        }

        public void TriggerSelling(Player player, List<IItem> InventoryItem, List<IItem> SellingItem, IItem item)
        {
            Selling?.Invoke(player, InventoryItem, item);
        }
    }


    public abstract class ClassGroup // 직업마다 각기 다른 부분을 구현하도록 '강제'해야하므로 virtual보다는 abstract가 적절
    {
        public abstract string Name { get; }
        public abstract int ClassAttack { get; }
        public abstract int ClassDefense { get; }
        public abstract float ClassHealth { get; }
        public abstract float ClassGold { get; }

        public void EnterAbility<T>(T target, T ability) // 다양한 타입의 필드에 값을 넣어야하기 때문에 제너릭으로 선언.
        {
            target = ability;
        }
    }

    public class Warrior : ClassGroup
    {
        public override string Name { get { return "전사"; } }
        public override int ClassAttack { get { return 20; } }

        public override int ClassDefense { get { return 5; } }
        public override float ClassHealth { get { return 100; } }
        public override float ClassGold { get { return 50; } }
    }

    public class Faker : ClassGroup
    {
        public override string Name { get { return "위조자"; } }
        public override int ClassAttack { get { return 0; } }

        public override int ClassDefense { get { return 0; } }
        public override float ClassHealth { get { return 100; } }
        public override float ClassGold { get { return 1000000; } }
    }
}

namespace GameLoop
{
    public class GameManager
    {
        static GameManager instance;

        SceneManager SM = new SceneManager();

        Scene currentScene = new TownScene();

        ClassGroup classGroup = new Warrior();

        Shop shop = new Shop();





        private GameManager() { }
        public static GameManager GetInstance()
        {
            if (instance == null)
            {
                instance = new GameManager();
            }

            return instance;
        }


        bool isRunning = true;

        public void GameStart()
        {
            Player player = new Player(1, "Rtani", classGroup);
            Inventory inventory = new Inventory();
            player.Gold += 100000;
            //SM.EnterScene += currentScene.ShowDescription;
            //SM.EnterScene += currentScene.ShowChoice;

            //SM.EnterCharacterUIScene += currentScene.ShowInfo;

            //shop.Buying += inventory.BuyFromShop;
            //shop.Selling += inventory.SellToShop;

            Novice_Armor novice_armor = new Novice_Armor();
            Spartan_Spear spartan_Spear = new Spartan_Spear();
            HealingPotion healingPotion = new HealingPotion();
            Old_Sword old_Sword = new Old_Sword();

            shop.SellingItem.AddRange(new List<IItem> { novice_armor, spartan_Spear, healingPotion, old_Sword });
            //샵 구매 디버깅
            // shop.TriggerBuying(player, inventory.InventoryItem, novice_armor);

            //착용하기, 이미 착용 문구 보기, 착용 해제하기 테스팅 
            //spartan_Spear.EquipOrUnEquip(player, spartan_Spear, inventory);
            //old_Sword.EquipOrUnEquip(player, old_Sword, inventory);
            //spartan_Spear.EquipOrUnEquip(player, spartan_Spear, inventory);

            while (isRunning)
            {
                //씬 소개 및 선택지 출력 단계
                SM.TriggerEnterScene(currentScene, player);

                //선택 단계
                bool inputSuccess = false;
                int input = 0;

                while (!inputSuccess)
                {

                    Console.Write("행동을 선택해주세요: ");
                    //씬 선택지 입력, //입력을 씬 매개변수에 입력.
                    inputSuccess = int.TryParse(Console.ReadLine(), out input);
                    inputSuccess = 0 < input && input < currentScene.engage.Count + currentScene.connectedScene.Count + 1;
                    if (!inputSuccess)
                    {
                        Console.Write("올바른 입력해주세요!");
                        Console.WriteLine("");
                    }
                }
                inputSuccess = false;

                if (input > currentScene.engage.Count)
                {
                    // 씬 이동 선택지를 고른 경우
                    int inputNum = input - currentScene.engage.Count;

                    SM.ChangeCurrentScene(currentScene.connectedScene[inputNum - 1], ref currentScene);
                }
                else
                {
                    //bool engageQuit = false;
                    //int newInput;
                    //while (!engageQuit)
                    //{
                    // 행동 선택지를 고른 경우
                    currentScene.engageMethod[input - 1](player, currentScene, shop, inventory, input);

                    //if (int.TryParse(Console.ReadLine(), out newInput))
                    //{
                    //}
                    //}
                }
            }
        }
    }

    public class SceneManager
    {
        public Action<Scene> EnterScene; // 씬 입장시 발생하는 이벤트:(씬 소개 메서드, 씬 선택지 메서드)
        public Action<Player> EnterCharacterUIScene; // UI 씬 입장시 발생하는 이벤트:(씬 소개 메서드, 씬 선택지 메서드)

        Scene tempForUI;


        public void ChangeCurrentScene(string ToSceneName, ref Scene currentScene)
        {
            if (ToSceneName == "돌아가기")
            {
                currentScene = tempForUI;
            }
            {
                switch (ToSceneName)
                {

                    case "마을":
                        currentScene = new TownScene();
                        break;
                    case "캐릭터 정보창":
                        currentScene = new CharacterScene(currentScene, ref tempForUI);
                        break;
                    case "아이템창":
                        currentScene = new InventoryScene(currentScene, ref tempForUI);
                        break;
                    case "상점":
                        currentScene = new ShopScene();
                        break;
                    case "여관":
                        currentScene = new RestScene();
                        break;
                    case "던전":
                        currentScene = new DungeonScene();
                        break;
                }
            }
        }

        public void TriggerEnterScene(Scene i, Player player)
        {
            Console.Clear(); // 로그 클리어.
            i.ShowName(i);
            Console.ResetColor();
            i.ShowDescription(i);
            if (i is UIScene)
            {
                i.ShowInfo(player); // 이렇게 하면 됨.
            }
            i.ShowChoice(i);

            // 이벤트는 virtual 오버라이드가 안되네
            //if (i is UIScene)
            //{
            //    EnterCharacterUIScene?.Invoke(player); 
            //}
            //EnterScene?.Invoke(i); 

        }
    }

    public class Scene
    {
        public string name;
        protected string description;
        public List<string> engage = new List<string>();
        public List<string> connectedScene = new List<string>();
        public List<Action<Player, Scene, Shop, Inventory, int>> engageMethod = new List<Action<Player, Scene, Shop, Inventory, int>>();
        public virtual void ShowName(Scene sceneName)
        {
            PrintText(sceneName.name);
        }
        public void ShowDescription(Scene sceneName)
        {
            Console.WriteLine(sceneName.description + "\n");

            ///// 여기서 description이 직전에 했던 것으로 고정되는 문제가 있음. 처음 생성된 Scene 클래스의 객체가 할당한 값으로 고정돼버림. 이유는 몰라.
        }

        public virtual void ShowChoice(Scene sceneName)
        {
            int i = 1;
            while (i - 1 < sceneName.engage.Count)
            {
                Console.WriteLine($"{i}. {sceneName.engage[i - 1]} 을/를 하기");
                i++;
            }

            Console.WriteLine();

            while (i - sceneName.engage.Count - 1 < sceneName.connectedScene.Count)
            {
                Console.WriteLine($"{i}. {sceneName.connectedScene[i - sceneName.engage.Count - 1]} (으)로 이동하기");
                i++;
            }

            Console.WriteLine();
        }

        public virtual void ShowInfo(Player player) { }

        public void PrintText(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }

    public class LocationScene : Scene
    {

    }



    public class TownScene : LocationScene
    {
        //////////////////생성자로 부모 클래스가 생성한 Engage,ConnectScene, description 필드의 값을 새로이 할당/////////////////
        public TownScene()
        {
            name = "마을";
            description = "이곳은 마을입니다. 무기를 정비하고 휴식을 취할 수 있습니다.";
            engage.AddRange(new List<string> { "하늘 보기", "마을 순찰" });
            connectedScene.AddRange(new List<string> { "여관", "상점", "던전", "캐릭터 정보창", "아이템창" });
        }
    }

    public class RestScene : LocationScene
    {
        public RestScene()
        {
            name = "여관";
            description = "여관에 들어왔습니다. 휴식을 취하거나 식사를 할 수 있습니다.";
            engage.AddRange(new List<string> { "휴식 하기(20G)", "식사 하기(50G)" });
            connectedScene.AddRange(new List<string> { "마을", "캐릭터 정보창", "아이템창" });
        }
    }
    public class DungeonScene : LocationScene
    {
        public override void ShowName(Scene sceneName)
        {
            PrintText(sceneName.name, ConsoleColor.Red);
        }
        public DungeonScene()
        {
            name = "던전";
            description = "이곳은 던전 입구입니다. 조심하세요.";
            engage.AddRange(new List<string> { "사냥터 입장", "요새 입장", "지옥 입장" });
            connectedScene.AddRange(new List<string> { "마을", "캐릭터 정보창", "아이템창" });
        }
    }

    public class ShopScene : LocationScene
    {
        //Shop shop = new Shop(); // 없어도 될듯?
        public ShopScene()
        {
            name = "상점";
            description = "이곳은 상점입니다. 물품을 구매하고 판매할 수 있습니다.";
            engage.AddRange(new List<string> { "물품 구매", "물품 판매" });
            connectedScene.AddRange(new List<string>() { "마을", "캐릭터 정보창", "아이템창" });

            engageMethod.Add(TradeEngage);
            engageMethod.Add(TradeEngage);
        }

        public void TradeEngage(Player player, Scene sceneName, Shop shop, Inventory inventory, int input)
        {
            bool stopTrading = false;
            List<IItem> seller;
            List<IItem> buyer;
            bool isBuying = input == 1;

            if (isBuying) // 누가 구매자인지
            {
                seller = shop.SellingItem;
                buyer = inventory.InventoryItem;
            }
            else
            {
                seller = inventory.InventoryItem;
                buyer = shop.SellingItem;
            }

            while (!stopTrading)
            {
                Console.WriteLine($"[잔여 골드]");
                PrintText($"  {player.Gold} \n ", ConsoleColor.Yellow);
                Console.WriteLine($"[아이템 목록]");

                ShowSellerItem(seller);

                ShowTrading(player, seller, buyer, ref stopTrading, isBuying);
            }
        }
        public void ShowSellerItem(List<IItem> seller)
        {
            {
                int i = 1;
                Console.WriteLine();
                foreach (IItem item in seller)
                {
                    Console.WriteLine($"- [{i}] \t {item.Name}\t |\t{item.CoreValueName} +{item.CoreValue}\t|\t{item.Description}\t \t|  가격: {item.Price}골드");
                    i++;
                }
                Console.WriteLine($"\n{i} 돌아가기");
                Console.WriteLine();
            }
        }

        public void ShowTrading(Player player, List<IItem> seller, List<IItem> buyer, ref bool stopBuying, bool isBuying)
        {
            int input;
            bool inputSuccess = false;

            while (!inputSuccess)
            {
                if (int.TryParse(Console.ReadLine(), out input))
                {
                    if (input == seller.Count + 1)
                    {
                        //돌아가기
                        inputSuccess = true;
                        stopBuying = true;
                        break;
                    }
                    else if (input < seller.Count + 1 && input > 0)
                    {
                        if (player.Gold > seller[input - 1].Price || isBuying == false)
                        {
                            string tempItemName = seller[input - 1].Name.ToString();
                            if (isBuying == true)
                            {
                                player.Gold -= seller[input - 1].Price;
                            }
                            else
                            {
                                player.Gold += seller[input - 1].Price * (float)0.5;
                            }
                            buyer.Add(seller[input - 1]);
                            seller.RemoveAt(input - 1);

                            Thread.Sleep(100);
                            if (isBuying == true)
                            {
                                PrintText($"{tempItemName} 구매에 성공했습니다!", ConsoleColor.DarkGreen);
                            }
                            else
                            {
                                PrintText($"{tempItemName} 판매에 성공했습니다!", ConsoleColor.DarkGreen);
                            }
                            Thread.Sleep(200);

                            inputSuccess = true;
                        }
                        else
                        {
                            Console.WriteLine("소지 골드가 부족합니다!");
                        }

                    }
                }
            }
        }
        //public void SellingEngage(Player player, Scene sceneName, Shop shop, Inventory inventory)
        //{
        //    bool stopSelling = false;

        //    while (!stopSelling)
        //    {
        //        ShowInventoryItem(inventory);
        //        ShowSelling(player, shop, inventory, ref stopSelling);
        //    }
        //}
        //public void ShowInventoryItem(Inventory inventory)
        //{
        //    {
        //        int i = 1;
        //        Console.WriteLine();
        //        foreach (IItem item in inventory.InventoryItem)
        //        {
        //            Console.WriteLine($"- [{i}] \t {item.Name}\t|\t{item.CoreValueName} +{item.CoreValue}\t|\t{item.Description}\t \t|  가격: {item.Price * 0.5}골드");
        //            i++;
        //        }
        //        Console.WriteLine($"\n{i} 돌아가기");
        //        Console.WriteLine();
        //    }
        //}
        //public void ShowSelling(Player player, Shop shop, Inventory inventory, ref bool stopSelling)
        //{
        //    int input;
        //    bool inputSuccess = false;

        //    while (!inputSuccess)
        //    {
        //        if (int.TryParse(Console.ReadLine(), out input))
        //        {
        //            if (input == inventory.InventoryItem.Count + 1)
        //            {
        //                //돌아가기
        //                inputSuccess = true;
        //                stopSelling = true;
        //                break;
        //            }
        //            else if (input < inventory.InventoryItem.Count + 1 && input > 0)
        //            {
        //                string tempItemName = inventory.InventoryItem[input - 1].ToString();
        //                player.Gold += inventory.InventoryItem[input - 1].Price;
        //                shop.SellingItem.Add(inventory.InventoryItem[input - 1]);
        //                inventory.InventoryItem.RemoveAt(input - 1);

        //                Thread.Sleep(10);
        //                PrintText($"{tempItemName} 판매에 성공했습니다!", ConsoleColor.Green);
        //                Thread.Sleep(100);

        //                inputSuccess = true;


        //            }
        //        }
        //    }
        //}
    }

    public class UIScene : Scene
    {
        public UIScene(Scene currentScene, ref Scene tempForUI)
        {
            tempForUI = currentScene;
        }

        public override void ShowChoice(Scene sceneName)
        {

            int i = 1;
            while (i - 1 < sceneName.engage.Count)
            {
                Console.WriteLine($"{i}. {sceneName.engage[i - 1]}");
                i++;
            }
            Console.WriteLine();

            while (i - sceneName.engage.Count - 1 < sceneName.connectedScene.Count)
            {
                Console.WriteLine($"{i}. {sceneName.connectedScene[i - sceneName.engage.Count - 1]}");
                i++;
            }
            Console.WriteLine();
        }
    }

    public class InventoryScene : UIScene
    {
        public InventoryScene(Scene currentScene, ref Scene tempForUI) : base(currentScene, ref tempForUI)
        {
            name = "아이템창";
            description = "이곳은 아이템창입니다. 아이템을 관리할 수 있습니다.";
            engage.AddRange(new List<string> { "장착 관리" });
            connectedScene.AddRange(new List<string> { "돌아가기" });

            engageMethod.Add(ManageEquipAndUse);
        }

        public void ManageEquipAndUse(Player player, Scene sceneName, Shop shop, Inventory inventory, int input)
        {
            bool inputSuccess = false;
            int inputEngage; 

            while (!inputSuccess)
            {
                Console.Clear();
                ShowItem(inventory);
                if (int.TryParse(Console.ReadLine(), out inputEngage))
                {
                    if (inputEngage == inventory.InventoryItem.Count + 1)
                    {
                        //돌아가기
                        inputSuccess = true;
                        break;
                    }
                    else if (inputEngage < inventory.InventoryItem.Count + 1 && inputEngage > 0)
                    {
                        if(inventory.InventoryItem[inputEngage - 1] is IEquipment)
                        {
                            IEquipment temp = inventory.InventoryItem[inputEngage - 1] as IEquipment;
                            temp.EquipOrUnEquip( player, temp,  inventory); //손봐야함
                        }
                        else if(inventory.InventoryItem[inputEngage - 1] is IPotion)
                        {
                            IPotion temp = inventory.InventoryItem[inputEngage - 1] as IPotion;
                            temp.UseItem(player, temp, inventory);
                        }
                    }
                }
            }
        }
        public void ShowItem(Inventory inventory)
        {
            int i = 0;
            Console.WriteLine();

            foreach (IItem item in inventory.InventoryItem)
            {
                if (inventory.EquippedItem.Contains(item))
                {
                    PrintText($"[{i + 1}]\t[E]{item.Name}\t|\t{item.CoreValueName} +{item.CoreValue}\t|\t{item.Description}\t|  가격: {item.Price}골드", ConsoleColor.Green);
                    i++;
                }
                else
                {
                    Console.WriteLine($"[{i + 1}]\t[ ]{item.Name}\t|\t{item.CoreValueName} +{item.CoreValue}\t|\t{item.Description}\t|  가격: {item.Price}골드");
                    i++;
                }
            }
            Console.WriteLine($"\n{i + 1} 돌아가기");
            Console.WriteLine();

        }

    }

    public class CharacterScene : UIScene
    {
        public CharacterScene(Scene currentScene, ref Scene tempForUI) : base(currentScene, ref tempForUI)
        {
            name = "캐릭터 정보창";
            description = "이곳은 캐릭터창입니다. 캐릭터의 정보를 확인할 수 있습니다.";
            connectedScene.AddRange(new List<string> { "돌아가기" });
        }

        public override void ShowInfo(Player player)
        {
            Console.WriteLine("이름\t {0}", player.Name);
            Console.WriteLine("Lv\t {0}", player.Lv);
            Console.WriteLine("직업\t {0}", player.playerClass.Name);
            Console.WriteLine("체력\t {0}", player.Health);
            Console.WriteLine("공격력\t {0}", player.Attack);
            Console.WriteLine("방어력\t {0}", player.Defense);
            Console.WriteLine("골드\t {0}", player.Gold);
            Console.WriteLine();
        }
    }
    public class SceneChoice
    {

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager instance = GameManager.GetInstance();
            instance.GameStart();
            //

            //SceneManager SM = new SceneManager();

            //Scene currentScene = new DungeonScene();

            //SM.EnterScene += currentScene.ShowDescription;
            //SM.EnterScene += currentScene.ShowChoice;

            //bool gameQuit = false;



            ////게임 루프
            //while (!gameQuit || isRunning)
            //{

            //    //씬 소개 및 선택지 출력 단계
            //    SM.TriggerEnterScene(currentScene);

            //    //선택 단계
            //    bool inputSuccess = false;
            //    int input = 0;

            //    while (!inputSuccess)
            //    {
            //        Console.Write("행동을 선택해주세요: ");
            //        //씬 선택지 입력, //입력을 씬 매개변수에 입력.
            //        inputSuccess = int.TryParse(Console.ReadLine(), out input);
            //        inputSuccess = input > 0;
            //        if (!inputSuccess)
            //        {
            //            Console.Write("올바른 입력해주세요!");
            //            Console.WriteLine("");
            //        }
            //    }
            //    inputSuccess = false;

            //    //씬 이동
            //    int inputNum = input - currentScene.engage.Count;
            //    SM.ChangeCurrentScene(currentScene.connectedScene[inputNum - 1], ref currentScene);
            //}
        }
    }
}
