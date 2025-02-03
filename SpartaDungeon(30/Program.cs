using ItemPlayer;
using GameLoop;

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

        public Player(string name, float health, int attack, int defense)
        {
            Name = name;
            Health = health;
            Attack = attack;
            Defense = defense;
        }
    }


}

namespace GameLoop
{
    public class SceneManager
    {
        public Action<Scene> EnterScene; // 씬 입장시 발생하는 이벤트:(씬 소개 메서드, 씬 선택지 메서드)

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

        public void TriggerEnterScene(Scene i)
        {
            EnterScene?.Invoke(i);
        }
    }

    public class Scene
    {
        protected string description;
        public List<string> engage = new List<string>();
        public List<string> connectedScene = new List<string>();

        public void ShowDescription(Scene sceneName)
        {
            Console.Clear();
            Console.WriteLine(sceneName.description);  
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
    }
    public class SceneChoice
    {

    }

    internal class Program
    {
        static void Main(string[] args)
        {

            SceneManager SM = new SceneManager();

            Scene currentScene = new DungeonScene();

            SM.EnterScene += currentScene.ShowDescription;
            SM.EnterScene += currentScene.ShowChoice;

            bool gameQuit = false;



            //게임 루프
            while (!gameQuit)
            {

                //씬 소개 및 선택지 출력 단계
                SM.TriggerEnterScene(currentScene);

                //선택 단계
                bool inputSuccess = false;
                int input=0;

                while (!inputSuccess)
                {
                    Console.Write("행동을 선택해주세요: ");
                    //씬 선택지 입력, //입력을 씬 매개변수에 입력.
                    inputSuccess = int.TryParse(Console.ReadLine(), out input);
                    inputSuccess = input > 0;
                    if (!inputSuccess)
                    {
                        Console.Write("올바른 입력해주세요!");
                        Console.WriteLine("");
                    }
                }
                inputSuccess = false;

                //씬 이동
                int inputNum = input - currentScene.engage.Count;
                SM.ChangeCurrentScene(currentScene.connectedScene[inputNum-1], ref currentScene);
            }
        }
    }
}
