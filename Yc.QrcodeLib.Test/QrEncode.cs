using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using Yc.QrCodeLib.custom;
using Yc.QrCodeLib.custom.define;
using System.Drawing.Drawing2D;

namespace Yc.QrcodeLib.Test
{
    public class QrEncode : Yc.QrCodeLib.custom.CustomEncode
    {
        public QrEncode(string key)
            : base(key)
        {

        }

        public override void SetParam()
        {
            base.SetParam();
        }

        //TODO:一系列个性二维码生成方案
        public override Bitmap Encode(string content)
        {
            try
            {
                matrix = QrCodeEncoder.calQrcode(EnCoding.GetBytes(content));
            }
            catch { throw new Exception("内容超出范围，请选择更高版本或者降低容错率"); }

            this.SetParam();

            //SolidBrush Backbrush = new SolidBrush(QrCodeEncoder.QRCodeBackgroundColor);
            SolidBrush Backbrush = new SolidBrush(Color.Transparent);//背景透明
            SolidBrush Forebrush = new SolidBrush(QrCodeEncoder.QRCodeForegroundColor);

            Bitmap image = new Bitmap(this.QrCodeW, this.QrCodeH);
            Graphics g = Graphics.FromImage(image);

            Rectangle rect = new Rectangle();

            g.FillRectangle(Backbrush, new Rectangle(0, 0, image.Width, image.Height));

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix.Length; j++)
                {
                    rect = new Rectangle((j + this.SpacingW) * QrCodeEncoder.QRCodeScale, (i + this.SpacingH) * QrCodeEncoder.QRCodeScale, QrCodeEncoder.QRCodeScale, QrCodeEncoder.QRCodeScale);
                    if (matrix[j][i])
                    {
                        ChangeFillShape(g, Forebrush, rect, EN_FillShape.FillRectangle, new FillShape(), Backbrush);
                    }
                    else
                        ChangeFillShape(g, Backbrush, rect, EN_FillShape.FillRectangle, new FillShape(), Backbrush);
                }
            }
            return image;
        }

        //    public override Bitmap Encode(string content)
        //    {
        //        //return base.Encode(content);
        //        //QrCodeEncoder.FrameSpacing = 20;

        //        //读取图片                                             
        //        string _imagePath1 = Environment.CurrentDirectory + @"\image\test8.png";
        //        Image _img1 = Image.FromFile(_imagePath1);

        //        string _imagePath2 = Environment.CurrentDirectory + @"\image\test5.jpg";
        //        Image _img2 = Image.FromFile(_imagePath2);

        //        try
        //        {
        //            this.matrix = QrCodeEncoder.calQrcode(EnCoding.GetBytes(content));
        //        }
        //        catch { throw new Exception("内容超出范围，请选择更高版本或者降低容错率"); }

        //        base.SetParam();


        //        //SolidBrush Backbrush = new SolidBrush(QrCodeEncoder.QRCodeBackgroundColor);
        //        SolidBrush Backbrush = new SolidBrush(Color.Transparent);//背景透明
        //        SolidBrush Forebrush = new SolidBrush(QrCodeEncoder.QRCodeForegroundColor);

        //        Bitmap image = new Bitmap(this.QrCodeW, this.QrCodeH);

        //        Graphics g = Graphics.FromImage(image);

        //        //g.DrawImage(_img1, 0, 0, 500, 500);//后背景

        //        Rectangle rect = new Rectangle();

        //        g.FillRectangle(Backbrush, new Rectangle(0, 0, image.Width, image.Height));

        //        for (int i = 0; i < matrix.Length; i++)
        //        {
        //            for (int j = 0; j < matrix.Length; j++)
        //            {
        //                rect = new Rectangle((j + QrCodeEncoder.FrameSpacing / 2) * QrCodeEncoder.QRCodeScale, (i + QrCodeEncoder.FrameSpacing / 2) * QrCodeEncoder.QRCodeScale, QrCodeEncoder.QRCodeScale, QrCodeEncoder.QRCodeScale);

        //                if (matrix[j][i])
        //                {
        //                    //ChangeFillShape(g, Forebrush, rect, EN_FillShape.DrawImageFill, new FillShape() { img = _img }, Backbrush);
        //                    //ChangeFillShape(g, Forebrush, rect, EN_FillShape.DrawImage, new FillShape() { img = _img }, Backbrush);
        //                    ChangeFillShape(g, Forebrush, rect, EN_FillShape.FillRectangle, new FillShape(), Backbrush);
        //                    //ChangeFillShape(g, Forebrush, rect, EN_FillShape.FillRoundRectangle, new FillShape() { radius = 30, roundStyle = RoundStyle.All }, Backbrush);
        //                    ////渐变画刷
        //                    //LinearGradientBrush brush = new LinearGradientBrush(rect, ImageFix.GetRandomColor(), ImageFix.GetRandomColor(), 0f);

        //                    //ChangeFixedShape(EN_FixedShape.Fixed1, g, brush, rect, EN_FillShape.FillRectangle, new FillShape(), Backbrush, j, i);
        //                    //ChangeFixedShape(EN_FixedShape.Fixed1_0, g, Forebrush, rect, EN_FillShape.FillEllipse, null, Backbrush, j, i);


        //                    //ChangeFillShape(g, Forebrush, rect, EN_FillShape.DrawImageFill, new FillShape() { img = _img }, Backbrush);

        //                    //FillShape _fillShape = new FillShape();
        //                    //_fillShape = InkPaint(matrix, i, j, rect, 20, true);
        //                    //ChangeFillShape(g, Forebrush, rect, EN_FillShape.FillRoundRectangle, _fillShape, Backbrush);

        //                    //ChangeFixedShape(EN_FixedShape.Fixed1, g, Backbrush, rect, EN_FillShape.FillRectangle, new FillShape(), Backbrush, j, i);
        //                    //ChangeFixedShape(EN_FixedShape.Fixed2, g, Backbrush, rect, EN_FillShape.FillRectangle, new FillShape(), Backbrush, j, i);
        //                    //ChangeFixedShape(EN_FixedShape.Fixed3, g, Backbrush, rect, EN_FillShape.FillRectangle, new FillShape(), Backbrush, j, i);

        //                    //SolidBrush brush = new SolidBrush(Color.FromArgb(134, 0, 24));

        //                    //ChangeFillShape(g, Forebrush, rect, EN_FillShape.FillPolygon, new FillShape() { pointed = 3 }, Backbrush);

        //                    //this.ChangeFillShape(g, Forebrush, rect, EN_FillShape.FillRectangle, new FillShape(), Backbrush);

        //                    //this.ChangeFixedShape(EN_FixedShape.Fixed1, g, Backbrush, rect, EN_FillShape.FillHeart, new FillShape(), Backbrush, j, i);
        //                    //this.ChangeFixedShape(EN_FixedShape.Fixed2, g, Backbrush, rect, EN_FillShape.FillRectangle, new FillShape(), Backbrush, j, i);
        //                    //this.ChangeFixedShape(EN_FixedShape.Fixed3, g, Backbrush, rect, EN_FillShape.FillRectangle, new FillShape(), Backbrush, j, i);
        //                }
        //                else
        //                {
        //                    //FillShape _fillShape = new FillShape();
        //                    //_fillShape = InkPaint(matrix, i, j, rect, 20, false);
        //                    //ChangeFillShape(g, Backbrush, rect, EN_FillShape.FillRoundRectangle, _fillShape, Forebrush);//注意：液化背景色传入填充色ForeBrush

        //                    //this.ChangeFillShape(g, Backbrush, rect, EN_FillShape.FillRectangle, new FillShape(), Backbrush);
        //                }
        //            }
        //        }
        //        //this.ChangeFixedShape(Backbrush, Forebrush, g, EN_FillShape.FillRectangle, EN_FillShape.FillHeart, new FillShape(), new FillShape(), true);//注意: 在外修改定位方形，最后isOutsiode需要为true
        //        //this.ChangeFixedShape(Backbrush, Forebrush, g, EN_FillShape.FillEllipse, EN_FillShape.FillDiamond, new FillShape(), new FillShape(), true);

        //        //this.ChangeFixedShape(Backbrush, Forebrush, g, EN_FixedShape.Fixed1, EN_FillShape.FillRectangle, new FillShape(), true);
        //        //this.ChangeFixedShape(Backbrush, Forebrush, g, EN_FixedShape.Fixed1_0, EN_FillShape.FillHeart, new FillShape(), true);

        //        //this.ChangeFixedShape(Backbrush, Forebrush, g, EN_FixedShape.Fixed2, EN_FillShape.FillRectangle, new FillShape(), true);
        //        //this.ChangeFixedShape(Backbrush, Forebrush, g, EN_FixedShape.Fixed2_0, EN_FillShape.FillPlumBlossom, new FillShape(), true);

        //        //this.ChangeFixedShape(Backbrush, Forebrush, g, EN_FixedShape.Fixed3, EN_FillShape.FillRectangle, new FillShape(), true);
        //        //this.ChangeFixedShape(Backbrush, Forebrush, g, EN_FixedShape.Fixed3_0, EN_FillShape.FillPeach, new FillShape(), true);


        //        //this.ChangeFillShape(g, Forebrush, new Rectangle(0, 0, (matrix.Length * qrCodeEncoder.QRCodeScale), (matrix.Length * qrCodeEncoder.QRCodeScale)), EN_FillShape.FillPeach, new FillShape(), Backbrush);

        //        //液化平铺
        //        //GraphicsPath _path = ImageFix.GetWindowRegion(image, QrCodeEncoder.QRCodeForegroundColor);
        //        //TextureBrush texture1 = new TextureBrush(_img1);
        //        //g.FillPath(texture1, _path);

        //        ////渐变画刷
        //        //LinearGradientBrush brush = new LinearGradientBrush(rect, ImageFix.GetRandomColor(), ImageFix.GetRandomColor(), 0f);
        //        //Rectangle rect2 = new Rectangle(qrCodeEncoder.fixed1.Point[5].Y * qrCodeEncoder.qrCodeScale, qrCodeEncoder.fixed1.Point[5].X * qrCodeEncoder.qrCodeScale, qrCodeEncoder.qrCodeScale, qrCodeEncoder.qrCodeScale);
        //        //ChangeFillShape(g, brush, rect2, EN_FillShape.FillRectangle, null, Backbrush);

        //        //透明背景
        //        //image = ImageFix.MakeTransparentGif(image, qrCodeEncoder.QRCodeBackgroundColor);

        //        //修改透明度
        //        //image = ImageFix.ChangeOpacity((Image)image, 0.3f);

        //        //修改渐变
        //        //image = ImageFix.BothAlpha(image,true,true);

        //        //锐化图片s
        //        //image = ImageFix.KiSharpen(image, 100);

        //        //马赛克
        //        //image = ImageFix.KiMosaic(image, 5);

        //        //对比度调整
        //        //image = ImageFix.KiContrast(image,80);

        //        //亮度调整
        //        //image = ImageFix.KiLighten(image, 100);

        //        //色彩调整
        //        //image = ImageFix.KiColorBalance(image, 255, 200, 100);

        //        //柔化
        //        //image = KiBlur(image,9);

        //        //黑白
        //        //image = ToGray(image,1);
        //        //反色(不能识别)
        //        //image = RePic(image, image.Width, image.Height);
        //        //雾化(不能识别)
        //        //image = Atomization(image);
        //        //浮雕(不能识别)
        //        //image = ImageFix.Relief(image);
        //        //平滑(效率过低)
        //        //image = (Bitmap)BlurProcess(image,5);
        //        //霓虹(不能识别)
        //        //image = NeonProcess(image, 1f, 0.5f, 0.1f);
        //        //梯度(不能识别)
        //        //image = ImageFix.Grads(image);
        //        //光照
        //        //image = Light(image, image.Width / 2, image.Height / 2, 200, 100);
        //        //油画(不能识别)
        //        //image = ImageFix.OilPaint(image);
        //        //积木(不能识别)
        //        //image = ImageFix.Blocks(image);
        //        //图片旋转
        //        //image = Rotate(image, 45);
        //        //羽化
        //        //image = ImageFix.Render(image, 100, 0.7f);

        //        //g.DrawImage(_img2, 20 * QrCodeEncoder.QRCodeScale, 20 * QrCodeEncoder.QRCodeScale, 200, 200);//前背景
        //        return image;
        //    }
    }
}
