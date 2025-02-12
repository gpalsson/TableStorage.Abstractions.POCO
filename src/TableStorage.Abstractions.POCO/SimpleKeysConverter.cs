﻿using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Linq.Expressions;
using TableStorage.Abstractions.TableEntityConverters;

namespace TableStorage.Abstractions.POCO
{
  public class SimpleKeysConverter<T, TPartitionKey, TRowKey> : IKeysConverter<T, TPartitionKey, TRowKey>
    where T : new()
  {
    private readonly Expression<Func<T, object>> _partitionProperty;
    private readonly Expression<Func<T, object>> _rowProperty;
    private readonly Expression<Func<T, object>>[] _ignoredProperties;

    public SimpleKeysConverter(Expression<Func<T, object>> partitionProperty,
      Expression<Func<T, object>> rowProperty, Expression<Func<T, object>>[] ignoredProperties)
    {
      _partitionProperty = partitionProperty;
      _rowProperty = rowProperty;
      _ignoredProperties = ignoredProperties;
    }

    public DynamicTableEntity ToEntity(T obj)
    {
      return obj.ToTableEntity(_partitionProperty, _rowProperty, _ignoredProperties);
    }

    public T FromEntity(DynamicTableEntity entity)
    {
      return entity.FromTableEntity<T, TPartitionKey, TRowKey>(_partitionProperty, _rowProperty);
    }

    public string PartitionKey(TPartitionKey key)
    {
      return key.ToString();
    }

    public string RowKey(TRowKey key)
    {
      return key.ToString();
    }
  }
}
