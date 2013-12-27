using System;
using System.Collections.Generic;
using System.Text;
using Emgu.CV;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using Emgu.CV.UI;

namespace ServerDLL
{
    class Face_detection
    {
        private HaarCascade faces;
        private HaarCascade eyes;
        double detected;
        double total;
        ImageViewer viewer;
        public   double HeadCounting(string ImageURL)
        {
          
            viewer = new ImageViewer();
            faces = new HaarCascade(System.Environment.CurrentDirectory + "\\haarcascade_frontalface_default.xml");
            eyes = new HaarCascade( System.Environment.CurrentDirectory+"\\haarcascade_eye.xml");
            total = 0;
            detected = 0;
            Image<Gray, byte> selectedFrame = new Image<Gray, byte>(ImageURL);
            double faceDetected = 0;
            if (selectedFrame != null) {
                Image<Gray, Byte> grayFrame = selectedFrame.Convert<Gray, Byte>();
                var detectedFaces = grayFrame.DetectHaarCascade(faces)[0];
                var detectedEyes = grayFrame.DetectHaarCascade(eyes)[0];
                foreach (var face in detectedFaces)
                {
                    faceDetected++;
                    detected++;
                }
               
            }


            return faceDetected;
        }
       



    }
}
