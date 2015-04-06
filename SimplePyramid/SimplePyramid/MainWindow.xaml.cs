﻿using System;
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
using HelixToolkit.Wpf;

namespace SimplePyramid
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DrawPyramid();
        }
        private Color myColor = Colors.Green;

        private int Length = 10;

        private Model3DGroup CreateTriangleModel(Point3D p0, Point3D p1, Point3D p2)
        {
            var myMeshBuilder = new MeshBuilder();
            var edgeMeshBuilder = new MeshBuilder();
            myMeshBuilder.AddTriangle(p0, p1, p2);
            edgeMeshBuilder.AddCylinder(p0, p1, 0.3, 100);
            edgeMeshBuilder.AddCylinder(p1, p2, 0.3, 100);
            edgeMeshBuilder.AddCylinder(p2, p0, 0.3, 100);
            var mesh = myMeshBuilder.ToMesh(true);
            var edgeMesh = edgeMeshBuilder.ToMesh(true);
            var material = MaterialHelper.CreateMaterial(this.myColor);
            var edgeMaterial = MaterialHelper.CreateMaterial(this.myColor);

            Model3DGroup Group = new Model3DGroup();
            //Group.Children.Add(new GeometryModel3D { Geometry = mesh, Material = material });
            Group.Children.Add(new GeometryModel3D { Geometry = edgeMesh, Material = edgeMaterial });
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
            if (LengthBox != null)
            {
                if (LengthBox.Text == "" | LengthBox.Text == "Length") this.Length = 10;
                else this.Length = Convert.ToInt32(LengthBox.Text);
            }
            else this.Length = 10;
            Model3DGroup triangle = new Model3DGroup();

            Point3D p0 = new Point3D(0, 0, 0);
            Point3D p1 = new Point3D(this.Length, -this.Length, 0);
            Point3D p2 = new Point3D(this.Length, 0, this.Length);
            Point3D p3 = new Point3D(0, 0, this.Length);
            Point3D p4 = new Point3D(0, 0, 0);
            Point3D p5 = new Point3D(this.Length, 0, 0);
            Point3D p6 = new Point3D(this.Length, 0, 2 * this.Length);

            triangle.Children.Add(CreateTriangleModel(p1, p5, p6));
            triangle.Children.Add(CreateTriangleModel(p1, p4, p5));
            triangle.Children.Add(CreateTriangleModel(p5, p4, p6));
            triangle.Children.Add(CreateTriangleModel(p6, p4, p1));


            this.MainViewPort.InvalidateVisual();
            MyModel.Content = triangle;
        }


        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            Color myColor = new Color();
            myColor.A = ClrPcker_Background.SelectedColor.A;
            myColor.R = ClrPcker_Background.SelectedColor.R;
            myColor.G = ClrPcker_Background.SelectedColor.G;
            myColor.B = ClrPcker_Background.SelectedColor.B;
            this.myColor = myColor;
            DrawPyramid();
        }

        private void Length_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Convert.ToInt32(LengthBox.Text);
            }
            catch
            {
                LengthBox.Text = "10";
                this.Length = 10;
            }
            DrawPyramid();
        }
       
    }
}
