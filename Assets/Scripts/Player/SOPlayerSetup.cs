using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu]
public class SOPlayerSetup : ScriptableObject
{

    public SOString soStringName;

    [Header("Animator")]
    public Animator player;

    [Header("Speed Setup")]
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float runSpeedMultplier;
    public float airSpeedMultiplier;

    [Header("Jump Setup")]
    public float jumpForce = 20f;

    [Header("Jump Animation Setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = .7f;
    public float jumpAnimationDur = .2f;
    public float landAnimationDur = .2f;
    public Ease jumpEase = Ease.OutBack;

    [Header("Move Animation Setup")]
    public float moveAnimationDur = .2f;
    public Ease moveEase = Ease.OutBack;
    public float moveAnimationStop = 3f;
    public string boolRun = "Running";


    [Header("Death Animation Setup")]
    public string triggerDie = "Die";

    [Header("Damage")]
    public float dmgDur = .2f;
    public Ease dmgEase = Ease.InBack;
}
