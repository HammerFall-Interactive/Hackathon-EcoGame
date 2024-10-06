using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

[System.Serializable]
public class Question
{
    public string questionText;
    public List<string> answerOptions;
    public int correctAnswerIndex;
}

public class QuizManager : MonoBehaviour
{


    public TMP_Text questionTextTMP;
    public Text currentQuestion;
    public Button[] answerButtons;
    private int currentQuestionIndex;
    private int correctanswers;

    public List<Question> questions = new List<Question>
{
    new Question
    {
        questionText = "����� �� ��������� �������� �������� �������� �������� � ���������� �������?",
        answerOptions = new List<string> { "����", "�������", "���", "�����" },
        correctAnswerIndex = 1
    },
    new Question
    {
        questionText = "��� ����� ���������� ������?",
        answerOptions = new List<string> { "������� ����������� �������", "���������� ��������� �����", "������������ �����", "����������� ����" },
        correctAnswerIndex = 1
    },
    new Question
    {
        questionText = "����� �� ��������� �������� ����� ������ ��������� ���������� ����?",
        answerOptions = new List<string> { "������������� ������������� ����������", "���������� ����������� ����", "������������� ����������� ����������� �������", "������������� �����������" },
        correctAnswerIndex = 0
    },
    new Question
    {
        questionText = "����� ��� �������� �������� ���������� ����������� ����������?",
        answerOptions = new List<string> { "��������", "������� ���", "���������� ���", "����" },
        correctAnswerIndex = 2
    },
    new Question
    {
        questionText = "��� ����� ���������� ��������?",
        answerOptions = new List<string> { "��������, �� ���������� ���������", "��������, ���������� �� ������� ������ �����", "��������, ������� ���������� ����������", "�������� ������ ��� �����" },
        correctAnswerIndex = 0
    },
    new Question
    {
        questionText = "����� �� ��������� �������� ����� ������ ��������� ���������������?",
        answerOptions = new List<string> { "���������� ������������ ���� ��������", "������ �����, ����������� ��� ������� ������������", "������� �����", "���������� ������������� ����������" },
        correctAnswerIndex = 1
    },
    new Question
    {
        questionText = "����� �� ��������� �������� �������� �������� ����� ��� ����������?",
        answerOptions = new List<string> { "���", "�����", "�����", "����" },
        correctAnswerIndex = 1
    },
    new Question
    {
        questionText = "��� ����� ����������?",
        answerOptions = new List<string> { "�������, ��������� �� ����� ���������� � �� ���������", "������ �������, ��������� �� ��������", "������ �������, ��������� �� ��������", "�������, ��������� ������ �� ����" },
        correctAnswerIndex = 0
    },
    new Question
    {
        questionText = "����� �� ��������� ���������� ������� �������� ��������������?",
        answerOptions = new List<string> { "�����", "�����", "��������� �������", "��������� ���" },
        correctAnswerIndex = 2
    },
    new Question
    {
        questionText = "����� �� ��������� �������� ����� ������ ��������� ����?",
        answerOptions = new List<string> { "���������� ������� ����", "���������� ������ ������", "������������� ������", "������������� �������� ���������� ���� � �������� �����" },
        correctAnswerIndex = 0
    }
};
    void Start()
    {
        currentQuestionIndex = 0;
        DisplayQuestion();

    }

    void DisplayQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            Question currentQuestion = questions[currentQuestionIndex];
            questionTextTMP.text = currentQuestion.questionText;

            for (int i = 0; i < answerButtons.Length; i++)
            {
                if (i < currentQuestion.answerOptions.Count)
                {
                    answerButtons[i].GetComponentInChildren<TMP_Text>().text = currentQuestion.answerOptions[i];
                    int index = i;
                    answerButtons[i].onClick.RemoveAllListeners();
                    answerButtons[i].onClick.AddListener(() => CheckAnswer(index));
                    answerButtons[i].gameObject.SetActive(true);
                }
                else
                {
                    answerButtons[i].gameObject.SetActive(false);
                }
            }
        }
    }
    void CheckAnswer(int selectedAnswerIndex)
    {
        
        if (selectedAnswerIndex == questions[currentQuestionIndex].correctAnswerIndex)
        {
            Debug.Log("���������� �����!");
            correctanswers++;
        }
        else
        {
            Debug.Log("������������ �����.");
        }
        currentQuestionIndex++;
        currentQuestion.text = currentQuestionIndex.ToString() + "/" + questions.Count.ToString();
        if (currentQuestionIndex < questions.Count)
        {
            DisplayQuestion();
        }
        else
        {
            HandleQuizEnd();
        }
    }
    void HandleQuizEnd()
    {
        foreach (Button button in answerButtons)
        {
            button.gameObject.SetActive(false);
        }
        questionTextTMP.text = "�����������! �� ������ ���� � ������� " + correctanswers.ToString() + " ������ �� " + questions.Count.ToString();

        //������� �� ���������� �����.

        //FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None)
    }
}
