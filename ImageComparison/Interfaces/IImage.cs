using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageComparison.Interfaces
{
    public interface IImage
    {
        int Width { get; }

        int Height { get; }

        void LockBits();

        void UnlockBits();

        void Resize(int width, int height);

        Color GetPixel(int x, int y);

        void SetPixel(int x, int y, Color color);
    }
}
