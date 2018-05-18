namespace Techbart.DB.Interfaces
{
	public interface IPage
    {
		string OrderBy { get; set; }

		int Rows { get; set; }

		int Skip { get; set; }

		int Take { get; set; }

		int Count { get; set; }

		bool IsDesc { get; set; }
	}
}
