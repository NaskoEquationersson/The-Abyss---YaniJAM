using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private List<DialogueLine> dialogueLines;
    [SerializeField] private bool triggerOnStart = true; // Oyun başlar başlamaz çalışsın mı?

    private void Start()
    {
        if (triggerOnStart && dialogueManager != null)
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogueLines);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Eğer oyuncu belirli bir alana (örneğin helikopter pistine) girince konuşma başlayacaksa:
        if (other.CompareTag("Player"))
        {
            TriggerDialogue();
            gameObject.GetComponent<Collider>().enabled = false; // Tekrar tekrar tetiklenmesin
        }
    }
}