using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace PylonLogGraphUserControl
{
    public partial class PylonLogGraphUserControl: UserControl
    {
        public PylonLogGraphUserControl()
        {
            InitializeComponent();
            CreateExampleGraph(zgcPylonLogGraph);
            SetSize();
        }

        public PylonLogGraphUserControl(List<Double[]> firstList, List<Double[]> secondList)
        {
            InitializeComponent();
            CreateGraph(zgcPylonLogGraph, new PointPairList(firstList[0],firstList[1]) , new PointPairList(secondList[0], secondList[1]));
            SetSize();
        }

        public new int Width
        {
            get { return zgcPylonLogGraph.Width; }
            set { zgcPylonLogGraph.Width = value; }
        }
        public new int Height
        {
            get { return zgcPylonLogGraph.Height; }
            set { zgcPylonLogGraph.Height = value; }
        }


        private void CreateGraph(ZedGraphControl zgc, PointPairList firstList, PointPairList secondList)
        {
            // get a reference to the GraphPane
            GraphPane myPane = zgc.GraphPane;

            // Set the Titles
            myPane.Title.Text = "Telemetry Graph";
            myPane.XAxis.Title.Text = "My X Axis";
            myPane.YAxis.Title.Text = "My Y Axis";
            myPane.XAxis.Scale.MajorStep = 5;


            // Generate a red curve with diamond
            // symbols, and "Porsche" in the legend
            LineItem myCurve = myPane.AddCurve("First List",
                  firstList, System.Drawing.Color.Red, SymbolType.Diamond);

            // Generate a blue curve with circle
            // symbols, and "Piper" in the legend
            LineItem myCurve2 = myPane.AddCurve("Second List",
                  secondList, System.Drawing.Color.Blue, SymbolType.Circle);

            // Tell ZedGraph to refigure the
            // axes since the data have changed
            zgc.AxisChange();

            zgc.IsShowPointValues = true;
        }
        private void CreateExampleGraph(ZedGraphControl zgc)
        {
            // get a reference to the GraphPane
            GraphPane myPane = zgc.GraphPane;

            // Set the Titles
            myPane.Title.Text = "Telemetry Graph";
            myPane.XAxis.Title.Text = "My X Axis";
            myPane.YAxis.Title.Text = "My Y Axis";
            myPane.XAxis.Scale.MajorStep = 5;

            // Make up some data arrays based on the Sine function
            double x, y1, y2;
            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();
            for (int i = 0; i < 36; i++)
            {
                x = (double)i + 5;
                y1 = 1.5 + Math.Sin((double)i * 0.2);
                y2 = Math.Round(3.0 * (1.5 + Math.Sin((double)i * 0.2)), 2);
                list1.Add(x, y1);
                list2.Add(x, y2);
            }

            // Generate a red curve with diamond
            // symbols, and "Porsche" in the legend
            LineItem myCurve = myPane.AddCurve("Porsche",
                  list1, System.Drawing.Color.Red, SymbolType.Diamond);

            // Generate a blue curve with circle
            // symbols, and "Piper" in the legend
            LineItem myCurve2 = myPane.AddCurve("Piper",
                  list2, System.Drawing.Color.Blue, SymbolType.Circle);

            // Tell ZedGraph to refigure the
            // axes since the data have changed
            zgc.AxisChange();

            zgc.IsShowPointValues = true;
        }

        private void SetSize()
        {
            zgcPylonLogGraph.Location = new System.Drawing.Point(10, 10);

            // Leave a small margin around the outside of the control
            zgcPylonLogGraph.Size = new System.Drawing.Size((int)this.Width - 20,
                                    (int)this.Height - 20);
        }
    }
}
