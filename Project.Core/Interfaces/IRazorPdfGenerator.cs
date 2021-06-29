using System.IO;
using System.Threading.Tasks;

namespace Project.Core.Interfaces
{
	public interface IRazorPdfGenerator
	{
		MemoryStream RenderToString(string template, object model);
	}
}
