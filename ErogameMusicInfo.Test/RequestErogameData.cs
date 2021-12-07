using Microsoft.VisualStudio.TestTools.UnitTesting;
using ErogameMusicInfo.Internet;
using System.Threading.Tasks;
using System.Diagnostics;

/// <summary>
/// テストです
/// </summary>
namespace ErogameMusicInfo.Test
{
    /// <summary>
    /// ErogameScapeのクエリ叩く機能がちゃんと動いているか確認するテスト
    /// </summary>
    [TestClass]
    public class RequestErogameData
    {
        /// <summary>
        /// 実際にPOSTリクエストを飛ばす
        /// </summary>
        [TestMethod]
        public async Task POSTRequest()
        {
            var gameData = await ErogameScape.GetErogameData("冬に咲く華");
            Debug.WriteLine(gameData.ToString());
            Assert.AreEqual(gameData.MusicName, "冬に咲く華", "曲名が違う");
            Assert.AreEqual(gameData.GameName, "彼女のセイイキ", "ゲーム名が違う");
        }
    }
}