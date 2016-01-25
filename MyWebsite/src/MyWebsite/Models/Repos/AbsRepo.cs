﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using MyWebsite.Models.Enitites;

namespace MyWebsite.Models.Repos {

	public interface IRepo<T> : IAbsRepo<T> where T : AbsEntity { }

	public interface IAbsRepo<T> where T : AbsEntity {
		IEnumerable<T> GetItems();
		IEnumerable<T> GetItemsWithInclude<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath);
		T GetItem(int itemId);
		T AddItem(T item);
		bool DeleteItem(int itemId);
        T UpdateItem(int itemId, T item);
        AbsDbContext GetContext();
        DbSet<T> GetDbSet();
    }

	public class AbsRepo<T> : IAbsRepo<T> where T : AbsEntity {

		private readonly AbsDbContext _db;
		private readonly string _table;

		public AbsRepo(AbsDbContext db) {
			_db = db;
			// ReSharper disable once PossibleNullReferenceException
			_table = _db.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(DbSet<T>)).Name;

		}

		public IEnumerable<T> GetItems() {
			return GetDbSet().AsEnumerable();
		}

		public IEnumerable<T> GetItemsWithInclude<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath) {
			//var type = typeof(T).GetProperties().FirstOrDefault(p => p.PropertyType == typeof(TProperty)).Name;
			return GetDbSet().Include(navigationPropertyPath).AsEnumerable();
		}

		public T GetItem(int itemId) {
			// ReSharper disable once PossibleNullReferenceException
			var type = typeof(T).GetProperties().FirstOrDefault(prop => prop.CustomAttributes.Any(attr => attr.AttributeType.Name.Equals("KeyAttribute"))).Name;

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

		public T UpdateItem(int itemId, T item) {
            DeleteItem(itemId);
            return AddItem(item);
        }

		public AbsDbContext GetContext() {
            return _db;
        }

		public DbSet<T> GetDbSet() {
			return (DbSet<T>)_db.GetType().GetProperty(_table).GetValue(_db, null);
		}
	}
}
