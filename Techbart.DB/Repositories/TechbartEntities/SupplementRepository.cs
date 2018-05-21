using Techbart.DB.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Text;
using System.Data;
using System;

namespace Techbart.DB.Repositories
{
	public class SupplementRepository : ISupplementRepository
	{
		private readonly IDbConnection _context;

		public SupplementRepository()
		{
			_context = Bakery.Sql();
		}

		public bool DeleteSupplement(int supplementid) =>
				_context.Execute(@"
                    DELETE FROM Supplements
                    WHERE
                        SupplementId = @supplementid
                ", new
				{
					supplementid
				}) != 0;

		public ISupplement GetSupplement(int supplementid) =>
				_context.Query<Supplement>(@"
                    SELECT
                        SupplementId
                        ,SupplementName
                        ,SupplementDescription
                        ,SupplementPrice
                        ,SupplementWeight
                    FROM
                        Supplements
                    WHERE
                        SupplementId = @supplementid
                ", new
				{
					supplementid
				}).FirstOrDefault();

		public IList<Supplement> GetSupplements() =>
				_context.Query<Supplement>(@"
                    SELECT
                        SupplementId
                        ,SupplementName
                        ,SupplementDescription
                        ,SupplementPrice
                        ,SupplementWeight
                    FROM
                        Supplements
                ").ToList();

		public bool InsertSupplement(ISupplement supplement)
		{
			supplement.SupplementId = GetIdForNextSupplement();

			if (supplement.SupplementId == 0)
			{
				supplement.SupplementId++;
			}

			return _context.Execute(@"
                    INSERT
                        Supplements(SupplementId, SupplementName, SupplementDescription, SupplementPrice, SupplementWeight)
                    VALUES
                        (@supplementid, @supplementname, @supplementdescription, @supplementprice, @supplementweight)
                ", new
			{
				supplementid = supplement.SupplementId,
				supplementname = supplement.SupplementName,
				supplementdescription = supplement.SupplementDescription,
				supplementprice = supplement.SupplementPrice,
				supplementweight = supplement.SupplementWeight
			}) != 0;
		}

		public bool UpdateSupplement(ISupplement updateSupplement) =>
				_context.Execute(@"
                    UPDATE
                        Supplements
                    SET
                        SupplementName         = @supplementname
                        ,SupplementDescription = @supplementdescription
                        ,SupplementPrice       = @supplementprice
                        ,SupplementWeight      = @supplementweight
                    WHERE
                        SupplementId           = @supplementid
                ", new
				{
					supplementid = updateSupplement.SupplementId,
					supplementname = updateSupplement.SupplementName,
					supplementdescription = updateSupplement.SupplementDescription,
					supplementprice = updateSupplement.SupplementPrice,
					supplementweight = updateSupplement.SupplementWeight
				}) != 0;

		private string CreateQuery(ISupplement suppement)
		{
			var query = new StringBuilder();

			if (suppement.SupplementId != 0)
			{
				query.Append($"WHERE SupplementId={suppement.SupplementId}");
			}

			if (suppement.SupplementName != null && !suppement.SupplementName.Equals(string.Empty))
			{
				if (query.Length == 0)
				{
					query.Append("WHERE ");
				}
				else
				{
					query.Append(" AND ");
				}

				query.Append($"SupplementName LIKE N'%{suppement.SupplementName}%'");
			}

			if (suppement.SupplementWeight != 0)
			{
				if (query.Length == 0)
				{
					query.Append("WHERE ");
				}
				else
				{
					query.Append(" AND ");
				}

				query.Append($"SupplementWeight ={suppement.SupplementWeight}");
			}

			if (suppement.SupplementPrice != 0)
			{
				if (query.Length == 0)
				{
					query.Append("WHERE ");
				}
				else
				{
					query.Append(" AND ");
				}

				query.Append($"SupplementPrice={suppement.SupplementPrice}");
			}

			return query.ToString();
		}

		public IList<Supplement> GetSupplements(SearchSupplementModel searchSupplement)
		{
			if (!searchSupplement.Validate())
			{
				throw new ArgumentException("SearchSupplementModel didn't pass validation");
			}

			return _context.Query<Supplement>($@"
                    SELECT
                        SupplementId
                        ,SupplementName
                        ,SupplementDescription
                        ,SupplementPrice
                        ,SupplementWeight
                    FROM
                        Supplements
                    {CreateQuery(searchSupplement)}
                   ORDER BY {searchSupplement.OrderBy}{(searchSupplement.IsDesc ? " DESC" : string.Empty)}
                    OFFSET @skip ROWS
                    FETCH NEXT @take ROWS ONLY
                ", new
			{
				skip = searchSupplement.Skip,
				take = searchSupplement.Take
			}
				).ToList();
		}

		public int Count(ISupplement searchSupplement)
		{
			string query = string.Empty;

			if (searchSupplement != null)
			{
				query = CreateQuery(searchSupplement);
			}

			return _context.ExecuteScalar<int>(@"
                    SELECT COUNT(SupplementId)       
                    FROM 
                        Supplements
                    " + query);
		}

		public int Count() =>
				_context.ExecuteScalar<int>(@"
                    SELECT COUNT(SupplementId)       
                    FROM 
                        Supplements");

		private int GetIdForNextSupplement()
		{
			var supplementID = Count();

			while (IsExists(supplementID))
			{
				supplementID++;
			}
			return supplementID;
		}

		public bool IsExists(int supplementid)=>
			_context.ExecuteScalar<int>(@"
                SELECT COUNT(SupplementId)
                FROM
                    Supplements
                WHERE
                    SupplementId = @supplementid
                ", new
				{
					supplementid
				}) != 0;

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}