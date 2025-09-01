using Code.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _cardOfConditions;
    [SerializeField] private Button _buttonCardOfConditions;
    [SerializeField] private List<GameObject> _cardOfRounds = new();
    [SerializeField] private Button _buttonCardOfRounds;

    private int _numOfRound = 0;

    void Start()
    {
        _cardOfConditions.transform.position = new Vector3(-0.008930326f, -1.02f, 0);
        _buttonCardOfRounds.gameObject.SetActive(false);

        foreach (GameObject card in _cardOfRounds)
        {
            card.transform.position = new Vector3(-0.008930326f, -1.02f, 0);
            card.SetActive(false);
        }
    }

    public void PassToCardCondition()
    {
        if (_cardOfConditions == null)
            GameObject.FindWithTag("WelcomeCard").SetActive(true);
        _buttonCardOfConditions.gameObject.SetActive(true);
    }

    public void PassToNextCard()
    {
        _cardOfConditions.SetActive(false);
        _buttonCardOfConditions.gameObject.SetActive(false);

        _cardOfRounds[_numOfRound].SetActive(true);
        ++_numOfRound;

        _buttonCardOfRounds.gameObject.SetActive(true);
    }

    public void SetCardOfRound()
    {
        _cardOfRounds[_numOfRound - 1].SetActive(false);
        _buttonCardOfRounds.gameObject.SetActive(false);
    }

    public void RestartCardToShow()
    {
        _numOfRound = 0;
        _cardOfRounds[_numOfRound].SetActive(true);
        ++_numOfRound;
    }
}
