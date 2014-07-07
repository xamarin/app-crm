//
// SignaturePad.cs: User Control subclass for Windows Phone to allow users to draw their signature on 
//                   the device to be captured as an image or vector.
//
// Author:
//   Timothy Risi (timothy.risi@gmail.com)
//
// Copyright (C) 2012 Timothy Risi
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Ink;
using System.Windows.Media.Imaging;

namespace Xamarin.Controls
{
	public partial class SignaturePadView : UserControl
	{
		Stroke currentStroke;
		List<Stroke> strokes;
		List<Point> currentPoints;
		List<Point []> points;

		//Create an array containing all of the points used to draw the signature.  Uses (-10000, -10000)
		//to indicate a new line.
		public Point [] Points
		{
			get
			{
				if (points == null || points.Count () == 0)
					return new Point [0];

				List<Point> pointsList = points [0].ToList ();

				for (var i = 1; i < points.Count; i++) {
					pointsList.Add (new Point (-10000, -10000));
					pointsList = pointsList.Concat (points [i]).ToList ();
				}

				return pointsList.ToArray ();
			}
		}

		public bool IsBlank
		{
			get { return Points.Count () == 0; }
		}

		Color strokeColor;
		public Color StrokeColor
		{
			get { return strokeColor; }
			set { 
				strokeColor = value;
				if (currentStroke != null)
					currentStroke.DrawingAttributes.Color = strokeColor;
				foreach (var stroke in inkPresenter.Strokes)
					stroke.DrawingAttributes.Color = strokeColor;
			}
		}

		Color backgroundColor;
		public Color BackgroundColor
		{
			get { return backgroundColor; }
			set { 
				backgroundColor = value;
				LayoutRoot.Background = new SolidColorBrush (value);
			}
		}

		float lineWidth;
		public float LineWidth
		{
			get { return lineWidth; }
			set { 
				lineWidth = value;
				if (currentStroke != null) {
					currentStroke.DrawingAttributes.Height = lineWidth;
					currentStroke.DrawingAttributes.Width = lineWidth;
				}
				foreach (var stroke in inkPresenter.Strokes) {
					stroke.DrawingAttributes.Height = lineWidth;
					stroke.DrawingAttributes.Width = lineWidth;
				}
			}
		}

    public SignaturePadView()
		{
			InitializeComponent ();
			Initialize ();
		}

		void Initialize ()
		{
			currentPoints = new List<Point> ();
			points = new List<Point []> ();
			strokes = new List<Stroke> ();
			strokeColor = Colors.White;
			backgroundColor = Colors.Black;
			LayoutRoot.Background = new SolidColorBrush (backgroundColor);
			lineWidth = 3f;
		}

		public override void OnApplyTemplate ()
		{
			
		}

		//Delete the current signature
		public void Clear ()
		{
			currentPoints.Clear ();
			currentStroke = null;
			btnClear.Visibility = Visibility.Collapsed;
			inkPresenter.Strokes.Clear ();
			image.Source = null;
			points.Clear ();
			strokes.Clear ();
		}

		private void btnClear_Click (object sender, RoutedEventArgs e)
		{
			Clear ();
		}

		#region GetImage
		//Create a WriteableBitmap of the currently drawn signature with default colors.
		public WriteableBitmap GetImage (bool shouldCrop = true)
		{
			return GetImage (strokeColor, 
				         Colors.Transparent, 
					 RenderSize,
					 1, 
					 shouldCrop);
		}

		public WriteableBitmap GetImage (Size size, bool shouldCrop = true)
		{
			return GetImage (strokeColor, 
			                 Colors.Transparent, 
					 size, 
			                 getScaleFromSize (size, RenderSize), 
					 shouldCrop);
		}

		public WriteableBitmap GetImage (float scale, bool shouldCrop = true)
		{
			return GetImage (strokeColor,
			                 Colors.Transparent, 
					 getSizeFromScale (scale, RenderSize), 
					 scale, 
					 shouldCrop);
		}

		//Create a WriteableBitmap of the currently drawn signature with the specified Stroke color.
		public WriteableBitmap GetImage (Color strokeColor, bool shouldCrop = true)
		{
			return GetImage (strokeColor, 
			                 Colors.Transparent,
					 RenderSize,
					 1, 
					 shouldCrop);
		}

		public WriteableBitmap GetImage (Color strokeColor, Size size, bool shouldCrop = true)
		{
			return GetImage (strokeColor, 
			                 Colors.Transparent, 
					 size, 
					 getScaleFromSize (size, RenderSize), 
					 shouldCrop);
		}

		public WriteableBitmap GetImage (Color strokeColor, float scale, bool shouldCrop = true)
		{
			return GetImage (strokeColor, 
			                 Colors.Transparent, 
					 getSizeFromScale (scale, RenderSize), 
					 scale, 
					 shouldCrop);
		}

		//Create a WriteableBitmap of the currently drawn signature with the specified Stroke and Fill colors.
		public WriteableBitmap GetImage (Color strokeColor, Color fillColor, bool shouldCrop = true)
		{
			return GetImage (strokeColor, 
			                 fillColor,
			                 RenderSize,
					 1, 
					 shouldCrop);
		}

		public WriteableBitmap GetImage (Color strokeColor, Color fillColor, Size size, bool shouldCrop = true)
		{
			return GetImage (strokeColor, 
			                 fillColor, 
					 size, 
					 getScaleFromSize (size, RenderSize), 
					 shouldCrop);
		}

		public WriteableBitmap GetImage (Color strokeColor, Color fillColor, float scale, bool shouldCrop = true)
		{
			return GetImage (strokeColor,
			                 fillColor, 
					 getSizeFromScale (scale, RenderSize), 
					 scale, 
					 shouldCrop);
		}

		WriteableBitmap GetImage (Color strokeColor, Color fillColor, Size size, float scale, bool shouldCrop = true)
		{
			if (size.Width == 0 || size.Height == 0 || scale <= 0 || strokeColor == null ||
			    fillColor == null)
				return null;

			float uncroppedScale;
			Size uncroppedSize;
			Rect croppedRectangle;

			if (shouldCrop && Points.Count () > 0) {
				croppedRectangle = getCroppedRectangle ();
				double scaleX = croppedRectangle.Width / size.Width;
				double scaleY = croppedRectangle.Height / size.Height;
				uncroppedScale = (float) (1 / Math.Max (scaleX, scaleY));
				uncroppedSize = getSizeFromScale (uncroppedScale, size);
			} else {
				uncroppedScale = scale;
				uncroppedSize = size;
			}

			InkPresenter presenter = new InkPresenter () { 
				Width = ActualWidth, 
				Height = ActualHeight, 
				Strokes = new StrokeCollection (),
 				Background = new SolidColorBrush (fillColor)
			};
			foreach (Stroke stroke in strokes) {
				stroke.DrawingAttributes.Color = strokeColor;
				presenter.Strokes.Add (stroke);
			}
			WriteableBitmap bitmap = new WriteableBitmap (presenter, new ScaleTransform () { ScaleX = uncroppedScale, ScaleY = uncroppedScale });
			
			if (shouldCrop) {
				croppedRectangle = getCroppedRectangle ();
				Rect scaledRectangle;
				scaledRectangle = new Rect (croppedRectangle.X * uncroppedScale,
				                            croppedRectangle.Y * uncroppedScale,
				                            size.Width,
				                            size.Height);
				if (scaledRectangle.X >= 5) {
					scaledRectangle.X -= 5;
					scaledRectangle.Width += 5;
				}
				if (scaledRectangle.Y >= 5) {
					scaledRectangle.Y -= 5;
					scaledRectangle.Height += 5;
				}
				if (scaledRectangle.X + scaledRectangle.Width <= uncroppedSize.Width - 5)
					scaledRectangle.Width += 5;
				if (scaledRectangle.Y + scaledRectangle.Height <= uncroppedSize.Height - 5)
					scaledRectangle.Height += 5;

				
				bitmap = crop (bitmap, scaledRectangle);
			}
			
			return bitmap;
		}
		#endregion

		WriteableBitmap crop (WriteableBitmap input, Rect croppedRectangle)
		{
			Image tempImage = new Image { Source = input, Width = input.PixelWidth, Height = input.PixelHeight };
			WriteableBitmap cropped = new WriteableBitmap ((int) croppedRectangle.Width,
			                                               (int) croppedRectangle.Height);
			cropped.Render (tempImage, new TranslateTransform { X = -croppedRectangle.X, Y = -croppedRectangle.Y });
			cropped.Invalidate ();
			return cropped;
		}

		Rect getCroppedRectangle ()
		{
			var xMin = Points.Where (point => point != new Point (-10000, -10000)).Min (point => point.X) - LineWidth / 2;
			var xMax = Points.Where (point => point != new Point (-10000, -10000)).Max (point => point.X) + LineWidth / 2;
			var yMin = Points.Where (point => point != new Point (-10000, -10000)).Min (point => point.Y) - LineWidth / 2;
			var yMax = Points.Where (point => point != new Point (-10000, -10000)).Max (point => point.Y) + LineWidth / 2;

			xMin = Math.Max (xMin, 0);
			xMax = Math.Min (xMax, ActualWidth);
			yMin = Math.Max (yMin, 0);
			yMax = Math.Min (yMax, ActualHeight);

			return new Rect (xMin, yMin, xMax - xMin, yMax - yMin);
		}

		float getScaleFromSize (Size size, Size original)
		{
			double scaleX = size.Width / original.Width;
			double scaleY = size.Height / original.Height;

			return (float) Math.Min (scaleX, scaleY);
		}

		Size getSizeFromScale (float scale, Size original)
		{
			double width = original.Width * scale;
			double height = original.Height * scale;

			return new Size (width, height);
		}

		/*
		 *Obtain a smoothed stroke with the specified granularity from the current stroke using Catmull-Rom spline.  
		 *Implemented using a modified version of the code in the solution at 
		 *http://stackoverflow.com/questions/8702696/drawing-smooth-curves-methods-needed.
		 *Also outputs a List of the points corresponding to the smoothed stroke.
		 */
		Stroke smoothedPathWithGranularity (int granularity, out List<Point> smoothedPoints)
		{
			List<Point> pointsArray = currentPoints;
			smoothedPoints = new List<Point> ();

			//Not enough points to smooth effectively, so return the original path and points.
			if (pointsArray.Count < 4) {
				smoothedPoints = pointsArray;
				return currentStroke;
			}

			//Create a new bezier path to hold the smoothed path.
			Stroke smoothedStroke = new Stroke ();
			smoothedStroke.DrawingAttributes.Color = strokeColor;
			smoothedStroke.DrawingAttributes.Width = lineWidth;
			smoothedStroke.DrawingAttributes.Height = lineWidth;

			//Duplicate the first and last points as control points.
			pointsArray.Insert (0, pointsArray [0]);
			pointsArray.Add (pointsArray [pointsArray.Count - 1]);

			//Add the first point
			smoothedStroke.StylusPoints.Add (GetPoint (pointsArray [0]));
			smoothedPoints.Add (pointsArray [0]);

			for (var index = 1; index < pointsArray.Count - 2; index++) {
				Point p0 = pointsArray [index - 1];
				Point p1 = pointsArray [index];
				Point p2 = pointsArray [index + 1];
				Point p3 = pointsArray [index + 2];

				//Add n points starting at p1 + dx/dy up until p2 using Catmull-Rom splines
				for (var i = 1; i < granularity; i++) {
					float t = (float) i * (1f / (float) granularity);
					float tt = t * t;
					float ttt = tt * t;

					//Intermediate point
					Point mid = new Point ();
					mid.X = 0.5f * (2f * p1.X + (p2.X - p0.X) * t +
						(2f * p0.X - 5f * p1.X + 4f * p2.X - p3.X) * tt +
						(3f * p1.X - p0.X - 3f * p2.X + p3.X) * ttt);
					mid.Y = 0.5f * (2 * p1.Y + (p2.Y - p0.Y) * t +
						(2 * p0.Y - 5 * p1.Y + 4 * p2.Y - p3.Y) * tt +
						(3 * p1.Y - p0.Y - 3 * p2.Y + p3.Y) * ttt);

					smoothedStroke.StylusPoints.Add (GetPoint (mid));
					smoothedPoints.Add (mid);
				}

				//Add p2
				smoothedStroke.StylusPoints.Add (GetPoint (p2));
				smoothedPoints.Add (p2);
			}

			//Add the last point
			smoothedStroke.StylusPoints.Add (GetPoint (pointsArray [pointsArray.Count - 1]));
			smoothedPoints.Add (pointsArray [pointsArray.Count - 1]);

			return smoothedStroke;
		}

		#region Touch Events
		protected void inkPresenter_OnMouseLeftButtonDown (object sender, MouseButtonEventArgs e)
		{
			currentPoints.Clear ();
			
			Point point = e.GetPosition (inkPresenter);
			btnClear.Visibility = Visibility.Visible;

			inkPresenter.CaptureMouse ();
			//Create a new stroke and set the attributes.
			currentStroke = new Stroke ();
			currentStroke.StylusPoints.Add (GetPoint (point));
			currentStroke.DrawingAttributes.Color = strokeColor;
			currentStroke.DrawingAttributes.Width = lineWidth;
			currentStroke.DrawingAttributes.Height = lineWidth;

			// Only add the point to the stroke if it is on the current view.
			if (point.X < 0 || point.Y < 0 || point.X > ActualWidth || point.Y > ActualHeight)
				return;

			currentPoints.Add (point);
			inkPresenter.Strokes.Add (currentStroke);
		}

		protected void inkPresenter_OnMouseMove (object sender, MouseEventArgs e)
		{
			Point point = e.GetPosition (inkPresenter);

			// Only add the point to the stroke if it is on the current view.
			if (point.X < 0 || point.Y < 0 || point.X > ActualWidth || point.Y > ActualHeight)
				return;

			if (currentStroke != null) {
				currentPoints.Add (point);
				currentStroke.StylusPoints.Add (GetPoint (point));
			}
		}

		protected void inkPresenter_OnMouseLeftButtonUp (object sender, MouseButtonEventArgs e)
		{
			Point point = e.GetPosition (inkPresenter);

			// Only add the point to the stroke if it is on the current view.
			if (currentStroke != null && point.X >= 0 && point.Y >= 0 && point.X <= ActualWidth && 
			    point.Y <= ActualHeight) {
				currentPoints.Add (point);	
				currentStroke.StylusPoints.Add (GetPoint (point));
			}

			//Clear the ink presenter and display an image of all strokes.
			inkPresenter.Strokes.Clear ();
			currentStroke = smoothedPathWithGranularity (40, out currentPoints);
			strokes.Add (currentStroke);
			points.Add (currentPoints.ToArray ());
			
			currentStroke = null;
			image.Source = GetImage (false);
		}
		#endregion

		//Get a StylusPoint from a Point
		public StylusPoint GetPoint (Point pos)
		{
			return new StylusPoint (pos.X, pos.Y);
		}

		//Allow the user to import an array of points to be used to draw a signature in the view, with new
		//lines indicated by a PointF.Empty in the array.
		public void LoadPoints (Point [] loadedPoints)
		{
			if (loadedPoints == null || loadedPoints.Count () == 0)
				return;

			var startIndex = 0;
			var emptyIndex = loadedPoints.ToList ().IndexOf (new Point (-10000, -10000));

			if (emptyIndex == -1)
				emptyIndex = loadedPoints.Count ();

			//Clear any existing paths or points.
			strokes = new List<Stroke> ();
			points = new List<Point []> ();

			do {
				//Create a new path and set the line options
				currentStroke = new Stroke ();
				currentStroke.DrawingAttributes.Color = strokeColor;
				currentStroke.DrawingAttributes.Width = lineWidth;
				currentStroke.DrawingAttributes.Height = lineWidth;

				currentPoints = new List<Point> ();

				//Move to the first point and add that point to the current_points array.
				currentStroke.StylusPoints.Add (GetPoint (loadedPoints [startIndex]));
				currentPoints.Add (loadedPoints [startIndex]);

				//Iterate through the array until an empty point (or the end of the array) is reached,
				//adding each point to the current_path and to the current_points array.
				for (var i = startIndex + 1; i < emptyIndex; i++) {
					currentStroke.StylusPoints.Add (GetPoint  (loadedPoints [i]));
					currentPoints.Add (loadedPoints [i]);
				}

				//Add the current_path and current_points list to their respective Lists before
				//starting on the next line to be drawn.
				strokes.Add (currentStroke);
				points.Add (currentPoints.ToArray ());

				//Obtain the indices for the next line to be drawn.
				startIndex = emptyIndex + 1;
				if (startIndex < loadedPoints.Count () - 1) {
					emptyIndex = loadedPoints.ToList ().IndexOf (new Point (-10000, -10000), startIndex);

					if (emptyIndex == -1)
						emptyIndex = loadedPoints.Count ();
				} else
					emptyIndex = startIndex;
			} while (startIndex < emptyIndex);

			//Obtain the image for the imported signature and display it in the image view.
			image.Source = GetImage (false);
			//Display the clear button.
			btnClear.Visibility = Visibility.Visible;
		}
	}
}
