using UnityEngine;

public class Object_Crafter : Object_NPC, IInteractable
{
    private Animator anim;
    private Inventory_Player inventory;
    private Inventory_Storage storage;


    protected override void Awake()
    {
        base.Awake();
        storage = GetComponent<Inventory_Storage>();
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("isCrafter", true);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        inventory = player.GetComponent<Inventory_Player>();
        storage.SetInventory(inventory);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        ui.SwitchOffAllTooltip();
        ui.storageUI.gameObject.SetActive(false);
    }

    public void Interact()
    {
        Debug.Log("Interacting with Crafter");
        if (inventory == null || storage == null)
            return;
        ui.storageUI.SetupStorage(inventory, storage);
        ui.storageUI.gameObject.SetActive(true);
    }
}
