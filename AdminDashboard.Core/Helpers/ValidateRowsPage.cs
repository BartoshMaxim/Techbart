using Techbart.DB.Interfaces;

namespace AdminDashboard.Core.Helpers
{
	public class ValidateRowsPage
	{
		/// <summary>
		/// Contains number of rows and number of current page
		/// </summary>
		private IPage _page;

		/// <summary>
		/// Entities Count
		/// </summary>
		private int _entitiesCount;

		public ValidateRowsPage(IPage page, int entitiesCount)
		{
			_page = page;
			_entitiesCount = entitiesCount;
		}

		/// <summary>
		/// Validate number of rows and number of current page
		/// </summary>
		/// <returns>Entities count</returns>
		public int ValidateGetPageCount()
		{
			var maxCountOfRows = 100;

			if (_page.Rows <= 0)
			{
				_page.Rows = 1;
			}
			else if (_page.Rows > maxCountOfRows)
			{
				_page.Rows = maxCountOfRows;
			}

			var pagesCount = _entitiesCount / _page.Rows;

			if (_entitiesCount % _page.Rows != 0)
			{
				pagesCount++;
			}

			//if (_page.Page <= 0)
			//{
			//	_page.Page = 1;
			//}
			//else if (_page.Page > pagesCount)
			//{
			//	_page.Page = pagesCount;
			//}

			return pagesCount;
		}
	}
}
