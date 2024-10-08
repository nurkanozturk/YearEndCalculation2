﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using YearEndCalculation.Entities.Concrete;

namespace YearEndCalculation.Business.Concrete
{
    public class ManuelMatchManager
    {
        string _queryId = null;
        static readonly string _path = "C:/TdmsMkys";
        static readonly string _fileName = "Matches.xml";
        readonly List<string> _matchedRecords = new List<string>();
        readonly XmlDocument xmlDocument = new XmlDocument();
        public static XmlNodeList TakeMachedRecords(string queryId)
        {
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
            try
            {
                if (!File.Exists(_path+"/"+_fileName))
                {
                    return null;
                }

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(_path + "/" + _fileName);
                XmlNode datas = xmlDocument.SelectSingleNode("datas");

                XmlNode query = datas.SelectNodes(string.Format("//query[@id='{0}']", queryId))[0];
                if (query == null)
                {
                    return null;
                }
                return query.SelectNodes("match");
            }
            catch
            {
                return null;
            }
            
            
        }
        public List<string> SaveMatches(string queryId, List<ActionRecord> matchedItems)
        {
            
            XmlNode query = null;
            _queryId = queryId;

            if (!File.Exists(_path + "/" + _fileName))
            {
                XmlElement datas = xmlDocument.CreateElement("datas");
                xmlDocument.AppendChild(datas);

                query = xmlDocument.CreateElement("query");
                XmlAttribute id = xmlDocument.CreateAttribute("id");
                id.Value = _queryId;
                query.Attributes.Append(id);
                datas.AppendChild(query);

            }
            else
            {
                xmlDocument.Load(_path + "/" + _fileName);
                XmlNode datas = xmlDocument.SelectSingleNode("datas");

                bool queryExist = false;
                if (datas!=null)
                {
                    XmlNodeList queries = datas.SelectNodes("query");

                    foreach (XmlNode node in queries)
                    {
                        if (node.Attributes[0].Value == queryId)
                        {
                            queryExist = true;
                            query = node;
                        }
                    }
                }

                if (!queryExist)
                {
                    query = xmlDocument.CreateElement("query");
                    XmlAttribute id = xmlDocument.CreateAttribute("id");
                    id.Value = _queryId;
                    query.Attributes.Append(id);
                    datas.AppendChild(query);
                }
            }
            CreateQueryRecord(xmlDocument, query, matchedItems);
           
            xmlDocument.Save(_path + "/" + _fileName);
            return _matchedRecords;
        }

        void CreateQueryRecord(XmlDocument xmlDocument, XmlNode query, List<ActionRecord> matchedItems)
        {
            string matchIdValue = "";
            foreach (ActionRecord item in matchedItems)
            {
                matchIdValue += item.Id + "-";
            }
            XmlElement match = xmlDocument.CreateElement("match");
            XmlAttribute matchId = xmlDocument.CreateAttribute("id");
            matchId.Value = matchIdValue;
            match.Attributes.Append(matchId);
            query.AppendChild(match);

            foreach (ActionRecord matchedItem in matchedItems)
            {
                XmlElement item = xmlDocument.CreateElement("item");

                XmlAttribute id = xmlDocument.CreateAttribute("id");
                id.Value = matchedItem.Id;
                item.Attributes.Append(id);

                XmlAttribute docNumber = xmlDocument.CreateAttribute("docNumber");
                docNumber.Value = matchedItem.DocNumber;
                item.Attributes.Append(docNumber);

                XmlAttribute docDate = xmlDocument.CreateAttribute("docDate");
                docDate.Value = matchedItem.DocDate.ToString();
                item.Attributes.Append(docDate);

                XmlAttribute type = xmlDocument.CreateAttribute("type");
                type.Value = matchedItem.Type;
                item.Attributes.Append(type);

                XmlAttribute explanation = xmlDocument.CreateAttribute("explanation");
                explanation.Value = matchedItem.Explanation;
                item.Attributes.Append(explanation);

                XmlAttribute price = xmlDocument.CreateAttribute("price");
                price.Value = matchedItem.Price.ToString();
                item.Attributes.Append(price);

                match.AppendChild(item);

                _matchedRecords.Add(matchedItem.Id);
            }

        }

        public void RemoveMatchFromXml(string queryId, string matchId)
        {
            xmlDocument.Load(_path + "/" + _fileName);
            XmlNode datas = xmlDocument.SelectSingleNode("datas");
            XmlNode query = datas.SelectNodes(string.Format("//query[@id='{0}']", queryId))[0];
            XmlNode match = query.SelectNodes(string.Format("//match[@id='{0}']", matchId))[0];
            query.RemoveChild(match);
            xmlDocument.Save(_path + "/" + _fileName);
            
        }
    }
}