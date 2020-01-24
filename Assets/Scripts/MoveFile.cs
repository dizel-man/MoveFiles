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


    string from_;
    string to_;




    void Update()
    {
        RemoveSlash();
        ListenForChatSubmissionRequest();
    }

    void ListenForChatSubmissionRequest()
    {
        if (Input.GetKeyUp(KeyCode.Return) && from_ != string.Empty && to_ != string.Empty)
        {
            foreach (string sFilePath in System.IO.Directory.GetFiles(from_, RetrieveInput(fileName)))
            {
                //  Debug.Log("Move this file : " + sFilePath + " -> " + t + " with name : " + sFilePath);

                string sFileName = System.IO.Path.GetFileName(sFilePath);
                System.IO.File.Copy(sFilePath, to_ + sFileName);
            }
        }
    }

    string RetrieveInput(InputField field)
    {
        return field.text;
    }

    void RemoveSlash()
    {
        from_ = RetrieveInput(from).Replace("\\", "/") + "/";
        to_ = RetrieveInput(to).Replace("\\", "/") + "/";
    }


}
