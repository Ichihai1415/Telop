using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Telop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private async void Xml_Tick(object sender, EventArgs e)
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
            Xml.Interval = 60000;
            ////json変換
            XmlDocument XmlDocument_Main = new XmlDocument();
            XmlDocument_Main.Load("https://www.data.jma.go.jp/developer/xml/feed/eqvol.xml");
            string JsonText_Main = JsonConvert.SerializeXmlNode(XmlDocument_Main);
            JMAxml_EqVol_Main.JMAXML EqVol_Main = JsonConvert.DeserializeObject<JMAxml_EqVol_Main.JMAXML>(JsonText_Main);

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
            for (int i = 0; i < EqVol_Main.Feed.Entry[i].Title.Count(); i++)
            {
                EqVolMain_forViewer[i].Title = EqVol_Main.Feed.Entry[i].Title;
                EqVolMain_forViewer[i].URL = EqVol_Main.Feed.Entry[i].Id;
                EqVolMain_forViewer[i].UpdateTime = EqVol_Main.Feed.Entry[i].Updated + TimeSpan.FromHours(9);
                EqVolMain_forViewer[i].Source = EqVol_Main.Feed.Entry[i].Author.Name;
                EqVolMain_forViewer[i].Content = EqVol_Main.Feed.Entry[i].Content.Text.Replace("\n", "　").Replace("　　", "");
                if (Convert.ToString(EqVol_Main.Feed.Entry[i].Title).Contains("火") || Convert.ToString(EqVol_Main.Feed.Entry[i].Title).Contains("ああああ速報"))
                {
                    if (EqVolMain.Title == null)
                    {
                        EqVolMain.Title = EqVol_Main.Feed.Entry[i].Title;
                        EqVolMain.URL = EqVol_Main.Feed.Entry[i].Id;
                        EqVolMain.UpdateTime = EqVol_Main.Feed.Entry[i].Updated + TimeSpan.FromHours(9);
                        EqVolMain.Source = EqVol_Main.Feed.Entry[i].Author.Name;
                        EqVolMain.Content = EqVol_Main.Feed.Entry[i].Content.Text.Replace("\n", "　").Replace("　　", "");
                    }
                }
            }
            if (AccessedURLs.Contains(EqVolMain.URL))
            {
                if (TelopHide.Location.X == 1280)
                    await ViewClose();
            }
            else
            {
                //噴火警報
                //EqVolMain.URL = "https://www.data.jma.go.jp/developer/xml/data/20220724112234_0_VFVO56_400000.xml";
                AccessedURLs.Add(EqVolMain.URL);
                string TelopText = EqVolMain.Content;
                if (EqVolMain.Title == "火山の状況に関する解説情報")
                {

                }
                else
                {
                    XmlDocument XmlDocument_Detail = new XmlDocument();
                    XmlDocument_Detail.Load(EqVolMain.URL);
                    string JsonText_Detail = JsonConvert.SerializeXmlNode(XmlDocument_Detail);
                    if (EqVolMain.Title == "噴火に関する火山観測報")
                    {
                        JMAxml_EqVol_Detail_Observation.JMAXML EqVol_Detail = JsonConvert.DeserializeObject<JMAxml_EqVol_Detail_Observation.JMAXML>(JsonText_Detail);

                        string Time = $"日時:{EqVol_Detail.JmxReport.Body.VolcanoInfo.Item.EventTime.EventDateTime.Text}";
                        string Point = $"{EqVol_Detail.JmxReport.Body.VolcanoInfo.Item.Areas.CodeType}:{EqVol_Detail.JmxReport.Body.VolcanoInfo.Item.Areas.Area.Name}";
                        string Kind = $"現象:{EqVol_Detail.JmxReport.Body.VolcanoInfo.Item.Kind.Name}";
                        string PlumeHeight = $"{EqVol_Detail.JmxReport.Body.VolcanoObservation.ColorPlume.JmxEbPlumeHeightAboveCrater.Type}:{EqVol_Detail.JmxReport.Body.VolcanoObservation.ColorPlume.JmxEbPlumeHeightAboveCrater.Description}";
                        string PlumeDirection = $"{EqVol_Detail.JmxReport.Body.VolcanoObservation.ColorPlume.JmxEbPlumeDirection.Type}:{EqVol_Detail.JmxReport.Body.VolcanoObservation.ColorPlume.JmxEbPlumeDirection.Description}";
                        string Other = $"{EqVol_Detail.JmxReport.Body.VolcanoObservation.OtherObservation}";
                        TelopText = $"{Time}  {Point}  {Kind}  {PlumeHeight}  {PlumeDirection}  {Other}";
                    }
                    /*else if (EqVolMain.Title == "噴火に関する火山観測報")//噴火警報時
                    {
                        JMAxml_EqVol_Detail_EruptionBulletin.JMAXML EqVol_Detail = JsonConvert.DeserializeObject<JMAxml_EqVol_Detail_EruptionBulletin.JMAXML>(JsonText_Detail);
                        string Main = $"{EqVol_Detail.JmxReport.Body.VolcanoInfoContent.VolcanoActivity}";
                        string Areas = "";
                        for (int i = 0; i < EqVol_Detail.JmxReport.Body.VolcanoInfo[1].Item.Areas.Area.Count(); i++)
                        {
                            Areas += "・" + EqVol_Detail.JmxReport.Body.VolcanoInfo[1].Item.Areas.Area[i].Name;
                        }
                        Areas = Areas.Remove(0, 1);
                        TelopText = $"{Main}  対象市区町村等:{Areas}";
                        EqVolMain.Title += EqVol_Detail.JmxReport.Body.VolcanoInfoContent.VolcanoHeadline;
                    }*/
                    else
                    {
                        File.WriteAllText($"Log\\UnknownInfo\\{DateTime.Now:yyyyMMddHHmm}.txt", $"{EqVolMain.URL}\n\n{JsonText_Main}\n\n{JsonText_Detail}");

                    }
                }
                await ViewOpen();
                Title.Text = $"{EqVolMain.Title}　　　{EqVolMain.UpdateTime}発表";
                MainText.Text = TelopText;
            }
        }
        private void Move_Tick(object sender, EventArgs e)
        {
            if (MainText.Location.X > MainText.Width * -1 && MainText.Width > 1280)
            {
                MainText.Location = new Point(MainText.Location.X - 10, MainText.Location.Y);
            }
            else if (1280 > MainText.Width)
            {
                MainText.Location = new Point(0, MainText.Location.Y);
            }
            else if (MainText.Location.X < MainText.Width * -1)
            {
                MainText.Location = new Point(1280, MainText.Location.Y);
            }
        }
        public List<string> AccessedURLs = new List<string>();
        public string NowTime_ = "";
        private void Time_Tick(object sender, EventArgs e)
        {
            NowTime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }
        private void TimeCheck_Tick(object sender, EventArgs e)
        {
            if (NowTime_ == "")
            {
                NowTime_ = DateTime.Now.ToString("ss");
            }
            if (NowTime_ != DateTime.Now.ToString("ss"))
            {
                Time.Enabled = true;
                TimeCheck.Enabled = false;
            }
        }
        public async Task ViewOpen()
        {
            for (int i = 40; i <= 1280; i += 40)
            {
                TelopHide.Location = new Point(i, 0);
                await Task.Delay(10);
            }
            MainText.Location = new Point(1280, MainText.Location.Y);
            return;
        }
        public async Task ViewClose()
        {
            for (int i = 1240; i >= 0; i -= 40)
            {
                TelopHide.Location = new Point(i, 0);
                await Task.Delay(10);
            }
            MainText.Location = new Point(1280, MainText.Location.Y);
            Title.Text = "";
            MainText.Text = "";
            return;
        }
    }
}