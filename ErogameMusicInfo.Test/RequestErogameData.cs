using Microsoft.VisualStudio.TestTools.UnitTesting;
using ErogameMusicInfo.Internet;
using System.Threading.Tasks;
using System.Diagnostics;

/// <summary>
/// テストです
/// </summary>
namespace ErogameMusicInfo.Test
{
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
            Assert.AreEqual(gameData.GameName, "彼女のセイイキ", "ゲーム名が違う");
        }
    }
}