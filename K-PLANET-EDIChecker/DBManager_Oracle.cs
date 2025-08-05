using DecryptPassword;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;

namespace K_PLANET_EDIChecker
{
    /// <summary>
	/// データベース管理クラス (共通メソッド, システム テーブル)
    /// </summary>
    public static class DBManager
    {
        /// <summary>
        /// Oracle データベース スキーマ接続成否
        /// </summary>
        /// <param name="dbConf">データベース設定</param>
        /// <param name="oraCnn">EM データベースへの接続クラス</param>
        /// <returns>結果 (false: 失敗, true: 成功)</returns>
        public static bool IsConnectOraSchema(ref OracleConnection oraCnn)
        {
            bool ret;

            // App.config からデータベース情報を読み込む
            string userid = ConfigurationManager.AppSettings["USERID"];
            string encpassed = ConfigurationManager.AppSettings["ENCPASSWD"];
            string host = ConfigurationManager.AppSettings["HOST"];
            string sid = ConfigurationManager.AppSettings["SID"];

            // パスワード復号化
            var dpc = new DecryptPasswordClass();
            dpc.DecryptPassword(encpassed, out string decPasswd);

            // データソース
            string ds = "(DESCRIPTION="
                        + "(ADDRESS="
                          + "(PROTOCOL=tcp)"
                          + "(HOST=" + host + ")"
                          + "(PORT=1521)"
                        + ")"
                        + "(CONNECT_DATA="
                          + "(SERVICE_NAME=" + sid + ")"
                        + ")"
                      + ")";  // Oracle Client を使用せず直接接続する

            // Oracle 接続文字列を組み立てる
            string connectString = "User Id="     + userid + "; "
                                 + "Password="    + decPasswd + "; "
                                 + "Data Source=" + ds;
            try
            {
                oraCnn = new OracleConnection(connectString);

                // Oracle へのコネクションの確立
                oraCnn.Open();
                ret = true;
            }
            catch
            {
                // 接続を閉じる
                CloseOraSchema(ref oraCnn);
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// Oracle データベース スキーマからの切断
        /// </summary>
        /// <param name="oraCnn">Oracle データベースへの接続クラス</param>
        public static void CloseOraSchema(ref OracleConnection oraCnn)
        {
            oraCnn?.Close();
        }


    }
}
