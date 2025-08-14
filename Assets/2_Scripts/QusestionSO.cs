using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question" , fileName = "New Quesstion")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6) ]
    [SerializeField] string question = "���⿡ ������ �����ּ���.";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswerIndex = 0;

    public string GetQuestion()
    {
        return question;
    }

    public string GetAnswers(int index)
    {
        return answers[index];
    }

    public string GetCorrectAnswer()
    {
        return answers[correctAnswerIndex];
    }

    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }
}
