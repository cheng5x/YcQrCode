YcQrCode
========

码晒客/疯狂创意二维码，底层 ,模版制作开源



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

blog:		
http://www.cnblogs.com/cheng5x

码晒客讨论QQ群：
28629273