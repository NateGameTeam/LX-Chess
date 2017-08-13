using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public GameObject grids;
    public GameObject goal;
    public GameObject winText;
    public float moveSpeed = 10.0f;

    public int currentGrid;
    private bool isMoving;
    // Use this for initialization
    private delegate void ShowTextAction();
    private ShowTextAction ShowText;
    private int currentStep;
    void Start()
    {
        winText.SetActive(false);
        currentGrid = -1;
    }

    private void OnEnable()
    {
        SaveDataManager.onSave += Save;
        SaveDataManager.onLoad += Load;
    }

    private void OnDestroy()
    {
        SaveDataManager.onSave -= Save;
        SaveDataManager.onLoad -= Load;
    }

    public void Move()
    {
        if (!isMoving)
        {
            int step = (int)Random.Range(1.0f, 6.0f);
            StartCoroutine(Move(step));
        }
    }
    private void ShowStep()
    {
        GUILayout.Label(currentStep.ToString());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
    }
    public IEnumerator Move(int step)
    {
        isMoving = true;
        ShowText += ShowStep;
        Transform target;
        for (currentStep = step - 1; currentStep > 0; currentStep--)
        {
            if (currentGrid < grids.transform.childCount - 1)
            {
                currentGrid++;

                target = grids.transform.GetChild(currentGrid);
            }
            else
            {

                target = goal.transform;
            }
            while (Vector3.Distance(transform.position, target.transform.position) != 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
        ShowText -= ShowStep;
        isMoving = false;
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
    public GUIStyle style;
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 50, 50));
        if (GUILayout.Button("前进"))
            Move();
        if (ShowText != null)
        {
            ShowText();
        }
        GUILayout.EndArea();

    }
}

