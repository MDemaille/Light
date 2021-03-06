﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class Level : MonoBehaviour
{
    [HideInInspector]
    public LevelSize Size = LevelSize.Small;
    [HideInInspector]
    public List<GameObject> RedElements = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> GreenElements = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> BlueElements = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> AllLayersElements = new List<GameObject>();

    public List<GameObject> LevelObjects;

    public void Clear()
    {
        RedElements.Clear();
        GreenElements.Clear();
        BlueElements.Clear();
        AllLayersElements.Clear();
    }

    void InstantiateLevel()
    {
        List<GameObject> elementList = new List<GameObject>();
        elementList.AddRange(RedElements);
        elementList.AddRange(GreenElements);
        elementList.AddRange(BlueElements);
        elementList.AddRange(AllLayersElements);

        foreach (GameObject element in elementList)
        {
            Instantiate(element, element.transform.position, element.transform.localRotation);
        }
    }

    public void LoadLevel(string filename)
    {
        XmlReader myXmlTextReader = new XmlTextReader(Application.dataPath + "/" + filename + ".xml");
        Debug.Log(Application.dataPath + "/" + filename + ".xml");

        GameObject levelObject = new GameObject();

        while (myXmlTextReader.Read())
        {
            if (myXmlTextReader.IsStartElement() && myXmlTextReader.Name == "Level")
            {
                Size = (LevelSize)int.Parse(myXmlTextReader.GetAttribute("Size"));
            }

            if (myXmlTextReader.IsStartElement() && myXmlTextReader.Name == "LevelObject")
            {
                int ID = int.Parse(myXmlTextReader.GetAttribute("ID"));
                levelObject = Instantiate(LevelObjects[ID]);
                ColorEnum layer = (ColorEnum) int.Parse(myXmlTextReader.GetAttribute("Layer"));
                levelObject.GetComponent<LevelObject>().Layer =layer;
                levelObject.GetComponent<LevelObject>().ObjectId = ID;

                if (layer.Equals(ColorEnum.All))
                    AllLayersElements.Add(levelObject);
                if (layer.Equals(ColorEnum.Red))
                    RedElements.Add(levelObject);
                if (layer.Equals(ColorEnum.Green))
                    GreenElements.Add(levelObject);
                if (layer.Equals(ColorEnum.Blue))
                    BlueElements.Add(levelObject);
            }

            if (myXmlTextReader.IsStartElement() && myXmlTextReader.Name == "Position")
            {
                float x = float.Parse(myXmlTextReader.GetAttribute("X"));
                float y = float.Parse(myXmlTextReader.GetAttribute("Y"));
                levelObject.transform.position = new Vector2(x, y);
            }

            if (myXmlTextReader.IsStartElement() && myXmlTextReader.Name == "Rotation")
            {
                float value = float.Parse(myXmlTextReader.GetAttribute("value"));
                levelObject.transform.localRotation = Quaternion.AngleAxis(value, Vector3.forward);
            }

            if (myXmlTextReader.IsStartElement() && myXmlTextReader.Name == "Scale")
            {
                float x = float.Parse(myXmlTextReader.GetAttribute("X"));
                float y = float.Parse(myXmlTextReader.GetAttribute("Y"));
                levelObject.transform.localScale = new Vector2(x, y);
            }
        }

        myXmlTextReader.Close();
    }

    public void SaveLevel(string filename)
    {
        List<GameObject> elementList = new List<GameObject>();
        elementList.AddRange(RedElements);
        elementList.AddRange(GreenElements);
        elementList.AddRange(BlueElements);
        elementList.AddRange(AllLayersElements);

        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        settings.NewLineOnAttributes = true;
        settings.CloseOutput = true;
        XmlWriter xmlWriter = XmlWriter.Create(Application.dataPath + "/" + filename + ".xml", settings);
        
        Debug.Log(Application.dataPath + "/" + filename + ".xml");

        xmlWriter.WriteStartDocument();
        xmlWriter.WriteStartElement("Level");
        xmlWriter.WriteAttributeString("Size", ((int)Size).ToString());

        foreach (GameObject element in elementList)
        {
            LevelObject levelObject = element.GetComponent<LevelObject>();
            int Id = levelObject.ObjectId;
            int Layer = (int)levelObject.Layer;

            xmlWriter.WriteStartElement("LevelObject");
            xmlWriter.WriteAttributeString("ID", Id.ToString());
            xmlWriter.WriteAttributeString("Layer", Layer.ToString());

            xmlWriter.WriteStartElement("Position");
            xmlWriter.WriteAttributeString("X", element.transform.position.x.ToString());
            xmlWriter.WriteAttributeString("Y", element.transform.position.y.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Rotation");
            xmlWriter.WriteAttributeString("value", element.transform.rotation.eulerAngles.z.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Scale");
            xmlWriter.WriteAttributeString("X", element.transform.localScale.x.ToString());
            xmlWriter.WriteAttributeString("Y", element.transform.localScale.y.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
        }

        xmlWriter.WriteEndDocument();
        xmlWriter.Close();
    }
}
