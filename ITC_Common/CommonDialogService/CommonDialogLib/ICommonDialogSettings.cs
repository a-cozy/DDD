﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDialogs
{
	public interface ICommonDialogSettings
	{
		string InitialDirectory { get; set; }

		string Title { get; set; }
	}
}
