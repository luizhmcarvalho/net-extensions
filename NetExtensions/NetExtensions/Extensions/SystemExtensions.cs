#region References

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

#endregion

namespace System
{
	/// <summary>
	///     System extenions
	/// </summary>
	public static class SystemExtensions
	{
		#region	Stream

		public static void Compress(this byte[] stream, string filePath)
		{
			using (var originalFileStream = new MemoryStream(stream))
			{
				{
					using (var compressedFileStream = File.Create(filePath))
					{
						using (var compressionStream = new GZipStream(compressedFileStream, CompressionLevel.Optimal))
						{
							originalFileStream.CopyTo(compressionStream);
						}
					}
				}
			}
		}

		#endregion

		#region DateTime

		public static DateTime FirstDayOfTheMonth(this DateTime pDate)
		{
			return new DateTime(pDate.Year, pDate.Month, 1);
		}

		public static DateTime LastDayOfTheMonth(this DateTime pDate)
		{
			return pDate.FirstDayOfTheMonth().AddMonths(1).AddDays(-1);
		}

		public static bool IsBetween(this DateTime pDate, DateTime pStart, DateTime pEnd)
		{
			return pDate.Date >= pStart.Date && pDate.Date <= pEnd.Date;
		}

		#endregion

		#region IQueryable

		public static IQueryable<T> OrderByField<T>(this IQueryable<T> q, string SortField, bool Ascending)
		{
			var param = Expression.Parameter(typeof(T), "p");
			var prop = Expression.Property(param, SortField);
			var exp = Expression.Lambda(prop, param);
			var method = Ascending ? "OrderBy" : "OrderByDescending";
			Type[] types = { q.ElementType, exp.Body.Type };
			var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
			return q.Provider.CreateQuery<T>(mce);
		}

		public static IQueryable<T> ThenByField<T>(this IQueryable<T> q, string SortField, bool Ascending)
		{
			var param = Expression.Parameter(typeof(T), "p");
			var prop = Expression.Property(param, SortField);
			var exp = Expression.Lambda(prop, param);
			var method = Ascending ? "ThenBy" : "ThenByDescending";
			Type[] types = { q.ElementType, exp.Body.Type };
			var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
			return q.Provider.CreateQuery<T>(mce);
		}

		#endregion

		#region Property

		public static string GetPropertyName<TObject>(this TObject type,
			Expression<Func<TObject, object>> propertyRefExpr)
		{
			return GetPropertyNameCore(propertyRefExpr.Body);
		}

		public static string GetPropertyNames<TObject>(this TObject type,
			params Expression<Func<TObject, object>>[] propertyRefExpr)
		{
			return GetPropertyNameCore(propertyRefExpr[0].Body);
		}

		public static string GetName<TObject>(Expression<Func<TObject, object>> propertyRefExpr)
		{
			return GetPropertyNameCore(propertyRefExpr.Body);
		}

		public static string GetPropertyName(Expression pExpression)
		{
			return GetPropertyNameCore(pExpression);
		}

		private static string GetPropertyNameCore(Expression propertyRefExpr)
		{
			if (propertyRefExpr == null)
				throw new ArgumentNullException("propertyRefExpr", "propertyRefExpr is null.");

			var memberExpr = propertyRefExpr as MemberExpression;
			if (memberExpr == null)
			{
				var unaryExpr = propertyRefExpr as UnaryExpression;
				if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
					memberExpr = unaryExpr.Operand as MemberExpression;
			}

			if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
				return memberExpr.Member.Name;

			throw new ArgumentException("No property reference expression was found.",
				"propertyRefExpr");
		}

		public static string GetDisplayName<TObject>(Expression<Func<TObject, object>> propertyRefExpr)
		{
			if (propertyRefExpr == null)
				throw new ArgumentNullException("propertyRefExpr", "propertyRefExpr is null.");

			var memberExpr = propertyRefExpr.Body as MemberExpression;
			if (memberExpr == null)
			{
				var unaryExpr = propertyRefExpr.Body as UnaryExpression;
				if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
					memberExpr = unaryExpr.Operand as MemberExpression;
			}

			if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
			{
				var att = memberExpr.Member.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() as DisplayNameAttribute;
				return att.DisplayName;
			}

			return null;
		}

		public static string GetDisplayName<T>(this T obj)
		{
			var displayName = obj.GetType().GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() as DisplayNameAttribute;

			if (displayName != null)
				return displayName.DisplayName;

			return null;
		}

		#endregion

		#region Crypt
		public static string GetHash(string input)
		{
			HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

			byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

			byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

			return Convert.ToBase64String(byteHash);
		}
		#endregion
	}
}