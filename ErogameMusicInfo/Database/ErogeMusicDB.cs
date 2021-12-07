using ErogameMusicInfo.Data;
using System;
using System.Data.SQLite;

/// <summary>
/// .NET有識者おる？データベースにSQLite使いたいんだけど、System.Data.SQLiteをNugetで入れるので正解なの？
/// </summary>
namespace ErogameMusicInfo.Database
{
    public class ErogeMusicDB
    {

        private string connectionText = "Data Source=eroge_music.db;Version=3;";

        /// <summary>
        /// コンストラクタ。データベースのファイルを指定して下さい
        /// </summary>
        /// <param name="dbPath">データベースのファイルパス</param>
        public ErogeMusicDB(string dbPath = "eroge_music.db")
        {
            connectionText = $"Data Source={dbPath};Version=3;";
        }

        /// <summary>
        /// テーブルを作成する。ない場合は作成しますが、ある場合はIF NOT EXISTSでスキップするはず
        /// </summary>
        public void CreateDB()
        {
            using (var connection = new SQLiteConnection(connectionText))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
CREATE TABLE IF NOT EXISTS eroge_music(
id INTEGER NOT NULL PRIMARY KEY,
erogame_scape_id TEXT NOT NULL,
music_title TEXT NOT NULL,
game_title TEXT NOT NULL,
sell_day TEXT NOT NULL,
thumbnail TEXT NOT NULL,
brand_name TEXT NOT NULL,
home_page TEXT NOT NULL,
is_erogame TEXT NOT NULL
);
";
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// データベースへデータを入れる
        /// </summary>
        /// <param name="data"> ErogeMusicData </param>
        public void InsertData(ErogeMusicData data)
        {
            using (var connection = new SQLiteConnection(connectionText))
            {
                connection.Open();
                // トランザクションを有効にしたほうが速いらしい
                using (var transaction = connection.BeginTransaction())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $@"
INSERT INTO eroge_music (erogame_scape_id, music_title, game_title, sell_day, thumbnail, brand_name, home_page, is_erogame) VALUES(
'{data.Id}', '{data.MusicName}', '{data.GameName}', '{data.Sellday}', '{data.Thumbnail}', '{data.BrandName}', '{data.Homepage}', '{data.IsErogame}'
);
";
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// エロゲソング名を利用してデータベースからデータを取得
        /// </summary>
        /// <param name="musicTitle">エロゲソング名</param>
        /// <returns> ErogeMusicData </returns>
        public ErogeMusicData GetDataFromMusicTitle(string musicTitle)
        {
            ErogeMusicData? returnData = null;
            using (var connection = new SQLiteConnection(connectionText))
            {
                connection.Open();
                // トランザクションを有効にしたほうが速いらしい
                using (var transaction = connection.BeginTransaction())
                {
                    using (var command = connection.CreateCommand())
                    {
                        // SELECT 実行
                        command.CommandText = $@"
SELECT * FROM eroge_music WHERE music_title = '{musicTitle}'
";
                        // データを読む
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            // 多分一個しか無いので
                            reader.Read();
                            returnData = new ErogeMusicData(
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetString(5),
                                reader.GetString(6),
                                reader.GetString(7),
                                reader.GetString(8) == "true"
                            );
                        }
                    }
                    transaction.Commit();
                }
            }
            return returnData;
        }

        /// <summary>
        /// エロゲソング名を渡してデータが存在するか確認する
        /// </summary>
        /// <param name="musicTitle">エロゲソング名</param>
        /// <returns>trueなら存在する</returns>
        public bool IsExistsDataFromMusicTitle(string musicTitle)
        {
            bool isExists = false;
            using (var connection = new SQLiteConnection(connectionText))
            {
                connection.Open();
                // トランザクションを有効にしたほうが速いらしい
                using (var transaction = connection.BeginTransaction())
                {
                    using (var command = connection.CreateCommand())
                    {
                        // https://stackoverflow.com/questions/15684039/checking-if-record-exists-in-sqlite-c-sharp
                        command.CommandText = $@"
SELECT count(*) FROM eroge_music WHERE music_title = '{musicTitle}'
";
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        isExists = count != 0;
                    }
                }
            }
            return isExists;
        }


    }
}
