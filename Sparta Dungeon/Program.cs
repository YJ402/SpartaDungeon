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
        public enum SceneName
        {
            StartScene = 1,
            TownScene,
            CharacterScene,
            InventoryScene,
            ShopScene,
            RestScene,
            DungeonScene
        }

        public void TriggerEnterScene(Scene i)
        {
            EnterScene?.Invoke(i); 
        }
    }

    public class Scene
    {
        public List<string> ConnectScene = new List<string>();   
        string Description { get; set; }


        public void ShowSceneDescription(Scene sceneName)
        {
            
            Console.WriteLine(sceneName.Description);
        }

        public virtual void GetConnectScene(Scene currentScene)
        {
            // 자식 클래스에서 커넥트씬 리스트에 씬 넣기
        }
    }

    public class LocationScene : Scene
    {
        // UI 및 연결된 씬으로만 이동 가능.

    }

    public class TownScene : LocationScene
    {
        string description = "이곳은 마을입니다.무기를 정비하고 휴식을 취할 수 있습니다.";


        //ConnectScene.Add("RestScene");
        //ConnectScene.Add("DungeonScene");
        //ConnectScene.Add("CharacterScene");
        //ConnectScene.Add("InventoryScene");

        public override void GetConnectScene(Scene currentScene)
        {
            for(int i = 0; i < Enum.GetValues(typeof(ConnectedScene)).Length; i++)
            {
                ConnectScene.Add(Enum.GetName(typeof(ConnectedScene), i));
            }
        }

        enum ConnectedScene
        {
            RestScene,
            DungeonScene,
            UIScene,
            InventoryScene
        }
    }


    public class UIScene : Scene
    {
        // 이전 장소씬으로만 이동 가능.
    }

    public class Inventory : UIScene
    {
        string description = "이곳은 장비창입니다. 장비를 관리할 수 있습니다.";
    }

    public class CharacterScene : UIScene
    {
        string description = "이곳은 캐릭터창입니다. 당신의 정보를 확인할 수 있습니다.";
    }

    public class SceneChoice
    {

    }

    internal class Program
    {
        static void Main(string[] args)
        {

            SceneManager SM = new SceneManager();
            Scene currentScene = new TownScene();

            SM.EnterScene += currentScene.ShowSceneDescription; 

            bool quite = false;

            //씬 변수 선언
            //Scene currentScene = Scene StartScene;

            //게임 루프
            while (!quite)
            {

                //씬 소개 출력(현재씬) //씬 선택지 출력(현재씬)
                SM.TriggerEnterScene(currentScene);
                
                //씬 선택지 입력, //입력을 씬 매개변수에 입력.
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out int input))
                {
                    currentScene = (currentScene.ConnectScene)input;
                }


            }
        }
    }
}
