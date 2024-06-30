using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Random = System.Random;

public class ItemCharacteristics
{
    public KeyValuePair<string, int> Name; 
    public KeyValuePair<string, int> FirstSentence; 
    public KeyValuePair<string, int> SecondSentence;
    public KeyValuePair<string, int> ThirdSentence; 
    public KeyValuePair<string, int> FourthSentence;

    public ItemCharacteristics(KeyValuePair<string, int> name, KeyValuePair<string, int> firstSentence, KeyValuePair<string, int> secondSentence,
        KeyValuePair<string, int> thirdSentence, KeyValuePair<string, int> fourthSentence)
    {
        Name = name;
        FirstSentence = firstSentence;
        SecondSentence = secondSentence;
        ThirdSentence = thirdSentence;
        FourthSentence = fourthSentence;
    }
    
    public int GetTotalScore()
    {
        return Name.Value + FirstSentence.Value + SecondSentence.Value + ThirdSentence.Value + FourthSentence.Value;
    }
}

public class Note : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI itemInfoText;

    private ItemCharacteristics currentCharacteristics;
    public int oldScore = 0;
    public bool guilty;

    private Dictionary<string, KeyValuePair<string, int>[]> characteristics = new Dictionary<string, KeyValuePair<string, int>[]>
    {
        { "Name", new KeyValuePair<string, int>[]
            {
                new KeyValuePair<string, int>("Алексей", 0),
                new KeyValuePair<string, int>("Мария", 0),
                new KeyValuePair<string, int>("Дмитрий", 0),
                new KeyValuePair<string, int>("Иван", 0),
                new KeyValuePair<string, int>("Рамазан", 0),
                new KeyValuePair<string, int>("Карина", 0),
                new KeyValuePair<string, int>("Антон", 0),
                new KeyValuePair<string, int>("Адиль", 0),
                new KeyValuePair<string, int>("Болат", 0),
                new KeyValuePair<string, int>("Айжан", 0),
                new KeyValuePair<string, int>("Адема", 0),
                new KeyValuePair<string, int>("Мирас", 0),
                new KeyValuePair<string, int>("Илья", 0),
                new KeyValuePair<string, int>("Данил", 0),
                new KeyValuePair<string, int>("Айдар", 0),
                new KeyValuePair<string, int>("Бека", 0),
                new KeyValuePair<string, int>("Даниал", 0),
            }
        },
        { "First", new KeyValuePair<string, int>[]
            {
                new KeyValuePair<string, int>("Ученик часто отвлекает класс во время уроков.", 100),
                new KeyValuePair<string, int>("Проявляет агрессию по отношению к одноклассникам.", 200),
                new KeyValuePair<string, int>("Игнорирует замечания учителя и нарушает дисциплину.", 150),
                new KeyValuePair<string, int>("Разговаривает без разрешения.", 50),
                new KeyValuePair<string, int>("Использует грубую лексику.", 75),
                new KeyValuePair<string, int>("Отказывается выполнять задания.", 80),
                new KeyValuePair<string, int>("Участвует в конфликтах с одноклассниками.", 80),
                new KeyValuePair<string, int>("Нарушает дисциплину на переменах.", 70),
                new KeyValuePair<string, int>("Отказывается подчиняться правилам школы.", 90),
                new KeyValuePair<string, int>("Не проявляет уважения к учителям.", 100),
                new KeyValuePair<string, int>("Повреждает школьное имущество.", 120),
                new KeyValuePair<string, int>("Постоянно опаздывает на уроки.", 110),
                new KeyValuePair<string, int>("Обычный спокойный ученик.", 20),
                new KeyValuePair<string, int>("Добрый волонтер.", 5),
                new KeyValuePair<string, int>("Тихоня класса.", 10),
                new KeyValuePair<string, int>("Спортсмен школы.", 5),
                new KeyValuePair<string, int>("Помогает учителям с заданиями", 1),
                new KeyValuePair<string, int>("Постоянно сидит на дополнительных занятиях", 1),
                new KeyValuePair<string, int>("Проводит внутришкольные мероприятия", 1),
                new KeyValuePair<string, int>("Делает все задания в классе", 1),
                new KeyValuePair<string, int>("Никогда не отвлекает одноклассников", 1),
                new KeyValuePair<string, int>("Совсем не агресивный", 1),
            }
        },
        { "Second", new KeyValuePair<string, int>[]
            {
                new KeyValuePair<string, int>("пришел/пришла без рюкзака сегодня", 20),
                new KeyValuePair<string, int>("сегодня подрался/подралась", 200),
                new KeyValuePair<string, int>("бросил/бросила пенал в одноклассника", 100),
                new KeyValuePair<string, int>("ушел/ушла с уроков гораздо раньше", 80),
                new KeyValuePair<string, int>("парил/парила в туалете", 90),
                new KeyValuePair<string, int>("наорал/а матом на уборщицу", 130),
                new KeyValuePair<string, int>("задержал/а всех после звонка", 80),
                new KeyValuePair<string, int>("смешал/а химикаты в лаборатории", 180),
                new KeyValuePair<string, int>("продал/а чужой учебник", 120),
                new KeyValuePair<string, int>("включил/а сирену во время урока", 160),
                new KeyValuePair<string, int>("написал/а анонимку учителю", 110),
                new KeyValuePair<string, int>("пришел/пришла в форме другой школы", 140),
            }
        },
        { "Third", new KeyValuePair<string, int>[]
            {

            }
        },
        { "Fourth", new KeyValuePair<string, int>[]
            {
                
            }
        }
    };

    public void GenerateRandomCharacteristics()
    {
        KeyValuePair<string, int> randomName = GetRandomCharacteristic("Name");
        KeyValuePair<string, int> randomFirst = GetRandomCharacteristic("First");
        KeyValuePair<string, int> randomSecond = GetRandomCharacteristic("Second");
        KeyValuePair<string, int> randomThird = GetRandomCharacteristic("Third");
        KeyValuePair<string, int> randomFourth = GetRandomCharacteristic("Fourth");

        currentCharacteristics = new ItemCharacteristics(randomName, randomFirst, randomSecond, randomThird, randomFourth);
        UpdateItemInfoText(currentCharacteristics);
        
        CheckIfCorrect();
    }

    private KeyValuePair<string, int> GetRandomCharacteristic(string category)
    {
        KeyValuePair<string, int>[] possibleCharacteristics = characteristics[category];
        Random rnd = new Random();
        int randomIndex = rnd.Next(0, possibleCharacteristics.Length);
        return possibleCharacteristics[randomIndex];
    }

    public void UpdateItemInfoText(ItemCharacteristics characteristics)
    {
        currentCharacteristics = characteristics;

        string newText = $"{characteristics.Name.Key}\n" +
                         $"{characteristics.FirstSentence.Key}\n" +
                         $"{characteristics.SecondSentence.Key}\n" +
                         $"{characteristics.ThirdSentence.Key}\n" +
                         $"{characteristics.FourthSentence.Key}";

        itemInfoText.text = newText;
    }

    public int GetScore()
    {
        return currentCharacteristics.GetTotalScore();
    }


    private void OnDisable()
    {
        _canvasGroup.DOFade(0f, 1);
    }
    
    private void CheckIfCorrect()
    {
        if (GetScore() > 400)
        {
            guilty = true;
        }
        else
        {
            guilty = false;
        }
    }
}
