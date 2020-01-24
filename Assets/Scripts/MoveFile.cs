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


    private void Start()
    {
        string f = FileUtility.GetResourcesDirectories()[0].Replace("\\", "/") + "/";
        from.text = f;
    }

    //void Update()
    //{
       
      
    //}

   public void CopyFileToDirectory()
    {
        Debug.Log(" press send");

        if (from_ != string.Empty && to_ != string.Empty)// && Input.GetKeyUp(KeyCode.Return))
        {
            RemoveSlash();

            foreach (string sFilePath in System.IO.Directory.GetFiles(from_, GetInputField(fileName)))
            {
                  Debug.Log("Move this file : " + sFilePath + " -> " + to_ + " with name : " + sFilePath);

                string sFileName = System.IO.Path.GetFileName(sFilePath);
                System.IO.File.Copy(sFilePath, to_ + sFileName);

                Debug.Log("sended");
            }
        }
        else
        {
            Debug.Log(" path is empty!");
        }
    }

    string GetInputField(InputField field)
    {
        return field.text;
    }

    void RemoveSlash()
    {
        from_ = GetInputField(from).Replace("\\", "/") + "/";
        to_ = GetInputField(to).Replace("\\", "/") + "/";
    }


}
