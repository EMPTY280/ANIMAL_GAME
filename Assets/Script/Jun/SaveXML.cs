using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Net.NetworkInformation;

public enum SaveLocateType
{
    None,
    Resources,
    Streaming,
}

public enum XMLDATANUMBER
{
    EventData,

    End
}



public class SaveXML : MonoBehaviour
{
    [SerializeField] private XMLDATANUMBER[] XMLData;
    [SerializeField] private SaveLocateType saveLocateType;
    private int[] dataCount;
    private void Awake()
    {
        dataCount = new int[XMLData.Length];
        for (int i = 0; i < XMLData.Length; i++)
        {
            string xmlName = XMLData[i].ToString();
            switch (saveLocateType)
            {
                case SaveLocateType.None:
                    break;
                case SaveLocateType.Resources:
                    Process_R(xmlName, i);
                    break;
                case SaveLocateType.Streaming:
                    Process_S(xmlName, i);
                    break;
            }
            
        }
    }


    private void Process_R(string name, int num)
    {
        TextAsset data = Resources.Load("XML/" + name, typeof(TextAsset)) as TextAsset;
        Interpret(data.text, num);
    }


    private void Process_S(string name, int num)
    {
//        string path = null;

//#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)

//        path = "file:///" + Application.streamingAssetsPath;

//#elif UNITY_ANDROID

//                            path =  "jar:file://" + Application.dataPath + "!/assets";

//#elif UNITY_IOS

//                            path = "file:///" + Application.streamingAssetsPath;

//#endif

        string strPath = string.Empty;

#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)

        strPath += ("file:///");

        strPath += (Application.streamingAssetsPath + "/" + name + ".xml");

#elif UNITY_ANDROID

       strPath =  "jar:file://" + Application.dataPath + "!/assets/" + name + ".xml";

#endif

        WWW www = new WWW(strPath);
        while (!www.isDone) { }

        Interpret(www.text, num);
    }


    private void Interpret(string _strSource, int num)
    {
        StringReader stringReader = new StringReader(_strSource);

        //stringReader.Read();

        XmlNodeList xmlNodeList = null;

        XmlDocument xmlDoc = new XmlDocument();

        xmlDoc.LoadXml(stringReader.ReadToEnd());

        xmlNodeList = xmlDoc.SelectNodes(XMLData[num].ToString() + "DB/" + XMLData[num].ToString());

        SetNewData(num);

        int i = 0;
        foreach (XmlNode node in xmlNodeList)
        {
            switch (XMLData[num])
            {
                case XMLDATANUMBER.EventData:
                    GetEventData(node);
                    break ;
            }
            i++;
        }
    }
    private void SetNewData(int num) // ÃÊ±âÈ­.
    {
        switch (XMLData[num])
        {
            case XMLDATANUMBER.EventData:
                EventManager.Instance.eventCase = new Dictionary<int, EventData>();
                break ;
        }
    }

    private void GetEventData(XmlNode node)
    {
        EventData eventData = new EventData();

        int index = int.Parse(node.SelectSingleNode("EventIndex").InnerText);
        eventData.index = index;
        eventData.pieceIndex = int.Parse(node.SelectSingleNode("PieceIndex").InnerText);
        eventData.time = float.Parse(node.SelectSingleNode("Time").InnerText);
        eventData.condition = int.Parse(node.SelectSingleNode("Condition").InnerText);
        eventData.eventText = (node.SelectSingleNode("Text").InnerText);

        EventManager.Instance.eventCase.Add(index, eventData);

        //EX)
        //int talkCount = int.Parse(node.SelectSingleNode("TalkCount").InnerText);
        //string[] talks = new string[talkCount];
        //for (int i = 0; i < talkCount; i++)
        //{
        //    talks[i] = node.SelectSingleNode("Content_" + (i).ToString()).InnerText;
        //}
        //ChapterTraining chapter = (ChapterTraining)Enum.Parse(typeof(ChapterTraining), node.SelectSingleNode("TalkTrainingType").InnerText);
        //if(DataManager.Instance.GetNoticeList.ContainsKey(chapter) == false)
        //    DataManager.Instance.GetNoticeList.Add(chapter, talks);
    }

  
}






