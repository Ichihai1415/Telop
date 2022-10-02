using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Telop
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            Task ServerOpen = Task.Run(() => { SocketServer(); });
        }
        /// <summary>
        /// 気象庁xmlからデータを取得します。
        /// </summary>
        private void Xml_Tick(object sender, EventArgs e)
        {
            try
            {
                Xml.Interval = 60000;
                //地震火山
                XmlDocument XmlDocument_EqVolMain = new XmlDocument();
                XmlDocument_EqVolMain.Load("https://www.data.jma.go.jp/developer/xml/feed/eqvol.xml");
                string JsonText_EqVolMain = JsonConvert.SerializeXmlNode(XmlDocument_EqVolMain);
                JMAxml_EqVol_Main.JMAXML EqVol_Main = JsonConvert.DeserializeObject<JMAxml_EqVol_Main.JMAXML>(JsonText_EqVolMain);
                if (AccessedURLs1.Count != 0)//初回以外動作　通常!=
                {
                    for (int i = 10; i > 0; i--)
                    {
                        if (AccessedURLs1.Contains(EqVol_Main.Feed.Entry[i].Id) == false)
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
                                AccessedURLs1.Add(URL);
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
                        AccessedURLs1.Add(EqVol_Main.Feed.Entry[i].Id);
                }
                //随時
                XmlDocument XmlDocument_ExtraMain = new XmlDocument();
                XmlDocument_ExtraMain.Load("https://www.data.jma.go.jp/developer/xml/feed/extra.xml");
                string JsonText_ExtraMain = JsonConvert.SerializeXmlNode(XmlDocument_ExtraMain);
                JMAxml_Extra_Main.JMAXML Extra_Main = JsonConvert.DeserializeObject<JMAxml_Extra_Main.JMAXML>(JsonText_ExtraMain);
                if (AccessedURLs2.Count != 0)//初回以外動作　通常!=
                {
                    for (int i = 20; i >= 0; i--)
                    {
                        Console.WriteLine(i);
                        if (AccessedURLs2.Contains(Extra_Main.Feed.Entry[i].Id) == false)
                        {
                            string Title = Extra_Main.Feed.Entry[i].Title;
                            if (!Title.Contains("警報・注意報") && Title != "熱中症警戒アラート" && Title != "早期天候情報" && !Title.Contains("台風解析・予報情報") && Title != "竜巻注意情報（目撃情報付き）")
                            {
                                if (Title.Contains("台風") && Title != "全般台風情報（定型）")
                                    continue;
                                string URL = Extra_Main.Feed.Entry[i].Id;
                                DateTime UpdateTime = Extra_Main.Feed.Entry[i].Updated + TimeSpan.FromHours(9);
                                string Content = Extra_Main.Feed.Entry[i].Content.Text.Replace("回", "回　").Replace("\n", "　").Replace("　　", "");
                                string TelopText = Content;
                                XmlDocument XmlDocument_ExtraDetail = new XmlDocument();
                                XmlDocument_ExtraDetail.Load(URL);
                                AccessedURLs2.Add(URL);
                                string JsonText_ExtraDetail = JsonConvert.SerializeXmlNode(XmlDocument_ExtraDetail);
                                JObject Extra_Detail = JObject.Parse(JsonText_ExtraDetail);
                                if (Title.Contains("気象情報") || Title == "竜巻注意情報" || Title == "全般台風情報（定型）)" || Title == "スモッグ気象情報" || Title == "記録的短時間大雨情報")
                                    Console.WriteLine("(そのまま)");
                                else if (Title == "指定河川洪水予報")
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
                                                Area1 += "  " + Extra_Detail.SelectToken($"jmx:Report.Head.Headline.Information.Item[0].Areas.Area[{j}].Name") + "　";
                                        else
                                            Area1 = (string)Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item[0].Areas.Area.Name") + "　";
                                        Kind2 = (string)Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item[1].Kind.Condition");
                                        if (Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item[1].Areas.Area.Name") == null)//地域が複数の場合
                                            for (int j = 0; j < Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item[1].Areas.Area").Count(); j++)
                                                Area2 += "  " + Extra_Detail.SelectToken($"jmx:Report.Head.Headline.Information.Item[1].Areas.Area[{j}].Name") + "　";
                                        else
                                            Area2 = (string)Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item[1].Areas.Area.Name") + "　";
                                        if (Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item[1].Kind.Condition") == null)
                                        {//発表,継続,解除があるとき
                                            Kind3 = (string)Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item[2].Kind.Condition");
                                            if (Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item[2].Areas.Area.Name") == null)//地域が複数の場合
                                                for (int j = 0; j < Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item[2].Areas.Area").Count(); j++)
                                                    Area3 += "  " + Extra_Detail.SelectToken($"jmx:Report.Head.Headline.Information.Item[2].Areas.Area[{j}].Name") + "　";
                                            else
                                                Area3 = (string)Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item[2].Areas.Area.Name") + "　";
                                        }
                                    }
                                    else
                                    {//発表/全解除
                                        Kind1 = (string)Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item.Kind.Condition");
                                        if (Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item.Areas.Area.Name") == null)//地域が複数の場合
                                            for (int j = 0; j < Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item.Areas.Area").Count(); j++)
                                                Area1 += Extra_Detail.SelectToken($"jmx:Report.Head.Headline.Information.Item.Areas.Area[{j}].Name") + "　";
                                        else
                                            Area1 = (string)Extra_Detail.SelectToken("jmx:Report.Head.Headline.Information.Item.Areas.Area.Name") + "　";
                                    }
                                    if (Kind1 == "発表")//発表/追加　//順番は発表、解除、継続になるように
                                        Text += " <発表>  " + Area1;
                                    if (Kind1 == "解除")//全解除
                                        Text += " <解除>  " + Area1;
                                    if (Kind2 == "解除")//一部解除/発表と解除(継続なし)
                                        Text += " <解除>  " + Area2;
                                    if (Kind3 == "解除")//追加と解除(継続あり)
                                        Text += " <解除>  " + Area3;
                                    if (Kind1 == "継続")//一部解除
                                        Text += " <継続>  " + Area1;
                                    if (Kind2 == "継続")//追加/追加と解除(継続あり)
                                        Text += " <継続>  " + Area2;
                                    TelopText = Text + TelopText;
                                }
                                else
                                    File.WriteAllText($"Log\\UnknownInfo\\{DateTime.Now:yyyyMMddHHmmss}.txt", $"{Title}\n\n{JsonText_ExtraMain}\n\n{JsonText_ExtraDetail}");
                                DisplayTitles.Add($"{Title}");//一行時UpdateTimeを本文に
                                DisplayTexts.Add($"{UpdateTime}  {TelopText}");
                                Console.WriteLine(Title);
                                Console.WriteLine(TelopText);
                            }
                            else
                                Console.WriteLine("(対象外)");
                        }
                    }
                }
                else//初回動作
                {
                    for (int i = 20; i > 0; i--)
                        AccessedURLs2.Add(Extra_Main.Feed.Entry[i].Id);
                }

            }
            catch (WebException)
            {

            }
            catch (Exception ex)
            {
                try
                {
                    if (!Directory.Exists("Log"))
                        Directory.CreateDirectory("Log");
                    if (!Directory.Exists("Log\\ErrorLog"))
                        Directory.CreateDirectory("Log\\ErrorLog");
                    string ErrorText = File.ReadAllText($"Log\\ErrorLog\\{DateTime.Now:yyyyMMdd}.txt") + "\n--------------------------------------------------\n" + ex;
                    File.WriteAllText($"Log\\ErrorLog\\{DateTime.Now:yyyyMMdd}.txt", ErrorText);
                }
                catch (Exception ex2)
                {
                    File.WriteAllText($"Log\\ErrorLog\\{DateTime.Now:yyyyMMdd}.txt", $"{ex}");
                    Console.WriteLine("TimeChangeSocketCheck " + ex2);
                }

            }
        }
        private void LabelMove_Tick(object sender, EventArgs e)
        {
            if (MainText.Location.X > MainText.Width * -1 + Title.Width)//流し中
                MainText.Location = new Point(MainText.Location.X - 10, MainText.Location.Y);
            else//流し終了
                MainText.Location = new Point(1280 - NowTime.Width, MainText.Location.Y);
        }

        public List<string> AccessedURLs1 = new List<string>();
        public List<string> AccessedURLs2 = new List<string>();
        public string NowTimeTemp = "";
        public int RemainingDisplayNumberDefalt = -1;
        public int UserTextInt = 0;
        public List<string> DisplayTitles = new List<string>();
        public List<string> DisplayTexts = new List<string>();
        /// <summary>
        /// 時計の表示・ソケット通信の接続確認をします。
        /// </summary>
        /// <remarks>両方0.5秒ごとに、接続確認は毎分00秒の時のみします。通信に失敗した場合クライアントの再設定を行います。</remarks>
        private void TimeChangeSocketCheck_Tick(object sender, EventArgs e)
        {
            try
            {
                NowTime.Text = DateTime.Now.ToString("HH:mm:ss");
                NowTime.Location = new Point(1280- NowTime.Width, 0);
                if (DateTime.Now.Second == 0)
                {
                    try
                    {
                        IPEndPoint IPEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 31401);
                        using (TcpClient TCPC = new TcpClient())
                        {
                            TCPC.Connect(IPEP);
                            using (NetworkStream NetworkStream = TCPC.GetStream())
                            {
                                byte[] Bytes = new byte[4096];
                                Bytes = Encoding.UTF8.GetBytes("-99,Socket接続確認");
                                NetworkStream.Write(Bytes, 0, Bytes.Length);
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("再接続");
                        Task.Run(() => { SocketServer(); });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("TimeChangeSocketCheck " + ex);
            }
        }
        /// <summary>
        /// メイン部分を表示させます。ViewCloseと統合予定です。
        /// </summary>
        /// <remarks>Delay:1280pxを40pxずつ動かすごとの遅延(ミリ秒)</remarks>
        /// <param name="Delay">1280pxを40pxずつ動かすごとの遅延(ミリ秒)</param>
        public async Task ViewOpen(int Delay)
        {
            MainText.Location = new Point(1280, MainText.Location.Y);
            for (int i = TelopHide.Location.X; i <= 1280; i += 40)
            {
                TelopHide.Location = new Point(i, 0);
                await Task.Delay(Delay);
            }
            MainText.Location = new Point(1280, MainText.Location.Y);
            return;
        }
        /// <summary>
        /// メイン部分を隠します。ViewOpenと統合予定です。
        /// </summary>
        /// <remarks>Delay:1280pxを40pxずつ動かすごとの遅延(ミリ秒)</remarks>
        /// <param name="Delay">1280pxを40pxずつ動かすごとの遅延(ミリ秒)</param>
        public async Task ViewClose(int Delay)
        {
            Title.Text = "";
            MainText.Text = "";
            MainText.Location = new Point(1280, MainText.Location.Y);
            for (int i = TelopHide.Location.X; i >= 0; i -= 40)
            {
                TelopHide.Location = new Point(i, 0);
                await Task.Delay(Delay);
            }
            return;
        }
        /// <summary>
        /// ソケット通信のクライアントを設定します。
        /// </summary>
        /// <remarks>データ受信時にはSocketTextReceiveメゾットが呼び出されます。</remarks>
        public void SocketServer()
        {
            IPEndPoint IPEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 31401);
            byte[] Bytes = new byte[4096];
            Regex Regex = new Regex("\0");
            try
            {
                TcpListener TCPL = new TcpListener(IPEP);
                TCPL.Start();
                while (true)
                {
                    using (TcpClient TCPC = TCPL.AcceptTcpClient())
                    {
                        using (NetworkStream NetworkStream = TCPC.GetStream())
                        {
                            if (NetworkStream.Read(Bytes, 0, Bytes.Length) != 0)
                            {
                                string Text = Regex.Replace(Encoding.UTF8.GetString(Bytes), "");
                                Task.Run(() => { SocketTextReceive(Text); });
                                Console.WriteLine(Text);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SocketServer " + ex);

            }
        }
        /// <summary>
        /// ソケット通信で受信したデータを処理します。
        /// </summary>
        /// <remarks>Text:受信したテキスト</remarks>
        /// <param name="Text">受信したテキスト</param>
        public void SocketTextReceive(string Text)
        {
            try//0識別ID(↓一覧),1タイトル,2本文,3タイトル時計R,4G,5B,6文字(Black/White),7本文R,8G,9B,10Black/White,
            { //11固定(True/False),12表示時間(秒),13表示X座標(タイトルからの相対座標は-で[※-1以下])
                //if (InvokeRequired)
                {
                    Invoke((MethodInvoker)(() => //いらないかも？
                    {
                        string[] SocketText = Text.Split(',');
                        if (SocketText.Length != 0)
                        {
                            if (Convert.ToInt32(SocketText[0]) == 0)
                            {

                                Title.Text = SocketText[1].Replace("!comma", ",");
                                MainText.Text = SocketText[2].Replace("!comma", ",");
                                Title.BackColor = Color.FromArgb(Convert.ToInt32(SocketText[3]), Convert.ToInt32(SocketText[4]), Convert.ToInt32(SocketText[5]));
                                NowTime.BackColor = Title.BackColor;
                                BackColor = Color.FromArgb(Convert.ToInt32(SocketText[7]), Convert.ToInt32(SocketText[8]), Convert.ToInt32(SocketText[9]));
                                MainText.BackColor = BackColor;
                                if (SocketText[6] == "White")
                                    Title.ForeColor = Color.White;
                                else if (SocketText[6] == "Black")
                                    Title.ForeColor = Color.Black;
                                NowTime.ForeColor = Title.ForeColor;
                                if (SocketText[10] == "White")
                                    ForeColor = Color.White;
                                else if (SocketText[10] == "Black")
                                    ForeColor = Color.Black;
                                MainText.ForeColor = ForeColor;
                                int XPoint = 1280;
                                try
                                {
                                    if (Convert.ToInt32(SocketText[13]) < 0)//タイトルからの相対座標
                                    {
                                        XPoint = Convert.ToInt32(SocketText[13]) * -1 + Title.Width;
                                    }
                                    else
                                    {
                                        XPoint = Convert.ToInt32(SocketText[13]);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("SocketTextReceive![13] " + ex);
                                }
                                Console.WriteLine("X->" + XPoint);

                                if (SocketText[11] == "True")//固定
                                {
                                    LabelMove.Enabled = false;
                                    MainText.Location = new Point(XPoint, MainText.Location.Y);
                                }
                                else
                                {
                                    LabelMove.Enabled = true;
                                    MainText.Location = new Point(1280, MainText.Location.Y);

                                }
                                TextChangeTimer.Enabled = false;
                                TextChangeTimer.Interval = Convert.ToInt32(SocketText[12]) * 1000;
                                TextChangeTimer.Enabled = true;
                            }

                            if (Convert.ToInt32(SocketText[0]) == 1)
                            {
                                DisplayTitles.Add(SocketText[1].Replace("!comma", ","));
                                DisplayTexts.Add(SocketText[2].Replace("!comma", ","));
                            }



                        }
                        else if(Convert.ToInt32(SocketText[0]) == -99)
                        {
                            if (Text == "-99,Socket接続確認")
                                Console.WriteLine("Socket接続は正常です。");
                        }
                    }));
                }/*
                else
                {
                    string[] SocketText = Text.Split(',');
                    if (SocketText.Length != 0)
                    {
                        if (Convert.ToInt32(SocketText[0]) == 0)
                        {

                            Title.Text = SocketText[1].Replace("!comma", ",");
                            MainText.Text = SocketText[2].Replace("!comma", ",");
                            Title.BackColor = Color.FromArgb(Convert.ToInt32(SocketText[3]), Convert.ToInt32(SocketText[4]), Convert.ToInt32(SocketText[5]));
                            NowTime.BackColor = Title.BackColor;
                            BackColor = Color.FromArgb(Convert.ToInt32(SocketText[7]), Convert.ToInt32(SocketText[8]), Convert.ToInt32(SocketText[9]));
                            MainText.BackColor = BackColor;
                            if (SocketText[6] == "White")
                                Title.ForeColor = Color.White;
                            else if (SocketText[6] == "Black")
                                Title.ForeColor = Color.Black;
                            NowTime.ForeColor = Title.ForeColor;
                            if (SocketText[10] == "White")
                                ForeColor = Color.White;
                            else if (SocketText[10] == "Black")
                                ForeColor = Color.Black;
                            MainText.ForeColor = ForeColor;
                            int XPoint = 1280;
                            try
                            {
                                if (Convert.ToInt32(SocketText[13]) < 0)//タイトルからの相対座標
                                {
                                    XPoint = Convert.ToInt32(SocketText[13]) * 1 + Title.Width;
                                }
                                else
                                {
                                    XPoint = Convert.ToInt32(SocketText[13]);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("SocketTextReceive![13] " + ex);
                            }
                            if (SocketText[11] == "True")//固定
                            {
                                LabelMove.Enabled = false;
                                MainText.Location = new Point(0, MainText.Location.Y);
                            }
                            else
                            {
                                LabelMove.Enabled = true;
                                MainText.Location = new Point(1280, MainText.Location.Y);

                            }
                            TextChangeTimer.Enabled = false;
                            TextChangeTimer.Interval = Convert.ToInt32(SocketText[12]) * 1000;
                            TextChangeTimer.Enabled = true;
                        }

                        if (Convert.ToInt32(SocketText[0]) == 1)
                        {
                            DisplayTitles.Add(SocketText[1].Replace("!comma", ","));
                            DisplayTexts.Add(SocketText[2].Replace("!comma", ","));
                        }



                    }
                    else
                    {
                        if (Text == "Socket接続確認")
                            Console.WriteLine("Socket接続は正常です。");
                    }
                }*/
                

            }
            catch (Exception ex)
            {
                Console.WriteLine("SocketTextReceive " + ex);
            }
        }
        /// <summary>
        /// 時間でテキストの切り替えを実行します。
        /// </summary>
        private async void TextChangeTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                TextChangeTimer.Interval = 60000;
                LabelMove.Enabled = true;
                if (DisplayTitles.Count > 0)
                {
                    await ViewClose(5);
                    await ViewOpen(5);
                    Title.Text = DisplayTitles[0];
                    MainText.Text = DisplayTexts[0];
                    DisplayTitles.RemoveRange(0, 1);
                    DisplayTexts.RemoveRange(0, 1);
                }
                else
                {
                    List<string> UserTexts = new List<string>();
                    try
                    {
                        UserTexts = File.ReadAllText("UserText.txt").Split(',').ToList();
                    }
                    catch
                    {
                        File.WriteAllText("UserText.txt", "");
                    }

                    if (UserTexts.Count % 2 == 0)//テキストあり、偶数
                    {
                        try//次のユーザーテキスト
                        {
                            if (Title.Text == "")//初回
                                await ViewOpen(10);
                            else
                            {
                                await ViewClose(8);
                                await ViewOpen(8);
                            }
                            Title.Text = UserTexts[UserTextInt * 2];
                            MainText.Text = UserTexts[UserTextInt * 2 + 1];
                            UserTextInt++;

                        }
                        catch
                        {
                            try//最初に戻る
                            {
                                UserTextInt = 0;
                                Title.Text = UserTexts[0];
                                MainText.Text = UserTexts[1];
                            }
                            catch//ユーザーテキストなし
                            {
                                await ViewClose(10);
                            }
                        }
                    }
                    else
                    {
                        await ViewClose(10);
                    }
                }
                BackColor = Color.FromArgb(0, 0, 250);
                MainText.BackColor = BackColor;
                Title.BackColor = Color.FromArgb(0, 0, 200);
                NowTime.BackColor = Title.BackColor;
                ForeColor = Color.White;
                MainText.ForeColor = Color.White;
                Title.ForeColor = Color.White;
                NowTime.ForeColor = Color.White;
            }
            catch(Exception ex)
            {
                Console.WriteLine("TextChangeTimer_Tick " + ex);
            }
        }
    }
}