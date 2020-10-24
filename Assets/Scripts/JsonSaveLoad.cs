using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Data
{
    public int m_nLevel;
    public Vector2 m_vecPosition;

    public void printData()
    {
        Debug.Log("Level :" + m_nLevel);
        Debug.Log("Position : " + m_vecPosition);
    }
}

public class JsonSaveLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Data data = new Data();
        data.m_nLevel = 12;
        data.m_vecPosition = new Vector2(3.4f, 5.6f);

        string str = JsonUtility.ToJson(data);
        Debug.Log("ToJson : " + str);

        Data data2 = JsonUtility.FromJson<Data>(str);
        data2.printData();
        

        //파일 저장
        Data data3 = new Data();
        data3.m_nLevel = 99;
        data3.m_vecPosition = new Vector2(99, 99);

        File.WriteAllText(Application.dataPath + "/TestJson.json", JsonUtility.ToJson(data3));

        //파일 불러오기
        string str2 = File.ReadAllText(Application.dataPath + "/TestJson.json");

        Data data4 = JsonUtility.FromJson<Data>(str2);
        data4.printData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
