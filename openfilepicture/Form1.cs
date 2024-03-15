using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace openfilepicture
{
    public partial class Form1 : Form
    {
        private string[] imageFiles;
        private int currentIndex = 0;

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn Thư Mục Chứa Hình Ảnh";
            openFileDialog.Filter = "Tệp Hình Ảnh|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog.Multiselect = true;

            // Hiển thị hộp thoại và lấy đường dẫn thư mục đã chọn
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folderPath = Path.GetDirectoryName(openFileDialog.FileName);
                imageFiles = Directory.GetFiles(folderPath)
                    .Where(file => file.ToLower().EndsWith(".jpg") || file.ToLower().EndsWith(".jpeg")
                                    || file.ToLower().EndsWith(".png") || file.ToLower().EndsWith(".gif")
                                    || file.ToLower().EndsWith(".bmp"))
                    .ToArray();

                if (imageFiles.Length == 0)
                {
                    MessageBox.Show("Không tìm thấy tệp hình ảnh trong thư mục đã chọn.");
                    return;
                }

                string selectedImagePath = openFileDialog.FileName;

                // Hiển thị hình ảnh đã chọn trong PictureBox chính
                pictureBox1.Image = Image.FromFile(selectedImagePath);

                // Hiển thị các hình thu nhỏ
                HienThiHinhThuNho();
                button1.Visible = false;
            }
        }


        private void HienThiHinhThuNho()
        {
            
            flowLayoutPanel1.Controls.Clear();

            // Tải các hình thu nhỏ
            foreach (string imagePath in imageFiles)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = Image.FromFile(imagePath);
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Width = 100;
                pictureBox.Height = 100;
                pictureBox.Margin = new Padding(5);

                // Thêm sự kiện Click để chọn hình ảnh
                pictureBox.Click += (sender, e) =>
                {
                    PictureBox selectedPictureBox = sender as PictureBox;
                    int index = flowLayoutPanel1.Controls.IndexOf(selectedPictureBox);
                    HienThiHinhAnh(index);
                };


                pictureBox1.TabStop = true;
                flowLayoutPanel1.Controls.Add(pictureBox);
            }
        }

        private void HienThiHinhAnh(int index)
        {
            if (index >= 0 && index < imageFiles.Length)
            {
                // Hiển thị hình ảnh tương ứng trong PictureBox chính
                pictureBox1.Image = Image.FromFile(imageFiles[index]);
                currentIndex = index;
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
               
        private void pictureBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                // Tăng chỉ số hiện tại lên một
                currentIndex++;

                // nếu vượt qua số lượng hình ảnh 
                if (currentIndex >= imageFiles.Length)
                {
                    // Quay lại chỉ số đầu tiên
                    currentIndex = 0;
                }

                // Hiển thị hình ảnh tương ứng với chỉ số mới trong PictureBox
                HienThiHinhAnh(currentIndex);
            }
            else if (e.KeyCode == Keys.Left)
            {
                // Giảm chỉ số hiện tại đi một
                currentIndex--;

                // Kiểm tra nếu chỉ số mới nhỏ hơn 0
                if (currentIndex < 0)
                {
                    // Đặt chỉ số là chỉ số cuối cùng trong danh sách
                    currentIndex = imageFiles.Length - 1;
                }

                // Hiển thị hình ảnh tương ứng với chỉ số mới trong PictureBox
                HienThiHinhAnh(currentIndex);
            }
        }


    }
}