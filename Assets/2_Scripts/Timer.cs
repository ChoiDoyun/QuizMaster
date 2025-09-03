using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float problemTIme = 10f;
    [SerializeField] float solutionTime = 3f;
    float time = 0;

    [HideInInspector] public bool isProblemTime = true;
    [HideInInspector] public float fillAmount;
    [HideInInspector] public bool loadNextQuestion;

    private void Start()
    {
        time = problemTIme;
    }

    private void Update()
    {
        TimerCountDown();
        UpdateFillAmount();
    }

    private void UpdateFillAmount()
    {
        if (isProblemTime)
        {
            fillAmount = time / problemTIme;
        }
        else
        {
            fillAmount = time / solutionTime;
        }
    }

    private void TimerCountDown()
    {
        Debug.Log("Time remaining: " + time);
        time -= Time.deltaTime;
        if (time <= 0)
        {
            if (isProblemTime)
            {
                isProblemTime = false;
                time = solutionTime;
            }
            else
            {
                isProblemTime = true;
                time = problemTIme;
                loadNextQuestion = true;
            }
        }
    }

    public void CancelTimer()
    {
        time = 0;
    }
}
