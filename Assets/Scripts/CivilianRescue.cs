using UnityEngine;

public class CivilianRescue : MonoBehaviour, IInteractable
{
    private SpriteRenderer civilianSpriteRenderer;
    private bool isRescued = false;

    private void Awake()
    {
        civilianSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Interact()
    {

        if (isRescued) return;

        isRescued = true;

        if (civilianSpriteRenderer != null)
        {
            civilianSpriteRenderer.color = Color.green;
        }

        if (RescueManager.Instance != null)
        {
            RescueManager.Instance.RegisterRescue();
        }
    }
}
