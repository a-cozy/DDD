using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDialogs
{
	public class OpenFileDialogSettings : SaveFileDialogSettings,IDisposable
	{
		public bool Multiselect { get; set; } = false;

		public List<string> FileNames { get; set; } = new List<string>();

        public void Dispose()
        {
            Multiselect = false;
            FileNames = null;
        }
    }
}
