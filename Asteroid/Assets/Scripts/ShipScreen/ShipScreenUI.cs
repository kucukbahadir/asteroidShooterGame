using UnityEngine;
using TMPro;

public class ShipScreenUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI answerAText;
    [SerializeField] private TextMeshProUGUI answerBText;
    
    public void UpdateScreenUI(string question, string answerA, string answerB)
    {
        questionText.text = $"Question: {question}";
        answerAText.text = $"A: {answerA}";
        answerBText.text = $"B: {answerB}";
    }
}
