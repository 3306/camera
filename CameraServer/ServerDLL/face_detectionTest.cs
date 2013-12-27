using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace ServerDLL
{
    class face_detectionTest
    {
        public  static  Face_detection face_detection=new Face_detection();
        public static void main() {
            string ImageURL = "C:\\1.jpg";
            MessageBox.Show(face_detection.HeadCounting(ImageURL).ToString());
        
        }
    }
}
