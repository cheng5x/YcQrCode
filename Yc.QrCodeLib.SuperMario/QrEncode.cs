using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Yc.QrCodeLib.custom;
using Yc.QrCodeLib.custom.define;

namespace Yc.QrCodeLib.SuperMario
{
    public class QrEncode : Yc.QrCodeLib.custom.CustomEncode
    {
        public QrEncode(string key)
            : base(key)
        { }


        private Image _imgSky;

        private Image _imgBrick;

        private Image _imgChanceBox;

        private Image _imgMonster;

        private Image _imgRedTortoise;

        private Image _imgGreedTortoise;

        private Image _imgStar;

        public override void SetParam()
        {
            base.SetParam();

            string _imagePath = Environment.CurrentDirectory + @"\QrCodeModel\Images\SuperMario\sky.png";
            _imgSky = Image.FromFile(_imagePath);

            string _imagePath1 = Environment.CurrentDirectory + @"\QrCodeModel\Images\SuperMario\point1.jpg";
            _imgBrick = Image.FromFile(_imagePath1);

            string _imagePath2 = Environment.CurrentDirectory + @"\QrCodeModel\Images\SuperMario\point2.jpg";
            _imgChanceBox = Image.FromFile(_imagePath2);

            string _imagePath3 = Environment.CurrentDirectory + @"\QrCodeModel\Images\SuperMario\point3.png";
            _imgMonster = Image.FromFile(_imagePath3);

            string _imagePath4 = Environment.CurrentDirectory + @"\QrCodeModel\Images\SuperMario\point4.png";
            _imgRedTortoise = Image.FromFile(_imagePath4);

            string _imagePath5 = Environment.CurrentDirectory + @"\QrCodeModel\Images\SuperMario\point5.png";
            _imgGreedTortoise = Image.FromFile(_imagePath5);

            string _imagePath6 = Environment.CurrentDirectory + @"\QrCodeModel\Images\SuperMario\point8.png";
            _imgStar = Image.FromFile(_imagePath6);
        }

        public override Bitmap Encode(string content)
        {
            try
            {
                this.matrix = QrCodeEncoder.calQrcode(EnCoding.GetBytes(content));
            }
            catch { throw new Exception("内容超出范围，请选择更高版本或者降低容错率"); }

            this.SetParam();

            //SolidBrush Backbrush = new SolidBrush(QrCodeEncoder.QRCodeBackgroundColor);
            SolidBrush Backbrush = new SolidBrush(Color.Transparent);//背景透明
            SolidBrush Forebrush = new SolidBrush(QrCodeEncoder.QRCodeForegroundColor);

            Bitmap image = new Bitmap(this.QrCodeW, this.QrCodeH);

            Graphics g = Graphics.FromImage(image);
            g.DrawImage(_imgSky, 0, 0, image.Width, image.Height);//后背景
            Rectangle rect = new Rectangle();

            g.FillRectangle(Backbrush, new Rectangle(0, 0, image.Width, image.Height));

            FillShape _FillShape = new FillShape();

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix.Length; j++)
                {
                    rect = new Rectangle((j + this.SpacingW) * QrCodeEncoder.QRCodeScale, (i + this.SpacingH) * QrCodeEncoder.QRCodeScale, QrCodeEncoder.QRCodeScale, QrCodeEncoder.QRCodeScale);

                    if (matrix[j][i])
                    {
                        rect = ChangeSuperMario(matrix, Backbrush, Forebrush, g, rect, i, j);
                    }
                    else
                    {
                        this.ChangeFillShape(g, Backbrush, rect, EN_FillShape.FillEllipse, new FillShape(), Backbrush);
                    }
                }
            }
            return image;
        }

        /// <summary>
        /// 超级马里奥样式
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="Backbrush"></param>
        /// <param name="Forebrush"></param>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private Rectangle ChangeSuperMario(bool[][] matrix, SolidBrush Backbrush, SolidBrush Forebrush, Graphics g, Rectangle rect, int i, int j)
        {
            //定义判定条件,防止边界不被判断
            int _bottomStep = i - 1 < 0 ? 0 : i - 1;
            int _topStep = i + 1 > matrix.Length - 1 ? matrix.Length - 1 : i + 1;
            int _rightStep = j + 1 > matrix.Length - 1 ? matrix.Length - 1 : j + 1;
            int _leftStep = j - 1 < 0 ? 0 : j - 1;

            if (matrix[j][i] == true
                && matrix[j][_bottomStep ] != true
                && matrix[_rightStep ][i] != true
                && matrix[_leftStep][i] != true
                && matrix[j][ _topStep ] == true)
            {
                if (matrix[_leftStep][ _topStep ] != true
                    && matrix[_rightStep ][ _topStep ] != true)
                {
                    this.ChangeFillShape(g, Forebrush, rect, EN_FillShape.DrawImage, new FillShape() { img = _imgStar }, Backbrush);
                }

                else if (matrix[_rightStep ][ _topStep ] != true)
                {
                    this.ChangeFillShape(g, Forebrush, rect, EN_FillShape.DrawImage, new FillShape() { img = _imgGreedTortoise }, Backbrush);
                }
                else if (matrix[_leftStep][ _topStep ] != true)
                {
                    this.ChangeFillShape(g, Forebrush, rect, EN_FillShape.DrawImage, new FillShape() { img = _imgRedTortoise }, Backbrush);
                }
                else
                    this.ChangeFillShape(g, Forebrush, rect, EN_FillShape.DrawImage, new FillShape() { img = _imgMonster }, Backbrush);
            }
            else if ((matrix[j][_bottomStep ] != true
                && matrix[j][ _topStep ] != true
                && matrix[_rightStep ][i] != true
                && matrix[_leftStep][i] != true
                && matrix[_leftStep][_bottomStep ] != true
                && matrix[_rightStep ][_bottomStep ] != true))
            {
                this.ChangeFillShape(g, Forebrush, rect, EN_FillShape.DrawImage, new FillShape() { img = _imgChanceBox }, Backbrush);
            }
            else
                this.ChangeFillShape(g, Forebrush, rect, EN_FillShape.DrawImage, new FillShape() { img = _imgBrick }, Backbrush);
            return rect;
        }
    }
}
