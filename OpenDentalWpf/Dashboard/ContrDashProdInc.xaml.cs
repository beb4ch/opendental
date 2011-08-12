﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OpenDentalWpf {
	/// <summary></summary>
	public partial class ContrDashProdInc:UserControl {
		/// <summary>Path 0 is prod, and path 1 is inc.</summary>
		private List<Path> ListPaths;
		/// <summary>0 is prod, and 1 is inc.  Each item contains a list of dots.</summary>
		private List<List<Ellipse>> ListDots;
		/// <summary>0 is prod, and 1 is inc.  Each item contains a list of dots that are used for hover effects, not visible.</summary>
		private List<List<Ellipse>> ListDotsBig;
		private bool IsHovering;
		/// <summary>0 is prod, and 1 is inc.  Each item contains a list of amounts. This is the data that will be plotted.</summary>
		private List<List<Decimal>> ListData;
		/// <summary>Each path gets a different color.</summary>
		private List<Color> ListColors;
		private DateTime DateStart;

		public ContrDashProdInc() {
			InitializeComponent();
		}

		public void FillData() {
			GetData();
			FillGraph();
		}

		private void GetData() {
			//simulated for now
			//colors-------------------------------------------------------------------------------
			ListColors=new List<Color>();
			ListColors.Add(Colors.MediumBlue);//production
			ListColors.Add(Colors.Green);//income
			//data---------------------------------------------------------------------------------
			ListData=new List<List<decimal>>();
			//production
			List<decimal> listDecimal=new List<decimal>();
			listDecimal.Add(68000);//12 months ago
			listDecimal.Add(72000);
			listDecimal.Add(60000);
			listDecimal.Add(56000);
			listDecimal.Add(61000);
			listDecimal.Add(68000);
			listDecimal.Add(71000);
			listDecimal.Add(64000);
			listDecimal.Add(69000);
			listDecimal.Add(70000);
			listDecimal.Add(63000);
			listDecimal.Add(76000);
			ListData.Add(listDecimal);
			//income
			listDecimal=new List<decimal>();
			listDecimal.Add(62000);
			listDecimal.Add(67000);
			listDecimal.Add(66000);
			listDecimal.Add(62000);
			listDecimal.Add(53000);
			listDecimal.Add(63000);
			listDecimal.Add(67000);
			listDecimal.Add(70000);
			listDecimal.Add(60000);
			listDecimal.Add(68000);
			listDecimal.Add(68000);
			listDecimal.Add(64000);
			ListData.Add(listDecimal);
		}

		private void FillGraph() {
			DateTime dateFirstThisMonth=new DateTime(DateTime.Today.Year,DateTime.Today.Month,1);
			DateTime dateEnd=dateFirstThisMonth.AddDays(-1);
			DateStart=dateFirstThisMonth.AddMonths(-12);
			double wCol=rectMain.Width/11d;
			//vertical lines----------------------------------------------------------------------
			for(double i=1;i<11;i++) {
				Line line=new Line();
				line.X1=rectMain.Left()+(i*wCol);
				line.Y1=rectMain.Top();
				line.X2=rectMain.Left()+(i*wCol);
				line.Y2=rectMain.Bottom();
				line.Stroke=Brushes.LightGray;
				line.StrokeThickness=1;
				canvasMain.Children.Add(line);
				//year marker
				if(DateStart.AddMonths((int)i).Month==1) {
					line=new Line();
					line.X1=rectMain.Left()+(i*wCol)-wCol/2d;
					line.Y1=rectMain.Top();
					line.X2=rectMain.Left()+(i*wCol)-wCol/2d;
					line.Y2=rectMain.Bottom();
					line.Stroke=Brushes.Black;
					line.StrokeThickness=1.5;
					Canvas.SetZIndex(line,5);//same as grid outline, causing horizontal lines to go under
					canvasMain.Children.Add(line);
				}
			}
			//x axis numbers-----------------------------------------------------------------------
			for(double i=0;i<12;i++) {
				Label label=new Label();
				string content=DateStart.AddMonths((int)i).ToString("%M");
				label.Content=content;
				label.MaxWidth=100;
				Canvas.SetTop(label,rectMain.Bottom()-4);
				Typeface typeface=new Typeface(FontFamily,FontStyle,FontWeight,FontStretch);
				FormattedText ft=new FormattedText(content,CultureInfo.CurrentCulture,FlowDirection.LeftToRight,typeface,FontSize,Foreground);
				double wText=ft.Width;
				//Debug.WriteLine(content+": "+wText.ToString("F0"));
				Canvas.SetLeft(label,rectMain.Left()+(i*wCol)-wText/2d-5);
				canvasMain.Children.Add(label);
			}
			//calculate max y and increments---------------------------------------------------------
			decimal maxVal=0;
			for(int p=0;p<ListData.Count;p++) {
				for(int i=0;i<ListData[p].Count;i++) {
					if(ListData[p][i]>maxVal) {
						maxVal=ListData[p][i];
					}
				}
			}
			//round up to nearest 10k
			decimal remainder=(Decimal)Math.IEEERemainder((double)maxVal,10000);
			maxVal=maxVal-remainder+10000;
			int yCount=(int)(maxVal/10000);
			double hRow=rectMain.Height/(yCount-1);
			//horizontal lines----------------------------------------------------------------------
			for(double i=1;i<yCount-1;i++) {
				Line line=new Line();
				line.X1=rectMain.Left();
				line.Y1=rectMain.Bottom()-(i*hRow);
				line.X2=rectMain.Right();
				line.Y2=rectMain.Bottom()-(i*hRow);
				line.Stroke=Brushes.LightGray;
				line.StrokeThickness=1;
				canvasMain.Children.Add(line);
			}
			//y axis numbers-----------------------------------------------------------------------
			for(double i=1;i<yCount;i++) {
				Label label=new Label();
				string content=i.ToString()+"0k";
				label.Content=content;
				label.MaxWidth=100;
				Canvas.SetTop(label,rectMain.Bottom()-(i*hRow)-14);
				label.Width=rectMain.Left();
				label.HorizontalContentAlignment=HorizontalAlignment.Right;
				canvasMain.Children.Add(label);
			}
			//Initialize-----------------------------------------------------------------------------
			ListPaths=new List<Path>();
			ListDots=new List<List<Ellipse>>();
			ListDotsBig=new List<List<Ellipse>>();
			//Paths and dots-------------------------------------------------------------------------
			for(int p=0;p<ListData.Count;p++) {
				PathFigure pathFig=new PathFigure();
				List<Ellipse> listDotsOneType=new List<Ellipse>();
				List<Ellipse> listDotsBigOneType=new List<Ellipse>();
				for(int i=0;i<ListData[p].Count;i++) {
					Point pt=new Point(rectMain.Left()+(i*wCol),rectMain.Bottom()-(double)(ListData[p][i]/10000*(decimal)hRow));
					if(i==0) {
						pt.X+=1;
					}
					if(i==ListData[p].Count-1) {
						pt.X-=1;
					}
					if(i==0) {
						pathFig.StartPoint=pt;
					}
					else {
						LineSegment lineSeg=new LineSegment();
						lineSeg.Point=pt;
						pathFig.Segments.Add(lineSeg);
					}
					//dots
					Ellipse ellipse=new Ellipse();
					ellipse.Height=4;
					ellipse.Width=4;
					Canvas.SetLeft(ellipse,pt.X-2);
					Canvas.SetTop(ellipse,pt.Y-2);
					ellipse.Fill=new SolidColorBrush(ListColors[p]);
					Panel.SetZIndex(ellipse,6);
					canvasMain.Children.Add(ellipse);
					listDotsOneType.Add(ellipse);
					//dotsBig
					ellipse=new Ellipse();
					ellipse.Height=14;
					ellipse.Width=14;
					Canvas.SetLeft(ellipse,pt.X-7);
					Canvas.SetTop(ellipse,pt.Y-7);
					ellipse.Opacity=0;
					ellipse.Fill=Brushes.Red;
					Panel.SetZIndex(ellipse,7);//in front of everything
					ellipse.MouseEnter+=new MouseEventHandler(dotBig_MouseEnter);
					ellipse.MouseLeave+=new MouseEventHandler(dotBig_MouseLeave);
					canvasMain.Children.Add(ellipse);
					listDotsBigOneType.Add(ellipse);
				}
				PathGeometry pathGeo=new PathGeometry();
				pathGeo.Figures.Add(pathFig);
				Path path=new Path();
				path.Data=pathGeo;
				path.Stroke=new SolidColorBrush(ListColors[p]);
				path.StrokeThickness=1.5;
				Panel.SetZIndex(path,6);//in front of grid
				canvasMain.Children.Add(path);
				ListPaths.Add(path);
				ListDots.Add(listDotsOneType);
				ListDotsBig.Add(listDotsBigOneType);
			}
		}

		void dotBig_MouseEnter(object sender,MouseEventArgs e) {
			if(IsHovering) {
				return;//don't jump when two big dots overlap
			}
			int idxPath=-1;
			int idxDot=-1;
			for(int i=0;i<ListDotsBig.Count;i++) {
				if(i==0 && checkProduction.IsChecked!=true) {
					continue;
				}
				if(i==1 && checkIncome.IsChecked!=true) {
					continue;
				}
				if(ListDotsBig[i].IndexOf((Ellipse)sender)==-1) {
					continue;
				}
				idxPath=i;
				idxDot=ListDotsBig[i].IndexOf((Ellipse)sender);
			}
			if(idxDot==-1) {
				return;
			}
			labelHover.Opacity=1;
			string content=DateStart.AddMonths(idxDot).ToString("MMM")+": "
				+ListData[idxPath][idxDot].ToString("c0");
			labelHover.Content=content;
			double xCenter=Canvas.GetLeft((Ellipse)sender)+7;
			double yCenter=Canvas.GetTop((Ellipse)sender)+7;
			Typeface typeface=new Typeface(FontFamily,FontStyle,FontWeight,FontStretch);
			FormattedText ft=new FormattedText(labelHover.Content.ToString(),CultureInfo.CurrentCulture,FlowDirection.LeftToRight,typeface,FontSize,Foreground);
			double wText=ft.Width;
			Canvas.SetLeft(labelHover,xCenter-wText/2d-1);
			Canvas.SetTop(labelHover,yCenter-23);
			Panel.SetZIndex(labelHover,8);//bring to front of other element
			IsHovering=true;
		}

		void dotBig_MouseLeave(object sender,MouseEventArgs e) {
			if(!IsHovering) {
				return;
			}
			IsHovering=false;
			labelHover.Opacity=0;
			Panel.SetZIndex(labelHover,0);//send it to the back so it won't interfere with mouse hover
		}

		private void checkProduction_Checked(object sender,RoutedEventArgs e) {
			if(ListPaths==null) {
				return;
			}
			DoubleAnimation animation=new DoubleAnimation();
			animation.From=0;
			animation.To=1;
			animation.Duration=TimeSpan.FromMilliseconds(200);
			animation.FillBehavior=FillBehavior.HoldEnd;
			ListPaths[0].BeginAnimation(OpacityProperty,animation);
			for(int i=0;i<ListDots[0].Count;i++) {
				ListDots[0][i].BeginAnimation(OpacityProperty,animation);
			}
		}

		private void checkProduction_Unchecked(object sender,RoutedEventArgs e) {
			if(ListPaths==null) {
				return;
			}
			DoubleAnimation animation=new DoubleAnimation();
			animation.From=1;
			animation.To=0;
			animation.Duration=TimeSpan.FromMilliseconds(200);
			animation.FillBehavior=FillBehavior.HoldEnd;
			ListPaths[0].BeginAnimation(OpacityProperty,animation);
			for(int i=0;i<ListDots[0].Count;i++) {
				ListDots[0][i].BeginAnimation(OpacityProperty,animation);
			}
		}

		private void checkIncome_Checked(object sender,RoutedEventArgs e) {
			if(ListPaths==null) {
				return;
			}
			DoubleAnimation animation=new DoubleAnimation();
			animation.From=0;
			animation.To=1;
			animation.Duration=TimeSpan.FromMilliseconds(200);
			animation.FillBehavior=FillBehavior.HoldEnd;
			ListPaths[1].BeginAnimation(OpacityProperty,animation);
			for(int i=0;i<ListDots[0].Count;i++) {
				ListDots[1][i].BeginAnimation(OpacityProperty,animation);
			}
		}

		private void checkIncome_Unchecked(object sender,RoutedEventArgs e) {
			if(ListPaths==null) {
				return;
			}
			DoubleAnimation animation=new DoubleAnimation();
			animation.From=1;
			animation.To=0;
			animation.Duration=TimeSpan.FromMilliseconds(200);
			animation.FillBehavior=FillBehavior.HoldEnd;
			ListPaths[1].BeginAnimation(OpacityProperty,animation);
			for(int i=0;i<ListDots[0].Count;i++) {
				ListDots[1][i].BeginAnimation(OpacityProperty,animation);
			}
		}



	}
}