# ErogameMusicInfo
エロゲソングからエロゲ情報を取得するアプリ。  
音楽プレーヤーは**Music Center**のみ対応しています。

![Imgur](https://imgur.com/S65J3ak.png)

(コンピレーションアルバムだと何のゲームの曲なのかわからんのよ)

## ダウンロード
Releaseに置いておきました。

## 仕組み
- MusicCenterのウィンドウタイトルを取得
    - なんとウィンドウタイトルに曲名、アーティスト名、アルバム名が文字列結合された状態で入っている
    - `曲名 / アーティスト名 / アルバム名`の形式で
- ErogameScapeのSQL叩く機能を使って曲名からゲームの情報を取得する
    - 一回取得したらデータベースに保存して、二回目以降はデータベースの値を使うようにしている
    - 利用してるSQL
```sql
SELECT 
  ml.name,
  gl.id,
  gl.gamename,
  gl.sellday,
  gl.dmm_genre,
  gl.dmm_genre_2,
  gl.dmm,
  bl.brandname,
  gl.shoukai,
  gl.erogame
  FROM musiclist ml
  INNER JOIN game_music gm ON gm.music = ml.id -- 曲情報DBとゲーム音楽DBを結合
  INNER JOIN gamelist gl ON gm.game = gl.id -- ゲーム情報DBと結合。これでゲームタイトル、メーカーIDが取得できる
  INNER JOIN brandlist bl ON bl.id = gl.brandname -- ブランド名DBと結合。これでメーカー名が取れる
  WHERE ml.name LIKE '冬に咲く華' -- 曲名を入れて曲情報DBから情報を取得
```
- WPFでそれっぽく表示
    - 最近知ったんですけどWPFってWindowsネイティブのUIコンポーネント使ってるわけじゃないっぽい？
        - Windows11でもボタンが四角いままらしい
        - WinFormはWin32のUIを叩いてるっぽい
    - 曲名からゲーム情報取得などはViewModelにやらせてるため、xamlでViewと紐付けてます。これがデータバインディングですか

## テストコード
`ErogameMusicInfo.Test`におまけ程度のテストコードがあります。  
ErogameScapeから値が返ってくるかと、二回目以降データを保持しておくSQLiteのクエリ動作確認用のテストコードがあります。