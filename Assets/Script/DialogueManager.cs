using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct DialogueLine
{
    public string speakerName;     // Konuşan kişinin adı (Örn: "Kaptan", "Telsiz Operatörü")
    public Sprite speakerPortrait; // Konuşan kişinin vesikalık resmi
    [TextArea(3, 5)]
    public string sentence;        // Söylediği cümle
}

public class DialogueManager : MonoBehaviour
{
    [Header("UI Elemanları")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image portraitImage;
    [SerializeField] private Button nextButton;

    [Header("Yazı Hızı")]
    [SerializeField] private float typingSpeed = 0.03f; // Harflerin ekrana gelme hızı

    private Queue<DialogueLine> linesQueue = new Queue<DialogueLine>();
    private bool isTyping = false;
    private string currentSentence;

    private void Start()
    {
        if (nextButton != null)
            nextButton.onClick.AddListener(DisplayNextSentence);
    }

    // Diyalog zincirini başlatmak için bu fonksiyon çağrılır
    public void StartDialogue(List<DialogueLine> lines)
    {
        dialoguePanel.SetActive(true);
        linesQueue.Clear();

        foreach (DialogueLine line in lines)
        {
            linesQueue.Enqueue(line);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // Eğer yazı henüz tam yazılmadıysa, butona basınca yazıyı anında tamamla
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = currentSentence;
            isTyping = false;
            return;
        }

        // Liste bittiyse paneli kapat
        if (linesQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = linesQueue.Dequeue();
        nameText.text = currentLine.speakerName;
        currentSentence = currentLine.sentence;

        if (currentLine.speakerPortrait != null)
        {
            portraitImage.sprite = currentLine.speakerPortrait;
            portraitImage.gameObject.SetActive(true);
        }
        else
        {
            portraitImage.gameObject.SetActive(false);
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        isTyping = true;

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        Debug.Log("Diyalog tamamlandı. Tutorial adımı veya oyun devam ettirilebilir.");
    }
}