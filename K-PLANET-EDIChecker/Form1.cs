using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace K_PLANET_EDIChecker
{
    public partial class Form1 : Form
    {
        // Oracle データベース
        private OracleConnection cnn = null;
        private bool IsOracle = false;          // EM接続が可能か
        private DataTable dtOrder = new DataTable();

        // < 識別コード, 識別内容 > ToolTipTextで使用
        Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "43", "生産参考" },
                { "44", "発注予定（内示）" },
                { "45", "納入指示" },
                { "46", "発注予定（補充）" },
                { "47", "補充注文" },
                { "05", "量産試作" },
                { "35", "日別内示" },
                { "64", "注文訂正" },
                { "65", "検収日報" },
                { "13", "受入明細" },
                { "03", "納入指示書（海外）" },
                { "25", "クボタ発注（納入指示）書（海外向け）" },
                { "27", "クボタ注文書（海外向け）" },
                { "54", "クボタ発注予定内示書（海外向け）" }
            };

        // Form1コンストラクタ
        public Form1()
        {
            InitializeComponent();

            listView1.Columns.Add("ファイル名", 300);
            listView1.Columns.Add("ｻｲｽﾞ".PadLeft(8), 100);
            listView1.Columns.Add("更新日時", 170);

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;

            // データベースが接続可能か確認
            if (DBManager.IsConnectOraSchema(ref cnn)) IsOracle = true;
            // 一旦コネクションは閉じる
            DBManager.CloseOraSchema(ref cnn);
        }
        // Form1開始イベント
        private void Form1_Load(object sender, EventArgs e)
        {
            // 設定ファイルから値を取得
            try
            {
                // DEFAULT_PATH を読み込み、存在しない場合はデスクトップに設定し直す
                pathTextBox.Text = ConfigurationManager.AppSettings["DEFAULT_PATH"];
                if (!Directory.Exists(pathTextBox.Text))
                {
                    pathTextBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                }
                checkBox1.Checked = (ConfigurationManager.AppSettings["識別"] == "true") ? true : false;
                checkBox2.Checked = (ConfigurationManager.AppSettings["種別"] == "true") ? true : false;
                checkBox3.Checked = (ConfigurationManager.AppSettings["接続"] == "true") ? true : false;
                if (!IsOracle) checkBox3.Checked = false;
                this.Text += (IsOracle) ? " (EM接続モード)" : " (未接続モード)";
                toolStripStatusLabel1.Text = string.Empty;

                // Form1を半透明にして常に手前に表示（別プロジェクトの検証用に記載）
                //this.Opacity = 0.9; // 透明度 (0.0 ～ 1.0)
                //this.TopMost = true; // 常に最前面に表示

            }
            catch
            {
                toolStripStatusLabel1.Text = "設定ファイルの読み込みに失敗しました．";
            }

            // サブフォルダ一覧を取得し表示
            SetListBox();
        }
        // Form1終了イベント
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 設定値を保存
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["DEFAULT_PATH"].Value = pathTextBox.Text;
            config.AppSettings.Settings["識別"].Value = checkBox1.Checked ? "true" : "false";
            config.AppSettings.Settings["種別"].Value = checkBox2.Checked ? "true" : "false";
            config.AppSettings.Settings["接続"].Value = checkBox3.Checked ? "true" : "false";
            config.Save();
        }
        // Form1ショートカットキーイベント
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btnSelectFolder_Click(sender, e);                 // 参照ボタン
            if (e.KeyCode == Keys.Escape) Close();                                      // 閉じる
            if (e.Control && e.KeyCode == Keys.A)                                       // 全て選択
                foreach (ListViewItem item in listView1.Items) item.Selected = true;
        }
        // Form1リサイズイベント
        private void Form1_Resize(object sender, EventArgs e)
        {
            listView1.Width = this.Width - 205;
            dataGridView1.Width = this.Width - 35;
            dataGridView1.Height = this.Height - 284;
        }



        // [参照ボタン] クリックイベント
        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            // フォルダ選択ダイアログボックス
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                // ダイアログの説明を設定
                folderDialog.Description = "フォルダを選択してください";

                // 初期フォルダーを設定
                folderDialog.SelectedPath = pathTextBox.Text;

                // ダイアログを表示
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    // 選択されたフォルダパスを取得
                    pathTextBox.Text = folderDialog.SelectedPath;
                    SetListBox();
                }
            }
        }
        // フォルダー入力ボックスEnterイベント
        private void pathTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SetListBox();
        }
        // [識別を全て表示] チェックイベント
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            listView1_SelectedIndexChanged(sender, e);
        }
        // [全てのファイルを表示] チェックイベント
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            string path = pathTextBox.Text;
            path += (listBoxFolders.Items.Count != 0) ? @"\" + listBoxFolders.Text : "";
            SetListView(path);
        }





        // listBoxFoldersフォルダー選択イベント
        private void listBoxFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path = pathTextBox.Text + @"\" + listBoxFolders.Text;
            SetListView(path);
        }
        // listBoxFolders サブフォルダ選択
        private void listBoxFolders_DoubleClick(object sender, EventArgs e)
        {
            pathTextBox.Text += @"\" + listBoxFolders.Text;
            SetListBox();
        }
        // listBoxFolders 上のフォルダに移動
        private void button1_Click(object sender, EventArgs e)
        {
            string filePath = pathTextBox.Text;
            filePath += (listBoxFolders.Items.Count != 0) ? @"\" + listBoxFolders.Text : "";
            string trimPath = Path.GetFileName(filePath.TrimEnd('\\'));
            pathTextBox.Text = filePath.Replace(trimPath, string.Empty).TrimEnd('\\');
            SetListBox();
        }
        // [ctrl + a] を効かせるために、キー入力に応じてリスト内の項目を検索する機能を無効化
        private void listBoxFolders_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // キー入力を無効化
        }
        // １．フォルダーリストボックスの作成
        private void SetListBox()
        {
            // 初期化
            dataGridView1.Rows.Clear();
            listView1.Items.Clear();

            string path = pathTextBox.Text;
            try
            {
                // 指定したパスのフォルダ一覧を取得
                string[] directories = Directory.GetDirectories(path);

                // ListBoxにフォルダ一覧を表示
                listBoxFolders.Items.Clear();
                foreach (string directory in directories)
                {
                    listBoxFolders.Items.Add(directory.Replace(pathTextBox.Text + @"\", ""));
                }
                // 当日日付フォルダ[yyyyMMdd_K]が存在していたらファイル一覧を初期表示させる
                for (int i = 0; i < listBoxFolders.Items.Count; i++)
                {
                    if (listBoxFolders.Items[i].ToString() == DateTime.Now.ToString("yyyyMMdd") + "_K")
                    {
                        listBoxFolders.SelectedIndex = i;
                    }
                }
                // サブフォルダーが存在しない場合は参照フォルダ直下のファイル一覧を初期表示させる
                if (listBoxFolders.Items.Count == 0) SetListView(path);
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = ex.Message;
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // ２．ファイル一覧リストビューの作成
        private void SetListView(string path)
        {
            if (Directory.Exists(path))
            {
                string searchPattern = (checkBox2.Checked) ? "*.*" : "*.dat"; // 例: .dat ファイルのみ
                string[] files = Directory.GetFiles(path, searchPattern);

                if (files.Length > 50)
                {
                    if (MessageBox.Show("対象ファイルが多すぎます． \n" + 
                        $"{String.Format("{0:#,0}", files.Length)} 個のファイル\n\n" + 
                        "このまま処理を続行しますか？", "質問"
                        , MessageBoxButtons.YesNo
                        , MessageBoxIcon.Exclamation)
                        == DialogResult.No) return;
                    toolStripStatusLabel1.Text = "対象ファイル読み込み中...";
                    Application.DoEvents();
                }

                // コントロールの初期化
                dataGridView1.Rows.Clear();
                listView1.Items.Clear();
                this.Cursor = Cursors.WaitCursor;

                foreach (string file in files)
                {
                    // ファイル属性を取得
                    FileAttributes attributes = File.GetAttributes(file);

                    // 隠しファイルかどうかを確認
                    if ((attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        ListViewItem item = new ListViewItem(fileInfo.Name);
                        string size = Math.Ceiling(fileInfo.Length / 1024d).ToString();
                        item.SubItems.Add(size.PadLeft(6) + " KB");
                        item.SubItems.Add(fileInfo.LastWriteTime.ToString());
                        listView1.Items.Add(item);
                    }
                }
                this.Cursor = Cursors.Default;
                if (listView1.Items.Count > 0)
                {
                    // 更新日付の昇順でファイル一覧を表示
                    listView1.Sorting = SortOrder.Ascending;
                    listView1.ListViewItemSorter = new DateTimeSorter(listView1.Sorting);
                    listView1.Sort();
                    toolStripStatusLabel1.Text = $"{String.Format("{0:#,0}", listView1.Items.Count)} 個のファイル．";
                }
            }
            else
            {
                toolStripStatusLabel1.Text = path;
                MessageBox.Show("指定されたフォルダが存在しません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        // listView1 [ファイル名] でソート
        public class FileNameSorter : IComparer
            {
            private SortOrder sortOrder;
            public FileNameSorter(SortOrder sortOrder) { this.sortOrder = sortOrder; }

            public int Compare(object x, object y)
            {
                // ListViewItemを取得
                ListViewItem itemX = x as ListViewItem;
                ListViewItem itemY = y as ListViewItem;

                // ファイル名を比較
                string fileNameX = itemX?.Text ?? string.Empty;
                string fileNameY = itemY?.Text ?? string.Empty;

                int result = string.Compare(fileNameX, fileNameY, StringComparison.OrdinalIgnoreCase);

                return sortOrder == System.Windows.Forms.SortOrder.Descending ? -result : result;
            }
        }
        // listView1 [更新日付] でソート
        public class DateTimeSorter : IComparer
        {
            private SortOrder sortOrder;
            public DateTimeSorter(SortOrder sortOrder) { this.sortOrder = sortOrder; }

            public int Compare(object x, object y)
            {
                // ListViewItemを取得
                ListViewItem itemX = x as ListViewItem;
                ListViewItem itemY = y as ListViewItem;

                DateTime dateX = DateTime.Parse(itemX.SubItems[2].Text);
                DateTime dateY = DateTime.Parse(itemY.SubItems[2].Text);

                int result = DateTime.Compare(dateX, dateY);

                return sortOrder == System.Windows.Forms.SortOrder.Descending ? -result : result;
            }
        }
        // listView1列ヘッダークリックイベント
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // クリックされた列のインデックスを取得
            int clickedColumnIndex = e.Column;

            if (clickedColumnIndex == 0)
            {
                if (listView1.Sorting == SortOrder.Ascending)
                    listView1.Sorting = SortOrder.Descending;
                else
                    listView1.Sorting = SortOrder.Ascending;
                listView1.ListViewItemSorter = new FileNameSorter(listView1.Sorting);
            }
            else if (clickedColumnIndex == 2)
            {
                if (listView1.Sorting == SortOrder.Ascending)
                    listView1.Sorting = SortOrder.Descending;
                else
                    listView1.Sorting = SortOrder.Ascending;
                listView1.ListViewItemSorter = new DateTimeSorter(listView1.Sorting);
            }
            listView1.Sort();
        }
        // listView1列ヘッダーの背景色設定
        private void ListView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            // カラムヘッダーの背景色を設定
            e.Graphics.FillRectangle(SystemBrushes.Control, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height - 3));

            // 境界線を描画
            using (Pen pen = new Pen(Color.DarkGray, 1)) // 境界線の色と太さを指定
            {
                e.Graphics.DrawRectangle(pen, new Rectangle(e.Bounds.X - 1, e.Bounds.Y, e.Bounds.Width - 2, e.Bounds.Height - 3));
            }
            
            // カラムヘッダーのテキストを上下センタリングで描画
            using (var sf = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center })
            {
                e.Graphics.DrawString(e.Header.Text, e.Font, Brushes.Black, e.Bounds, sf);
            }
        }
        // listView1描画イベント
        private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }
        // listView1ダブルクリックイベント
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            // .dat ファイルを標準アプリケーションで開く
            string filePath = @pathTextBox.Text;
            filePath += (listBoxFolders.Items.Count != 0) ? @"\" + listBoxFolders.Text : "";
            filePath += @"\" + listView1.SelectedItems[0].Text;
            Process.Start($@"{filePath}");

        }



        private void CreateDataTable()
        {
            dtOrder.Clear();
            dtOrder.Columns.Clear();
            dtOrder.Columns.Add("識別", typeof(string));
            dtOrder.Columns.Add("工場", typeof(string));
            dtOrder.Columns.Add("注番", typeof(string));
            dtOrder.Columns.Add("得意先品番", typeof(string));
        }

        // listView1ファイル選択イベント
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count == 0) return;

            // 初期化
            dataGridView1.Rows.Clear();
            CreateDataTable();
            int count = 0;
            string filePath = string.Empty;
            string errorID = string.Empty;

            // テキストファイル内の <識別(先頭2ﾊﾞｲﾄ), DataGridViewの行番号>
            Dictionary<string, int> pair = new Dictionary<string, int>();

            try
            {
                // *************** テキストファイル処理 *********************
                // 複数ファイルが選択された場合は合算して表示
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    if (item.Text.ToLower().EndsWith(".dat"))       // .dat ファイル以外は無視
                    {
                        filePath = @pathTextBox.Text;
                        filePath += (listBoxFolders.Items.Count != 0) ? @"\" + listBoxFolders.Text : "";
                        filePath += @"\" + item.Text;
                        using (StreamReader reader = new StreamReader(filePath, Encoding.ASCII))
                        {
                            while (!reader.EndOfStream)             // ファイルの終端までループ
                            {
                                string line = reader.ReadLine();    // 1行ずつ読み込む
                                count++;
                                if (line.Length != 256 && line.Length != 500 && line.Length != 1000)
                                {
                                    errorID += "?";                 // 想定外のフォーマットがあるか検証
                                }

                                // .dat ファイルの１行分を判定してDataGridViewに追加更新
                                SetDataGridViewLine(ref line, ref pair, ref errorID);

                            }
                        }
                        if (errorID != string.Empty)
                        {
                            toolStripStatusLabel1.Text = "想定外が発生 => " + errorID;
                        }
                        else
                        {
                            toolStripStatusLabel1.Text = String.Format("{0:#,0}", count) + "件 を読み込みました．";
                        }
                    }
                    else
                    {
                        if (count == 0)
                            toolStripStatusLabel1.Text = String.Empty;
                    }
                }

                // *************** データベース処理 *********************
                // EM接続モードの場合はEM接続コネクションを開始
                if (IsOracle && checkBox3.Checked)
                {
                    // マウスポインタを待機カーソル（砂時計）に変更
                    this.Cursor = Cursors.WaitCursor;

                    // データベース接続
                    DBManager.IsConnectOraSchema(ref cnn);

                    // 検索用データ作成
                    InsertDataGridViewEM();
                }
            }
            catch (FileNotFoundException)
            {
                toolStripStatusLabel1.Text = filePath;
                MessageBox.Show("ファイルが見つかりませんでした。","エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = ex.Message;
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // マウスポインタをデフォルト（矢印）に戻す
                this.Cursor = Cursors.Default;

                // EM接続モードの場合はEM接続コネクションを切断
                if (IsOracle) DBManager.CloseOraSchema(ref cnn);
            }
        }

        // .dat ファイルの１行分を判定してDataGridViewに追加更新
        // DataGridView1 : .datファイルの件数
        // DataGridView2 : D0010確定受注ファイルの件数
        private void SetDataGridViewLine(ref string line, ref Dictionary<string, int> pair, ref string errorID)
        {
            string 識別 = line.Substring(0, 2);
            string 工場 = line.Substring(2, 2);
            string 工場KEY = "_" + 工場;        // ColumnNameに数値のみは入らないので先頭に_を付けて列名を作成
            int row = 999;
            if (pair.Where(x => x.Key == 識別).Count() == 0)
            {
                // dataGridView1 への行追加
                if (checkBox1.Checked ||
                    (checkBox1.Checked == false && 識別 == "45") ||
                    (checkBox1.Checked == false && 識別 == "46") ||
                    (checkBox1.Checked == false && 識別 == "47") ||
                    (checkBox1.Checked == false && 識別 == "27") ||
                    (checkBox1.Checked == false && 識別 == "05")
                    )
                {
                    row = dataGridView1.Rows.Count;
                    pair.Add(識別, row);

                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[row].Cells[0].Value = 識別;
                    try
                    {
                        dataGridView1.Rows[row].Cells[0].ToolTipText = dic[識別];
                    }
                    catch
                    {
                        // ToolTipText は無くても例外にせず無視
                    }
                }
            }
            else
            {
                row = pair[識別];
            }

            // 行対象ありの場合
            if (row != 999)
            {
                // 工場IDを検索
                if (dataGridView1.Columns.Contains(dataGridView1.Columns[工場KEY]))
                {

                    // EM接続モードの場合はデータテーブルに追記
                    if (IsOracle && checkBox3.Checked && IsEMTarget(ref line)) {
                        string 取引先 = line.Substring(4, 5);
                        string 注番 = line.Substring(9, 8);
                        string 得意先品番 = line.Substring(18, 10);
                        DataRow dr = dtOrder.NewRow();
                        dr["識別"] = 識別;
                        dr["工場"] = 工場;
                        dr["注番"] = 注番;
                        dr["得意先品番"] = 得意先品番;
                        dtOrder.Rows.Add(dr);
                    }

                    int col = dataGridView1.Columns[工場KEY].Index;
                    if (dataGridView1.Rows[row].Cells[col].Value == null)
                    {
                        // カウント初期値
                        dataGridView1.Rows[row].Cells[col].Value = "1".PadLeft(4);
                    }
                    else
                    {
                        // 件数カウントアップ
                        int val;
                        if (Int32.TryParse(dataGridView1.Rows[row].Cells[col].Value.ToString(), out val))
                        {
                            val++;
                            dataGridView1.Rows[row].Cells[col].Value = val.ToString().PadLeft(4);
                        }
                    }
                }
                else
                {
                    // 想定外の工場IDがあるか検証
                    errorID += (errorID == string.Empty) ? 工場 : ";" + 工場;
                }
            }
        }

        // EM確定受注ファイルを検索し件数を取得
        private void InsertDataGridViewEM()
        {
            for (int row = 0; row < dataGridView1.Rows.Count; row++)
            {
                string 識別 = dataGridView1[0, row].Value.ToString();
                if (IsEMTarget(ref 識別))
                {
                    dataGridView1.Rows.Insert(row + 1);
                    dataGridView1[0, row + 1].Value = "EM";
                    foreach (DataGridViewCell c in dataGridView1.Rows[row].Cells)
                    {
                        if (c.ColumnIndex == 0) continue;
                        if (c.Value != null)
                        {
                            string 工場 = dataGridView1.Columns[c.ColumnIndex].HeaderText.ToString().Substring(0, 2);
                            // SQL条件を作成
                            string[] odrnos = dtOrder.AsEnumerable()
                                .Where(x => x["識別"].ToString() == 識別 && x["工場"].ToString() == 工場)
                                .Select(x => "'" + x["注番"].ToString() + "'")
                                .ToArray();
                            string 注番s = "(" + string.Join(",", odrnos) + ")";
                            // SQL条件を作成
                            string[] tkhmcds = dtOrder.AsEnumerable()
                                .Where(x => x["識別"].ToString() == 識別 && x["工場"].ToString() == 工場)
                                .Select(x => "'" + x["得意先品番"].ToString() + "'")
                                .ToArray();
                            string 得意先品番s = "(" + string.Join(",", tkhmcds) + ")";
                            // EM確定受注ファイルを検索し件数を取得
                            //int countOld = DBAccessor.IsD0010(ref cnn, 注番s, 得意先品番s); // 旧：SQLチューニング前



                            string targetPrimaryKey = GetD0010PrimaryKey();
                            int count = DBAccessor.CountD0010(ref cnn, 注番s, targetPrimaryKey, 工場);
                            // 失敗時
                            if (targetPrimaryKey == string.Empty || count < 0)
                            {
                                c.Style.BackColor = Color.LightCoral;
                                dataGridView1[c.ColumnIndex, c.RowIndex + 1].Style.BackColor = Color.LightCoral;
                            }



                            // EM件数を表示
                            string emTarget = count.ToString().PadLeft(4);
                            dataGridView1[c.ColumnIndex, c.RowIndex + 1].Value = emTarget;
                            // 色付け処理
                            if (emTarget == c.Value.ToString())
                            {
                                c.Style.BackColor = Color.LightGreen; // EMとの比較OK！
                                dataGridView1[c.ColumnIndex, c.RowIndex + 1].Style.BackColor = Color.LightGreen;
                            }
                            else
                            {
                                c.Style.BackColor = dataGridView1.DefaultCellStyle.BackColor;
                                dataGridView1[c.ColumnIndex, c.RowIndex + 1].Style.BackColor = dataGridView1.DefaultCellStyle.BackColor;
                            }
                        }
                    }
                    row++;
                }
            }
        }
        // 現在選択中の日付を返却
        private string GetD0010PrimaryKey()
        {
            string s = string.Empty;
            if (listBoxFolders.Items.Count == 0)
            {
                s = Path.GetFileName(pathTextBox.Text.TrimEnd('\\'));
            }
            else
            {
                s = listBoxFolders.Text;
            }
            if (s.Length >= 8)
            {
                DateTime d = DateTime.ParseExact(s.Substring(0, 8), "yyyyMMdd", null);
                if (d != null) return d.ToString("yyMM");
            }
            return s;
        }

        // EM確定受注ファイルの検索対象かを調べて、対象の場合は値を返却
        private bool IsEMTarget(ref string line)
        {
            string 識別 = line.Substring(0, 2);
            if (識別 == "45" || 識別 == "46" || 識別 == "47" || 識別 == "27" || 識別 == "05")
            {
                //取引先 = line.Substring(4, 5);
                //注番 = line.Substring(9, 8);
                //得意先品番 = line.Substring(18, 10);
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}
