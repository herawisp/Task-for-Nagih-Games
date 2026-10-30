using UnityEngine;

public enum Rarity { Metal, Bronze, Silver, Gold, Diamond }

[CreateAssetMenu(fileName = "RarityData", menuName = "Gacha/Rarity Data")]
public class RarityData : ScriptableObject {

    //==============================================================================================================//
    //==============================================================================================================//

    public Sprite Sprite;
    public Rarity RarityType;
    public int Value;

    //==============================================================================================================//
    //==============================================================================================================//
}