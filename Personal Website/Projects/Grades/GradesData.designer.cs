﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Personal_Website.Projects.Grades
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="GradesDB")]
	public partial class GradesDataDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertSimpleGrade(SimpleGrade instance);
    partial void UpdateSimpleGrade(SimpleGrade instance);
    partial void DeleteSimpleGrade(SimpleGrade instance);
    #endregion
		
		public GradesDataDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["GradesDBConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public GradesDataDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public GradesDataDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public GradesDataDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public GradesDataDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<SimpleGrade> SimpleGrades
		{
			get
			{
				return this.GetTable<SimpleGrade>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SimpleGrades")]
	public partial class SimpleGrade : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private char _term;
		
		private int _year;
		
		private string _title;
		
		private string _courseID;
		
		private double _gradePercent;
		
		private string _gradeLetter;
		
		private string _status;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OntermChanging(char value);
    partial void OntermChanged();
    partial void OnyearChanging(int value);
    partial void OnyearChanged();
    partial void OntitleChanging(string value);
    partial void OntitleChanged();
    partial void OncourseIDChanging(string value);
    partial void OncourseIDChanged();
    partial void OngradePercentChanging(double value);
    partial void OngradePercentChanged();
    partial void OngradeLetterChanging(string value);
    partial void OngradeLetterChanged();
    partial void OnstatusChanging(string value);
    partial void OnstatusChanged();
    #endregion
		
		public SimpleGrade()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_term", DbType="Char(1) NOT NULL")]
		public char term
		{
			get
			{
				return this._term;
			}
			set
			{
				if ((this._term != value))
				{
					this.OntermChanging(value);
					this.SendPropertyChanging();
					this._term = value;
					this.SendPropertyChanged("term");
					this.OntermChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_year", DbType="Int NOT NULL")]
		public int year
		{
			get
			{
				return this._year;
			}
			set
			{
				if ((this._year != value))
				{
					this.OnyearChanging(value);
					this.SendPropertyChanging();
					this._year = value;
					this.SendPropertyChanged("year");
					this.OnyearChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_title", DbType="VarChar(200) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string title
		{
			get
			{
				return this._title;
			}
			set
			{
				if ((this._title != value))
				{
					this.OntitleChanging(value);
					this.SendPropertyChanging();
					this._title = value;
					this.SendPropertyChanged("title");
					this.OntitleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_courseID", DbType="VarChar(10) NOT NULL", CanBeNull=false)]
		public string courseID
		{
			get
			{
				return this._courseID;
			}
			set
			{
				if ((this._courseID != value))
				{
					this.OncourseIDChanging(value);
					this.SendPropertyChanging();
					this._courseID = value;
					this.SendPropertyChanged("courseID");
					this.OncourseIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_gradePercent", DbType="Float NOT NULL")]
		public double gradePercent
		{
			get
			{
				return this._gradePercent;
			}
			set
			{
				if ((this._gradePercent != value))
				{
					this.OngradePercentChanging(value);
					this.SendPropertyChanging();
					this._gradePercent = value;
					this.SendPropertyChanged("gradePercent");
					this.OngradePercentChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_gradeLetter", DbType="VarChar(2) NOT NULL", CanBeNull=false)]
		public string gradeLetter
		{
			get
			{
				return this._gradeLetter;
			}
			set
			{
				if ((this._gradeLetter != value))
				{
					this.OngradeLetterChanging(value);
					this.SendPropertyChanging();
					this._gradeLetter = value;
					this.SendPropertyChanged("gradeLetter");
					this.OngradeLetterChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_status", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
		public string status
		{
			get
			{
				return this._status;
			}
			set
			{
				if ((this._status != value))
				{
					this.OnstatusChanging(value);
					this.SendPropertyChanging();
					this._status = value;
					this.SendPropertyChanged("status");
					this.OnstatusChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
