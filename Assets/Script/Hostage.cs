using UnityEngine;
using UnityEngine.InputSystem;

public class Hostage : MonoBehaviour
{
    [Header("Follow Settings")]
    public float followSpeed = 4f;
    public float stopDistance = 1.5f; // don't let hostage stack directly on top of player

    [Header("Interact Prompt")]
    public GameObject interactPrompt; // a small "Press E" world-space UI element, child of this GameObject

    private Transform player;
    private bool isFollowing = false;
    private bool playerInRange = false;

    void Update()
    {
        // Check for interact input only while player is nearby and not already following
        if (playerInRange && !isFollowing && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            StartFollowing();
        }

        if (isFollowing && player != null)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * followSpeed * Time.deltaTime;
        }
    }

    void StartFollowing()
    {
        isFollowing = true;
        if (interactPrompt != null) interactPrompt.SetActive(false); // hide prompt once picked up
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            playerInRange = true;

            if (!isFollowing && interactPrompt != null)
            {
                interactPrompt.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (!isFollowing && interactPrompt != null)
            {
                interactPrompt.SetActive(false);
            }
        }
    }

    public bool IsFollowing()
    {
        return isFollowing;
    }
}