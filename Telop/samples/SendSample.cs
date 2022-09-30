//このソフトに関するC#のソースのサンプルです。初心者なのでいろいろ間違ってるかもしれません…

//Socket通信でデータを送るサンプルです。
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SendSample
{
    class TelopSampleProgram
    {
        /// <summary>
        /// IPアドレスとポート番号を指定して送信します。
        /// </summary>
        static void TelopSendSample()
        {
            IPEndPoint IPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 31401);//既定は127.0.0.1:31401
            try
            {
                using (TcpClient TcpClient = new TcpClient())
                {
                    TcpClient.Connect(IPEndPoint);
                    using (NetworkStream NetworkStream = TcpClient.GetStream())
                    {
                        byte[] Bytes = new byte[4096];
                        //クライアント側(このソフトは4096)より大きい場合文が切れます。
                        //配列のサイズを指定して初期化しないと処理がうまくできないことがあります。
                        Bytes = Encoding.UTF8.GetBytes("ここに送信したい文を入力してください。");
                        //このソフトへの送信　基本【】内は必須です。　 「,」で分けているのでタイトルや本文の中に「,」を入れたい場合「!comma」に置き換えてください。
                        //(例:"素数は2,3,5,7,…"→"素数は2!comma3!comma5!comma7!comma…")　
                        //【識別ID(↓一覧),タイトル,本文,タイトル時計部分背景色R値,G値,B値,文字色(Black/White),本文部分背景色R値,G値,B値,文字色(Black/White),固定(True/False),表示時間(秒)】,オプション1
                        //0:すぐ表示(表示されているものに上書き)切り替えアニメーションなし
                        //1:通常テキスト追加(通常取得データと同様に)【0,タイトル,本文 だけ】
                        //例:0,テストタイトル,テストテキスト・・・・・・,0,0,200,White,0,0,255,White,True,5
                        NetworkStream.Write(Bytes, 0, Bytes.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}