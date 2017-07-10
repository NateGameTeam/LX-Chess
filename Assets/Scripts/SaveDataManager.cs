using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour {
    public delegate void SaveAction();
    public static event SaveAction onSave;
    public static event SaveAction onLoad;

    public void Save()
    {
        if (onSave != null)
        {
            onSave();
            PlayerPrefs.Save();
        }
    }

    public static void Load()
    {
        if (onLoad != null)
        {
            onLoad();

        }
    }

}
