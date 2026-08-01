using UnityEngine;

public class DirectionalBillboard : MonoBehaviour
{
    [Header("Sprite Setup")]
    public SpriteRenderer spriteRenderer;

    [Tooltip("Assign in Inspector. Index order: 0=South,1=SW,2=W,3=NW,4=North,5=NE,6=East,7=SE")]
    public Sprite[] directionSprites = new Sprite[8];

    [Header("References")]
    public Transform cameraTransform;   // drag Main Camera here
    public MehmetEken playerMovement;   // drag the player object here (or reference logicalFacingDirection directly)

    void LateUpdate()
    {
        if (cameraTransform == null || spriteRenderer == null) return;

        // Keep sprite flat-facing camera (no tilt)
        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0;
        if (camForward.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(camForward);
        }

        // Figure out character's logical facing vs camera, pick sprite index
        Vector3 charForward = playerMovement != null ? playerMovement.LastMoveDirection : Vector3.forward;
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
        return Mathf.RoundToInt(angle / 45f) % 8;
    }
}