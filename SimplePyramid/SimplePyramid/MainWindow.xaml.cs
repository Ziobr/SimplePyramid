using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Media.Media3D.Converters;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimplePyramid
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //AxisAngleRotation3D ax3d;
        //private ModelVisual3D MyModel { set; get; }
        public MainWindow()
        {
            InitializeComponent();
            DrawPyramid();
            //ax3d = new AxisAngleRotation3D(new Vector3D(0, 2, 0), 1);
            //RotateTransform3D myRotateTransform = new RotateTransform3D(ax3d);
            //MyModel.Transform = myRotateTransform;
        }
        private SolidColorBrush myBrush = new SolidColorBrush(Colors.Black);
        //private ModelVisual3D MyModel { get; set; }

        private Model3DGroup CreateTriangleModel(Point3D p0, Point3D p1, Point3D p2)
        {
            MeshGeometry3D mymesh = new MeshGeometry3D();
            mymesh.Positions.Add(p0);
            mymesh.Positions.Add(p1);
            mymesh.Positions.Add(p2);
            mymesh.TriangleIndices.Add(0);
            mymesh.TriangleIndices.Add(1);
            mymesh.TriangleIndices.Add(2);
            Vector3D Normal = CalculateTraingleNormal(p0, p1, p2);
            mymesh.Normals.Add(Normal);
            mymesh.Normals.Add(Normal);
            mymesh.Normals.Add(Normal);
            Material Material = new DiffuseMaterial(this.myBrush);
            GeometryModel3D model = new GeometryModel3D(
                mymesh, Material);
            Model3DGroup Group = new Model3DGroup();
            Group.Children.Add(model);
            return Group;
        }
        private Vector3D CalculateTraingleNormal(Point3D p0, Point3D p1, Point3D p2)
        {
            Vector3D v0 = new Vector3D(
                p1.X - p0.X, p1.Y - p0.Y, p1.Z - p0.Z);
            Vector3D v1 = new Vector3D(
                p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            return Vector3D.CrossProduct(v0, v1);
        }
        private void DrawPyramid()
        {
            int l;
            if(Length.Text == " " | Length.Text == "Length")
            {
                l = 10;
            }
            else
            {
                l = Convert.ToInt32(Length.Text);
            }
            Model3DGroup triangle = new Model3DGroup();
            Point3D p0 = new Point3D(0, 0, 0);
            Point3D p1 = new Point3D(l, -l, 0);
            Point3D p2 = new Point3D(5, 0, 5);
            Point3D p3 = new Point3D(0, 0, 5);
            Point3D p4 = new Point3D(0, 0, 0);
            Point3D p5 = new Point3D(l, 0, 0);
            Point3D p6 = new Point3D(l, 0, l);


            //            triangle.Children.Add(CreateTriangleModel(p1, p4, p3));
            //            triangle.Children.Add(CreateTriangleModel(p1, p4, p6));
            //            triangle.Children.Add(CreateTriangleModel(p3, p1, p6));

            triangle.Children.Add(CreateTriangleModel(p1, p5, p6));
            triangle.Children.Add(CreateTriangleModel(p1, p4, p5));
            triangle.Children.Add(CreateTriangleModel(p5, p4, p6));
            triangle.Children.Add(CreateTriangleModel(p6, p4, p1));

            //triangle.Children.Add(CreateTriangleModel(p0, p4, p1));
            //triangle.Children.Add(CreateTriangleModel(p0, p3, p4));
            //triangle.Children.Add(CreateTriangleModel(p4, p3, p1));


            this.MainViewPort.InvalidateVisual();
            //ModelVisual3D myModel = new ModelVisual3D();
            //this.MainViewPort.Children.Add(MyModel);
            MyModel.Content = triangle;
            //MyModel = myModel;
        }

        //private void rotateButtonClick(object sender, RoutedEventArgs e)
        //{
        //    ax3d.Angle += 1 ;
        //}

        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            Color myColor = new Color();
            myColor.A = ClrPcker_Background.SelectedColor.A;
            myColor.R = ClrPcker_Background.SelectedColor.R;
            myColor.G = ClrPcker_Background.SelectedColor.G;
            myColor.B = ClrPcker_Background.SelectedColor.B;
            this.myBrush = new SolidColorBrush(myColor);
            DrawPyramid();
            //PyramidButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            //TextBox.Text = "#" + ClrPcker_Background.SelectedColor.R.ToString() + ClrPcker_Background.SelectedColor.G.ToString() + ClrPcker_Background.SelectedColor.B.ToString();
        }
    }
}
