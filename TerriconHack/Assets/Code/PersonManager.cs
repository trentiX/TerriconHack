using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Random = UnityEngine.Random;

public class PersonManager : MonoBehaviour
{
    [SerializeField] private GameObject[] person;
    [SerializeField] private Transform spawnPoint;
    
    [SerializeField] private GameObject notePad;
    [SerializeField] private GameObject trigger;
    [SerializeField] private GameObject lamp;
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject note;
    [SerializeField] private GameObject correct;
    [SerializeField] private GameObject SecondLive;
    
    [SerializeField] private TextMeshProUGUI correctAnswers;
    [SerializeField] private TextMeshProUGUI lamps;


    public GameObject student;
    private Note _note;
    private int prevIndex = 0;
    private int index = 0;

    private int answers;
    private int lampsInt;

    private void Awake()
    {
        _note = FindObjectOfType<Note>();
    }

    private void Spawn()
    {
        index = Random.Range(0, person.Length);

        while (prevIndex == index)
        {
            index = Random.Range(0, person.Length);
        }
        
        prevIndex = index;
        student = Instantiate(person[index], spawnPoint.position, Quaternion.Euler(0f, 180f, 0f));
        student.transform.DOMoveZ(-8, 1);
        
    }

    public void GreenTick()
    {
        note.SetActive(false);
        
        if (_note.guilty == true)
        {
            Lose();
            return;
        }
        else
        {
            Win();
        }
        
        student.transform.DOMoveX(2,1f).OnComplete((() =>
        {
            NewDossier();
        }));
    }

    public void RedCross()
    {
        note.SetActive(false);
        
        if (_note.guilty == true)
        {
            Win();
        }
        else
        {
            Lose();
            return;
        }
        
        student.transform.DOMoveX(-2,1f).OnComplete((() =>
        {
            NewDossier();
        }));
    }
    
    public void StartGame()
    {
        startMenu.transform.DOMoveX(4000, 3f).OnComplete(() => startMenu.SetActive(false));
        trigger.SetActive(true);
        notePad.transform.DOMoveX(0, 2);
        lamp.transform.DOMoveX(100, 2);
        correct.transform.DOMoveX(900, 2);
        NewDossier();
    }

    private void NewDossier()
    {
        Destroy(student);
        Spawn();
        _note.GenerateRandomCharacteristics();
    }

    private void Win()
    {
        answers = int.Parse(correctAnswers.text);
        answers++;
        correctAnswers.text = answers.ToString();

        if (answers % 5 == 0)
        {
            lampsInt = int.Parse(lamps.text);
            lampsInt++;
            lamps.text = lampsInt.ToString();
        }
    }

    private void Lose()
    {
        SecondLive.SetActive(true);
        notePad.transform.DOMoveX(3, 2);
        correct.transform.DOMoveX(1700, 2);
    }

    public void SecLive()
    {
        lampsInt = int.Parse(lamps.text);
        lampsInt--;
        lamps.text = lampsInt.ToString();
        
        student.transform.DOMoveY(-2,1f).OnComplete((() =>
        {
            StartGame();
        }));
        
        SecondLive.SetActive(false);
    }

    public void Again()
    {
        answers = 0;
        correctAnswers.text = "0";
        
        student.transform.DOMoveY(-2,1f).OnComplete((() =>
        {
            StartGame();
        }));
        
        SecondLive.SetActive(false);
    }
}
