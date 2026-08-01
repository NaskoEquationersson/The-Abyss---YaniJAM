using UnityEngine;

public class NPCBillboard : MonoBehaviour
{
    [Header("Sprite Setup")]
    public SpriteRenderer spriteRenderer;

    [Tooltip("Index order: 0=South, 1=West, 2=North, 3=East")]
    public Sprite[] directionSprites = new Sprite[4];

    [Header("References")]
    public Transform cameraTransform;

    [Tooltip("Direction this character is facing. Player script updates this, or leave static for a stationary NPC.")]
    public Vector3 facingDirection = Vector3.forward;

    void LateUpdate()
    {
        if (cameraTransform == null || spriteRenderer == null) return;

        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0;
        if (camForward.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(camForward);
        }

        Vector3 charForward = facingDirection;
        charForward.y = 0;

        if (charForward.sqrMagnitude > 0.001f)
        {
            float angle = Vector3.SignedAngle(camForward, charForward, Vector3.up);
            int index = AngleToSpriteIndex(angle);

            if (directionSprites[index] != null)
            {
                spriteRenderer.sprite = directionSprites[index];
            }
        }
    }

    int AngleToSpriteIndex(float angle)
    {
        if (angle < 0) angle += 360;
        return Mathf.RoundToInt(angle / 90f) % 4;
    }
}