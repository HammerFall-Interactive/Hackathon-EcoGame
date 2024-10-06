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
        questionText = "Какое из следующих животных является наиболее уязвимым к изменениям климата?",
        answerOptions = new List<string> { "Слон", "Пингвин", "Лев", "Кошка" },
        correctAnswerIndex = 1
    },
    new Question
    {
        questionText = "Что такое парниковый эффект?",
        answerOptions = new List<string> { "Процесс образования облаков", "Нагревание атмосферы Земли", "Исчезновение лесов", "Загрязнение воды" },
        correctAnswerIndex = 1
    },
    new Question
    {
        questionText = "Какое из следующих действий может помочь уменьшить углеродный след?",
        answerOptions = new List<string> { "Использование общественного транспорта", "Увеличение потребления мяса", "Использование одноразовых пластиковых изделий", "Игнорирование переработки" },
        correctAnswerIndex = 0
    },
    new Question
    {
        questionText = "Какой газ является основным виновником глобального потепления?",
        answerOptions = new List<string> { "Кислород", "Водяной пар", "Углекислый газ", "Азот" },
        correctAnswerIndex = 2
    },
    new Question
    {
        questionText = "Что такое устойчивое развитие?",
        answerOptions = new List<string> { "Развитие, не нарушающее экосистем", "Развитие, основанное на быстрых темпах роста", "Развитие, которое игнорирует экосистемы", "Развитие только для людей" },
        correctAnswerIndex = 0
    },
    new Question
    {
        questionText = "Какое из следующих действий может помочь сохранить биоразнообразие?",
        answerOptions = new List<string> { "Разрушение естественных мест обитания", "Охрана видов, находящихся под угрозой исчезновения", "Вырубка лесов", "Увеличение использования пестицидов" },
        correctAnswerIndex = 1
    },
    new Question
    {
        questionText = "Какое из следующих животных является ключевым видом для экосистемы?",
        answerOptions = new List<string> { "Лев", "Бобер", "Крыса", "Слон" },
        correctAnswerIndex = 1
    },
    new Question
    {
        questionText = "Что такое экосистема?",
        answerOptions = new List<string> { "Система, состоящая из живых организмов и их окружения", "Только система, состоящая из растений", "Только система, состоящая из животных", "Система, состоящая только из воды" },
        correctAnswerIndex = 0
    },
    new Question
    {
        questionText = "Какой из следующих источников энергии является возобновляемым?",
        answerOptions = new List<string> { "Уголь", "Нефть", "Солнечная энергия", "Природный газ" },
        correctAnswerIndex = 2
    },
    new Question
    {
        questionText = "Какое из следующих действий может помочь сохранить воду?",
        answerOptions = new List<string> { "Сокращение времени душа", "Увеличение полива газона", "Игнорирование утечек", "Использование большего количества воды в домашних делах" },
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
            Debug.Log("Правильный ответ!");
            correctanswers++;
        }
        else
        {
            Debug.Log("Неправильный ответ.");
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
        questionTextTMP.text = "Поздравляем! Вы прошли тест и набрали " + correctanswers.ToString() + " Баллов из " + questions.Count.ToString();

        //Переход на предыдущую сцену.

        //FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None)
    }
}
