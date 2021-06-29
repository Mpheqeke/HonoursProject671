using Project.Core.Interfaces;
using Project.Core.Utilities;
using Microsoft.Extensions.Options;
using System;
using System.Drawing;
using System.IO;
using System.Web;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Project.Core.Global;

namespace Project.Infrastructure.Utilities
{
	public class MediaHelper : IMediaHelper
    {
		private IOptions<AppSettings> _appSettings;
		public MediaHelper(IOptions<AppSettings> settings)
		{
			_appSettings = settings;
		}



		public byte[] CreateThumbFromImage(byte[] fileData, int canvasWidth = 120, int canvasHeight = 120)
		{
			var image = Image.FromStream(new MemoryStream(fileData));

			var newImage = new Bitmap(canvasWidth, canvasHeight);
			var graphic = Graphics.FromImage(newImage);

			graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
			graphic.SmoothingMode = SmoothingMode.HighQuality;
			graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
			graphic.CompositingQuality = CompositingQuality.HighQuality;


			double ratioX = (double)canvasWidth / (double)image.Width;
			double ratioY = (double)canvasHeight / (double)image.Height;

			double ratio = ratioX < ratioY ? ratioX : ratioY;

			int newHeight = Convert.ToInt32(image.Height * ratio);
			int newWidth = Convert.ToInt32(image.Width * ratio);

			int posX = Convert.ToInt32((canvasWidth - (image.Width * ratio)) / 2);
			int posY = Convert.ToInt32((canvasHeight - (image.Height * ratio)) / 2);

			var color = Color.White;

			graphic.Clear(color);
			graphic.DrawImage(image, posX, posY, newWidth, newHeight);


			var info = ImageCodecInfo.GetImageEncoders();
			EncoderParameters encoderParameters;
			encoderParameters = new EncoderParameters(1);
			encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
			var outputStream = new MemoryStream();
			newImage.Save(outputStream, ImageFormat.Png);
			return outputStream.ToArray();
		}
	}
}


