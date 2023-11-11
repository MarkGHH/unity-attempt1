using System.Collections.Generic;

/// <summary>
/// This script can be expanded so that new data is being saved, in addition to expanding this script, an object specific script should be created to get the related data.
/// </summary>
public class SaveData
{
    public List<string> collectedItems;
    public SerializableDictionary<string, ItemPickUpSaveData> activeItems;
    public SerializableDictionary<string, InventorySaveData> chestDictionary;
    public SerializableDictionary<string, PlayerSaveData> playerDictionary;
    public InventorySaveData playerInventory;

    public SaveData()
    {
        collectedItems = new List<string>();
        activeItems = new SerializableDictionary<string, ItemPickUpSaveData>();
        chestDictionary = new SerializableDictionary<string, InventorySaveData>();
        playerDictionary = new SerializableDictionary<string, PlayerSaveData>();
        playerInventory = new InventorySaveData();
    }
}
