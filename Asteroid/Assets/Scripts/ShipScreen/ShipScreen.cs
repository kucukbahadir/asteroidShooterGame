using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class ShipScreen : MonoBehaviour
{
    public UnityEvent OnRightAnswer;
    public UnityEvent OnWrongAnswer;

    [SerializeField] private ShipScreenUI shipScreenUI;
    [SerializeField] private Question[] questions;

    private Question currentQuestion;
    
    private void Start()
    {
         GetNewQuestion();
    }

    public void AnswerA()
    {
        if (!currentQuestion.answersA.answerIsRight)
        {
            OnWrongAnswer?.Invoke();
            return;
        } 
        OnRightAnswer?.Invoke();
        GetNewQuestion();
    }

    public void AnswerB()
    {
        if (!currentQuestion.answersB.answerIsRight)
        {
            OnWrongAnswer?.Invoke();
            return;
        } 
        OnRightAnswer?.Invoke();
        GetNewQuestion();
    }

    private void GetNewQuestion()
    {
        var NewQuestion = GetRandomQuestion();
        currentQuestion = NewQuestion;
        shipScreenUI.UpdateScreenUI(NewQuestion.question, NewQuestion.answersA.answer, NewQuestion.answersB.answer);
    }

    private Question GetRandomQuestion()
    {
        return questions[Random.Range(0, questions.Length)];
    }
}
