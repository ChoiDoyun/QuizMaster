using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("질문")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("보기")]
    [SerializeField] GameObject[] answerButtons;

    [Header("버튼 색깔")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    [SerializeField] Sprite problemTimerSprite;
    [SerializeField] Sprite solutionTimerSprite;
    Timer timer;
    bool chooseAnswer = false;

    [Header("점수")]
    [SerializeField] TextMeshProUGUI socreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    void Start()
    {
        timer = FindFirstObjectByType<Timer>();
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
        GetNExtQuestion();
    }

    private void Update()
    {
        if (timer.isProblemTime)
            timerImage.sprite = problemTimerSprite;
        else
            timerImage.sprite = solutionTimerSprite;
        timerImage.fillAmount = timer.fillAmount;

        if (timer.loadNextQuestion)
        {
            if (questions.Count <= 0)
            {
                GameManager.Instance.ShowEndSceen();
            }
            else
            {
                timer.loadNextQuestion = false;
                GetNExtQuestion();
            }
        }

        if (timer.isProblemTime == false && chooseAnswer == false)
        {
            DisplaySolution(-1);
        }
    }

    private void GetNExtQuestion()
    {
        if (questions.Count <= 0)
        {
            Debug.Log("남은 문제가 없습니다.");
            return;
        }

        chooseAnswer = false;
        SetButtonState(true);
        SetDefaultButtonSprites();
        GetRandomQuestion();
        OnDisplayQuestion();
        scoreKeeper.IncrementQuestionSeen();
        progressBar.value++;
    }

    private void GetRandomQuestion()
    {
       int Randomindex = UnityEngine.Random.Range(0, questions.Count);
        currentQuestion = questions[Randomindex];
        questions.RemoveAt(Randomindex);
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
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI> ().text = currentQuestion.GetAnswers(i);
        }
    }

    public void OnAnswerButtonClick(int index)
    {
        chooseAnswer = true;
        DisplaySolution(index);
        timer.CancelTimer();
        socreText.text = $"Score: { scoreKeeper.CalculateScore()}%";
    }

    private void DisplaySolution(int index)
    {
        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "정답입니다!";
            answerButtons[index].GetComponent<Image>().sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            questionText.text = "틀렸습니다. 정답은: " + currentQuestion.GetCorrectAnswer();
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