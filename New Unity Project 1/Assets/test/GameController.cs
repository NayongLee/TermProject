﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;

public class GameController : MonoBehaviour {

    public GameObject[] hazard;
    public Vector3 spawnPos;
    public float spawnRate;

    private float nextSpawn;

    public GUIText ScoreText;
    public GUIText DistanceText;
    public GUIText RestartText;
    public GUIText GameOverText;
    public GUIText GameOverText1;

    private int score;
    private int distance;
    private bool restart;
    private bool gameOver;

    private int gameLevel = 0;

    // Use this for initialization
    void Start () {
        score = 0;
        distance = 0;
        restart = false;
        gameOver = false;
        RestartText.text = "";
        GameOverText.text = "";
        GameOverText1.text = "";
        gameLevel = 0;
        SetScore();
        Cursor.lockState = CursorLockMode.Locked;
        // GameObject moverObject = GameObject.FindWithTag("Mover");
        //SpawnWave();
	}

    void Update()
    {
        if (Time.time > nextSpawn   && !gameOver)
        {
            SpawnWave();
            AddScore(10);
            AddDistance(1);
            nextSpawn = Time.time + spawnRate;
        }
        if ((Input.GetKeyDown(KeyCode.Mouse1) && restart)|| Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(distance >= 10*(gameLevel+1))
        {
            spawnRate -= (float)0.1;
            gameLevel++;
            if (spawnRate <= (float)0.1) spawnRate = (float)0.1;
        }
    }
    void SpawnWave()
    {
        int n = Random.Range(0,3);
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnPos.x,spawnPos.x), spawnPos.y, spawnPos.z);
        Quaternion spawnRotation = Quaternion.identity;

        GameObject astroid = Instantiate(hazard[n], spawnPosition, spawnRotation) as GameObject;

        float tmp_speed = astroid.GetComponent<Mover>().speed;

        astroid.GetComponent<Mover>().speed = tmp_speed - gameLevel;
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        SetScore();
    }
    void SetScore()
    {
        ScoreText.text = "Score : " + score;
    }

    public void AddDistance(int newDistance)
    {
        distance += newDistance;
        SetDistance();
    }
    void SetDistance()
    {
        DistanceText.text = "Distance : " + distance + "km";
    }

    public void SetRestart()
    {
        RestartText.text = "Press 'R' to restart";
        restart = true;
    }
    public void SetGameOver()
    {
        GameOverText1.text = "Game Over";
        gameOver = true;
    }
    public void SetScoreBoard()
    {
        //System.Threading.Thread.Sleep(5000);
        string path = @"D:\Unity3D_M\test.txt";
        string s;
        string[] str = new string[1024];
        int[] score_F = new int[1024];
        int index = 0;

        GameOverText.text = "";

        using(StreamWriter sw = File.AppendText(path))
        {
            sw.WriteLine("a " + score);
        }

        using (StreamReader sr = File.OpenText(path))
        {
            while ((s = sr.ReadLine()) != null)
            {
                string[] sTmp = s.Split(' ');
                str[index] = sTmp[0];
                if (sTmp[1] != null)
                    score_F[index++] = int.Parse(sTmp[1]);
            }
        }
        System.Array.Sort(score_F);
        System.Array.Reverse(score_F);
        for(int i = 0; i < index && i < 10; i++)
        {
            GameOverText.text += (i + 1) + ". " + score_F[i] + "\n";
        }
    }
}