using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ViewModels : MonoBehaviour
{
    public GameObject anhor;
    public List<GameObject> obj = new List<GameObject>();

    public List<string> filesName = new List<string>();

    public int currentIndex = 0;
    

    void Start()
    {
        GetFileName(FileUtility.GetResourcesDirectories()[0]);
        GetObjectFromResources();

    }

    
    void Update()
    {
      
    }

    void GetFileName(string dir)
    {

        DirectoryInfo d = new DirectoryInfo(dir);

        foreach (var file in d.GetFiles("*.fbx"))
        {
            //filesName.Add(file.FullName);//full path
            filesName.Add(file.Name);
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

    }

    public void ShowPrev()
    {
        DestroyOld();

        currentIndex--;
        if (currentIndex < 0)
            currentIndex = obj.Count - 1;

        GameObject o = Instantiate(obj[currentIndex], anhor.transform.position, anhor.transform.rotation);
        o.transform.SetParent(anhor.transform);

    }
}
