public enum EquipSlot
{
    head,
    body
}
public class Armor
{
    public EquipSlot FillSlot = EquipSlot.body;

    public void Equipping(Player player)
    {
        // 착용한 부위인지 검사
        if (!player.equippedSlot.ContainsValue(FillSlot)) 
        {
            player.equippedSlot.Add("body", FillSlot);
        }
        else
        {
            Console.WriteLine("이미 착용한 장비가 있습니다!!!");
        }
    }
}

public class IronArmor : Armor { }
public class GoldArmor : Armor { }

public class Player
{
    public Dictionary<string, EquipSlot> equippedSlot = new Dictionary<string, EquipSlot>();
}
internal class Program
{
    static void Main()
    {
        Player player = new Player();

        Armor ironArmor = new IronArmor();
        Armor goldArmor = new GoldArmor();

        ironArmor.Equipping(player); // 아이언 아머 착용
        goldArmor.Equipping(player); // 골드 아머 착용 => 이미 착용한 부위 => 착용 실패

    }
}
