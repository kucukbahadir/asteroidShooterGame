using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShipScreen : MonoBehaviour
{
    [SerializeField] private ShipScreenUI shipScreenUI;
    [SerializeField] private Question[] questions;

    private Question currentQuestion;
    
    private void Start()
    {
         GetNewQuestion();
    }

    public void AnswerA()
    {
        if (!currentQuestion.answersA.answerIsRight) return;
        GetNewQuestion();
    }

    public void AnswerB()
    {
        if (!currentQuestion.answersB.answerIsRight) return;
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
