using UnityEngine;
using System.Collections;
[RequireComponent(typeof(UniqueID))]

public class Chest : InventoryHolder, IInteract
{
    private bool audioPlaying;
    public bool Interact(Interactor interactor)
    {
        OnDynamicInventoryDisplayRequested?.Invoke(InventorySystem, 0);
        StartCoroutine(ChestAudio());
        return true;
    }

    private IEnumerator ChestAudio()
    {
        if (!audioPlaying)
        {
            audioPlaying = true;
            AudioManager.Instance.PlaySFX("Chest");
            yield return new WaitForSeconds(0.3f);
            audioPlaying = false;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        SaveLoad.OnLoadGame += LoadInventory;
    }

    private void Start()
    {
        var chestSaveData = new InventorySaveData(primaryInventorySystem, transform.position);

        SaveGameManager.data.chestDictionary.Add(GetComponent<UniqueID>().ID, chestSaveData);
    }

    protected override void LoadInventory(SaveData data) // From the corresponding ID, get the data of the chest and assign it the values from loaded data
    {
        if (data.chestDictionary.TryGetValue(GetComponent<UniqueID>().ID, out InventorySaveData chestData))
        {
            this.primaryInventorySystem = chestData.invSystem;
            this.transform.position = chestData.position;
        }
    }
}




