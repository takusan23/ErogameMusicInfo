using ErogameMusicInfo.Internet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Threading.Tasks;

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
            Assert.IsNotNull(gameData, "曲名が違うかクエリ間違えてますね");
            Debug.WriteLine(gameData.ToString());
            Assert.AreEqual(gameData.MusicTitle, "冬に咲く華", "曲名が違う");
            Assert.AreEqual(gameData.GameTitle, "彼女のセイイキ", "ゲーム名が違う");
        }
    }
}