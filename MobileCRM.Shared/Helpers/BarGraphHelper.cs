using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileCRM.Shared.Models;


namespace MobileCRM.Shared.Helpers
{
		public class BarGraphHelper
		{
				private List<WeeklySalesData> listSales;
				private List<CategorySalesData> listCategory;
				private IEnumerable<Order> orders;
				private bool bolIsOpen;


				public BarGraphHelper(IEnumerable<Order> Orders, bool IsOpen)
				{
						listSales = new List<WeeklySalesData>();
						listCategory = new List<CategorySalesData>();

						orders = Orders;
						bolIsOpen = IsOpen;

						this.ProcessDates();
						this.ProcessCategories();
				} //end ctor


				public List<WeeklySalesData> SalesData
				{
						get
						{
								return listSales;
						}
				}

				public List<CategorySalesData> CategoryData
				{
						get
						{
								return listCategory;
						}
				}

				private void ProcessCategories()
				{
						foreach(string s in Order.ItemTypes)
						{
								double dblAmt = this.ProcessCategoryOrders(s);
								listCategory.Add(new CategorySalesData() { Category = s, Amount = dblAmt });
						}
				}

				private double ProcessCategoryOrders(string category)
				{
						double dblTotal = 0;

						var results = from o in orders
													where o.IsOpen == bolIsOpen
													&& o.Item == category
													select o;

						foreach (var order in results)
						{
								dblTotal = dblTotal + order.Price;
						}

						return dblTotal;
				}


				//Aggregate sales for prior 6 weeks
				private void ProcessDates()
				{
						DateTime dateStart = DateTime.Today;

						DateTime dateWkStart = dateStart.Subtract(new TimeSpan(dateStart.DayOfWeek.GetHashCode(), 0, 0, 0));
						DateTime dateWkEnd = dateWkStart.AddDays(6);

						double dblAmt = this.ProcessWeekOrders(dateWkStart, dateWkEnd);
						listSales.Add(new WeeklySalesData() { DateStart = dateWkStart, DateEnd = dateWkEnd, Amount = dblAmt });

						for (int i = 1; i < 6; i++)
						{
								dateWkStart = dateWkStart.AddDays(-7);
								dateWkEnd = dateWkStart.AddDays(6);
								dblAmt = this.ProcessWeekOrders(dateWkStart, dateWkEnd);
								listSales.Add(new WeeklySalesData() { DateStart = dateWkStart, DateEnd = dateWkEnd, Amount = dblAmt });
						} //end for

				} //end Processdates


				private double ProcessWeekOrders(DateTime dateStart, DateTime dateEnd)
				{
						double dblTotal = 0;

						var results = from o in orders
													where o.IsOpen == bolIsOpen
                          && o.ClosedDate >= dateStart
													&& o.ClosedDate <= dateEnd
													select o;

						foreach (var order in results)
						{
								dblTotal = dblTotal + order.Price;
						}

						return dblTotal;
				} //end ProcessWeekOrders


		} //end class


		public class CategorySalesData
		{
					private string category;

					public string Category
					{
						get { return category;}
						set { category = value;}
					}

					
				private double amount;

					public double Amount
					{
						get { return amount;}
						set { amount = value;}
					}
	

		}

		public class WeeklySalesData
		{

				public WeeklySalesData()
				{
						dateStart = DateTime.MinValue;
						dateEnd = DateTime.MaxValue;
						dblAmt = 0;
				} //end ctor


				private DateTime dateStart;

				public DateTime DateStart
				{
						get { return dateStart; }
						set { dateStart = value; }
				}


				private DateTime dateEnd;

				public DateTime DateEnd
				{
						get { return dateEnd; }
						set { dateEnd = value; }
				}


				private double dblAmt;

				public double Amount
				{
						get { return dblAmt; }
						set { dblAmt = value; }
				}


				public string DateStartString
				{
						get
						{
								return dateStart.ToString("M/dd");
						}
				}


				public string DateEndString
				{
						get
						{
								return dateEnd.ToString("M/dd");
						}
				}


		} //end class

} //end ns
