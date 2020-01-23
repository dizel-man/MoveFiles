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
    [Space(5)]
    public InputField fileName;


    void Update()
    {
       
        ListenForChatSubmissionRequest();

    }

 
    static void MoveSomething(string from, string to)
    {
       
        FileUtil.MoveFileOrDirectory(from, to);
    }


    void ListenForChatSubmissionRequest()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            string f = RetrieveFrom().Replace("\\", "/") + "/";
            string t = RetrieveTo().Replace("\\", "/") + "/";

            if (f != string.Empty && t != string.Empty)
            {

                
                foreach (string sFilePath in System.IO.Directory.GetFiles(f, fileName.text))
                {
                    Debug.Log("Move this file : " + sFilePath + " -> " + t + " with name : " + sFilePath);
                    string sFileName = System.IO.Path.GetFileName(sFilePath);
                    System.IO.File.Copy(sFilePath, t + sFileName);
                }

            }

           
        }
    }

    public string RetrieveFrom()
    {
        return from.GetComponent<InputField>().text;
    }

    public string RetrieveTo()
    {
        return to.GetComponent<InputField>().text;
    }


}
