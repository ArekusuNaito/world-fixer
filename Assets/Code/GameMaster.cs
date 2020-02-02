using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using EZCameraShake;

public class GameMaster : MonoBehaviour
{
    private static GameMaster self = null;
    [Header("Game Balancing")]
    public Balance balance;

    [Header("Player Planets")]
    public Planet player1;
    public Planet player2;
    [Header("Audio")]
    public AudioDatabase audioDatabase;
    public AudioSource sfxChannel;
    public AudioSource musicChannel;
    public AudioSource bgsChannel;
    [Header("UI")]

    public Text readyText;
    public Image healthBar;
    public GameObject readyUI;
    public GameOverPanel gameOverPanel;
    public GameObject repairWarning;
    private int timeToStart = 3;

    

    public static AudioDatabase audioDB;
    void Awake()
    {
        if (self == null)
        {
            self = this;
            DontDestroyOnLoad(self);
            audioDB = self.audioDatabase;
        } else Destroy(this);
        player1.Enemy=player2;
        player2.Enemy = player1;
        player1.GameOver = GameOver;
        player2.GameOver = GameOver;

    }
    void Start()
    {
        var intervalObservable = Observable.Interval(TimeSpan.FromSeconds(1)).Take(4)
        .DelaySubscription(TimeSpan.FromMilliseconds(250))
        .DoOnCompleted(() =>
        {
            StartGame();
        });
        intervalObservable.Subscribe(count=>
        {
            readyText.text = timeToStart.ToString();
            //SFX GOES HERE
            timeToStart--;
        }).AddTo(this);
        
        
    }

    void SpawnRepairWarning()
    {

    }
    void StartGame()
    {
        readyUI.SetActive(false);
        player1.TransitionTo(Planet.State.CHARGING);
        player2.TransitionTo(Planet.State.CHARGING);
        //SFX Here?
        //Debug force game over after start
        //You have to know who wins and which side that player is
        // GameOver(player1,player2);  
        // GameOver(winner:player2,loser:player1);
    }

    void GameOver(Planet winner, Planet loser)
    {
        winner.TransitionTo(Planet.State.IDLE);
        loser.TransitionTo(Planet.State.IDLE);
        //Inits game over anim
        if(winner==player1)
        {
            this.gameOverPanel.SetLeftRightIcons();
        }
        else
        {
            this.gameOverPanel.SetRightLeft();
        }

        this.gameOverPanel.Execute();
        

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Repair(player1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Repair(player2);
        }
    }

      

    void Repair(Planet player)
    {
        player.Repair(this.balance.repairAmount);
        
    }

    public static void PlaySFX(AudioClip clip)
    {
        self.sfxChannel.pitch = 1;
        Debug.Assert(clip!=null,$"Audio Clip is null ");
        if(clip!=null)
        {
            self.sfxChannel.PlayOneShot(clip);
        }
    }

    public static void PlayRandomPitchSFX(AudioClip clip)
    {
        self.sfxChannel.pitch = UnityEngine.Random.Range(-3f,1.2f);
        Debug.Assert(clip != null, $"Audio Clip is null ");
        if (clip != null)
        {
            self.sfxChannel.PlayOneShot(clip);
            self.sfxChannel.pitch = 1;
        }
    }

    public static void PlayMusic(AudioClip clip)
    {
        Debug.Assert(clip != null, $"Audio Clip is null ");
        if (clip != null)
        {
            self.musicChannel.clip = clip;
            self.musicChannel.Play();
        }
    }
}


