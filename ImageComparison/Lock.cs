using ImageComparison.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageComparison
{
    class Lock : IDisposable
    {
        private readonly IImage image;

        public Lock(IImage image)
        {
            this.image = image;

            image.LockBits();
        }

        public void Dispose()
        {
            image.UnlockBits();
        }
    }
}
