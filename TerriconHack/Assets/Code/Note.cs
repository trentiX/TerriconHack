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
                new KeyValuePair<string, int>("Проявляет агрессию по отношению к одноклассникам.", 100),
                new KeyValuePair<string, int>("Игнорирует замечания учителя и нарушает дисциплину.", 100),
                new KeyValuePair<string, int>("Разговаривает без разрешения.", 50),
                new KeyValuePair<string, int>("Использует грубую лексику.", 75),
                new KeyValuePair<string, int>("Отказывается выполнять задания.", 80),
                new KeyValuePair<string, int>("Участвует в конфликтах с одноклассниками.", 80),
                new KeyValuePair<string, int>("Нарушает дисциплину на переменах.", 70),
                new KeyValuePair<string, int>("Отказывается подчиняться правилам школы.", 90),
                new KeyValuePair<string, int>("Не проявляет уважения к учителям.", 100),
                new KeyValuePair<string, int>("Повреждает школьное имущество.", 120),
                new KeyValuePair<string, int>("Постоянно опаздывает на уроки.", 110),
                new KeyValuePair<string, int>("Обычный спокойный ученик.", 0),
                new KeyValuePair<string, int>("Добрый волонтер.", -30),
                new KeyValuePair<string, int>("Тихоня класса.", -10),
                new KeyValuePair<string, int>("Спортсмен школы.", -30),
                new KeyValuePair<string, int>("Помогает учителям с заданиями", -50),
                new KeyValuePair<string, int>("Постоянно сидит на дополнительных занятиях", -50),
                new KeyValuePair<string, int>("Проводит внутришкольные мероприятия", -100),
                new KeyValuePair<string, int>("Делает все задания в классе", -40),
                new KeyValuePair<string, int>("Никогда не отвлекает одноклассников", -50),
                new KeyValuePair<string, int>("Совсем не агресивный", -30),
            }
        },
        { "Second", new KeyValuePair<string, int>[]
            {
                new KeyValuePair<string, int>("пришел/ла без рюкзака сегодня", 40),
                new KeyValuePair<string, int>("сегодня подрался/подралась", 200),
                new KeyValuePair<string, int>("бросил/бросила пенал в одноклассника", 100),
                new KeyValuePair<string, int>("ушел/ушла с уроков гораздо раньше", 80),
                new KeyValuePair<string, int>("парил/парила в туалете", 90),
                new KeyValuePair<string, int>("наорал/а матом на уборщицу", 130),
                new KeyValuePair<string, int>("задержал/а всех после звонка", 80),
                new KeyValuePair<string, int>("смешал/а опасные химикаты в лаборатории", 180),
                new KeyValuePair<string, int>("продал/а чужой учебник", 120),
                new KeyValuePair<string, int>("включил/а сирену во время урока", 250),
                new KeyValuePair<string, int>("написал/а анонимку учителю", 110),
                new KeyValuePair<string, int>("пришел/пришла в форме другой школы", 140),
                new KeyValuePair<string, int>("разрисовал/а стены в классе", 120),
                new KeyValuePair<string, int>("сорвал/а контрольную работу", 110),
                new KeyValuePair<string, int>("подменил/а результаты теста", 180),
                new KeyValuePair<string, int>("принес/принесла домашнего питомца в школу", 80),
                new KeyValuePair<string, int>("прогулял/а школьное собрание", 90),
                new KeyValuePair<string, int>("списывал/а у соседа на контрольной", 120),
                new KeyValuePair<string, int>("использовал/а телефон на уроке", 70),
                new KeyValuePair<string, int>("разговаривал/а на уроке громким голосом", 100),
                new KeyValuePair<string, int>("съел/а чужой обед", 120),
                new KeyValuePair<string, int>("потерял/а школьные принадлежности", 50),
                new KeyValuePair<string, int>("оставил/а мусор в классе", 80),
            }
        },
        { "Third", new KeyValuePair<string, int>[]
            {
                new KeyValuePair<string, int>("действие было совершено неоднократно", 80),
                new KeyValuePair<string, int>("игнорировал/а предупреждения", 40),
                new KeyValuePair<string, int>("вовлек других учеников", 70),
                new KeyValuePair<string, int>("планировал/а заранее", 30),
                new KeyValuePair<string, int>("скрывал/а последствия", 40),
                new KeyValuePair<string, int>("действовал/а с намерением навредить", 80),
                new KeyValuePair<string, int>("не собирается извинятся", 50),
                
                new KeyValuePair<string, int>("впервые совершил/а подобное действие", -60),
                new KeyValuePair<string, int>("публично извинился/лась", -50),
                new KeyValuePair<string, int>("содействовал/а расследованию", -30),
                new KeyValuePair<string, int>("действовал/а под давлением сверстников", -40),
                new KeyValuePair<string, int>("осознал/а вину и готов/а исправиться", -40),
                new KeyValuePair<string, int>("пытался/лась исправить ущерб", -50),
                new KeyValuePair<string, int>("обстоятельства в семье повлияли на поведение", -70),
                new KeyValuePair<string, int>("его подставили", -100),
            }
        },
        { "Fourth", new KeyValuePair<string, int>[]
            {
                new KeyValuePair<string, int>("директор, я понимаю серьезность своих действий", -20),
                new KeyValuePair<string, int>("директор, я готов(а) отработать свою ошибку", -30),
                new KeyValuePair<string, int>("директор, прошу прощения за созданные неудобства", -20),
                new KeyValuePair<string, int>("директор, я готов(а) принять любые последствия", -10),
                new KeyValuePair<string, int>("директор, обещаю больше не повторять такого", -15),
                new KeyValuePair<string, int>("директор, мои действия были необдуманными", -15),
                new KeyValuePair<string, int>("директор, прошу учесть обстоятельства", -10),
                new KeyValuePair<string, int>("директор, искренне сожалею о своих поступках", -20),
                new KeyValuePair<string, int>("директор, глубоко раскаиваюсь в своих действиях", -20),
                new KeyValuePair<string, int>("директор, примите мои искренние извинения", -25),
                new KeyValuePair<string, int>("директор, понимаю, что ошибся(ась) и готов(а) исправиться", -35),
                new KeyValuePair<string, int>("директор, сожалею о своем неправильном поведении", -30),
                new KeyValuePair<string, int>("директор, хочу извиниться за свои действия и обещаю их не повторять", -20),

                new KeyValuePair<string, int>("директор, я не считаю свои действия ошибкой", 70),
                new KeyValuePair<string, int>("директор, я не понимаю, за что мне предъявляют обвинения", 80),
                new KeyValuePair<string, int>("директор, я не собираюсь извиняться за свои поступки", 75),
                new KeyValuePair<string, int>("директор, я считаю, что мои действия были оправданы", 85),
                new KeyValuePair<string, int>("директор, я готов(а) защищать свою позицию", 60),
                new KeyValuePair<string, int>("директор, я считаю, что школьные правила не относятся ко мне", 90),
                new KeyValuePair<string, int>("директор, я не собираюсь признавать свою вину", 80),
                new KeyValuePair<string, int>("директор, считаю, что правила школы не распространяются на меня", 90),
                new KeyValuePair<string, int>("директор, мои действия были полностью оправданы", 85),
                new KeyValuePair<string, int>("директор, не вижу ничего плохого в том, что я сделал(а)", 75),
                new KeyValuePair<string, int>("директор, я не считаю свои действия ошибкой", 70),
                new KeyValuePair<string, int>("директор, я считаю, что мои действия были необходимы", 60),
                new KeyValuePair<string, int>("директор, я не собираюсь извиняться, это было необходимо", 65),

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
        if (GetScore() > 210)
        {
            guilty = true;
        }
        else
        {
            guilty = false;
        }
    }
}
