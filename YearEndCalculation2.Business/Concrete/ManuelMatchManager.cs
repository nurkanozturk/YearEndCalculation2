using System;
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
        List<string> _matchedRecords = new List<string>();

        public static XmlNodeList TakeMachedRecords(string queryId)
        {
            if (!File.Exists("Matches.xml"))
            {
                return null;
            }
            
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("Matches.xml");
            XmlNode datas = xmlDocument.SelectSingleNode("datas");
            
            XmlNode query = datas.SelectNodes(string.Format("//query[@id='{0}']", queryId))[0];
            return query.SelectNodes("match");
            
        }
        public List<string> SaveMatches(string queryId, List<ActionRecord> matchedItems)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlNode query = null;
            _queryId = queryId;

            if (!File.Exists("Matches.xml"))
            {
                XmlElement datas = xmlDocument.CreateElement("datas");
                xmlDocument.AppendChild(datas);

                query = xmlDocument.CreateElement("query");
                XmlAttribute id = xmlDocument.CreateAttribute("id");
                id.Value = _queryId;
                query.Attributes.Append(id);
                datas.AppendChild(query);

            }


            if (File.Exists("Matches.xml"))
            {
                xmlDocument.Load("Matches.xml");
                XmlNode datas = xmlDocument.SelectSingleNode("datas");

                bool queryExist = false;
                XmlNodeList queries = datas.SelectNodes("query");

                foreach (XmlNode node in queries)
                {
                    if (node.Attributes[0].Value == queryId)
                    {
                        queryExist = true;
                        query = node;
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
            xmlDocument.Save("Matches.xml");
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

                XmlAttribute type = xmlDocument.CreateAttribute("type");
                type.Value = matchedItem.Type;
                item.Attributes.Append(type);

                XmlAttribute price = xmlDocument.CreateAttribute("price");
                price.Value = matchedItem.Price.ToString();
                item.Attributes.Append(price);

                match.AppendChild(item);

                _matchedRecords.Add(matchedItem.Id);
            }

        }


    }
}