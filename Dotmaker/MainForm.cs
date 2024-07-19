namespace Dotmaker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        // ���[�h���ꂽ�Ƃ��Ƀh�b�g�T�C�Y�̃f�t�H���g�l��ݒ�
        private void MainForm_Load(object sender, EventArgs e)
        {
            cmbDotSize.SelectedIndex = 3;
        }

        // �F���g���b�N�o�[�̒l���ύX���ꂽ�Ƃ��Ƀ��x�����X�V
        private void trackBarColorCount_Scroll(object sender, EventArgs e)
        {
            int value = trackBarColorCount.Value;
            lblColorCount.Text = $"�F��: {value}";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // �eUI�������l�Ƀ��Z�b�g
                    picConvertedImage.Image = null;
                    btnConvert.Enabled = false;
                    btnSave.Enabled = false;
                    btnCopy.Enabled = false;
                    txtFileName.Text = openFileDialog.FileName;

                    // �Q�Ƃ��ꂽ�摜��\��
                    Bitmap originalImage = new Bitmap(openFileDialog.FileName);
                    picOriginalImage.Image = originalImage;
                    btnConvert.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("�I�����ꂽ�t�@�C���̓T�|�[�g����Ă��Ȃ��摜�`���ł��B", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // �eUI�������l�Ƀ��Z�b�g
                    picConvertedImage.Image = null;
                    btnConvert.Enabled = false;
                    btnSave.Enabled = false;
                    btnCopy.Enabled = false;
                    txtFileName.Text = string.Empty;
                }
            }
        }

        // �摜���h�b�g�G�ɕϊ�����
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

            // �ϊ���̉摜�����݂���ꍇ�A�ۑ��ƃR�s�[��L���ɂ���
            if (picConvertedImage.Image != null)
            {
                btnSave.Enabled = true;
                btnCopy.Enabled = true;
            }
        }

        // �ϊ���̉摜���N���b�v�{�[�h�ɃR�s�[����
        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (picConvertedImage.Image != null)
            {
                Clipboard.SetImage((Bitmap)picConvertedImage.Image);
            }
            else
            {
                MessageBox.Show("�ϊ���̉摜�����݂��Ă��܂���B");
            }
        }

        // �ϊ���̉摜��ۑ�����
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (picConvertedImage.Image != null)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PNG Image|*.png|BMP Image|*.bmp|GIF Image|*.gif";
                    saveFileDialog.Title = "�摜��ۑ�";
                    saveFileDialog.FileName = "image";
                    saveFileDialog.OverwritePrompt = false; // �㏑���m�F�_�C�A���O�𖳌��ɂ���

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (System.IO.File.Exists(saveFileDialog.FileName))
                        {
                            MessageBox.Show("�t�@�C�������ɑ��݂��܂��B�ʂ̃t�@�C�������w�肵�Ă��������B", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            // �I�����ꂽ�t�@�C���̊g���q�Ɋ�Â��ĉ摜�`��������
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
                                    MessageBox.Show("�T�|�[�g����Ă��Ȃ��`���ł��B");
                                    break;
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("�ϊ���̉摜�����݂��Ă��܂���B");
            }
        }

        /// <summary>
        /// �摜���h�b�g�G�ɕϊ����郁�\�b�h
        /// </summary>
        /// <param name="originalImage">�Q�Ɖ摜</param>
        /// <param name="pixelSize">�h�b�g�T�C�Y</param>
        /// <param name="colorCount">�F��</param>
        /// <returns>�ϊ���̃h�b�g�G</returns>
        private Bitmap ConvertToPixelArt(Bitmap originalImage, int pixelSize, int colorCount)
        {
            Bitmap pixelArtImage = new Bitmap(originalImage.Width, originalImage.Height);
            List<Color> colors = ExtractColors(originalImage, colorCount);

            // �h�b�g������
            using (Graphics g = Graphics.FromImage(pixelArtImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                // �摜���h�b�g�T�C�Y�ɕ������ĕ`�悷��
                for (int x = 0; x < originalImage.Width; x += pixelSize)
                {
                    for (int y = 0; y < originalImage.Height; y += pixelSize)
                    {
                        Color pixelColor = originalImage.GetPixel(x, y);

                        // �����F�𔒂ɒu��������
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
        /// �摜����w�肳�ꂽ���̐F�𒊏o���郁�\�b�h
        /// </summary>
        /// <param name="image">�Q�Ɖ摜</param>
        /// <param name="colorCount">�F��</param>
        /// <returns>�Q�Ƃ��ꂽ�摜���璊�o�����F�̔z��</returns>
        private List<Color> ExtractColors(Bitmap image, int colorCount)
        {
            Dictionary<Color, int> colorFrequency = new Dictionary<Color, int>();

            // �摜����F�𒊏o���ĕp�x���v�Z���ă\�[�g����
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);

                    // �����F�𔒂ɒu��������
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

            for (int i = 0; i < colorCount - 1; i++) // �����܂߂Ďc���ǉ����邽�߁A-1 ����(2�F�̏ꍇ�͍�+��)
            {
                if (i < sortedColors.Count && !colors.Contains(sortedColors[i])) // �d���h�~
                {
                    colors.Add(sortedColors[i]);
                }
            }

            return colors;
        }

        /// <summary>
        /// �w�肳�ꂽ�F�ɍł��߂��F��T�����\�b�h
        /// </summary>
        /// <param name="targetColor">�ΏېF</param>
        /// <param name="colors">�F�̔z��</param>
        /// <returns>�ł��߂��F</returns>
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
        /// 2�̐F�̋������v�Z���郁�\�b�h
        /// </summary>
        /// <param name="color1">1�F��</param>
        /// <param name="color2">2�F��</param>
        /// <returns>2�̐F�̋���</returns>
        private int ColorDistance(Color color1, Color color2)
        {
            int rDiff = color1.R - color2.R;
            int gDiff = color1.G - color2.G;
            int bDiff = color1.B - color2.B;

            return rDiff * rDiff + gDiff * gDiff + bDiff * bDiff;
        }
    }
}
