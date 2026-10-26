using UnityEngine;

public class Object_NPC : MonoBehaviour
{
    protected Transform player;
    protected UI ui;
    private bool isFacingRight = true;
    private Vector3 startPosition;

    [SerializeField] private Transform npc;
    [SerializeField] private GameObject interactTooltip;
    [SerializeField] private float tooltipFloatRange = 1f;
    [SerializeField] private float tooltipFloatSpeed = 8f;


    protected virtual void Awake()
    {
        ui = FindFirstObjectByType<UI>();
        startPosition = interactTooltip.transform.position;
        interactTooltip.SetActive(false);
    }

    private void HandleNPCFlip()
    {
        if (player == null || npc == null) return;

        if (player.position.x < npc.position.x && isFacingRight) // Facing left
        {
            npc.transform.Rotate(0f, 180f, 0f);
            isFacingRight = false;
        }
        else if (player.position.x > npc.position.x && !isFacingRight) // Facing right
        {
            npc.transform.Rotate(0f, 180f, 0f);
            isFacingRight = true;
        }
    }

    private void HandleToolTipFloat()
    {
        if (interactTooltip.activeSelf)
        {
            float yOffset = Mathf.Sin(Time.time * tooltipFloatSpeed) * tooltipFloatRange;
            interactTooltip.transform.position = startPosition + new Vector3(0f, yOffset, 0f);
        }
    }

    protected virtual void Update()
    {
        HandleNPCFlip();
        HandleToolTipFloat();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with: " + collision.name);
        player = collision.transform;
        interactTooltip.SetActive(true);
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        interactTooltip.SetActive(false);
    }
}
