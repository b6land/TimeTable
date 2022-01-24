## 編譯方式

- 使用 Visual Studio 2015 或較新版本開啟，需安裝 Web 開發套件。
- 須先建立資料庫。以 Visual Studio 開啟 `SimpleTrainTable.edmx` 後，從右鍵功能表選擇「由模型產生資料庫」，需選擇資料庫連接，並按下新增連接，輸入資料庫名稱以建立資料庫檔案。
![新增資料庫視窗](doc/Add_Database.png)
- 資料庫名稱應為 `TimeTable\App_Data\SimpleTimeTable.mdf`。
- 建立完成後，需要連接剛剛建立的資料庫，並執行 `SimpleTrainTable.edmx.sql` 以建立結構。
- 按下鍵盤的 [Ctrl] + [Shift]+ [B] 開始編譯。
- 按下 F5 開啟瀏覽器偵錯與執行。

## 使用說明

- 左下角可查詢 10 筆車站資訊 (以 `string` 輸出)。
- 左下角可輸入車次，按下「設定」後，再按下「查詢車次資料」取得車次的停靠站與時間 (以 `string` 輸出)。
- 可輸入網址 `/api/tratime?start=起始站名&end=終點站名`，查詢停靠特定起終點的列車，例如 `/api/tratime?start=臺北&end=花蓮`。

## 設計概念

- 使用基礎的 **ASP.NET MVC v5** 架構，建立簡易的後端，並套用內建的 Bootstrap 產生網頁。
- 連接 **PTX 公開 API** 獲得台鐵當日的鐵路時刻表、車次資料等。若為查詢停靠特定起終點的列車，則取得當日時刻表資料後，將資訊轉為類別並寫入資料庫，減少不斷連線至公開 API 的情形。
- 使用 **Json.Net** 剖析資料，將自定義列車資訊與停靠站資訊寫入資料庫。
- 以 **Entity Framework (EF)**, *Model First* 建立資料庫，並使用 EF API 將剖析的資料寫入至資料庫。
- 於查詢時，使用 **SQL** 語法查詢特定起點、終點的列車時刻資料，以 JSON 格式輸出。

## 未來工作

- 設計網頁顯示車次、停靠特定起終點的列車資訊。
- 加入單元測試。

## 介接 API

- [公共運輸整合資訊流通服務平台 Public Transport data eXchange](https://ptx.transportdata.tw/PTX/)
- 連接程式碼採用 [ptxmotc/Sample-code](https://github.com/ptxmotc/Sample-code) 與參考 [入門指南 - motc-ptx-api-documentation](https://motc-ptx-api-documentation.gitbook.io/motc-ptx-api-documentation/) 的說明。
- [MOTC Transport API V3 (TRA)](https://ptx.transportdata.tw/MOTC?t=Rail&v=3#/)