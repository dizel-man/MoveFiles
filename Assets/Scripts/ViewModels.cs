using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ViewModels : MonoBehaviour
{
    public GameObject anhor;
    public Text countVerticesText;
    public List<GameObject> obj = new List<GameObject>();

    public List<string> filesName = new List<string>();

    public int currentIndex = 0;

    public float rotationSpeed = -20f;

    [Space(5)]
    public GameObject Content;
    public GameObject buttonFbx;
    public List<GameObject> buttons = new List<GameObject>();


    void Start()
    {
        /*
         * 
          // just show first object

        GetFileName(FileUtility.GetResourcesDirectories()[0]);
        GetObjectFromResources();


        GameObject o = Instantiate(obj[currentIndex], anhor.transform.position, anhor.transform.rotation);
        o.transform.SetParent(anhor.transform);
        GetComponent<MoveFile>().fileName.text = filesName[currentIndex];
        */
    }


    void Update()
    {
        ObjectsRotation();
        ChangeDirectory(GetComponent<MoveFile>().from.text);
    }

    // refresh 
    void ChangeDirectory(string dir)
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {

            foreach (Transform item in Content.transform)
            {
                Destroy(item.gameObject);
            }

            string newPath = GetComponent<MoveFile>().from.text.Replace("\\", "/");// + "/";
            GetComponent<MoveFile>().from.text = newPath;
            filesName.Clear();
            buttons.Clear();
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

            CountOfVertices(o);

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
            button.name = file.Name;

            buttons.Add(button);
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

    //destroy 3d model on view monitor
    void DestroyOld()
    {
        foreach (Transform item in anhor.transform)
        {
            if (item != null)
                Destroy(item.gameObject);
        }
    }

    //instance 3d model of next index
    public void ShowNext()
    {
        DestroyOld();

        currentIndex++;
        if (currentIndex > obj.Count - 1)
            currentIndex = 0;

        GameObject o = Instantiate(obj[currentIndex], anhor.transform.position, anhor.transform.rotation);
        CountOfVertices(o);
        o.transform.SetParent(anhor.transform);

        GetComponent<MoveFile>().fileName.text = filesName[currentIndex];

    }
    //instance 3d model of preview index
    public void ShowPrev()
    {
        DestroyOld();

        currentIndex--;
        if (currentIndex < 0)
            currentIndex = obj.Count - 1;

        GameObject o = Instantiate(obj[currentIndex], anhor.transform.position, anhor.transform.rotation);
        CountOfVertices(o);
        o.transform.SetParent(anhor.transform);

        GetComponent<MoveFile>().fileName.text = filesName[currentIndex];

    }
    // simple rotation 3d model on monitor
    void ObjectsRotation()
    {
        anhor.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
    }



    public void GetObgectFromButton(Button btn)
    {

        string t = btn.transform.GetChild(0).GetComponent<Text>().text;

        for (int i = 0; i < filesName.Count; i++)
        {
            if (t == filesName[i])
            {
                currentIndex = filesName.IndexOf(filesName[i]);
            }
        }

        DestroyOld();

        GameObject o = Instantiate(obj[currentIndex], anhor.transform.position, anhor.transform.rotation);
        CountOfVertices(o);
        o.transform.SetParent(anhor.transform);

        GetComponent<MoveFile>().fileName.text = filesName[currentIndex];

    }

    void CountOfVertices(GameObject ob)
    {
        if (ob.GetComponent<MeshFilter>() != null)
        {
            Mesh m = ob.GetComponent<MeshFilter>().mesh;

            countVerticesText.text = "count of vertices: " + m.vertexCount.ToString();
         //   Debug.Log("object " + ob.name + "count of vertices is: " + m.vertexCount);
        }
        else
        {
            Debug.Log("cant find Meshfilter component");
        }
    }


    #endregion
}
