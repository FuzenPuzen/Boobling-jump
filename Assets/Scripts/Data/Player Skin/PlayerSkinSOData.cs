using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkinSOData", menuName = "PlayerSkinSOData")]
public class PlayerSkinSOData : ScriptableObject
{
    public int Cost;
    public GameObject SkinPrefab;
    [ValueDropdown("GetDropdownValues")]
    public string Name;
    private string[] GetDropdownValues()
    {
        // Возвращает массив строк, которые будут использоваться в выпадающем меню
        return new string[] { "Полицца", "Пиратник", "Ведьма", "Шарки", "Бэмби", "Даваскинчик", "Сантанинский"};
    }
}