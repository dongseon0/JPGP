using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class VisualNovelSystem : MonoBehaviour
{
    [System.Serializable]
    public class DialogueLine
    {
        public string speaker;              // 캐릭터 이름
        public string text;                 // 대사 내용
        public Sprite characterSprite;      // 캐릭터 이미지
        public Sprite backgroundSprite;     // 배경 이미지
    }

    public List<DialogueLine> story;

    public Image backgroundImage;
    public Image characterImage;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Button skipButton;

    private int currentIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    private void Start()
    {
        skipButton.onClick.AddListener(SkipTyping);
        ShowLine();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))  // ← 여기!
        {
            if (isTyping)
            {
                SkipTyping();
            }
            else
            {
                ShowLine();
            }
        }
    }


    void ShowLine()
    {
        if (currentIndex >= story.Count)
        {
            SceneManager.LoadScene("PlayScene");  
            return;
        }

        DialogueLine line = story[currentIndex];

        nameText.text = line.speaker;
        dialogueText.text = "";

        if (line.characterSprite != null)
            characterImage.sprite = line.characterSprite;

        if (line.backgroundSprite != null)
            backgroundImage.sprite = line.backgroundSprite;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeLine(line.text));
        currentIndex++;
    }


    IEnumerator TypeLine(string text)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.02f);
        }

        isTyping = false;
    }

    void SkipTyping()
    {
        if (!isTyping) return;

        StopCoroutine(typingCoroutine);
        dialogueText.text = story[currentIndex - 1].text;
        isTyping = false;
    }
}
