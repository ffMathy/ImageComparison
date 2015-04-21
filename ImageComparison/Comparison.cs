using ImageComparison.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageComparison
{
    public class Comparison
    {

        public int ComparisonSize { get; set; }

        public Comparison()
        {
            ComparisonSize = 100;
        }

        public void Compare(IImage image1, IImage image2)
        {
            if (image1 == null) throw new ArgumentNullException("image1");
            if (image2 == null) throw new ArgumentNullException("image2");

            using (new Lock(image1))
            using (new Lock(image2))
            {
                Prepare(image1);
                Prepare(image2);
            }
        }

        private void Prepare(IImage image)
        {
            if (image == null) throw new ArgumentNullException("image");

            //first resize the images to a computable chunk.
            var imageWidth = image.Width;
            var imageHeight = image.Height;

            var downscaleFactor = 0d;

            if (imageWidth > ComparisonSize)
            {
                downscaleFactor = imageWidth / 5d;
            }
            if (imageHeight > ComparisonSize)
            {
                downscaleFactor = imageHeight / 5d;
            }

            imageHeight /= downscaleFactor;
            imageWidth /= downscaleFactor;

            image.Resize(imageWidth, imageHeight);

            //blur the image.
            for (var currentX = 1; currentX < image.Width - 1; currentX++)
            {
                for (var currentY = 1; currentY < image.Height - 1; currentY++)
                {
                    //get the average of surrounding pixels and set that to the target.
                    var surroundingPixels = new LinkedList<Color>();
                    for (var surroundingX = currentX - 1; surroundingX < currentX + 1; surroundingX++)
                    {
                        for (var surroundingY = currentY - 1; surroundingY < currentY + 1; surroundingY++)
                        {
                            var color = image.GetPixel(surroundingX, surroundingY);
                            surroundingPixels.AddLast(color);
                        }
                    }

                    var average = new Color()
                    {
                        Red = surroundingPixels.Average(c => c.Red),
                        Green = surroundingPixels.Average(c => c.Green),
                        Blue = surroundingPixels.Average(c => c.Blue)
                    };
                    image.SetPixel(currentX, currentY, average);
                }
            }
        }

        //discretize image into n different colors.
        //TODO        
    }
}
