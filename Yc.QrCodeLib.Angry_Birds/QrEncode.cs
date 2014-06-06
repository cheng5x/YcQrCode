using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using Yc.QrCodeLib.custom;
using Yc.QrCodeLib.custom.define;
using System.Drawing.Drawing2D;

namespace Yc.QrCodeLib.Angry_Birds
{
    public class QrEncode : Yc.QrCodeLib.custom.CustomEncode
    {
        public QrEncode(string key)
            : base(key)
        {

        }

        private Image _imggrass;

        private Image _imgfixedShape1;

        //private Image _imgfixedShape1_0;

        private Image _imgfixedShape2_0;

        private Image _imgAgri1;

        private Image _imgPig1;

        private Image _imgPig2;

        private Image _imgDesk;

        private Image _imgPig6;

        //private Image _imgIre;

        //private Image _imgTree1;

        //private Image _imgTree3;

        //private Image _imgTree4;

        private Image _imgagri4;

        private Image _imgagri5;

        private Image _imggrass2;

        public override void SetParam()
        {
            base.SetParam();
            #region 读取图片
            string _imagePath23 = Environment.CurrentDirectory + @"\QrCodeModel\Images\Angry_Birds\grass.png";
            _imggrass = Image.FromFile(_imagePath23);

            string _imagePath24 = Environment.CurrentDirectory + @"\QrCodeModel\Images\Angry_Birds\fixedShape1.png";
            _imgfixedShape1 = Image.FromFile(_imagePath24);

            //string _imagePath25 = Environment.CurrentDirectory + @"\QrCodeModel\Images\Angry_Birds\fixedShape1_0.png";
            //_imgfixedShape1_0 = Image.FromFile(_imagePath25);

            string _imagePath26 = Environment.CurrentDirectory + @"\QrCodeModel\Images\Angry_Birds\fixedShape2_0.png";
            _imgfixedShape2_0 = Image.FromFile(_imagePath26);

            string _imagePath1 = Environment.CurrentDirectory + @"\QrCodeModel\Images\Angry_Birds\agri1.png";
            _imgAgri1 = Image.FromFile(_imagePath1);

            string _imagePath3 = Environment.CurrentDirectory + @"\QrCodeModel\Images\Angry_Birds\pig1.png";
            _imgPig1 = Image.FromFile(_imagePath3);

            string _imagePath4 = Environment.CurrentDirectory + @"\QrCodeModel\Images\Angry_Birds\pig2.png";
            _imgPig2 = Image.FromFile(_imagePath4);

            string _imagePath8 = Environment.CurrentDirectory + @"\QrCodeModel\Images\Angry_Birds\desk.png";
            _imgDesk = Image.FromFile(_imagePath8);

            string _imagePath9 = Environment.CurrentDirectory + @"\QrCodeModel\Images\Angry_Birds\pig6.png";
            _imgPig6 = Image.FromFile(_imagePath9);

            //string _imagePath10 = Environment.CurrentDirectory + @"\QrCodeModel\Images\Angry_Birds\ire.png";
            //_imgIre = Image.FromFile(_imagePath10);

            //string _imagePath12 = Environment.CurrentDirectory + @"\QrCodeModel\Images\Angry_Birds\tree1.png";
            //_imgTree1 = Image.FromFile(_imagePath12);

            //string _imagePath14 = Environment.CurrentDirectory + @"\QrCodeModel\Images\Angry_Birds\tree3.png";
            //_imgTree3 = Image.FromFile(_imagePath14);

            //string _imagePath15 = Environment.CurrentDirectory + @"\QrCodeModel\Images\Angry_Birds\tree4.png";
            //_imgTree4 = Image.FromFile(_imagePath15);

            string _imagePath16 = Environment.CurrentDirectory + @"\QrCodeModel\Images\Angry_Birds\agri4.png";
            _imgagri4 = Image.FromFile(_imagePath16);

            string _imagePath17 = Environment.CurrentDirectory + @"\QrCodeModel\Images\Angry_Birds\agri5.png";
            _imgagri5 = Image.FromFile(_imagePath17);

            string _imagePath18 = Environment.CurrentDirectory + @"\QrCodeModel\Images\Angry_Birds\grass2.png";
            _imggrass2 = Image.FromFile(_imagePath18);

            #endregion
        }


        public override Bitmap Encode(string content)
        {
            try
            {
                this.matrix = QrCodeEncoder.calQrcode(EnCoding.GetBytes(content));
            }
            catch { throw new Exception("内容超出范围，请选择更高版本或者降低容错率"); }

            QrCodeEncoder.FrameSpacingH = 4;
            QrCodeEncoder.FrameSpacingW = 6 * QrCodeEncoder.QRCodeVersion;

            this.SetParam();

            //SolidBrush Backbrush = new SolidBrush(QrCodeEncoder.QRCodeBackgroundColor);
            SolidBrush Backbrush = new SolidBrush(Color.Transparent);//背景透明
            SolidBrush Forebrush = new SolidBrush(QrCodeEncoder.QRCodeForegroundColor);

            Bitmap image = new Bitmap(this.QrCodeW, this.QrCodeH);

            Graphics g = Graphics.FromImage(image);

            Rectangle rect = new Rectangle();

            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, image.Width, image.Height));

            int _spanCount = 0;

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix.Length; j++)
                {
                    rect = new Rectangle((j + this.SpacingW) * QrCodeEncoder.QRCodeScale, (i + this.SpacingH) * QrCodeEncoder.QRCodeScale, QrCodeEncoder.QRCodeScale, QrCodeEncoder.QRCodeScale);

                    if (matrix[j][i])
                    {
                        ChangeAngry_Birds(matrix, Backbrush, Forebrush, g, ref rect, ref _spanCount, i, j);

                        this.ChangeFixedShape(EN_FixedShape.Fixed1, g, new SolidBrush(Color.White), rect, EN_FillShape.FillRectangle, new FillShape(), Backbrush, j, i);

                        this.ChangeFixedShape(EN_FixedShape.Fixed2, g, new SolidBrush(Color.White), rect, EN_FillShape.FillRectangle, new FillShape(), Backbrush, j, i);

                        this.ChangeFixedShape(EN_FixedShape.Fixed3, g, new SolidBrush(Color.White), rect, EN_FillShape.FillRectangle, new FillShape(), Backbrush, j, i);
                    }
                    else
                    {
                        this.ChangeFillShape(g, Backbrush, rect, EN_FillShape.FillRectangle, new FillShape(), Backbrush);
                    }
                }
            }

            //计算比例
            //float _grassScale = Convert.ToSingle(QrCodeEncoder.QRCodeScale) / Convert.ToSingle(this.QrCodeH);
            float _grassScaleH = this.QrCodeH / 10 * 2;
            float _grassScaleW = this.QrCodeW / 6 * 2;

            g.DrawImage(_imggrass, 0, this.QrCodeH - _grassScaleH, _grassScaleW, _grassScaleH);

            int _addImgGrassCount = AddimgGrassCount((rect.Location.X + this.SpacingW) * QrCodeEncoder.QRCodeScale, Convert.ToInt32(_grassScaleW));
            for (int i = 1; i < _addImgGrassCount; i++)
            {
                g.DrawImage(_imggrass2, i * _grassScaleW, this.QrCodeH - _grassScaleH, _grassScaleW, _grassScaleH);
            }

            this.ChangeFixedShape(new SolidBrush(Color.White), Forebrush, g, EN_FixedShape.Fixed1, EN_FillShape.DrawImage, new FillShape() { img = _imgfixedShape1 }, true);

            this.ChangeFixedShape(new SolidBrush(Color.White), Forebrush, g, EN_FixedShape.Fixed2, EN_FillShape.DrawImage, new FillShape() { img = _imgfixedShape1 }, true);

            this.ChangeFixedShape(new SolidBrush(Color.White), Forebrush, g, EN_FixedShape.Fixed3, EN_FillShape.DrawImage, new FillShape() { img = _imgfixedShape1 }, true);


            this.ChangeFixedShape(new SolidBrush(Color.White), Forebrush, g, EN_FixedShape.Fixed1_0, EN_FillShape.DrawImage, new FillShape() { img = _imgfixedShape2_0 }, true);

            this.ChangeFixedShape(new SolidBrush(Color.White), Forebrush, g, EN_FixedShape.Fixed2_0, EN_FillShape.DrawImage, new FillShape() { img = _imgfixedShape2_0 }, true);

            this.ChangeFixedShape(new SolidBrush(Color.White), Forebrush, g, EN_FixedShape.Fixed3_0, EN_FillShape.DrawImage, new FillShape() { img = _imgfixedShape2_0 }, true);

            return image;
        }


        /// <summary>
        /// 返回新增绿草数量
        /// </summary>
        /// <param name="rectWidth"></param>
        /// <param name="imgGrassWidth"></param>
        /// <returns></returns>
        private int AddimgGrassCount(int rectWidth, int imgGrassWidth)
        {
            int i = 0;
            while (rectWidth > imgGrassWidth * i)
            {
                i++;
            }
            return i;
        }

        /// <summary>
        /// 愤怒的小鸟样式
        /// </summary>
        /// <param name="matrix">二维码矩阵数据</param>
        /// <param name="Backbrush">背景色</param>
        /// <param name="Forebrush">填充色</param>
        /// <param name="g">画板</param>
        /// <param name="rect">区域</param>
        /// <param name="_qrLen">二维码长度</param>
        /// <param name="_spanCount">返回木头跨度数量</param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void ChangeAngry_Birds(bool[][] matrix, SolidBrush Backbrush, SolidBrush Forebrush, Graphics g, ref Rectangle rect, ref int _spanCount, int i, int j)
        {
            //定义判定条件,防止边界不被判断
            int _bottomStep = i - 1 < 0 ? 0 : i - 1;
            int _topStep = i + 1 > matrix.Length - 1 ? matrix.Length - 1 : i + 1;
            int _rightStep = j + 1 > matrix.Length - 1 ? matrix.Length - 1 : j + 1;
            int _leftStep = j - 1 < 0 ? 0 : j - 1;

            int _topStep2 = i + 2 > matrix.Length - 1 ? matrix.Length - 1 : i + 2;
            int _topStep3 = i + 3 > matrix.Length - 1 ? matrix.Length - 1 : i + 3;

            int _rightStep2 = j + 2 > matrix.Length - 1 ? matrix.Length - 1 : j + 2;
            int _rightStep3 = j + 3 > matrix.Length - 1 ? matrix.Length - 1 : j + 3;


            if (matrix[j][_topStep] == true
                && matrix[_rightStep][i] == true
                && matrix[_rightStep][_topStep] == true
                && matrix[_leftStep][_topStep] != true
                && matrix[_leftStep][_bottomStep] != true
                && matrix[j][_bottomStep] != true
                && matrix[_rightStep2][_topStep2] == true
                && !isFixedShape(j, i)
                )
            {
                if (matrix[_rightStep2][_topStep2] == true)
                {
                    //猪1
                    matrix[j][_topStep] = false;
                    matrix[_rightStep][i] = false;
                    matrix[_rightStep][_topStep] = false;
                    Rectangle rect2 = new Rectangle((j + this.SpacingW) * QrCodeEncoder.QRCodeScale, (i + this.SpacingH) * QrCodeEncoder.QRCodeScale, QrCodeEncoder.QRCodeScale * 2, QrCodeEncoder.QRCodeScale * 2);
                    ChangeFillShape(g, Forebrush, rect2, EN_FillShape.DrawImage, new FillShape() { img = _imgPig1 }, Backbrush);
                }
                else
                {
                    //猪2
                    matrix[j][_topStep] = false;
                    matrix[_rightStep][i] = false;
                    matrix[_rightStep][_topStep] = false;
                    Rectangle rect2 = new Rectangle((j + this.SpacingW) * QrCodeEncoder.QRCodeScale, (i + this.SpacingH) * QrCodeEncoder.QRCodeScale, QrCodeEncoder.QRCodeScale * 2, QrCodeEncoder.QRCodeScale * 2);
                    ChangeFillShape(g, Forebrush, rect2, EN_FillShape.DrawImage, new FillShape() { img = _imgPig2 }, Backbrush);
                }
                if (matrix[_rightStep2][i] != true)
                {
                    //凳子上的猪
                    matrix[j][_topStep] = false;
                    matrix[_rightStep][i] = false;
                    matrix[_rightStep][_topStep] = false;
                    Rectangle rect2 = new Rectangle((j + this.SpacingW) * QrCodeEncoder.QRCodeScale, (i + this.SpacingH) * QrCodeEncoder.QRCodeScale, QrCodeEncoder.QRCodeScale * 2, QrCodeEncoder.QRCodeScale * 2);
                    ChangeFillShape(g, Forebrush, rect2, EN_FillShape.DrawImage, new FillShape() { img = _imgPig6 }, Backbrush);
                }
            }
            else if (matrix[_rightStep][i] == true
                    && matrix[_rightStep2][i] == true
                    && matrix[_rightStep3][i] == true
                    && matrix[_leftStep][i] == false
                    && j < matrix.Length - 3
                    && !isFixedShape(j, i)
                )
            {
                //木凳
                matrix[_rightStep][i] = false;
                matrix[_rightStep2][i] = false;
                matrix[_rightStep3][i] = false;
                Rectangle rect2 = new Rectangle((j + this.SpacingW) * QrCodeEncoder.QRCodeScale, (i + this.SpacingH) * QrCodeEncoder.QRCodeScale, QrCodeEncoder.QRCodeScale * 4, QrCodeEncoder.QRCodeScale * 1);
                ChangeFillShape(g, Forebrush, rect2, EN_FillShape.DrawImage, new FillShape() { img = _imgDesk }, Backbrush);
            }
            else if (isHorizontalWood(matrix, j, i, out _spanCount)
                && !isFixedShape(j, i)
                && _spanCount > 1
                )
            {
                //横木头
                for (int n = 0; n < _spanCount; n++)
                {
                    matrix[j + n][i] = false;
                }
                Rectangle rect2 = new Rectangle((j + this.SpacingW) * QrCodeEncoder.QRCodeScale, (i + this.SpacingH) * QrCodeEncoder.QRCodeScale, QrCodeEncoder.QRCodeScale * _spanCount, QrCodeEncoder.QRCodeScale * 1);
                ChangeFillShape(g, Forebrush, rect2, EN_FillShape.DrawImage, new FillShape() { img = _imgagri4 }, Backbrush);
            }
            else if (isVerticalWood(matrix, j, i, out _spanCount)
                && !isFixedShape(j, i)
                && _spanCount > 1
                )
            {
                //竖木头
                for (int n = 0; n < _spanCount; n++)
                {
                    matrix[j][i + n] = false;
                }
                Rectangle rect2 = new Rectangle((j + this.SpacingW) * QrCodeEncoder.QRCodeScale, (i + this.SpacingH) * QrCodeEncoder.QRCodeScale, QrCodeEncoder.QRCodeScale * 1, QrCodeEncoder.QRCodeScale * _spanCount);
                ChangeFillShape(g, Forebrush, rect2, EN_FillShape.DrawImage, new FillShape() { img = _imgagri5 }, Backbrush);
            }
            else
            {
                ChangeFillShape(g, Forebrush, rect, EN_FillShape.DrawImage, new FillShape() { img = _imgagri4 }, Backbrush);
            }

        }


        /// <summary>
        /// 判断是否在定位框内
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool isFixedShape(int x, int y)
        {
            foreach (var p in QrCodeEncoder.allFixed.Point)
            {
                if (x == p.X && y == p.Y)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 是否横木头
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="j"></param>
        /// <param name="i"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private bool isHorizontalWood(bool[][] matrix, int j, int i, out int count)
        {
            int _leftStep = j - 1 < 0 ? 0 : j - 1;
            for (int n = 0; n < matrix.Length; n++)
            {
                int _rightStepN1 = j + n + 1 > matrix.Length - 1 ? matrix.Length - 1 : j + n + 1;
                int _rightStepN = j + n > matrix.Length - 1 ? matrix.Length - 1 : j + n;

                if (matrix[_rightStepN][i] == true
                    && (matrix[_rightStepN1][i] != true || matrix[_leftStep][i] != true))
                {
                    count = n + 1;
                    return true;
                }
            }
            count = 0;
            return false;
        }

        /// <summary>
        /// 是否竖木头
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="j"></param>
        /// <param name="i"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private bool isVerticalWood(bool[][] matrix, int j, int i, out int count)
        {
            int _bottomStep = i - 1 < 0 ? 0 : i - 1;
            for (int n = 0; n < 4; n++)
            {
                int _topStepN1 = i + n + 1 > matrix.Length - 1 ? matrix.Length - 1 : i + n + 1;
                int _topStepN = i + n > matrix.Length - 1 ? matrix.Length - 1 : i + n;

                if (matrix[j][_topStepN] == true
                    && (matrix[j][_topStepN1] != true || matrix[j][_bottomStep] != true))
                {
                    count = n + 1;
                    return true;
                }
            }
            count = 0;
            return false;
        }
    }
}