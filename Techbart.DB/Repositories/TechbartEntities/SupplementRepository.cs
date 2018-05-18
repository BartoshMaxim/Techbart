using Techbart.DB.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Text;

namespace Techbart.DB.Repositories
{
    public class SupplementRepository : ISupplementRepository
    {
        public bool DeleteSupplement(int supplementid)
        {
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    DELETE FROM Supplements
                    WHERE
                        SupplementId = @supplementid
                ", new
                {
                    supplementid
                }) != 0;
            }
        }

        public ISupplement GetSupplement(int supplementid)
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<Supplement>(@"
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
            }
        }

        public IList<Supplement> GetSupplements()
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<Supplement>(@"
                    SELECT
                        SupplementId
                        ,SupplementName
                        ,SupplementDescription
                        ,SupplementPrice
                        ,SupplementWeight
                    FROM
                        Supplements
                ").ToList();
            }
        }

        public bool InsertSupplement(ISupplement supplement)
        {
            supplement.SupplementId = GetIdForNextSupplement();

            if (supplement.SupplementId == 0)
            {
                supplement.SupplementId++;
            }

            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
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
        }

        public bool UpdateSupplement(ISupplement updateSupplement)
        {
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
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
            }
        }

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

            if (suppement.SupplementWeight != 0 )
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

        public IList<Supplement> GetSupplements(int from, int to, ISupplement searchSupplement)
        {
            var query = string.Empty;
            if (searchSupplement != null)
            {
                query = CreateQuery(searchSupplement);
            }

            to = to - from;

            using (var context = Bakery.Sql())
            {
                return context.Query<Supplement>($@"
                    SELECT
                        SupplementId
                        ,SupplementName
                        ,SupplementDescription
                        ,SupplementPrice
                        ,SupplementWeight
                    FROM
                        Supplements
                    {query}
                    ORDER BY SupplementId DESC
                    OFFSET @from ROWS
                    FETCH NEXT @to ROWS ONLY
                ", new
                {
                    from,
                    to
                }
                ).ToList();
            }
        }

        public int GetCountRows(ISupplement searchSupplement)
        {
            string query = string.Empty;

            if (searchSupplement != null)
            {
                query = CreateQuery(searchSupplement);
            }

            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                    SELECT COUNT(SupplementId)       
                    FROM 
                        Supplements
                    " + query);
            }
        }

        public int GetCountRows()
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                    SELECT COUNT(SupplementId)       
                    FROM 
                        Supplements");
            }
        }

        private int GetIdForNextSupplement()
        {
            var supplementID = GetCountRows();

            while (IsExists(supplementID))
            {
                supplementID++;
            }
            return supplementID;
        }

        public bool IsExists(int supplementid)
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                SELECT COUNT(SupplementId)
                FROM
                    Supplements
                WHERE
                    SupplementId = @supplementid
                ", new
                {
                    supplementid
                }) != 0;
            }
        }
    }
}