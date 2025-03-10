using Shop.Data.Enums;
using System;
using System.Collections.Generic;

namespace Shop.Web.Models.Order
{
    public class OrderArchiveModel
	{
		public DateTime? MinDate { get; set; }
		public DateTime? MaxDate { get; set; }
		public decimal? MinPrice { get; set; }
		public decimal? MaxPrice { get; set; }
		public OrderBy OrderBy { get; set; }
		public int Page { get; set; }
		public int PageCount { get; set; }
		public string ZipCode { get; set; }
		public string UserId { get; set; }
		public IEnumerable<OrderIndexModel> Orders { get; set; }
	}
}