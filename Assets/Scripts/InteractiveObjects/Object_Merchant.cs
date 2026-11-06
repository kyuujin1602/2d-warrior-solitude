using UnityEngine;

public class Object_Merchant : Object_NPC, IInteractable
{
    private Inventory_Player playerInventory;
    private Inventory_Shop shopInventory;

    protected override void Awake()
    {
        base.Awake();
        shopInventory = GetComponent<Inventory_Shop>();
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Z))
            shopInventory.FillShopList();
    }

    public void Interact()
    {
        Debug.Log("Interacting with Merchant");
        ui.merchantUI.SetupMerchantUI(shopInventory, playerInventory);
        ui.merchantUI.gameObject.SetActive(true);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        playerInventory = player.GetComponent<Inventory_Player>();
        shopInventory.SetPlayerInventory(playerInventory);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        ui.SwitchOffAllTooltip();
        ui.merchantUI.gameObject.SetActive(false);
    }
}
