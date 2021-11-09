using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Padrao.Core.Singleton;

public class GameManager : Singleton<GameManager>
{

    [Header("Player")]
    public GameObject playerPrefab;

    [Header("Player")]
    public List<GameObject> enemies;

    [Header("References")]
    [SerializeField]
    private Transform startPoint;

    private GameObject _currentPlayer;

    void Start()
    {
        Init();
    }

    public void Init(){
        SpawnPlayer();
    }

    public void SpawnPlayer(){
        _currentPlayer = Instantiate(playerPrefab);
        _currentPlayer.transform.position = startPoint.position;
    }

}
