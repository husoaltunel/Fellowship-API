using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Core.Utilities.Sql
{
    public static class SqlQueryUtil<TEntity>
    {
        public static string GenerateGenericUpdateQuery(TEntity entity,string entityName)
        {                 
            var setQueryList = new List<string>();
            foreach (var property in GetEntityProperties(entity))
            {
                if (property.Name != "Id")
                {
                    setQueryList.Add($@" ""{property.Name}"" = @{property.Name}");
                }                
            }
            var setQuery  = String.Join(",", setQueryList);
            var updateQuery = $@"update ""{entityName}s"" set {setQuery} where ""Id"" =@id";
            return updateQuery.ToString();
        }
        public static string GenerateGenericInsertQuery(TEntity entity, string entityName)
        {
            var propertyNameList = new List<string>();
            foreach (var property in GetEntityProperties(entity))
            {
                if (property.Name != "Id")
                {
                    propertyNameList.Add($@"@{property.Name}");
                }
            }
            var valuesQueryString = String.Join(",",propertyNameList);
            var insertQuery = $@"insert into  ""{entityName}s""({GetEntityPropertiesString(entity)}) values({valuesQueryString}) ";
            return insertQuery.ToString();
        }
        public static PropertyInfo[] GetEntityProperties(TEntity entity)
        {
            var entityProperties = entity.GetType().GetProperties();
            return entityProperties;
        }
        public static string GetEntityPropertiesString(TEntity entity)
        {
            var entityProperties = GetEntityProperties(entity);
            List<string> entityPropertiesList = new List<string>();
            foreach (var property in entityProperties)
            {
                if (property.Name != "Id")
                {
                    entityPropertiesList.Add($@" ""{property.Name}"" ");
                }
            }
            var entityPropertiesToString = String.Join(",", entityPropertiesList);
            return entityPropertiesToString;
        }
        public static string GetEntityPropertyValuesString(TEntity entity)
        {
            var entityProperties = GetEntityProperties(entity);
            List<string> entityPropertyValueList = new List<string>();
            foreach (var property in entityProperties)
            {
                if (property.Name != "Id")
                {
                    entityPropertyValueList.Add($@" ""{property.GetValue(entity)}"" ");
                }
            }
            var entityPropertyValuesToString = String.Join(",", entityPropertyValueList);
            return entityPropertyValuesToString;
        }
    }
}
