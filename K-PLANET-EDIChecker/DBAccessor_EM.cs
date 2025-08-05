using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace K_PLANET_EDIChecker
{
    /// <summary>
	/// データベース アクセス クラス (EM テーブル)
    /// </summary>
    public static class DBAccessor
    {
        /// <summary>
        /// 確定受注ファイル件数取得 (D0010 確定受注ファイル)
        /// </summary>
        /// <param name="odrnoparam">注文番号の集合体</param>
        /// <param name="tkhmcdparam">得意先品番の集合体</param>
        /// <returns>結果 (0≦: 成功 (件数), 0≧: 失敗)</returns>
        public static int IsD0010(ref OracleConnection cnn, string odrnoparam, string tkhmcdparam)
        {
            int ret = -1;
            try
            {
                // SQL 構文を編集（インデックス検索で行いたい[IDX_D0010_1]：HMCD, TKCTLNO, JUDT）
                string judt = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd"); // Debug用に-7日してある（受注日が受信日より前とは考えずらい）
                string sql = $"select KJUNO from D0010 where SUBSTR(ODRNO,1,8) in {odrnoparam} " +
                    $"and JUDT > '{judt}' and HMCD in (" +
                    $"select HMCD from M0600 where replace(TKHMCD, '-', '') in {tkhmcdparam})";

                // 検索
                using (OracleCommand myCmd = new OracleCommand(sql, cnn))
                {
                    using (OracleDataAdapter myDa = new OracleDataAdapter(myCmd))
                    {
                        using (DataTable myDt = new DataTable())
                        {
                            // 結果取得
                            myDa.Fill(myDt);
                            return myDt.Rows.Count;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ret;
            }
        }


        /// <summary>
        /// 確定受注ファイル件数取得 (D0010 確定受注ファイル)
        /// </summary>
        /// <param name="odrnoparam">注文番号の集合体</param>
        /// <param name="tkhmcdparam">得意先品番の集合体</param>
        /// <returns>結果 (0≦: 成功 (件数), 0≧: 失敗)</returns>
        public static int CountD0010(ref OracleConnection cnn, string odrnoparam, string pkey, string 工場)
        {
            int ret = -1;
            try
            {
                // SQL 構文を編集（主キーでターゲットを絞ってから検索）
                // ODRSTS(受注状態) = 1:未納
                // ODRKBN(立案区分) = 2:EDI
                string sql = $"select a.KJUNO from D0010 a, D0020 b " +
                    $"where a.KJUNO = b.KJUNO " +
                    $"and a.KJUNO >= '{pkey}000000' and a.KJUNO <= '{pkey}999999' " +
                    $"and a.ODRKBN = '2' " +
                    $"and b.KOUJYOU = '{工場}'" + 
                    $"and SUBSTR(a.ODRNO,1,8) in {odrnoparam} ";

                // 検索
                using (OracleCommand myCmd = new OracleCommand(sql, cnn))
                {
                    using (OracleDataAdapter myDa = new OracleDataAdapter(myCmd))
                    {
                        using (DataTable myDt = new DataTable())
                        {
                            // 結果取得
                            myDa.Fill(myDt);
                            return myDt.Rows.Count;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ret;
            }
        }





    }
}
