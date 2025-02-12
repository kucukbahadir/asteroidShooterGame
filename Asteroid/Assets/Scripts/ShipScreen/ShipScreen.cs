using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShipScreen : MonoBehaviour
{
    [SerializeField] private Question[] questions;

    private Question currentQuestion;
    
    private void Start()
    {
         currentQuestion = questions[Random.Range(0, questions.Length)];
    }

    public void AnswerA()
    {
        if (!currentQuestion.answersA.answerIsRight) return;
        print("a");
    }

    public void AnswerB()
    {
        if (!currentQuestion.answersB.answerIsRight) return;
        print("b");
    }
}
