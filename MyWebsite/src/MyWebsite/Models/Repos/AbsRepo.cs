using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using MyWebsite.Models.Enitites;

namespace MyWebsite.Models.Repos {

	public interface IRepo<T> : IAbsRepo<T> where T : AbsEntity { }

	public interface IAbsRepo<T> where T : AbsEntity {
		IEnumerable<T> GetItems();
		T GetItem(int itemId);
		T AddItem(T item);
		bool DeleteItem(int itemId);
	}

	public class AbsRepo<T> : IAbsRepo<T> where T : AbsEntity {

		private readonly DbContext _db;
		private readonly string _table;

		public AbsRepo(DbContext db) {
			_db = db;
			// ReSharper disable once PossibleNullReferenceException
			_table = _db.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(DbSet<T>)).Name;

		}

		public IEnumerable<T> GetItems() {
			return GetDbSet().AsEnumerable();
		}

		public T GetItem(int itemId) {
			var type =
				// ReSharper disable once PossibleNullReferenceException
				typeof(T).GetProperties()
					.FirstOrDefault(p => p.GetCustomAttributes(typeof(T), false).Any(a => ((DisplayAttribute)a).Name.Equals("Key"))).Name;

			return GetDbSet().FirstOrDefault(t => (int)t.GetType().GetProperty(type).GetValue(t, null) == itemId);
		}

		public T AddItem(T item) {
			GetDbSet().Add(item);

			return _db.SaveChanges() > 0 ? item : null;
		}

		public bool DeleteItem(int itemId) {
			var item = GetItem(itemId);
			if (item == null) return false;
			GetDbSet().Remove(item);
			return _db.SaveChanges() > 0;
		}

		private DbSet<T> GetDbSet() {
			return (DbSet<T>)_db.GetType().GetProperty(_table).GetValue(_db, null);
		}
	}
}
