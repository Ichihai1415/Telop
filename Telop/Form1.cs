using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Telop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Xml_Tick(object sender, EventArgs e)
        {
            {
                /*xml linq
                            Console.WriteLine("gg1");

                            XElement EqVolXML = XElement.Load("https://www.data.jma.go.jp/developer/xml/feed/eqvol.xml");
                            IEnumerable<XElement> Elements = from EqVolXMLItems in EqVolXML.Descendants("feed")
                                                             //where Convert.ToString(EqVolXMLItems.Element("title")).Contains("火") == true
                                                             select EqVolXMLItems;
                            EqVolClassMain EqVolMain = new EqVolClassMain();
                            Console.WriteLine("gg2");
                            if (EqVolXML.Descendants("feed") == null)
                            {
                                Console.WriteLine("feed null.");
                            }
                            if (EqVolXML.Descendants("entry") == null)
                            {
                                Console.WriteLine("entry null.");
                            }
                            IEnumerable Elem = EqVolXML.Elements("feed");
                            Console.WriteLine(Elem);
                                string name = EqVolXML.Element("entry").Element("title").Value;          //要素「name」の値
                                Console.WriteLine("a{0}",name);

                            foreach (XElement info in Elements)
                            {
                                EqVolMain.Title.Add(info.Element("title").Value);
                                EqVolMain.URL.Add(info.Element("id").Value);
                                EqVolMain.UpdateTime.Add(DateTime.ParseExact(info.Element("updated").Value, "yyyy-MM-ddTHH:mm:ssZ", null) + TimeSpan.FromHours(9));
                                EqVolMain.Source.Add(info.Element("author").Element("name").Value);
                                EqVolMain.Content.Add(info.Element("content").Value);
                                Console.WriteLine(info.Element("title").Value);
                                Console.WriteLine(info.Element("id").Value);
                                Console.WriteLine(DateTime.ParseExact(info.Element("updated").Value, "yyyy-MM-ddTHH:mm:ssZ", null) + TimeSpan.FromHours(9));
                                Console.WriteLine(info.Element("author").Element("name").Value);
                                Console.WriteLine(info.Element("content").Value);
                                Console.WriteLine("gg-");
                            }
                            Console.WriteLine("gg3");
                                Console.WriteLine(EqVolXML.Element("title").Value);*/
            }//xml linq
            Xml.Interval = 999999999;
            ////json変換
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("https://www.data.jma.go.jp/developer/xml/feed/eqvol.xml");
            string jsonText = JsonConvert.SerializeXmlNode(xmlDoc);
            File.WriteAllText("eqvol.json", jsonText);
            JMAxml_EqVol_Main.JMAXML EqVol = JsonConvert.DeserializeObject<JMAxml_EqVol_Main.JMAXML>(jsonText);

            EqVolClassMain EqVolMain = new EqVolClassMain();
            List<EqVolClassMain> EqVolMain_forViewer = new List<EqVolClassMain>
            {
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
                new EqVolClassMain(),
            };
            for (int i = 0; i < EqVol.Feed.Entry[i].Title.Count(); i++)
            {
                EqVolMain_forViewer[i].Title = EqVol.Feed.Entry[i].Title;
                EqVolMain_forViewer[i].URL = EqVol.Feed.Entry[i].Id;
                EqVolMain_forViewer[i].UpdateTime = EqVol.Feed.Entry[i].Updated + TimeSpan.FromHours(9);
                EqVolMain_forViewer[i].Source = EqVol.Feed.Entry[i].Author.Name;
                EqVolMain_forViewer[i].Content = EqVol.Feed.Entry[i].Content.Text.Replace("\n", "　").Replace("　　", "");
                if (Convert.ToString(EqVol.Feed.Entry[i].Title).Contains("火") || Convert.ToString(EqVol.Feed.Entry[i].Title).Contains("ああああ速報"))
                {
                    if (EqVolMain.Title == null)
                    {
                        EqVolMain.Title = EqVol.Feed.Entry[i].Title;
                        EqVolMain.URL = EqVol.Feed.Entry[i].Id;
                        EqVolMain.UpdateTime = EqVol.Feed.Entry[i].Updated + TimeSpan.FromHours(9);
                        EqVolMain.Source = EqVol.Feed.Entry[i].Author.Name;
                        EqVolMain.Content = EqVol.Feed.Entry[i].Content.Text.Replace("\n", "　").Replace("　　", "");
                    }
                }
            }
            if (EqVolMain.Title == "火山の状況に関する解説情報")
            {
                Title.Text = $"{EqVolMain.Title}　　　{EqVolMain.UpdateTime}";
                MainText.Text = EqVolMain.Content;
            }
            else
            {
            XmlDocument xmlDoc2 = new XmlDocument();
            xmlDoc2.Load(EqVolMain.URL);
            string jsonText2 = JsonConvert.SerializeXmlNode(xmlDoc2);
            File.WriteAllText("eqvol2.json", jsonText2);


            string TelopText = "";

            if (EqVolMain.Title == "噴火に関する火山観測報")
            {
                JMAxml_EqVol_Detail_Observation.JMAXML EqVol2 = JsonConvert.DeserializeObject<JMAxml_EqVol_Detail_Observation.JMAXML>(jsonText2);

                string Time = $"日時:{EqVol2.JmxReport.Body.VolcanoInfo.Item.EventTime.EventDateTime.Text}"; 
                string Area = $"{EqVol2.JmxReport.Body.VolcanoInfo.Item.Areas.CodeType}:{EqVol2.JmxReport.Body.VolcanoInfo.Item.Areas.Area.Name}";
                string Kind = $"現象:{EqVol2.JmxReport.Body.VolcanoInfo.Item.Kind.Name}";
                string PlumeHeight = $"{EqVol2.JmxReport.Body.VolcanoObservation.ColorPlume.JmxEbPlumeHeightAboveCrater.Type}:{EqVol2.JmxReport.Body.VolcanoObservation.ColorPlume.JmxEbPlumeHeightAboveCrater.Description}";
                string PlumeDirection = $"{EqVol2.JmxReport.Body.VolcanoObservation.ColorPlume.JmxEbPlumeDirection.Type}:{EqVol2.JmxReport.Body.VolcanoObservation.ColorPlume.JmxEbPlumeDirection.Description}";
                string Other = $"{EqVol2.JmxReport.Body.VolcanoObservation.OtherObservation}";
                TelopText = $"{Time}  {Area}  {Kind}  {PlumeHeight}  {PlumeDirection}  {Other}";
            }
            
            Title.Text = $"{EqVolMain.Title}　　　{EqVolMain.UpdateTime}";
            MainText.Text = TelopText;
            }

            MainText.Location = new Point(1280, MainText.Location.Y);
        }
        private void Move_Tick(object sender, EventArgs e)
        {
            Console.WriteLine(MainText.Location.X);
            if (MainText.Location.X > MainText.Width * -1 && 1280 < MainText.Width && ViewTime > 0)
            {
                MainText.Location = new Point(MainText.Location.X - 10, MainText.Location.Y);
            }
            else if (1280 > MainText.Width)
            {
                MainText.Location = new Point(0, MainText.Location.Y);
            }
            else if (MainText.Location.X > MainText.Width * -1 || ViewTime < 0)
            {
                MainText.Location = new Point(1300, MainText.Location.Y);
            }
            else if (ViewTime < 0)
            {
                Title.Text = "";
            }
        }
        public List<string> AccessedURLs = new List<string>();
        public int ViewTime = 60;

        private void Time_Tick(object sender, EventArgs e)
        {
            ViewTime -= 1;
        }
    }
}