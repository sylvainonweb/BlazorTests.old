﻿//////////////////////////////////////////////////////////////
// <auto-generated>This code was generated by LLBLGen Pro 5.3.</auto-generated>
//////////////////////////////////////////////////////////////
// Code is generated on: 
// Code is generated using templates: SD.TemplateBindings.SharedTemplates
// Templates vendor: Solutions Design.
// Templates version: 
//////////////////////////////////////////////////////////////
using System;
using SD.LLBLGen.Pro.ORMSupportClasses;
using BlazorTests.Server.Entities.FactoryClasses;
using BlazorTests.Server.Entities;

namespace BlazorTests.Server.Entities.HelperClasses
{
	/// <summary>Field Creation Class for entity CustomerEntity</summary>
	public partial class CustomerFields
	{
		/// <summary>Creates a new CustomerEntity.Id field instance</summary>
		public static EntityField2 Id
		{
			get { return (EntityField2)EntityFieldFactory.Create(CustomerFieldIndex.Id);}
		}
		/// <summary>Creates a new CustomerEntity.Name field instance</summary>
		public static EntityField2 Name
		{
			get { return (EntityField2)EntityFieldFactory.Create(CustomerFieldIndex.Name);}
		}
		/// <summary>Creates a new CustomerEntity.FirstName field instance</summary>
		public static EntityField2 FirstName
		{
			get { return (EntityField2)EntityFieldFactory.Create(CustomerFieldIndex.FirstName);}
		}
		/// <summary>Creates a new CustomerEntity.CustomerTypeId field instance</summary>
		public static EntityField2 CustomerTypeId
		{
			get { return (EntityField2)EntityFieldFactory.Create(CustomerFieldIndex.CustomerTypeId);}
		}
	}

	/// <summary>Field Creation Class for entity CustomerTypeEntity</summary>
	public partial class CustomerTypeFields
	{
		/// <summary>Creates a new CustomerTypeEntity.Id field instance</summary>
		public static EntityField2 Id
		{
			get { return (EntityField2)EntityFieldFactory.Create(CustomerTypeFieldIndex.Id);}
		}
		/// <summary>Creates a new CustomerTypeEntity.Text field instance</summary>
		public static EntityField2 Text
		{
			get { return (EntityField2)EntityFieldFactory.Create(CustomerTypeFieldIndex.Text);}
		}
	}
	

}