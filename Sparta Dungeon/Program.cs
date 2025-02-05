using ItemPlayer;
using GameLoop;
using System.ComponentModel;
using System.Collections.Generic;
using System.Numerics;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;

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
    }

    public class BigHealingPotion : IPotion
    {
        public float CoreValue => healAmount;
        public string CoreValueName { get { return "치유량"; } }
        float healAmount = 50;
        public int Price { get { return 200; } }
        public string Name { get { return "대용량 치유물약"; } }
        public string Description { get { return "마시면 체력 50이 회복된다."; } }


        public void UseItem(Player player, IPotion item, Inventory inventory)
        {
            player.Health += 20;
            inventory.InventoryItem.Remove(item);
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
        public abstract int WeaponAttack { get; }
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

    }
    public class Old_Sword : Weapon
    {
        public override string Name { get { return "녹슬은 검"; } }
        public override int WeaponAttack { get { return 2; } }
        public override int Price { get { return 100; } }
        public override string Description { get { return "찔리면 파상풍에 걸릴 거 같은 검입니다."; } }
    }

    public class Bronze_Axe : Weapon
    {
        public override string Name { get { return "청동 도끼"; } }
        public override int WeaponAttack { get { return 5; } }
        public override int Price { get { return 1500; } }
        public override string Description { get { return "어디선가 사용됐던거 같은 도끼입니다"; } }
    }
    public class Spartan_Spear : Weapon
    {
        public override string Name { get { return "스파르타의 창"; } }
        public override int WeaponAttack { get { return 7; } }
        public override int Price { get { return 2000; } }
        public override string Description { get { return "스파르타의 전사들이 사용했다는 전설의 창입니다."; } }
    }

    public class Kratos_Sword : Weapon
    {
        public override string Name { get { return "크레토스의 검"; } }
        public override int WeaponAttack { get { return 17; } }
        public override int Price { get { return 15000; } }
        public override string Description { get { return "크레토스가 쓰다버린 검입니다."; } }
    }


    public abstract class Armor : IEquipment
    {
        public float CoreValue => ArmorDefense;
        public string CoreValueName { get { return "방어력"; } }
        public abstract int ArmorDefense { get; }
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
        public override int ArmorDefense { get { return 5; } }
        public override int Price { get { return 1000; } }
        public override string Description { get { return "수련에 도움을 주는 갑옷입니다."; } }


    }
    public class Iron_Armor : Armor
    {
        public override string Name { get { return "무쇠 갑옷"; } }
        public override int ArmorDefense { get { return 9; } }
        public override int Price { get { return 1900; } }
        public override string Description { get { return "무쇠로 만들어져 튼튼한 갑옷입니다."; } }


    }
    public class Spartan_Armor : Armor
    {
        public override string Name { get { return "스파르타의 갑옷"; } }
        public override int ArmorDefense { get { return 15; } }
        public override int Price { get { return 3500; } }
        public override string Description { get { return "스파르타의 전사들이 사용했다는 전설의 갑옷입니다."; } }


    }
    public class Kratos_Armor : Armor
    {
        public override string Name { get { return "크레토스의 갑옷"; } }
        public override int ArmorDefense { get { return 30; } }
        public override int Price { get { return 10000; } }
        public override string Description { get { return "전쟁의 신이 한번 입고 버린 갑옷입니다."; } }


    }

    /// <summary>
    /// 생물(플레이어)
    /// </summary>


    public abstract class Creature
    {
        public string Name { get; set; }

        public abstract float Health { get; set; }

        public int Attack { get; set; }

        public int Defense { get; set; }

    }

    public class Player : Creature
    {
        public ClassGroup playerClass;

        private bool isDie;
        public bool IsDie { get; private set; }

        private float gold;
        public float Gold
        {
            get { return gold; }
            set
            {
                gold = value;
            }
        }

        private float xp;

        public float XP
        {
            get { return xp; }
            set
            {
                if (value >= MaxXp)
                {
                    LevelUp();
                }
                else
                {
                    xp = value;
                }
            }
        }
        private float maxXp = 100;
        public float MaxXp { get { return maxXp; } set { maxXp = value; } }
        private int level;
        public int Lv { get; set; }
        private float health;
        public override float Health
        {
            get { return health; }
            set
            {
                if (value <= 0)
                {
                    IsDie = true;
                    health = 0;
                }
                else if (value > MaxHealth)
                {
                    health = MaxHealth;
                }
                else
                {
                    health = value;
                }
            }
        }
        public float MaxHealth { get; set; }

        public Dictionary<string, EquipmentSlot> equippingSlot = new Dictionary<string, EquipmentSlot>(); // 플레이어가 착용 중임을 의미하는 equippingSlot 딕셔너리 


        public Player(int level, string name, ClassGroup classGroup)
        {

            playerClass = classGroup;
            Attack = playerClass.ClassAttack;
            Defense = playerClass.ClassDefense;
            health = playerClass.ClassHealth;
            MaxHealth = playerClass.ClassMaxHealth;
            Gold = playerClass.ClassGold;
            Lv = level;
            Name = name;
        }

        public void LevelUp()
        {
            XP = 0;
            Lv += 1;
            Attack += 1;
            Defense += 1;


            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("레벨 업! {0} -> {1}", Lv - 1, Lv);
            Console.ResetColor();
            Thread.Sleep(2000);
            MaxXp = Lv * (float)(100 * 0.5) + 50; ;
        }

        public ref float GethealthRef()
        {
            return ref health;
        }

        public ref float GetgoldRef()
        {
            return ref gold;
        }

        public void ReadSelf()
        {
            Gold += 0;
            Defense += 0;
            Attack += 0;
            Health += 0;
            Lv += 0;
            xp += 0;
        }
    }

    public class Inventory
    {
        public List<IItem> InventoryItem = new List<IItem> { };
        public List<IEquipment> EquippedItem = new List<IEquipment> { }; ///////// EquipSlot이랑 통합할 수도 있을까?

        Shop shop = new Shop();
    }

    public class Shop
    {
        public List<IItem> SellingItem = new List<IItem> { };
    }


    public abstract class ClassGroup // 직업마다 각기 다른 부분을 구현하도록 '강제'해야하므로 virtual보다는 abstract가 적절
    {
        public abstract string Name { get; }
        public abstract int ClassAttack { get; }
        public abstract int ClassDefense { get; }
        public abstract float ClassHealth { get; }
        public abstract float ClassMaxHealth { get; }
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
        public override float ClassMaxHealth { get { return 100; } }
        public override float ClassGold { get { return 50; } }
    }

    public class Faker : ClassGroup
    {
        public override string Name { get { return "위조자"; } }
        public override int ClassAttack { get { return 0; } }

        public override int ClassDefense { get { return 0; } }
        public override float ClassHealth { get { return 100; } }
        public override float ClassMaxHealth { get { return 100; } }
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

            Novice_Armor novice_armor = new Novice_Armor();
            Iron_Armor iron_Armor = new Iron_Armor();
            Spartan_Armor spartan_Armor = new Spartan_Armor();
            Kratos_Armor kratos_Armor = new Kratos_Armor();
            Old_Sword old_Sword = new Old_Sword();
            Bronze_Axe bronze_Axe = new Bronze_Axe();
            Spartan_Spear spartan_Spear = new Spartan_Spear();
            Kratos_Sword kratos_Sword = new Kratos_Sword();
            HealingPotion healingPotion = new HealingPotion();
            BigHealingPotion bigHealingPotion = new BigHealingPotion();

            shop.SellingItem.AddRange(new List<IItem> { novice_armor, iron_Armor, spartan_Spear, kratos_Armor, old_Sword, bronze_Axe, spartan_Spear, kratos_Sword, healingPotion, bigHealingPotion });

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
                    // 행동 선택지를 고른 경우
                    currentScene.engage_Method[input - 1](player, currentScene, shop, inventory, input);

                }

                if (player.IsDie == true)
                {
                    isRunning = false;
                    currentScene.PrintText("당신은 사망하였습니다.", 5000, ConsoleColor.Red);
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
        }
    }

    public class Scene
    {
        public string name;
        protected string description;
        public List<string> engage = new List<string>();
        public List<string> connectedScene = new List<string>();
        public List<Action<Player, Scene, Shop, Inventory, int>> engage_Method = new List<Action<Player, Scene, Shop, Inventory, int>>();
        public virtual void ShowName(Scene sceneName)
        {
            PrintText(sceneName.name);
        }
        public void ShowDescription(Scene sceneName)
        {
            Console.WriteLine(sceneName.description + "\n");
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

        public void PrintText(string text, int ms = 0, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
            Thread.Sleep(ms);
        }

        public bool YesOrNo(string warning)
        {
            bool yes;

            Console.WriteLine(warning);
            Console.WriteLine("\n \n 1.네 \n 2.아니오");

            while (true)
            {
                int input;
                if (int.TryParse(Console.ReadLine(), out input))
                {
                    if (input == 1)
                    {
                        yes = true;
                        break;
                    }
                    else if (input == 2)
                    {
                        yes = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다");
                    }
                }
            }

            return yes;
        }

        public void SuccessOrNot(Player player, int Difficulty, string engageName, float minStat1, ref float rewardRef, string rewardName, float rewardDegree, ref float penaltyRef, string penaltyName, float penaltyDegree, float bonusstat, float stat1)
        {
            //보너스 스탯
            int tempBonusstat = (int)bonusstat;

            //원래의 보상, 페널티 스텟 저장
            float tempReward = rewardRef;
            float tempPenalty = penaltyRef;

            //보너스 스텟에 따른 랜덤값
            int randNum1 = new Random().Next(tempBonusstat, tempBonusstat * 2);


            //페널티 정도에 따른 성공시의 랜덤값
            int randNum2 = new Random().Next((int)(penaltyDegree), (int)(penaltyDegree * 1.75));

            //페널티 정도에 따른 실패시의 랜덤값
            int randNum3 = new Random().Next((int)(penaltyDegree * 1.25), (int)(penaltyDegree * 2));



            if (stat1 >= minStat1 || TryChance(100 + (stat1 - minStat1) / minStat1 * 100 - 30))
            {
                //보상 결정, 프로퍼티 로직 사용할 수 있게.
                rewardRef += rewardDegree * (1 + randNum1 / 100);

                //페널티 결정.
                if (randNum2 - stat1 + minStat1 > 0)
                    penaltyRef -= randNum2 - stat1 + minStat1;

                //경험치 상승
                player.XP += 1 * Difficulty;

                //셀프 리딩
                player.ReadSelf();

                PrintText($"{engageName}에 성공했습니다!", 300, ConsoleColor.Blue);
                PrintText("[탐험 결과]", 200, ConsoleColor.DarkBlue);
                PrintText($"{rewardName} {tempReward} -> {rewardRef}", 0, ConsoleColor.DarkBlue);
                PrintText($"{penaltyName} {tempPenalty} -> {penaltyRef}", 1000, ConsoleColor.DarkBlue);
            }
            else
            {

                //페널티 결정
                if (randNum3 - stat1 + minStat1 > 0)
                    penaltyRef -= randNum3 - stat1 + minStat1;

                player.ReadSelf();

                PrintText($"{engageName}에 실패했습니다!", 300, ConsoleColor.Red);
                PrintText("[탐험 결과]", 200, ConsoleColor.DarkRed);
                PrintText($"{rewardName} {tempReward} -> {rewardRef}", 0, ConsoleColor.DarkRed);
                PrintText($"{penaltyName} {tempPenalty} -> {penaltyRef}", 1000, ConsoleColor.DarkRed);
            }
        }

        public bool TryChance(float i)
        {
            float randomNum = new Random().Next(0, 100);
            return randomNum < i;
        }
    }

    public class LocationScene : Scene
    {

    }

    public class TownScene : LocationScene
    {
        public TownScene()
        {
            name = "마을";
            description = "이곳은 마을입니다. 상점에서 거래를 하거나 여관에서 휴식을 취할 수 있습니다.";
            engage.AddRange(new List<string> { "땅 파기" });
            connectedScene.AddRange(new List<string> { "여관", "상점", "던전", "캐릭터 정보창", "아이템창" });

            engage_Method.Add(Engage_Digging);
        }

        public void Engage_Digging(Player player, Scene sceneName, Shop shop, Inventory inventory, int input)
        {
            if (YesOrNo("땅을 파보시겠습니까?"))
            {
                SuccessOrNot(player, 1, "땅 파기", 10, ref player.GetgoldRef(), "골드", 10, ref player.GethealthRef(), "체력", 3, player.Attack, player.Attack);
            }
        }
    }

    public class RestScene : LocationScene
    {
        public RestScene()
        {
            name = "여관";
            description = "여관에 들어왔습니다. 휴식을 취할 수 있습니다.";
            engage.AddRange(new List<string> { "휴식 하기(500G)" });
            connectedScene.AddRange(new List<string> { "마을", "캐릭터 정보창", "아이템창" });

            engage_Method.Add(Engage_Rest);
        }

        public void Engage_Rest(Player player, Scene sceneName, Shop shop, Inventory inventory, int input)
        {
            if (YesOrNo("500 골드를 내고 휴식하시겠습니까?"))
            {
                player.Gold -= 500;
                player.Health = player.MaxHealth;
            }
        }

    }
    public class DungeonScene : LocationScene
    {
        public override void ShowName(Scene sceneName)
        {
            PrintText(sceneName.name, 0, ConsoleColor.Red);
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
                Console.WriteLine($"{i}. {sceneName.connectedScene[i - sceneName.engage.Count - 1]} (으)로 이동하기");
                i++;
            }

            Console.WriteLine();
        }
        public DungeonScene()
        {
            name = "던전";
            description = "이곳은 던전 입구입니다. 조심하세요.";
            engage.AddRange(new List<string> { "사냥터 입장\t| 권장 방어력 5", "폐허 입장\t| 권장 방어력 11", "미궁 입장\t| 권장 방어력 17" });
            connectedScene.AddRange(new List<string> { "마을", "캐릭터 정보창", "아이템창" });

            engage_Method.Add(Engage_Easy);
            engage_Method.Add(Engage_Normal);
            engage_Method.Add(Engage_Hard);
        }

        public void Engage_Easy(Player player, Scene sceneName, Shop shop, Inventory inventory, int input)
        {
            if (YesOrNo("사냥터는 쉬운 난이도의 던전입니다. 공략에 도전하시겠습니까?"))
            {
                SuccessOrNot(player, 20, "사냥터 공략", 5, ref player.GetgoldRef(), "골드", 1000, ref player.GethealthRef(), "체력", 20, player.Attack, player.Defense);
            }
        }
        public void Engage_Normal(Player player, Scene sceneName, Shop shop, Inventory inventory, int input)
        {
            if (YesOrNo("폐허는 노말 난이도의 던전입니다. 공략에 도전하시겠습니까?"))
            {
                SuccessOrNot(player, 40, "폐허 공략", 11, ref player.GetgoldRef(), "골드", 1700, ref player.GethealthRef(), "체력", 20, player.Attack, player.Defense);
            }
        }
        public void Engage_Hard(Player player, Scene sceneName, Shop shop, Inventory inventory, int input)
        {
            if (YesOrNo("미궁은 어려운 난이도의 던전입니다. 공략에 도전하시겠습니까?"))
            {
                SuccessOrNot(player, 60, "미궁 공략", 17, ref player.GetgoldRef(), "골드", 2500, ref player.GethealthRef(), "체력", 20, player.Attack, player.Defense);
            }
        }
    }

    public class ShopScene : LocationScene
    {
        public ShopScene()
        {
            name = "상점";
            description = "이곳은 상점입니다. 물품을 구매하고 판매할 수 있습니다.";
            engage.AddRange(new List<string> { "물품 구매", "물품 판매" });
            connectedScene.AddRange(new List<string>() { "마을", "캐릭터 정보창", "아이템창" });

            engage_Method.Add(Engage_Trade);
            engage_Method.Add(Engage_Trade);
        }

        public void Engage_Trade(Player player, Scene sceneName, Shop shop, Inventory inventory, int input)
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
                PrintText($"  {player.Gold} \n ", 0, ConsoleColor.Yellow);
                Console.WriteLine($"[아이템 목록]");

                ShowSellerItem(seller);

                ShowTrading(player, seller, buyer, inventory, ref stopTrading, isBuying);
            }
        }
        public void ShowSellerItem(List<IItem> seller)
        {
            {
                int i = 1;
                Console.WriteLine();
                foreach (IItem item in seller)
                {
                    Console.WriteLine($"-[{i}]\t{item.Name}\t|\t{item.CoreValueName} +{item.CoreValue}\t|\t{item.Description}\t \t|  가격: {item.Price}골드");
                    i++;
                }
                Console.WriteLine($"\n{i} 돌아가기");
                Console.WriteLine();
            }
        }

        public void ShowTrading(Player player, List<IItem> seller, List<IItem> buyer, Inventory inventory, ref bool stopBuying, bool isBuying)
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
                                //착용중인 아이템이라면 착용해제하기
                                if (inventory.EquippedItem.Contains(seller[input - 1]))
                                {
                                    if (seller[input - 1] is Weapon)
                                    {
                                        Weapon temp = seller[input - 1] as Weapon;
                                        player.Attack -= temp.WeaponAttack;
                                        player.equippingSlot.Remove(temp.Name);
                                        inventory.EquippedItem.Remove(temp);
                                    }
                                    else
                                    {
                                        Armor temp = seller[input - 1] as Armor;
                                        player.Defense -= temp.ArmorDefense;
                                        player.equippingSlot.Remove(temp.Name);
                                        inventory.EquippedItem.Remove(temp);
                                    }

                                }
                            }
                            buyer.Add(seller[input - 1]);
                            seller.RemoveAt(input - 1);
                            //
                            Thread.Sleep(100);
                            if (isBuying == true)
                            {
                                PrintText($"{tempItemName} 구매에 성공했습니다!", 200, ConsoleColor.DarkGreen);
                            }
                            else
                            {
                                PrintText($"{tempItemName} 판매에 성공했습니다!", 200, ConsoleColor.DarkGreen);
                            }

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

            engage_Method.Add(ManageEquipAndUse);
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
                        if (inventory.InventoryItem[inputEngage - 1] is IEquipment)
                        {
                            IEquipment temp = inventory.InventoryItem[inputEngage - 1] as IEquipment;
                            temp.EquipOrUnEquip(player, temp, inventory);
                        }
                        else if (inventory.InventoryItem[inputEngage - 1] is IPotion)
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
                    PrintText($"[{i + 1}]\t[E]{item.Name}\t|\t{item.CoreValueName} +{item.CoreValue}\t|\t{item.Description}\t|  가격: {item.Price}골드", 0, ConsoleColor.Green);
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
            Console.WriteLine("경험치\t {0} / {1}", player.XP, player.MaxXp);
            Console.WriteLine("직업\t {0}", player.playerClass.Name);
            Console.WriteLine("체력\t {0} / {1}", player.Health, player.MaxHealth);
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
        }
    }
}
