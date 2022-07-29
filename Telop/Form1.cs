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
            XmlDocument XmlDocument_Main = new XmlDocument();
            XmlDocument_Main.Load("https://www.data.jma.go.jp/developer/xml/feed/eqvol.xml");
            string JsonText_Main = JsonConvert.SerializeXmlNode(XmlDocument_Main);
            JMAxml_EqVol_Main.JMAXML EqVol_Main = JsonConvert.DeserializeObject<JMAxml_EqVol_Main.JMAXML>(JsonText_Main);

            EqVolClassMain EqVolMain = new EqVolClassMain
            {
                URL = "temp"//null防止(念のため)
            };
            List<EqVolClassMain> EqVolMain_forViewer = new List<EqVolClassMain>();//電文ビューワー用
            for (int i = 0; i < EqVol_Main.Feed.Entry[i].Title.Count(); i++)
            {
                EqVolClassMain EqVolMain_forViewerTemp = new EqVolClassMain
                {
                    Title = EqVol_Main.Feed.Entry[i].Title,
                    URL = EqVol_Main.Feed.Entry[i].Id,
                    UpdateTime = EqVol_Main.Feed.Entry[i].Updated + TimeSpan.FromHours(9),
                    Source = EqVol_Main.Feed.Entry[i].Author.Name,
                    Content = EqVol_Main.Feed.Entry[i].Content.Text.Replace("\n", "　").Replace("　　", "")
                };
                EqVolMain_forViewer.Add(EqVolMain_forViewerTemp);
                if (Convert.ToString(EqVol_Main.Feed.Entry[i].Title).Contains("火"))
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
            if (AccessedURLs.Contains(EqVolMain.URL))//重複防止
            {

            }
            else if (EqVolMain.Title != null)//対象あり
            {
                //噴火警報用
                //EqVolMain.URL = "https://www.data.jma.go.jp/developer/xml/data/20220724112234_0_VFVO56_400000.xml";
                //火山の状況用
                //EqVolMain.URL = "https://www.data.jma.go.jp/developer/xml/data/20220726070012_0_VFVO51_400000.xml";
                AccessedURLs.Add(EqVolMain.URL);
                string TelopText = EqVolMain.Content;
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
                else if (EqVolMain.Title == "火山の状況に関する解説情報")
                {
                    JMAxml_EqVol_Detail_Commentary.JMAXML EqVol_Detail = JsonConvert.DeserializeObject<JMAxml_EqVol_Detail_Commentary.JMAXML>(JsonText_Detail);
                    string Info1 = EqVol_Detail.JmxReport.Body.VolcanoInfoContent.VolcanoHeadline.Replace("\n", "").Replace("　", "");
                    string Info2 = EqVol_Detail.JmxReport.Body.VolcanoInfoContent.VolcanoActivity.Replace("\n", "").Replace("　", "");
                    string Info3 = EqVol_Detail.JmxReport.Body.VolcanoInfoContent.VolcanoPrevention.Replace("\n", "").Replace("　", "");
                    string Info4 = EqVol_Detail.JmxReport.Body.VolcanoInfoContent.NextAdvisory.Replace("\n", "").Replace("　", "");
                    TelopText = Info1 + Info2 + Info3 + Info4;
                }
                else if (EqVolMain.Title == "噴火警報")//未実装(.JmxReport.Body.VolcanoInfo[]で.Item.Areas.Areaに、[0]では"{}"、[1]では"[]"が含まれるため処理不可)
                {//VolcanoInfo type="噴火速報"とVolcanoInfo type="噴火速報（対象市町村等）"　　//classを削除すれば処理可能(削除するなら取得する意味ほぼないためパス)
                    /*   
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
                    */
                }
                else
                {
                    File.WriteAllText($"Log\\UnknownInfo\\{DateTime.Now:yyyyMMddHHmm}.txt", $"{EqVolMain.URL}\n\n{JsonText_Main}\n\n{JsonText_Detail}");
                }
                if (Title.Text == "")//初回
                {
                    await ViewOpen(true);
                    Title.Text = $"{EqVolMain.Title}　　　{EqVolMain.UpdateTime}発表";
                    MainText.Text = TelopText;
                }
                else if (Title.Text != $"{EqVolMain.Title}　　　{EqVolMain.UpdateTime}発表")//更新
                {
                    await ViewClose(false);
                    await ViewOpen(false);
                    Title.Text = $"{EqVolMain.Title}　　　{EqVolMain.UpdateTime}発表";
                    MainText.Text = TelopText;
                }
            }
            else//対象なし
            {

            }
        }
        private async void Move_Tick(object sender, EventArgs e)
        {
            if (MainText.Location.X > MainText.Width * -1)//流し中
            {
                MainText.Location = new Point(MainText.Location.X - 10, MainText.Location.Y);
            }
            if (MainText.Location.X <= MainText.Width * -1)//流し終了
            {
                if (RemainingDisplayNum > 0)//もう一度
                {
                    RemainingDisplayNum--;
                    MainText.Location = new Point(1280, MainText.Location.Y);
                }
                if (RemainingDisplayNumberDefalt == -1)//無限
                {
                    MainText.Location = new Point(1280, MainText.Location.Y);
                }
                else//流し終了
                {
                    if (UserText == "null" && TelopHide.Location.X != 1280)//終了
                    {
                        await ViewClose(true);
                    }
                    else//ユーザーテキスト表示
                    {
                        Title.Text = UserTitle;
                        MainText.Text = UserText;
                        RemainingDisplayNum = 2;
                        MainText.Location = new Point(1280, MainText.Location.Y);
                    }
                }
            }
        }
        public List<string> AccessedURLs = new List<string>();
        public string NowTimeTemp = "";
        public string UserTitle = "えええ";
        public string UserText = "ああああああ";
        public int RemainingDisplayNumberDefalt = -1;
        public int RemainingDisplayNum = 3;
        private void Time_Tick(object sender, EventArgs e)
        {
            NowTime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }
        private void TimeCheck_Tick(object sender, EventArgs e)
        {
            if (NowTimeTemp == "")
            {
                NowTimeTemp = DateTime.Now.ToString("ss");
            }
            if (NowTimeTemp != DateTime.Now.ToString("ss"))
            {
                Time.Enabled = true;
                TimeCheck.Enabled = false;
            }
        }
        public async Task ViewOpen(bool First)//初期はゆっくり
        {
            if (First)
            {
                for (int i = TelopHide.Location.X; i <= 1280; i += 40)
                {
                    TelopHide.Location = new Point(i, 0);
                    await Task.Delay(10);
                }
                Title.Text = "";
                MainText.Text = "";
            }
            else
            {
                for (int i = TelopHide.Location.X; i <= 1280; i += 40)
                {
                    TelopHide.Location = new Point(i, 0);
                    await Task.Delay(5);
                }
            }
            MainText.Location = new Point(1280, MainText.Location.Y);
            return;
        }
        public async Task ViewClose(bool Clear)//最後はゆっくり
        {
            if (Clear)
            {
                for (int i = TelopHide.Location.X; i >= 0; i -= 40)
                {
                    TelopHide.Location = new Point(i, 0);
                    await Task.Delay(10);
                }
                Title.Text = "";
                MainText.Text = "";
            }
            else
            {
                for (int i = TelopHide.Location.X; i >= 0; i -= 40)
                {
                    TelopHide.Location = new Point(i, 0);
                    await Task.Delay(5);
                }
            }
            MainText.Location = new Point(1280, MainText.Location.Y);
            return;
        }
    }
}