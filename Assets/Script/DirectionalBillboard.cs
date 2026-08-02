using UnityEngine;

public class DirectionalBillboard : MonoBehaviour
{
    [Header("Sprite Setup")]
    public SpriteRenderer spriteRenderer;

    [Tooltip("Index order: 0=South,1=SW,2=W,3=NW,4=North,5=NE,6=East,7=SE")]
    public Sprite[] idleSprites = new Sprite[8];
    public Sprite[] walkSprites = new Sprite[8];

    [Header("References")]
    public Transform cameraTransform;
    public MehmetEken playerMovement;

    [Header("Movement Threshold")]
    public float movingThreshold = 0.1f;

    void LateUpdate()
    {
        if (cameraTransform == null || spriteRenderer == null) return;

        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0;
        if (camForward.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(camForward);
        }

        Vector3 charForward = playerMovement != null ? playerMovement.LastMoveDirection : Vector3.forward;
        bool isMoving = charForward.sqrMagnitude > movingThreshold * movingThreshold;
        charForward.y = 0;

        if (charForward.sqrMagnitude > 0.001f)
        {
            float angle = Vector3.SignedAngle(camForward, charForward, Vector3.up);
            int index = AngleToSpriteIndex(angle);

            Sprite[] activeSet = isMoving ? walkSprites : idleSprites;

            if (activeSet[index] != null)
            {
                spriteRenderer.sprite = activeSet[index];
            }
        }
    }

    int AngleToSpriteIndex(float angle)
    {
        if (angle < 0) angle += 360;
        return Mathf.RoundToInt(angle / 45f) % 8;
    }
}