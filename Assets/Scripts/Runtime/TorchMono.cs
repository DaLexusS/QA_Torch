using UnityEngine;

public class TorchMono : MonoBehaviour
{
    public Torch Logic { get; private set; } = new Torch();
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color litColor = Color.yellow;
    [SerializeField] private Color unlitColor = Color.gray;

    private void Start()
    {
        UpdateVisuals();
    }

    public void Interact()
    {
        if (Logic.TryLight())
        {
            UpdateVisuals();
        }
    }

    public void UpdateVisuals()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Logic.IsLit ? litColor : unlitColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Logic.SetPlayerNearby(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Logic.SetPlayerNearby(false);
        }
    }
}