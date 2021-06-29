namespace Project.Core.Interfaces
{
	public interface IMediaHelper
    {
        byte[] CreateThumbFromImage(byte[] fileData, int thumbWidth, int thumbHeight);
    }
}
