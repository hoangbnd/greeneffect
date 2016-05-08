﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace GreenEffect.Web.Framework.UI
{
    public partial class AsIsBundleOrderer : IBundleOrderer
    {
        public virtual IEnumerable<System.IO.FileInfo> OrderFiles(BundleContext context, IEnumerable<System.IO.FileInfo> files)
        {
            return files;
        }

        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}
