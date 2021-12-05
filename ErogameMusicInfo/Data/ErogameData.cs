namespace ErogameMusicInfo.Data
{
    /// <summary>
    /// エロゲデータクラス
    /// </summary>
    /// <param name="Id">ErogameScapeのId</param>
    /// <param name="GameName">ゲーム名</param>
    /// <param name="Sellday">発売日</param>
    /// <param name="Thumbnail">サムネイルURL</param>
    /// <param name="BrandName">ブランド名</param>
    /// <param name="Homepage">ホームページのリンク</param>
    /// <param name="IsErogame">エロゲならtrue。全年齢ならfalse</param>
    public record ErogameData
     (
         string Id,
         string GameName,
         string Sellday,
         string Thumbnail,
         string BrandName,
         string Homepage,
         bool IsErogame
     );
}
