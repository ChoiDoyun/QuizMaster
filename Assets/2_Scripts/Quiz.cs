using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;
    [SerializeField] GameObject[] answerButtons;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    void Start()
    {
        GetNExtQuestion();
    }

    private void GetNExtQuestion()
    {
        SetButtonState(true);
        SetDefaultButtonSprites();
        OnDisplayQuestion();
    }

    private void SetDefaultButtonSprites()
    {
        foreach (GameObject obj in answerButtons)
        {
            obj.GetComponent<Image>().sprite = defaultAnswerSprite;
        }
    }

    private void OnDisplayQuestion()
    {
        questionText.text = question.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public void OnAnswerButtonClick(int index)
    {
        answerButtons[index].GetComponent<Image>().sprite = correctAnswerSprite;

        if (index == question.GetCorrectAnswerIndex())
        {
            questionText.text = "정답입니다!";    
        }
        else
        {
            questionText.text = "틀렸습니다. 정답은: " + question.GetCorrectAnswer();
        }
        SetButtonState(false);
    }

    private void SetButtonState(bool state)
    {
        foreach (GameObject obj in answerButtons)
        {
            obj.GetComponent<Button>().interactable = state;
        }
    }
}