using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace PPI.Plugin.Survey.Core.Graph
{
    using Smrf.NodeXL.Core;    
    using Smrf.NodeXL.Layouts;
    using Smrf.NodeXL.Visualization.Wpf;
    using System.IO;
    public class NetworkGraph
    {
        public enum VertexColors { 
            Default,
            InsideNetwork,
            OutsideNetwork,
            Junior,
            Senior,
            Peer,
            Other
        };
        protected Graph _Graph = null;
        protected IVertexCollection _Vertices;
        protected IEdgeCollection _Edges;
        public Int32 GraphWidth {get;set;}
        public Int32 GraphHeight { get; set; }
        public NetworkGraph()
        {
            _Graph = new Graph(GraphDirectedness.Mixed);            
            _Vertices = _Graph.Vertices;
            _Edges = _Graph.Edges;            
        }
        protected IVertex CheckVertices(string contacts, VertexColors color)
        {
            IVertex RetVal;
            var exists = _Vertices.FirstOrDefault(m => m.Name == contacts);
            if (exists != null)
            {
                RetVal = exists;
            }
            else
            {
                RetVal = _Vertices.Add();                
                RetVal.Name = contacts;
                SetVertexDrawingSettings(RetVal, color);
            }            
            return RetVal;
        }
        public void SetEdges(string contactA, VertexColors contactAColor, string contactB, VertexColors contactBColor)
        {
            IEdge currentEdge = _Edges.Add(CheckVertices(contactA, contactAColor), CheckVertices(contactB, contactBColor), false);
            SetEdgeDrawingSettings(currentEdge);
        }
        protected void SetVertexDrawingSettings(IVertex vertex, VertexColors color)
        {
            vertex.SetValue(ReservedMetadataKeys.PerVertexLabel, vertex.Name);
            if (vertex.Name == "You")
            {
                vertex.SetValue(ReservedMetadataKeys.IsSelected,false);
            }
            vertex.SetValue(ReservedMetadataKeys.PerVertexShape, VertexShape.Disk);
            vertex.SetValue(ReservedMetadataKeys.PerVertexRadius,15F);
            //Default
            System.Windows.Media.Color VertexColor = System.Windows.Media.Color.FromRgb(8, 104, 172);
            System.Windows.Media.Color DefaultVertexColor = System.Windows.Media.Color.FromRgb(8, 104, 172);
            switch (color)
            {                
                case VertexColors.InsideNetwork:
                    VertexColor = System.Windows.Media.Color.FromRgb(253, 191, 111);
                    break;
                case VertexColors.Junior:
                    VertexColor = System.Windows.Media.Color.FromRgb(120, 198, 121);
                    break;
                case VertexColors.Other:
                    VertexColor = System.Windows.Media.Color.FromRgb(181, 204, 216);
                    break;
                case VertexColors.OutsideNetwork:
                    VertexColor = System.Windows.Media.Color.FromRgb(215, 181, 216);
                    break;
                case VertexColors.Peer:
                    VertexColor = System.Windows.Media.Color.FromRgb(253, 191, 111);
                    break;
                case VertexColors.Senior:
                    VertexColor = System.Windows.Media.Color.FromRgb(221, 28, 119);
                    break;                
            }
            
            vertex.SetValue(ReservedMetadataKeys.PerVertexLabelPosition, VertexLabelPosition.TopCenter);
            vertex.SetValue(ReservedMetadataKeys.PerVertexLabelFontSize, 30F);            
            vertex.SetValue(ReservedMetadataKeys.PerColor, VertexColor);
                 
        }
        protected void SetEdgeDrawingSettings(IEdge Edge)
        {
            Edge.SetValue(ReservedMetadataKeys.PerEdgeWidth, 2F);
            Edge.SetValue(ReservedMetadataKeys.PerEdgeStyle, EdgeStyle.Solid);            
        }
        public void GenerateImage(string outputFilePath)
        {
            //ILayout oLayout = new Smrf.NodeXL.Layouts.HarelKorenFastMultiscaleLayout();
            //ILayout oLayout = new Smrf.NodeXL.Layouts.CircleLayout();
            ILayout oLayout = new Smrf.NodeXL.Layouts.FruchtermanReingoldLayout();

            LayoutContext oLayoutContext = new LayoutContext(
             new Rectangle(0, 0, GraphWidth, GraphHeight));                     
            oLayout.LayOutGraph(_Graph, oLayoutContext);            
            // Create an object that can render a NodeXL graph as a Visual.

            NodeXLVisual oNodeXLVisual = new NodeXLVisual();

            // Use the NodeXLVisual object's GraphDrawer to draw the graph onto the
            // Visual.

            GraphDrawingContext oGraphDrawingContext = new GraphDrawingContext(
             new Rect(0, 0, (GraphWidth), (GraphHeight)), oLayout.Margin,
             System.Windows.Media.Color.FromRgb(255, 255, 255));
            var Font = new Typeface(new System.Windows.Media.FontFamily("Times New Roman"), FontStyles.Normal, FontWeights.Bold, FontStretches.Normal);
            oNodeXLVisual.GraphDrawer.VertexDrawer.SetFont(Font,20f);
            oNodeXLVisual.GraphDrawer.DrawGraph(_Graph, oGraphDrawingContext);

            // Convert the Visual to a Bitmap.

            RenderTargetBitmap oRenderTargetBitmap = new RenderTargetBitmap(
             GraphWidth, GraphHeight, 96, 96, PixelFormats.Default);

            oRenderTargetBitmap.Render(oNodeXLVisual);
            BmpBitmapEncoder oBmpBitmapEncoder = new BmpBitmapEncoder();
            oBmpBitmapEncoder.Frames.Add(BitmapFrame.Create(oRenderTargetBitmap));
            MemoryStream oMemoryStream = new MemoryStream();
            oBmpBitmapEncoder.Save(oMemoryStream);
            Bitmap oBitmap = new Bitmap(oMemoryStream);

            // Write the Bitmap's contents to the response stream.

            oBitmap.Save(outputFilePath, ImageFormat.Bmp);
        }
    }
}
