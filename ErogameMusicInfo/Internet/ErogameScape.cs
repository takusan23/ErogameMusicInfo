using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using ErogameMusicInfo.Data;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ErogameMusicInfo.Internet
{
    public class ErogameScape
    {

        /// <summary>
        /// 曲名からゲーム情報を取得する。
        /// ErogameScapeのSQL叩く機能を使う。
        /// </summary>
        /// <param name="musicTitle">曲名</param>
        /// <returns> ErogameData </returns>
        public static async Task<ErogeMusicData> GetErogameData(string musicTitle)
        {
            // 実行するクエリ
            var query = @$"
SELECT 
  ml.name,
  gl.id,
  gl.gamename,
  gl.sellday,
  gl.dmm,
  bl.brandname,
  gl.shoukai,
  gl.erogame
  FROM musiclist ml
  INNER JOIN game_music gm ON gm.music = ml.id -- 曲情報DBとゲーム音楽DBを結合
  INNER JOIN gamelist gl ON gm.game = gl.id -- ゲーム情報DBと結合。これでゲームタイトル、メーカーIDが取得できる
  INNER JOIN brandlist bl ON bl.id = gl.brandname -- ブランド名DBと結合。これでメーカー名が取れる
  WHERE ml.name LIKE '{musicTitle}' -- 曲名を入れて曲情報DBから情報を取得
";
            // POSTする
            var url = "https://erogamescape.dyndns.org/~ap2/ero/toukei_kaiseki/sql_for_erogamer_form.php";
            // メソッドにPOSTを指定
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    { "sql", query }
                })
            };
            var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(request);
            // スクレイピングする
            var parser = new HtmlParser();
            var document = await parser.ParseDocumentAsync(await response.Content.ReadAsStringAsync());
            // 取得
            var dmmId = GetValueFromTableElementKey(document, "dmm");
            var thumbnailUrl = $"https://pics.dmm.co.jp/digital/pcgame/{dmmId}/{dmmId}pl.jpg";
            var erogameData = new ErogeMusicData(
                GetValueFromTableElementKey(document, "id"),
                GetValueFromTableElementKey(document, "name"),
                GetValueFromTableElementKey(document, "gamename"),
                GetValueFromTableElementKey(document, "sellday"),
                thumbnailUrl,
                GetValueFromTableElementKey(document, "brandname"),
                GetValueFromTableElementKey(document, "shoukai"),
                GetValueFromTableElementKey(document, "erogame") == "t"
            );
            return erogameData;
        }

        /// <summary>
        /// tableになっているので、テーブルのキーから値を取り出すやつ
        /// </summary>
        /// <param name="document"> HtmlParser#ParseDocumentAsync </param>
        /// <param name="key">テーブルのキー</param>
        /// <returns>テーブルの値</returns>
        private static string GetValueFromTableElementKey(IHtmlDocument document, string key)
        {
            var trElementList = document.GetElementsByTagName("tr");
            // 位置を出す
            var keyPos = trElementList[0].GetElementsByTagName("th").ToList().FindIndex(element => element.TextContent == key);
            // 取り出す
            return trElementList[1].GetElementsByTagName("td")[keyPos].TextContent;
        }

    }
}
