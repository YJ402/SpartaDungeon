namespace SpartaDungeon_2_
{
    public class Scene
    {

    }

    public class LocationScene : Scene 
    {
        // UI 및 연결된 씬으로만 이동 가능.
    }

    public class TownScene : LocationScene
    {
        string description;

        enum ConnectScene
        {
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

    }

    public class CharacterScene : UIScene
    {

    }
}
