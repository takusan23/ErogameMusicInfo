using ErogameMusicInfo.Data;
using ErogameMusicInfo.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;

namespace ErogameMusicInfo.Test
{

    /// <summary>
    /// データベースのクエリが間違っていないかのテスト
    /// </summary>
    [TestClass]
    public class MemoryDatabase
    {

        /// <summary>
        /// データベース
        /// </summary>
        public ErogeMusicDB database = new ErogeMusicDB("test.db");

        /// <summary>
        /// データベースの作成テスト
        /// </summary>
        [TestMethod]
        public void CreateDB() => database.CreateDB();

        /// <summary>
        /// データの追加テスト
        /// </summary>
        [TestMethod]
        public void InsertData()
        {
            // データベースを作っておく
            CreateDB();
            new List<ErogeMusicData>()
            {
                new ErogeMusicData("17429", "夢と色でできている", "夢と色でできている", "2019-02-22", "https://pics.dmm.co.jp/digital/pcgame/feng_0008/feng_0008pl.jpg", "feng", "http://www.feng.jp/feng8th/", true),
                new ErogeMusicData("20228", "冬に咲く華", "彼女のセイイキ", "2014-12-19", "https://pics.dmm.co.jp/digital/pcgame/feng_0003/feng_0003pl.jpg", "feng", "http://www.feng.jp/seiiki/", true),
                new ErogeMusicData("21641", "Because of", "妹のセイイキ", "2015-08-28", "https://pics.dmm.co.jp/digital/pcgame/feng_0004/feng_0004pl.jpg", "feng", "http://www.feng.jp/seiiki/imouto/", true),
            }.ForEach(data => database.InsertData(data));
        }

        /// <summary>
        /// データベースからデータ取得テスト
        /// </summary>
        [TestMethod]
        public void GetData()
        {
            // データを追加しておく
            InsertData();
            // 取り出す
            var data = database.GetDataFromMusicTitle("冬に咲く華");
            Debug.WriteLine(data);
            Assert.AreEqual(data.GameName, "彼女のセイイキ", "ゲーム名が違う");
        }

        /// <summary>
        /// データベースにデータが入っているかテスト
        /// </summary>
        [TestMethod]
        public void ExistsData()
        {
            // データを追加しておく
            InsertData();
            // 判断
            Assert.AreEqual(database.IsExistsDataFromMusicTitle("冬に咲く華"), true);
            // 無いとき
            Assert.AreEqual(database.IsExistsDataFromMusicTitle("キスのひとつで"), false);
        }

    }
}
