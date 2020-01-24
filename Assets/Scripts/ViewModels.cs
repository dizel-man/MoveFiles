using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ViewModels : MonoBehaviour
{
    public GameObject anhor;
    public List<GameObject> obj = new List<GameObject>();

    public List<string> filesName = new List<string>();

    public int currentIndex = 0;

    public float rotationSpeed = -20f;

    [Space(5)]
    public GameObject Content;
    public GameObject buttonFbx;
    

    void Start()
    {
        GetFileName(FileUtility.GetResourcesDirectories()[0]);
        GetObjectFromResources();

        // just show first object
        GameObject o = Instantiate(obj[currentIndex], anhor.transform.position, anhor.transform.rotation);
        o.transform.SetParent(anhor.transform);
        GetComponent<MoveFile>().fileName.text = filesName[currentIndex];

    }

    
    void Update()
    {
        ObjectsRotation();
        ChangeDirectory(GetComponent<MoveFile>().from.text);
    }


    void ChangeDirectory(string dir)
    {
       if(Input.GetKeyUp(KeyCode.Return))
        {
            foreach (Transform item in Content.transform)
            {
                Destroy(item.gameObject);
            }

            string newPath = GetComponent<MoveFile>().from.text.Replace("\\", "/") + "/";
            GetComponent<MoveFile>().from.text = newPath;
            filesName.Clear();
            GetFileName(newPath);
            // refresh lists
            obj.Clear();

            foreach (string item in filesName)
            {
                string f = item;
                int exten = item.LastIndexOf(".");
                if (exten >= 0)
                {
                    f = f.Substring(0, exten);
                    GameObject ob = Resources.Load<GameObject>(f);
                    obj.Add(ob);
                }
            }

            currentIndex = 0;
            DestroyOld();

            GameObject o = Instantiate(obj[currentIndex], anhor.transform.position, anhor.transform.rotation);
            o.transform.SetParent(anhor.transform);
            GetComponent<MoveFile>().fileName.text = filesName[currentIndex];

        }
    }

    void GetFileName(string dir)
    {

        DirectoryInfo d = new DirectoryInfo(dir);

        foreach (var file in d.GetFiles("*.fbx"))
        {
            //filesName.Add(file.FullName);//full path
            filesName.Add(file.Name);
            // generate button fbx
            GameObject button = Instantiate(buttonFbx);
            button.transform.SetParent(Content.transform);
            // naming button fbx
            button.transform.GetChild(0).GetComponent<Text>().text = file.Name;
        }
    }

    void GetObjectFromResources()
    {
        foreach (string item in filesName)
        {
            string f = item;
            int exten = item.LastIndexOf(".");
            if (exten >= 0)
            {
                f = f.Substring(0, exten);
                GameObject ob = Resources.Load<GameObject>(f);
                obj.Add(ob);
            }
        }
    }

    #region Show Models

    void DestroyOld()
    {
        foreach (Transform item in anhor.transform)
        {
            if(item != null)
                Destroy(item.gameObject);
        }
    }
    
    public void ShowNext()
    {
        DestroyOld();

        currentIndex++;
        if (currentIndex > obj.Count - 1)
            currentIndex = 0;

        GameObject o = Instantiate(obj[currentIndex], anhor.transform.position, anhor.transform.rotation);
        o.transform.SetParent(anhor.transform);

        GetComponent<MoveFile>().fileName.text = filesName[currentIndex];

    }

    public void ShowPrev()
    {
        DestroyOld();

        currentIndex--;
        if (currentIndex < 0)
            currentIndex = obj.Count - 1;

        GameObject o = Instantiate(obj[currentIndex], anhor.transform.position, anhor.transform.rotation);
        o.transform.SetParent(anhor.transform);

        GetComponent<MoveFile>().fileName.text = filesName[currentIndex];

    }

    void ObjectsRotation()
    {
        anhor.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
    }

    #endregion
}
