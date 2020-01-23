using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using System.IO;

public class MoveFile : MonoBehaviour
{
    public InputField from;
    public InputField to;
    public InputField fileName;


    string f;
    string t;

  


    void Update()
    {
        RemoveSlash();
        ListenForChatSubmissionRequest();
    }

    void ListenForChatSubmissionRequest()
    {
        if (Input.GetKeyUp(KeyCode.Return) && f != string.Empty && t != string.Empty)
        {
                foreach (string sFilePath in System.IO.Directory.GetFiles(f, RetrieveInput(fileName)))
                {
                    Debug.Log("Move this file : " + sFilePath + " -> " + t + " with name : " + sFilePath);
                    string sFileName = System.IO.Path.GetFileName(sFilePath);
                    System.IO.File.Copy(sFilePath, t + sFileName);
                }
        }
    }

    string RetrieveInput(InputField field)
    {
        return field.text;
    }

    void RemoveSlash()
    {
        f = RetrieveInput(from).Replace("\\", "/") + "/";
        t = RetrieveInput(to).Replace("\\", "/") + "/";
    }
    

}
