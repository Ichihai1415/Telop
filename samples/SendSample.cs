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
                        /*このソフトへの送信　 ""は必要ありません。
                        識別ID[*1],タイトル[*2],本文[*2],タイトル時計部分背景色R値[*3],G値[*3],B値[*3],文字色[*3],本文部分背景色R値[*4],G値[*4],B値[*4],文字色[*4],固定[*5],表示時間[*6],表示X座標[*7])
                        [*1]
                        0:すぐ表示(表示されているものに上書き)切り替えアニメーションなし([*7]まで)
                        1:通常テキスト追加(通常取得データと同様に) ([*2]まで)

                        -99:内部使用(接続確認)
                        [*2]
                        ","を入れたい場合"!comma"に置き換えてください。(例: 素数は2,3,5,7,… → 素数は2!comma3!comma5!comma7!comma… )
                        [*3]
                        タイトル・時計部分の色。背景はRGB値、文字色は"Black"か"White"
                        [*4]
                        本文部分の色。背景はRGB値、文字色は"Black"か"White"
                        [*5]
                        本文を固定するか "True"か"False"
                        [*6]
                        x秒後に通常取得テキストの表示を再開します。
                        [*7]
                        タイトル右端からの相対座標は-をつける[※-1以下](例:タイトル右端→ -1、タイトル右端から20px右→ -20)

                        例:
                        0,タイトル,テキスト・・・・・・,0,0,200,White,0,0,255,White,True,5,-1

                        */
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