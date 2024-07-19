namespace Dotmaker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        // ロードされたときにドットサイズのデフォルト値を設定
        private void MainForm_Load(object sender, EventArgs e)
        {
            cmbDotSize.SelectedIndex = 3;
        }

        // 色数トラックバーの値が変更されたときにラベルを更新
        private void trackBarColorCount_Scroll(object sender, EventArgs e)
        {
            int value = trackBarColorCount.Value;
            lblColorCount.Text = $"色数: {value}";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 各UIを初期値にリセット
                    picConvertedImage.Image = null;
                    btnConvert.Enabled = false;
                    btnSave.Enabled = false;
                    btnCopy.Enabled = false;
                    txtFileName.Text = openFileDialog.FileName;

                    // 参照された画像を表示
                    Bitmap originalImage = new Bitmap(openFileDialog.FileName);
                    picOriginalImage.Image = originalImage;
                    btnConvert.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("選択されたファイルはサポートされていない画像形式です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // 各UIを初期値にリセット
                    picConvertedImage.Image = null;
                    btnConvert.Enabled = false;
                    btnSave.Enabled = false;
                    btnCopy.Enabled = false;
                    txtFileName.Text = string.Empty;
                }
            }
        }

        // 画像をドット絵に変換する
        private void btnConvert_Click(object sender, EventArgs e)
        {
            int pixelSize = 1;

            if (cmbDotSize.SelectedItem != null)
            {
                pixelSize = Convert.ToInt32(cmbDotSize.SelectedItem.ToString());
            }
            int colorCount = trackBarColorCount.Value;
            Bitmap bitmapFromPictureBox = (Bitmap)picOriginalImage.Image;
            Bitmap pixelArtImage = ConvertToPixelArt(bitmapFromPictureBox, pixelSize, colorCount);
            picConvertedImage.Image = pixelArtImage;

            // 変換後の画像が存在する場合、保存とコピーを有効にする
            if (picConvertedImage.Image != null)
            {
                btnSave.Enabled = true;
                btnCopy.Enabled = true;
            }
        }

        // 変換後の画像をクリップボードにコピーする
        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (picConvertedImage.Image != null)
            {
                Clipboard.SetImage((Bitmap)picConvertedImage.Image);
            }
            else
            {
                MessageBox.Show("変換先の画像が存在していません。");
            }
        }

        // 変換後の画像を保存する
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (picConvertedImage.Image != null)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PNG Image|*.png|BMP Image|*.bmp|GIF Image|*.gif";
                    saveFileDialog.Title = "画像を保存";
                    saveFileDialog.FileName = "image";
                    saveFileDialog.OverwritePrompt = false; // 上書き確認ダイアログを無効にする

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (System.IO.File.Exists(saveFileDialog.FileName))
                        {
                            MessageBox.Show("ファイルが既に存在します。別のファイル名を指定してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            // 選択されたファイルの拡張子に基づいて画像形式を決定
                            string extension = System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower();
                            Bitmap bitmapToSave = (Bitmap)picConvertedImage.Image;

                            switch (extension)
                            {
                                case ".png":
                                    bitmapToSave.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                                    break;
                                case ".bmp":
                                    bitmapToSave.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                                    break;
                                case ".gif":
                                    bitmapToSave.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Gif);
                                    break;
                                default:
                                    MessageBox.Show("サポートされていない形式です。");
                                    break;
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("変換先の画像が存在していません。");
            }
        }

        /// <summary>
        /// 画像をドット絵に変換するメソッド
        /// </summary>
        /// <param name="originalImage">参照画像</param>
        /// <param name="pixelSize">ドットサイズ</param>
        /// <param name="colorCount">色数</param>
        /// <returns>変換後のドット絵</returns>
        private Bitmap ConvertToPixelArt(Bitmap originalImage, int pixelSize, int colorCount)
        {
            Bitmap pixelArtImage = new Bitmap(originalImage.Width, originalImage.Height);
            List<Color> colors = ExtractColors(originalImage, colorCount);

            // ドット化処理
            using (Graphics g = Graphics.FromImage(pixelArtImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                // 画像をドットサイズに分割して描画する
                for (int x = 0; x < originalImage.Width; x += pixelSize)
                {
                    for (int y = 0; y < originalImage.Height; y += pixelSize)
                    {
                        Color pixelColor = originalImage.GetPixel(x, y);

                        // 透明色を白に置き換える
                        if (pixelColor.A == 0)
                        {
                            pixelColor = Color.White;
                        }

                        Color nearestColor = FindNearestColor(pixelColor, colors);

                        using (Brush brush = new SolidBrush(nearestColor))
                        {
                            g.FillRectangle(brush, x, y, pixelSize, pixelSize);
                        }
                    }
                }
            }

            return pixelArtImage;
        }

        /// <summary>
        /// 画像から指定された数の色を抽出するメソッド
        /// </summary>
        /// <param name="image">参照画像</param>
        /// <param name="colorCount">色数</param>
        /// <returns>参照された画像から抽出した色の配列</returns>
        private List<Color> ExtractColors(Bitmap image, int colorCount)
        {
            Dictionary<Color, int> colorFrequency = new Dictionary<Color, int>();

            // 画像から色を抽出して頻度を計算してソートする
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);

                    // 透明色を白に置き換える
                    if (pixelColor.A == 0)
                    {
                        pixelColor = Color.White;
                    }

                    if (colorFrequency.ContainsKey(pixelColor))
                    {
                        colorFrequency[pixelColor]++;
                    }
                    else
                    {
                        colorFrequency[pixelColor] = 1;
                    }
                }
            }

            var sortedColors = colorFrequency.OrderByDescending(pair => pair.Value)
                                             .Select(pair => pair.Key)
                                             .ToList();

            List<Color> colors = new List<Color> { Color.Black };

            for (int i = 0; i < colorCount - 1; i++) // 黒を含めて残りを追加するため、-1 する(2色の場合は黒+α)
            {
                if (i < sortedColors.Count && !colors.Contains(sortedColors[i])) // 重複防止
                {
                    colors.Add(sortedColors[i]);
                }
            }

            return colors;
        }

        /// <summary>
        /// 指定された色に最も近い色を探すメソッド
        /// </summary>
        /// <param name="targetColor">対象色</param>
        /// <param name="colors">色の配列</param>
        /// <returns>最も近い色</returns>
        private Color FindNearestColor(Color targetColor, List<Color> colors)
        {
            Color nearestColor = colors[0];
            int minDistance = ColorDistance(targetColor, nearestColor);

            foreach (var color in colors)
            {
                int distance = ColorDistance(targetColor, color);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestColor = color;
                }
            }

            return nearestColor;
        }

        /// <summary>
        /// 2つの色の距離を計算するメソッド
        /// </summary>
        /// <param name="color1">1色目</param>
        /// <param name="color2">2色目</param>
        /// <returns>2つの色の距離</returns>
        private int ColorDistance(Color color1, Color color2)
        {
            int rDiff = color1.R - color2.R;
            int gDiff = color1.G - color2.G;
            int bDiff = color1.B - color2.B;

            return rDiff * rDiff + gDiff * gDiff + bDiff * bDiff;
        }
    }
}
