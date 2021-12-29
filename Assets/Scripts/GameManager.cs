using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Padrao.Core.Singleton;
using DG.Tweening;
using Cinemachine;

public class GameManager : Singleton<GameManager>
{

    [Header("Player")]
    public GameObject playerPrefab;

    [Header("Enemies")]
    public List<GameObject> enemies;

    [Header("References")]
    [SerializeField]
    private Transform startPoint;
    [SerializeField]
    private CinemachineVirtualCamera cinemachine;

    [Header("Animation")]
    [SerializeField]
    private float duration = .2f;
    [SerializeField]
    private float delay = .1f;
    [SerializeField]
    private Ease ease = Ease.OutBack;

    private GameObject _currentPlayer;

    void Start()
    {
        Init();
    }

    public void Init(){
        SpawnPlayer();
        cinemachine.Follow = _currentPlayer.transform;
        cinemachine.LookAt = _currentPlayer.transform;
    }

    public void SpawnPlayer(){
        _currentPlayer = Instantiate(playerPrefab);
        _currentPlayer.transform.position = startPoint.position;
        _currentPlayer.transform.DOScale(0, duration).SetEase(ease).From().SetDelay(delay);
    }

}
