using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {
    public GameObject grids;
    public GameObject goal;
    public GameObject winText;

    public int currentGrid;
	// Use this for initialization
	void Start () {
        print("Start");
        winText.SetActive(false);
    }

    //Update is called once per frame
    void Update () {
    }
    private void OnEnable()
    {
        SaveDataManager.onSave += Save;
        SaveDataManager.onLoad += Load;

    }
    private void Awake()
    {
        print("Awake");
        currentGrid = 0;

    }
    private void OnDestroy()
    {
        SaveDataManager.onSave -= Save;
        SaveDataManager.onLoad -= Load;
    }

    public void Move() {
        print("move");
        try
        {
            currentGrid += 1;
            Transform target = grids.transform.GetChild(currentGrid);
            transform.position = target.position;
        }
        catch(UnityException)
        {
            transform.position = goal.transform.position;
            winText.SetActive(true);
            Debug.Log("end");
        }
        finally
        {

        }
    }

    public void Move(int step)
    {
        try
        {
            print(grids);
            Transform target = grids.transform.GetChild(step);
            transform.position = target.position;
        }
        catch (UnityException)
        {
            transform.position = goal.transform.position;
            winText.SetActive(true);
            Debug.Log("end");
        }
        finally
        {

        }
    }

    private void Save()
    {
        PlayerPrefs.SetInt("currentGrid", currentGrid);
        Debug.Log("player.Save()");
    }

    private void Load()
    {
        Debug.Log("player.Load()");
        currentGrid = PlayerPrefs.GetInt("currentGrid");
        Move(currentGrid);
    }
}

