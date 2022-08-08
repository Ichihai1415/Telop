﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private void Xml_Tick(object sender, EventArgs e)
        {
            Xml.Interval = 60000;
            //地震火山
            XmlDocument XmlDocument_EqVolMain = new XmlDocument();
            XmlDocument_EqVolMain.Load("https://www.data.jma.go.jp/developer/xml/feed/eqvol.xml");
            string JsonText_EqVolMain = JsonConvert.SerializeXmlNode(XmlDocument_EqVolMain);
            JMAxml_EqVol_Main.JMAXML EqVol_Main = JsonConvert.DeserializeObject<JMAxml_EqVol_Main.JMAXML>(JsonText_EqVolMain);
            if (AccessedURLs.Count != 0)//初回以外動作　通常!=
            {
                for (int i = 10; i > 0; i--)
                {
                    if (AccessedURLs.Contains(EqVol_Main.Feed.Entry[i].Id) == false)
                    {
                        if (EqVol_Main.Feed.Entry[i].Title.Contains("火") || EqVol_Main.Feed.Entry[i].Title.Contains("灰"))
                        {

                            string Title = EqVol_Main.Feed.Entry[i].Title;
                            string URL = EqVol_Main.Feed.Entry[i].Id;
                            DateTime UpdateTime = EqVol_Main.Feed.Entry[i].Updated + TimeSpan.FromHours(9);
                            string Content = EqVol_Main.Feed.Entry[i].Content.Text.Replace("回", "回　").Replace("\n", "　").Replace("　　", "");
                            //噴火警報用
                            //EqVolMain.URL = "https://www.data.jma.go.jp/developer/xml/data/20220724112234_0_VFVO56_400000.xml";
                            //火山の状況用
                            //EqVolMain.URL = "https://www.data.jma.go.jp/developer/xml/data/20220726070012_0_VFVO51_400000.xml";
                            string TelopText = Content;
                            XmlDocument XmlDocument_Detail = new XmlDocument();
                            XmlDocument_Detail.Load(URL);
                            AccessedURLs.Add(URL);
                            string JsonText_EqVolDetail = JsonConvert.SerializeXmlNode(XmlDocument_Detail);
                            if (Title == "噴火に関する火山観測報")
                            {
                                JMAxml_EqVol_Detail_Observation.JMAXML EqVol_Detail = JsonConvert.DeserializeObject<JMAxml_EqVol_Detail_Observation.JMAXML>(JsonText_EqVolDetail);
                                string Time = $"日時:{EqVol_Detail.JmxReport.Body.VolcanoInfo.Item.EventTime.EventDateTime.Text}";
                                string Point = $"{EqVol_Detail.JmxReport.Body.VolcanoInfo.Item.Areas.CodeType}:{EqVol_Detail.JmxReport.Body.VolcanoInfo.Item.Areas.Area.Name}";
                                string Kind = $"現象:{EqVol_Detail.JmxReport.Body.VolcanoInfo.Item.Kind.Name}";
                                string PlumeHeight = $"{EqVol_Detail.JmxReport.Body.VolcanoObservation.ColorPlume.JmxEbPlumeHeightAboveCrater.Type}:{EqVol_Detail.JmxReport.Body.VolcanoObservation.ColorPlume.JmxEbPlumeHeightAboveCrater.Description}";
                                string PlumeDirection = $"{EqVol_Detail.JmxReport.Body.VolcanoObservation.ColorPlume.JmxEbPlumeDirection.Type}:{EqVol_Detail.JmxReport.Body.VolcanoObservation.ColorPlume.JmxEbPlumeDirection.Description}";
                                string Other = $"{EqVol_Detail.JmxReport.Body.VolcanoObservation.OtherObservation}";
                                TelopText = $"{Time}  {Point}  {Kind}  {PlumeHeight}  {PlumeDirection}  {Other}";
                            }
                            else if (Title == "火山の状況に関する解説情報")
                            {
                                JMAxml_EqVol_Detail_Commentary.JMAXML EqVol_Detail = JsonConvert.DeserializeObject<JMAxml_EqVol_Detail_Commentary.JMAXML>(JsonText_EqVolDetail);
                                string Info1 = EqVol_Detail.JmxReport.Body.VolcanoInfoContent.VolcanoHeadline.Replace("\n", "").Replace("　", "");
                                string Info2 = EqVol_Detail.JmxReport.Body.VolcanoInfoContent.VolcanoActivity.Replace("\n", "").Replace("　", "");
                                string Info3 = EqVol_Detail.JmxReport.Body.VolcanoInfoContent.VolcanoPrevention.Replace("\n", "").Replace("　", "");
                                string Info4 = EqVol_Detail.JmxReport.Body.VolcanoInfoContent.NextAdvisory.Replace("\n", "").Replace("　", "");
                                TelopText = Info1 + Info2 + Info3 + Info4;
                            }
                            else if (Title == "噴火警報")//未実装(.JmxReport.Body.VolcanoInfo[]で.Item.Areas.Areaに、[0]では"{}"、[1]では"[]"が含まれるため処理不可)
                            {//VolcanoInfo type="噴火速報"とVolcanoInfo type="噴火速報（対象市町村等）"　　//classを削除すれば処理可能(削除するなら取得する意味ほぼないためパス)
                                /*   
                                JMAxml_EqVol_Detail_EruptionBulletin.JMAXML EqVol_Detail = JsonConvert.DeserializeObject<JMAxml_EqVol_Detail_EruptionBulletin.JMAXML>(JsonText_EqVolDetail);
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
                            else if (Title.Contains("降灰予報"))
                            {
                                //そのまま
                            }
                            else
                                File.WriteAllText($"Log\\UnknownInfo\\{DateTime.Now:yyyyMMddHHmmss}.txt", $"{URL}\n\n{JsonText_EqVolMain}\n\n{JsonText_EqVolDetail}");
                            DisplayTitles.Add($"{Title}  {UpdateTime}");
                            DisplayTexts.Add(TelopText);
                        }

                    }
                }
            }
            else//初回動作
            {
                for (int i = 10; i > 0; i--)
                    AccessedURLs.Add(EqVol_Main.Feed.Entry[i].Id);
            }
            
            //随時
            XmlDocument XmlDocument_ExtraMain = new XmlDocument();
            XmlDocument_ExtraMain.Load("https://www.data.jma.go.jp/developer/xml/feed/extra.xml");
            string JsonText_ExtraMain = JsonConvert.SerializeXmlNode(XmlDocument_ExtraMain);
            JMAxml_Extra_Main.JMAXML Extra_Main = JsonConvert.DeserializeObject<JMAxml_Extra_Main.JMAXML>(JsonText_ExtraMain);
            if (AccessedURLs.Count != 0)//初回以外動作　通常!=
            {
                for (int i = 20; i >= 0; i--)
                {
                    if (AccessedURLs.Contains(Extra_Main.Feed.Entry[i].Id) == false)
                    {
                        if (!Extra_Main.Feed.Entry[i].Title.Contains("警報・注意報") && Extra_Main.Feed.Entry[i].Title != "熱中症警戒アラート" && Extra_Main.Feed.Entry[i].Title != "早期天候情報" && !Extra_Main.Feed.Entry[i].Title.Contains("台風解析・予報情報") && Extra_Main.Feed.Entry[i].Title != "竜巻注意情報（目撃情報付き）")
                        {
                            string Title = Extra_Main.Feed.Entry[i].Title;
                            string URL = Extra_Main.Feed.Entry[i].Id;
                            DateTime UpdateTime = Extra_Main.Feed.Entry[i].Updated + TimeSpan.FromHours(9);
                            string Content = Extra_Main.Feed.Entry[i].Content.Text.Replace("回", "回　").Replace("\n", "　").Replace("　　", "");
                            string TelopText = Content;
                            XmlDocument XmlDocument_ExtraDetail = new XmlDocument();
                            XmlDocument_ExtraDetail.Load(URL);
                            AccessedURLs.Add(URL);
                            string JsonText_ExtraDetail = JsonConvert.SerializeXmlNode(XmlDocument_ExtraDetail);
                            JObject Extra_Detail = JObject.Parse(JsonText_ExtraDetail);
                            if (Title.Contains("気象情報") || Title == "竜巻注意情報" || Title == "全般台風情報（定型）)" || Title == "スモッグ気象情報" || Title == "記録的短時間大雨情報")
                                Console.Write("そのまま");
                            else if (Title != "指定河川洪水予報")
                            {
                                string AddText = (string)Extra_Detail.SelectToken("jmx:Report.Body.Warning.Item[0].Kind.Property.Text");
                                TelopText += AddText;
                            }
                            else if (Title == "土砂災害警戒情報")
                            {
                                string Text = "";
                                string Kind1 = "";
                                string Area1 = "";
                                string Kind2 = "";
                                string Area2 = "";
                                string Kind3 = "";
                                string Area3 = "";
                                if (Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item[1].Kind.Condition") != null)
                                {//追加/一部解除
                                    Kind1 = (string)Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item[0].Kind.Condition");
                                    if (Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item[0].Areas.Area.Name") == null)//地域が複数の場合
                                        for (int j = 0; j < Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item[0].Areas.Area").Count(); j++)
                                            Area1 += " " + Extra_Detail.SelectToken($"jmx:Report.Head.Headline.Information.Item[0].Areas.Area[{j}].Name");
                                    else
                                        Area1 = (string)Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item[0].Areas.Area.Name");
                                    Kind2 = (string)Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item[1].Kind.Condition");
                                    if (Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item[1].Areas.Area.Name") == null)//地域が複数の場合
                                        for (int j = 0; j < Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item[1].Areas.Area").Count(); j++)
                                            Area2 += " " + Extra_Detail.SelectToken($"jmx:Report.Head.Headline.Information.Item[1].Areas.Area[{j}].Name");
                                    else
                                        Area2 = (string)Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item[1].Areas.Area.Name");
                                    if (Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item[1].Kind.Condition") == null)
                                    {//発表,継続,解除があるとき
                                        Kind3 = (string)Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item[2].Kind.Condition");
                                        if (Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item[2].Areas.Area.Name") == null)//地域が複数の場合
                                            for (int j = 0; j < Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item[2].Areas.Area").Count(); j++)
                                                Area3 += " " + Extra_Detail.SelectToken($"jmx:Report.Head.Headline.Information.Item[2].Areas.Area[{j}].Name");
                                        else
                                            Area3 = (string)Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item[2].Areas.Area.Name");
                                    }
                                }
                                else
                                {//発表/全解除
                                    Kind1 = (string)Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item.Kind.Condition");
                                    if (Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item.Areas.Area.Name") == null)//地域が複数の場合
                                        for (int j = 0; j < Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item.Areas.Area").Count(); j++)
                                            Area1 += " " + Extra_Detail.SelectToken($"jmx:Report.Head.Headline.Information.Item.Areas.Area[{j}].Name");
                                    else
                                        Area1 = (string)Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item.Areas.Area.Name");
                                }
                                if (Kind1 == "発表")
                                    Text += " <発表>" + Area1;
                                if (Kind1 == "解除")
                                    Text += " <解除>" + Area1;
                                if (Kind2 == "解除")
                                    Text += " <解除>" + Area2;
                                if (Kind3 == "解除")
                                    Text += " <解除>" + Area3;
                                if (Kind2 == "継続")
                                    Text += " <継続>" + Area2;
                                if (Kind3 == "継続")
                                    Text += " <継続>" + Area3;
                                TelopText = Text + TelopText;
                            }
                            else
                                File.WriteAllText($"Log\\UnknownInfo\\{DateTime.Now:yyyyMMddHHmmss}.txt", $"{URL}\n\n{JsonText_ExtraMain}\n\n{JsonText_ExtraDetail}");
                            DisplayTitles.Add($"{Title}  {UpdateTime}");
                            DisplayTexts.Add(TelopText);
                        }
                    }
                }
            }
            else//初回動作
            {
                for (int i = 20; i > 0; i--)
                    AccessedURLs.Add(Extra_Main.Feed.Entry[i].Id);
            }
            if (DisplayTitles.Count > 0 && Title.Text != DisplayTitles[0])
            {
                Title.Text = DisplayTitles[0];
                MainText.Text = DisplayTexts[0];
                DisplayTitles.RemoveRange(0, 1);
                DisplayTexts.RemoveRange(0, 1);
                SaveTitle = Title.Text;
                SaveText = MainText.Text;
            }
            else
            {
                if(DisplayTitles.Count > 0)
                {
                    DisplayTitles.RemoveRange(0, 1);
                    DisplayTexts.RemoveRange(0, 1);
                }
                Title.Text = UserTitle;
                MainText.Text = UserText;
            }
            BackColor = Color.FromArgb(0, 0, 255);
            MainText.BackColor = BackColor;
            Title.BackColor = Color.FromArgb(0, 0, 200);
            NowTime.BackColor = Color.FromArgb(0, 0, 150);
        }
        private async void LabelMove_Tick(object sender, EventArgs e)
        {
            if (MainText.Location.X > MainText.Width * -1)//流し中
            {
                MainText.Location = new Point(MainText.Location.X - 10, MainText.Location.Y);
                if (TelopHide.Location.X == 0)//流し開始
                    await ViewOpen(10);
            }
            else if (MainText.Location.X <= MainText.Width * -1)//流し終了
            {
                MainText.Location = new Point(1280, MainText.Location.Y);
                if (UserText == "null" && TelopHide.Location.X == 1280)//流し終了
                    await ViewClose(10);
            }
        }
        public List<string> AccessedURLs = new List<string>();
        public string NowTimeTemp = "";
        public string UserTitle = "2021年の有感地震回数";
        public string UserText = "1月:134回　2月:228回　3月:167回　4月:430回　5月:155回　6月:117回　7月:163回　8月:151回　9月:156回　10月:121回　11月:128回　12月:474回　合計:2424回　【震度別】1:1584回　2:605回　3:181回　4:44回　5-:4回　5+:5回　6-:0回　6+:1回　7:0回　　EEW警報:11回";
        public int RemainingDisplayNumberDefalt = -1;
        public string SaveTitle = " ";//ユーザー強制テキスト表示終了後復元用
        public string SaveText = " ";

        public List<string> DisplayTitles = new List<string>();
        public List<string> DisplayTexts = new List<string>();

        private void TextChange_Tick(object sender, EventArgs e)
        {
            TextChange.Interval = 60000;

        }
        private void Time_Tick(object sender, EventArgs e)
        {
            NowTime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }
        private void TimeCheck_Tick(object sender, EventArgs e)
        {
            if (NowTimeTemp == "")
                NowTimeTemp = DateTime.Now.ToString("ss");
            if (NowTimeTemp != DateTime.Now.ToString("ss"))
            {
                Time.Enabled = true;
                TimeCheck.Enabled = false;
            }
        }
        public async Task ViewOpen(int Delay)
        {
            MainText.Location = new Point(1280, MainText.Location.Y);
            for (int i = TelopHide.Location.X; i <= 1280; i += 40)
            {
                TelopHide.Location = new Point(i, 0);
                await Task.Delay(Delay);
            }
            return;
        }
        public async Task ViewClose(int Delay)
        {
            MainText.Location = new Point(1280, MainText.Location.Y);
            for (int i = TelopHide.Location.X; i >= 0; i -= 40)
            {
                TelopHide.Location = new Point(i, 0);
                await Task.Delay(Delay);
            }
            return;
        }
        private async void UserTextForced_Tick(object sender, EventArgs e)
        {
            UserTextForced.Interval = 1000;
            try//タイトル,メインテキスト,固定(t/f),アニメーションティック[T](Tx32ミリ秒かかる -Tで更新のみ無効、0で最初も無効),色(R),色(G),色(B)//文字色も
            {//TextChange
                LabelMove.Enabled = true;
                Xml.Enabled = true;
                if (File.Exists("UserForcedText.txt"))//アニメーションは無効に
                {
                    string[] UserForcedText = File.ReadAllText("UserForcedText.txt").Split(',');
                    if (UserForcedText.Length == 7)
                    {
                        Xml.Enabled = false;
                        int AnimeDelay = Convert.ToInt32(UserForcedText[3]);
                        if (UserForcedText[2] == "t")
                        {
                            LabelMove.Enabled = false;
                            MainText.Location = new Point(0, MainText.Location.Y);
                        }
                        Title.Text = UserForcedText[0];
                        MainText.Text = UserForcedText[1];
                        BackColor = Color.FromArgb(Convert.ToInt32(UserForcedText[4]), Convert.ToInt32(UserForcedText[5]), Convert.ToInt32(UserForcedText[6]));
                        MainText.BackColor = BackColor;
                        Title.BackColor = Color.FromArgb(Convert.ToInt32(UserForcedText[4]), Convert.ToInt32(UserForcedText[5]), Convert.ToInt32(UserForcedText[6]));
                        NowTime.BackColor = Color.FromArgb(Convert.ToInt32(UserForcedText[4]), Convert.ToInt32(UserForcedText[5]), Convert.ToInt32(UserForcedText[6]));
                    }
                    else
                    {
                        if (Title.Text != UserForcedText[0] && SaveTitle != " " && Title.Text != SaveTitle)
                        {
                            Title.Text = "";
                            MainText.Text = "";
                            await ViewClose(5);
                            await ViewOpen(5);
                            Title.Text = SaveTitle;
                            MainText.Text = SaveText;
                            BackColor = Color.FromArgb(0, 0, 255);
                            MainText.BackColor = BackColor;
                            Title.BackColor = Color.FromArgb(0, 0, 200);
                            NowTime.BackColor = Color.FromArgb(0, 0, 150);
                        }
                        if (SaveTitle == "")
                        {
                            Title.Text = "";
                            MainText.Text = "";
                            await ViewClose(10);
                        }
                    }
                }
                else
                    File.WriteAllText("UserForcedText.txt", "");
            }
            catch
            {

            }
        }
    }
}