﻿using Techbart.DB.Interfaces;

namespace Techbart.DB
{
	public class SearchImageModel : IImage, IPage
    {
        public int Rows { get; set; }

        public int Page { get; set; }

        public int ImageId { get; set; }

        public string ImageName { get; set; }

        public string ImagePath { get; set; }

		public string OrderBy { get; set; }

		public int Skip { get; set; }

		public int Take { get; set; }

		public int Count { get; set; }

		public bool IsDesc { get; set; }

		public bool Validate()
		{
			return !string.IsNullOrEmpty(OrderBy) && Skip >= 0 && Take > 1;
		}
	}
}