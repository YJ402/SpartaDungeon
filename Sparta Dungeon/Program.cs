namespace Sparta_Dungeon
{
    public class SceneManager
    {


        public Action<int> EnterScene; // 씬 입장시 발생하는 이벤트:(씬 소개 메서드, 씬 선택지 메서드)
        public enum SceneName
        {
            StartScene = 1,
            VillageScene,
            CharacterScene,
            InventoryScene,
            ShopScene,
            RestScene,
            DungeonScene
        }

        public void TriggerEnterScene(int i)
        {
            EnterScene?.Invoke(i);
        }
    }

    public class SceneInfo
    {
        public Dictionary<int, string> sceneInfo = new Dictionary<int, string>()
        {
            { 1, "Sparta Dungeon 게임에 오신 것을 환영합니다." },
            { 2, "이곳은 마을입니다. 무기를 정비하고 휴식을 취할 수 있습니다." },
            { 3, "이곳은 캐릭터창입니다. 당신의 정보를 확인할 수 있습니다." },
            { 4, "이곳은 장비창입니다. 장비를 관리할 수 있습니다." },
            { 5, "이곳은 상점입니다. 물품을 구매하고 판매할 수 있습니다." },
            { 6, "휴식 공간에 들어왔습니다. 돈을 지불하고 휴식을 취할 수 있습니다." },
            { 7, "이곳은 던전 입구입니다. 조심하세요." }
        };

        public void ShowSceneInfo(int i)
        {
            Console.WriteLine(sceneInfo[i]);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {

            SceneManager SM = new SceneManager();
            SceneInfo sceneInfo = new SceneInfo();

            SM.EnterScene += sceneInfo.ShowSceneInfo; 

            bool quite = false;

            //씬 변수 선언
            int currentScene = 1;

            //게임 루프
            while (!quite)
            {

                //씬 소개 출력(현재씬) //씬 선택지 출력(현재씬)
                SM.TriggerEnterScene(currentScene);
                //씬 선택지 입력, //입력을 씬 매개변수에 입력.
                currentScene = int.Parse(Console.ReadLine());

            }
        }
    }
}
