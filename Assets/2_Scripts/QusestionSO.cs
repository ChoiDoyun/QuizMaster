using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question" , fileName = "New Quesstion")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6) ]
    [SerializeField] string question = "���⿡ ������ �����ּ���.";
}
