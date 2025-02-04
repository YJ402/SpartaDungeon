using ItemPlayer;
using GameLoop;
using System.ComponentModel;

namespace ItemPlayer
{
    /// <summary>
    /// 아이템
    /// </summary>
    /// 

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
        public static Player player;

        public ClassGroup playerClass;

        public int Gold { get; set; }

        public int Lv { get; set; }



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

    public abstract class ClassGroup // 직업마다 각기 다른 부분을 구현하도록 '강제'해야하므로 virtual보다는 abstract가 적절
    {
        public abstract string Name { get;  }
        public abstract int ClassAttack { get; }
        public abstract int ClassDefense { get; }
        public abstract float ClassHealth { get; }
        public abstract int ClassGold { get; }

        public void EnterAbility<T>(T target, T ability) // 다양한 타입의 필드에 값을 넣어야하기 때문에 제너릭으로 선언.
        {
            target = ability;
        }
    }

    public class Warrior : ClassGroup
    {
        public override string Name { get { return "전사";  } }
        public override int ClassAttack { get { return 20; } }

        public override int ClassDefense { get { return 5; } }
        public override float ClassHealth { get { return 100; } }
        public override int ClassGold { get { return 50; } }
    }

    public class Faker : ClassGroup
    {
        public override string Name { get { return "위조자"; } }
        public override int ClassAttack { get { return 0; } }

        public override int ClassDefense { get { return 0; } }
        public override float ClassHealth { get { return 100; } }
        public override int ClassGold { get { return 1000000; } }
    }
}

namespace GameLoop
{
    public class GameManager
    {
        static GameManager instance;

        SceneManager SM = new SceneManager();

        Scene currentScene = new DungeonScene();

        ClassGroup classGroup = new Warrior();




        private GameManager() { }
        public static GameManager GetInstance()
        {
            if(instance == null)
            {
                instance = new GameManager();   
            }
            
            return instance;
        }


        bool isRunning = true;

        public void GameStart()
        {
            Player player = new Player(1, "Rtani", classGroup);

            SM.EnterScene += currentScene.ShowDescription;
            SM.EnterScene += currentScene.ShowChoice;

            SM.EnterCharacterUIScene += currentScene.ShowInfo;

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
                    inputSuccess =  0 < input && input < currentScene.engage.Count + currentScene.connectedScene.Count + 1;
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
                    Console.WriteLine(currentScene.engage[input]);
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

                    case "TownScene":
                        currentScene = new TownScene();
                        break;
                    case "CharacterScene":
                        currentScene = new CharacterScene(currentScene, ref tempForUI);
                        break;
                    case "InventoryScene":
                        currentScene = new InventoryScene(currentScene, ref tempForUI);
                        break;
                    case "ShopScene":
                        currentScene = new ShopScene();
                        break;
                    case "RestScene":
                        currentScene = new RestScene();
                        break;
                    case "DungeonScene":
                        currentScene = new DungeonScene();
                        break;
                }
            }
        }

        public void TriggerEnterScene(Scene i, Player player)
        {
            Console.Clear(); // 로그 클리어.

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
        protected string description;
        public List<string> engage = new List<string>();
        public List<string> connectedScene = new List<string>();

        public void ShowDescription(Scene sceneName)
        {
            // Console.Clear(); // 로그 클리어.
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

            while (i - sceneName.engage.Count - 1 < sceneName.connectedScene.Count)
            {
                Console.WriteLine($"{i}. {sceneName.connectedScene[i - sceneName.engage.Count - 1]} 로 이동하기");
                i++;
            }
            Console.WriteLine();
        }

        public virtual void ShowInfo(Player player) { }
    }

    public class LocationScene : Scene
    {

    }



    public class TownScene : LocationScene
    {
        //////////////////생성자로 부모 클래스가 생성한 Engage,ConnectScene, description 필드의 값을 새로이 할당/////////////////
        public TownScene()
        {
            description = "이곳은 마을입니다. 무기를 정비하고 휴식을 취할 수 있습니다.";
            engage.AddRange(new List<string> { "하늘 보기", "마을 순찰" });
            connectedScene.AddRange(new List<string> { "RestScene", "ShopScene", "DungeonScene", "CharacterScene", "InventoryScene" });
        }
    }

    public class RestScene : LocationScene
    {
        public RestScene()
        {
            description = "여관에 들어왔습니다. 휴식을 취하거나 식사를 할 수 있습니다.";
            engage.AddRange(new List<string> { "휴식 하기(20G)", "식사 하기(50G)" });
            connectedScene.AddRange(new List<string> { "TownScene", "CharacterScene", "InventoryScene" });
        }
    }
    public class DungeonScene : LocationScene
    {
        public DungeonScene()
        {
            description = "이곳은 던전 입구입니다. 조심하세요.";
            engage.AddRange(new List<string> { "사냥터 입장", "요새 입장", "지옥 입장" });
            connectedScene.AddRange(new List<string> { "TownScene", "CharacterScene", "InventoryScene" });
        }
    }

    public class ShopScene : LocationScene
    {
        public ShopScene()
        {
            description = "이곳은 상점입니다. 물품을 구매하고 판매할 수 있습니다.";
            engage.AddRange(new List<string> { "물품 구매", "물품 판매" });
            connectedScene.AddRange(new List<string>() { "TownScene", "CharacterScene", "InventoryScene" });
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
            description = "이곳은 장비창입니다. 장비를 관리할 수 있습니다.";
            engage.AddRange(new List<string> { "장착 관리" });
            connectedScene.AddRange(new List<string> { "돌아가기" });
        }
    }

    public class CharacterScene : UIScene
    {
        public CharacterScene(Scene currentScene, ref Scene tempForUI) : base(currentScene, ref tempForUI)
        {
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
