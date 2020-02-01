using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SharedEnums;

public abstract class Minigame : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected MinigamesConfig m_minigamesConfig;

    protected bool IsActive { get; private set; } = false;

    public virtual void StartMinigame()
    {
        IsActive = true;
    }

    public virtual void StopMinigame()
    {
        IsActive = false;
    }

    #region SUBCLASS SANDBOX

    protected InputButton GetRandomInputButton()
    {
        int rnd = Random.Range(0, (int)InputButton.COUNT);
        return (InputButton)rnd;
    }

    #endregion
}
